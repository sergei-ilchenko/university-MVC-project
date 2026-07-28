namespace Data;

public sealed class TeamData : EntityData<TeamData> {
    public string? Name { get; set; }
    public int PlayersCount { get; set; }
}