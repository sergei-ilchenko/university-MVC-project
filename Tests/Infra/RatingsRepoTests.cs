using Data;
using Domain;
using Infra;

namespace Tests.Infra;

[TestClass] public class RatingsRepoTests
    : RepoBaseTests<RatingsRepo, Rating, RatingData> {
    protected override Rating? createEntity(Func<RatingData> getData)
        => new(getData());
    protected override RatingsRepo CreateObject() => new(dbContext!);
}