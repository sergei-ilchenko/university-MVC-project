using Data;
namespace Domain;

public sealed class MatchEntry(MatchEntryData? d) : Entity<MatchEntryData>(d) {
    public MatchEntry() : this(null) { }
    public int MatchId => data?.MatchId ?? 0;
    public int TeamId => data?.TeamId ?? 0;
    public Match? Match => match;
    internal Match? match;
    public Team? Team => team;
    internal Team? team;
    public override async Task LoadLazy() {
        await base.LoadLazy();
        team = await getItem<ITeamsRepo, Team>(TeamId)!;
        match = await getItem<IMatchRepo, Match>(MatchId)!;
    }
}