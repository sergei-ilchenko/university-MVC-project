using Data;
namespace Tests.Data;

[TestClass] public class TournEntryDataTests : SealedTests<TournEntryData, EntityData<TournEntryData>>{
    [TestInitialize] public override void Initialize() {
        base.Initialize();
        if (obj == null) return;
        obj.Id = 1;
        obj.TourNId = 11;
        obj.TeamId = 7;
    }
    [TestMethod] public void CloneTest() {
        var d = obj?.Clone();
        IsNotNull(d);
        AreEqual(obj?.Id, d?.Id);
        AreEqual(obj?.TourNId, d?.TourNId);
        AreEqual(obj?.TeamId, d?.TeamId);
    }
    [TestMethod] public void TourNIdTest() => IsProperty<int>();
    [TestMethod] public void TeamIdTest() => IsProperty<int>();
}