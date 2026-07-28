using Data;
namespace Tests.Data;

[TestClass] public class TourNDataTests : SealedTests<TourNData, EntityData<TourNData>> {

    [TestInitialize] public override void Initialize() {
        base.Initialize();
        if(obj == null) return;
        obj.Id = 1;
        obj.Title = "ABC";
        obj.StartDate = DateTime.Today;
        obj.PrizePool = 9.99M;
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
        AreEqual(obj?.PrizePool, d?.PrizePool);
        AreEqual(obj?.Sponsor, d?.Sponsor);
        AreEqual(obj?.nrParticipants, d?.nrParticipants);
        AreEqual(obj?.Winner, d?.Winner);
        AreEqual(obj?.Status, d?.Status);
    }
    [TestMethod] public void TitleTest() => IsProperty<string>();
    [TestMethod] public void StartDateTest() => IsProperty<DateTime>();
    [TestMethod] public void PrizePoolTest() => IsProperty<decimal>();
    [TestMethod] public void SponsorTest() => IsProperty<string>();
    [TestMethod] public void nrParticipantsTest() => IsProperty<int>();
    [TestMethod] public void WinnerTest() => IsProperty<string>();
    [TestMethod] public void StatusTest() => IsProperty<Status?>();
}