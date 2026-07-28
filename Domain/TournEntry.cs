using Data;
namespace Domain;

public sealed class TournEntry(TournEntryData? d) : Entity<TournEntryData>(d) {
    public TournEntry() : this(null) {}
    public int TourNId => data?.TourNId ?? 0;
    public int TeamId => data?.TeamId ?? 0;
    public TourN? TourN => tourn;
    internal TourN? tourn;
    public Team? Team => team;
    internal Team? team;
    public override async Task LoadLazy() {
        await base.LoadLazy();
        team = await getItem<ITeamsRepo, Team>(TeamId)!;
        tourn = await getItem<ITourNRepo, TourN>(TourNId)!;
    }
}