using Core;
using Data;
using Domain;
using Random = Aids.Random;

namespace Tests.Domain;

[TestClass]
public class MatchTests : SealedTests<Match, Entity<MatchData>> {

    MatchData? data = null;
    protected override Match CreateObject() {
        data = Random.Object<MatchData>();
        return new Match(data);
    }
    [TestMethod] public void TitleTest() => IsReadOnly(data!.Title);
    [TestMethod] public void StartDateTest() => IsReadOnly(data!.StartDate);
    [TestMethod] public void SponsorTest() => IsReadOnly(data!.Sponsor);
    [TestMethod] public void nrParticipantsTest() => IsReadOnly(data!.nrParticipants);
    [TestMethod] public void WinnerTest() => IsReadOnly(data!.Winner);
    [TestMethod] public void StatusTest() => IsReadOnly(data!.Status);
    [TestMethod] public async Task LoadLazyTest() {
        var match = CreateObject();
        dynamic mockMatchEntryRepo = new MockMatchEntriesRepo(match.Id ?? 0);
        await mockMatchEntryRepo.Add(new MatchEntry(new MatchEntryData() { MatchId = match.Id ?? 0, TeamId = 999 }));

        Services.addMockRepo<IMatchEntriesRepo, MatchEntry>(mockMatchEntryRepo);

        await obj!.LoadLazy();
        IsTrue(obj.Entries.Any(e => e.TeamId == 999));
    }
    [TestClass]
    public class MatchSimpleUnitTests
    {
        [TestMethod]
        public void CanCreateWithNullData()
        {
            var match = new Match(null);
            Assert.IsNotNull(match);
            Assert.IsNull(match.data);
        }

        [TestMethod]
        public void CanCreateWithData()
        {
            var data = new MatchData
            {
                Title = "Test",
                StartDate = DateTime.Today,
                Sponsor = "Sponsor",
                nrParticipants = 4,
                Winner = "Winner",
                Status = Status.Live
            };
            var match = new Match(data);
            Assert.AreEqual("Test", match.data.Title);
            Assert.AreEqual(DateTime.Today, match.data.StartDate);
            Assert.AreEqual("Sponsor", match.data.Sponsor);
            Assert.AreEqual(4, match.data.nrParticipants);
            Assert.AreEqual("Winner", match.data.Winner);
            Assert.AreEqual(Status.Live, match.data.Status);
        }

        [TestMethod]
        public void EntriesAndTeamsAreInitialized()
        {
            var match = new Match(null);
            Assert.IsNotNull(match.Entries);
            Assert.IsNotNull(match.matchEntries);
            Assert.IsNotNull(match.Teams);
        }
    }

    [TestClass]
    public class MatchFullTests
    {
        [TestMethod]
        public void Constructor_NullData_Initializes()
        {
            var match = new Match(null);
            Assert.IsNotNull(match);
            Assert.IsNull(match.data);
        }

        [TestMethod]
        public void Constructor_WithData_InitializesProperties()
        {
            var data = new MatchData
            {
                Title = "Test Match",
                StartDate = new DateTime(2024, 1, 1),
                Sponsor = "Sponsor",
                nrParticipants = 4,
                Winner = "Team A",
                Status = Status.Upcoming
            };
            var match = new Match(data);
            Assert.AreEqual("Test Match", match.data.Title);
            Assert.AreEqual(new DateTime(2024, 1, 1), match.data.StartDate);
            Assert.AreEqual("Sponsor", match.data.Sponsor);
            Assert.AreEqual(4, match.data.nrParticipants);
            Assert.AreEqual("Team A", match.data.Winner);
            Assert.AreEqual(Status.Upcoming, match.data.Status);
        }

        [TestMethod]
        public void Entries_And_MatchEntries_Are_Initialized()
        {
            var match = new Match(null);
            Assert.IsNotNull(match.Entries);
            Assert.IsNotNull(match.matchEntries);
            Assert.AreEqual(0, match.Entries.Count);
            Assert.AreEqual(0, match.matchEntries.Count);
        }

        [TestMethod]
        public void Teams_Initialized()
        {
            var match = new Match(null);
            Assert.IsNotNull(match.Teams);
            Assert.AreEqual(0, match.Teams.Count);
        }
    }
}