using Data;
using Domain;
using Infra;

namespace Tests.Infra;

[TestClass] public class TourNRepoTests
    : RepoBaseTests<TourNRepo, TourN, TourNData> {
    protected override TourN? createEntity(Func<TourNData> getData)
        => new(getData());
    protected override TourNRepo CreateObject() => new(dbContext!);
}