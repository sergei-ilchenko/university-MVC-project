using Data;
namespace Domain;

public sealed class Rating(RatingData? d) : Entity<RatingData>(d) {
    public Rating() : this(null) { }
    public int TeamId => data?.TeamId ?? 0;
    public int Value => data?.Value ?? 0;
    public DateTime UpdatedAt => data?.UpdatedAt ?? DateTime.MinValue;
    public Team? Team => team;
    internal Team? team;
    public override async Task LoadLazy() {
        await base.LoadLazy();
        team = await getItem<ITeamsRepo, Team>(TeamId)!;
    }
}