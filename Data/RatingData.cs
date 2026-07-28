namespace Data;

public sealed class RatingData : EntityData<RatingData> {
    public int TeamId { get; set; }
    public int Value { get; set; } = 0;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}