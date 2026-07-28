namespace Data;

public sealed class TournEntryData : EntityData<TournEntryData> {
    public int TourNId { get; set; }
    public int TeamId { get; set; }
}