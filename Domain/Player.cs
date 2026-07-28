using Data;
namespace Domain;

public sealed class Player(PlayerData? d) : User<PlayerData>(d) {
    public Player() : this(null) { }
    public Nationality? Nationality => data?.Nationality;
    public DateTime Born => data?.Born?? default;
    public int Age => data?.Age?? 0;
    public int TeamId => data?.TeamId ?? 0;
    public Team? Team => team;
    internal Team? team;
    public override async Task LoadLazy() {
        await base.LoadLazy();
        team = await getItem<ITeamsRepo, Team>(TeamId)!;
    }
}