using Data;

namespace Domain;
public sealed class TourN(TourNData? d) : Entity<TourNData>(d) {
    public TourN() : this(null){}
    public string Title => data?.Title?? string.Empty;
    public DateTime StartDate => data?.StartDate?? default;
    public decimal PrizePool => data?.PrizePool?? 0;
    public string Sponsor => data?.Sponsor ?? string.Empty;
    public int nrParticipants => data?.nrParticipants?? 0;
    public string Winner => data?.Winner ?? string.Empty;
    public Status? Status => data?.Status;
    
    internal List<TournEntry> tournEntries = [];
    internal List<TournEntry> Entries => tournEntries;
    public List<Team?> Teams => tournEntries?
        .Where(t => t.Team is not null).Select(t => t.Team).ToList() ?? []; 
     public override async Task LoadLazy() {
         await base.LoadLazy();
         tournEntries.Clear();
         var list = await getList<ITournEntriesRepo, TournEntry>(nameof(TournEntry.TourNId), Id ?? 0);
         foreach (var e in list) {
             if (e is null) continue;
             await e.LoadLazy();
             tournEntries.Add(e);
         }
     }
}