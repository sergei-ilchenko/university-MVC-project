using Core;
using Data;
using Domain;
using Random = Aids.Random;

namespace Tests.Domain;

[TestClass] public class TournEntryTests : SealedTests<TournEntry, Entity<TournEntryData>> {

    TournEntryData? data = null;
    protected override TournEntry CreateObject() {
        data = Random.Object<TournEntryData>();
        return new TournEntry(data);
    }
    [TestMethod] public void TourNIdTest() => IsReadOnly(data!.TourNId);
    [TestMethod] public void TeamIdTest() => IsReadOnly(data!.TeamId);
    [TestMethod] public void TourNTest() => IsReadOnly<TourN>(null);
    [TestMethod] public void TeamTest() => IsReadOnly<Team>(null);
    [TestMethod] public async Task LoadLazyTest() {

    var tourNRepo = new MockTourNRepo(obj!.TourNId);
    await Services.addMockRepo<ITourNRepo, TourN>(tourNRepo);
    var teamRepo = new MockTeamRepo(obj.TeamId);
    await Services.addMockRepo<ITeamsRepo, Team>(teamRepo);
    await obj.LoadLazy();
    AreEqual(obj.TourN?.Id, obj.TourNId, "TourN ID mismatch after lazy loading.");
    AreEqual(obj.Team?.Id, obj.TeamId, "Team ID mismatch after lazy loading.");
    }
}