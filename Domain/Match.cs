using Data;
namespace Domain;

public sealed class Match(MatchData? d) : Entity<MatchData>(d) {
    public Match() : this(null) { }
    public string Title => data?.Title ?? string.Empty;
    public DateTime StartDate => data?.StartDate ?? default;
    public string Sponsor => data?.Sponsor ?? string.Empty;
    public int nrParticipants => data?.nrParticipants ?? 0;
    public string Winner => data?.Winner ?? string.Empty;
    public Status? Status => data?.Status;

    internal List<MatchEntry> matchEntries = [];
    internal List<MatchEntry> Entries => matchEntries;
    public List<Team?> Teams => matchEntries?
        .Where(t => t.Team is not null).Select(t => t.Team).ToList() ?? [];
    public override async Task LoadLazy() {
        await base.LoadLazy();
        matchEntries.Clear();
        var list = await getList<IMatchEntriesRepo, MatchEntry>(nameof(MatchEntry.MatchId), Id ?? 0);
        foreach (var e in list) {
            if (e is null) continue;
            await e.LoadLazy();
            matchEntries.Add(e);
        }
    }
}