using Core;
using Data;
using Domain;
using Random = Aids.Random;

namespace Tests.Domain;

[TestClass] public class TourNTests : SealedTests<TourN, Entity<TourNData>> {

    TourNData? data = null;
    protected override TourN CreateObject()
    {
        data = Random.Object<TourNData>();
        return new TourN(data);
    }
    [TestMethod] public void TitleTest() => IsReadOnly(data!.Title);
    [TestMethod] public void StartDateTest() => IsReadOnly(data!.StartDate);
    [TestMethod] public void PrizePoolTest() => IsReadOnly(data!.PrizePool);
    [TestMethod] public void SponsorTest() => IsReadOnly(data!.Sponsor);
    [TestMethod] public void nrParticipantsTest() => IsReadOnly(data!.nrParticipants);
    [TestMethod] public void WinnerTest() => IsReadOnly(data!.Winner);
    [TestMethod] public void StatusTest() => IsReadOnly(data!.Status);
    [TestMethod] public async Task LoadLazyTest() {
        var tourn = CreateObject();
        dynamic mockTournEntryRepo = new MockTournEntryRepo(tourn.Id ?? 0);
        await mockTournEntryRepo.Add(new TournEntry(new TournEntryData() { TourNId = tourn.Id ?? 0, TeamId = 999 }));
        Services.addMockRepo<ITournEntriesRepo, TournEntry>(mockTournEntryRepo);
        await obj!.LoadLazy();
        IsTrue(obj.Entries.Any(e => e.TeamId == 999));
    }
}