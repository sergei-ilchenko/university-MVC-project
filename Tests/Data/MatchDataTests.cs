using Data;
namespace Tests.Data;

[TestClass] public class MatchDataTests : SealedTests<MatchData, EntityData<MatchData>> {

    [TestInitialize] public override void Initialize() {
        base.Initialize();
        if (obj == null) return;
        obj.Id = 1;
        obj.Title = "ABC";
        obj.StartDate = DateTime.Today;
        obj.Sponsor = "O";
        obj.nrParticipants = 333;
        obj.Winner = "W";
        obj.Status = 0;
    }
    [TestMethod] public void CloneTest() {
        var d = obj?.Clone();
        IsNotNull(d);
        AreEqual(obj?.Id, d?.Id);
        AreEqual(obj?.Title, d?.Title);
        AreEqual(obj?.StartDate, d?.StartDate);
        AreEqual(obj?.Sponsor, d?.Sponsor);
        AreEqual(obj?.nrParticipants, d?.nrParticipants);
        AreEqual(obj?.Winner, d?.Winner);
        AreEqual(obj?.Status, d?.Status);
    }
    [TestMethod] public void TitleTest() => IsProperty<string>();
    [TestMethod] public void StartDateTest() => IsProperty<DateTime>();
    [TestMethod] public void SponsorTest() => IsProperty<string>();
    [TestMethod] public void nrParticipantsTest() => IsProperty<int>();
    [TestMethod] public void WinnerTest() => IsProperty<string>();
    [TestMethod] public void StatusTest() => IsProperty<Status?>();
}