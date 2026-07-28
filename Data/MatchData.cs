namespace Data;

public sealed class MatchData : EntityData<MatchData> {
    public string? Title { get; set; }
    public DateTime StartDate { get; set; }
    public string? Sponsor { get; set; }
    public int nrParticipants { get; set; }
    public string? Winner { get; set; }
    public Status? Status { get; set; }
}