using Core;
using Data;
namespace Domain;

public sealed class Team(TeamData? d) : Entity<TeamData>(d) {
    public Team() : this(null) {}
    public string Name => data?.Name ?? string.Empty;
    public int PlayersCount => data?.PlayersCount ?? 0;
    internal List<Player> players = [];
    public List<Player> Players => players;
    public Rating? Rating {
        get => rating;
        set => rating = value;
    }
    internal Rating? rating;
    public override async Task LoadLazy()
    {
        await base.LoadLazy();
        players.Clear();
        var list = await (Services.Get<IPlayersRepo>()?
            .Get(nameof(Player.TeamId), Id ?? 0))!;
        foreach (var p in list) {
            await p.LoadLazy();
            players.Add(p);
        }

        var ratingsRepo = Services.Get<IRatingsRepo>();
        if (ratingsRepo != null)
        {
            rating = await ratingsRepo.GetByTeamId(Id ?? 0);
        }
    }
    public async Task<int> GetPlayersCount() {
        await base.LoadLazy();
        players.Clear();
        var list = await (Services.Get<IPlayersRepo>()?
            .Get(nameof(Player.TeamId), Id ?? 0))!;
        foreach (var p in list) {
            await p.LoadLazy();
            players.Add(p);
        }
        return players.Count;
    }
}