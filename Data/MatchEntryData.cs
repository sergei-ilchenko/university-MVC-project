namespace Data;

public sealed class MatchEntryData : EntityData<MatchEntryData> {
    public int MatchId { get; set; }
    public int TeamId { get; set; }
    public string? MatchName { get; set; }
    public string? TeamName { get; set; }
}