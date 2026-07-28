using Data;
namespace Tests.Data;

[TestClass] public class MatchEntryDataTests : SealedTests<MatchEntryData, EntityData<MatchEntryData>> {

    [TestInitialize] public override void Initialize() {
        base.Initialize();
        if (obj == null) return;
        obj.Id = 1;
        obj.MatchId = 7;
        obj.TeamId = 9;
        obj.MatchName = "M";
        obj.TeamName = "T";
    }
    [TestMethod] public void CloneTest() {
        var d = obj?.Clone();
        IsNotNull(d);
        AreEqual(obj?.Id, d?.Id);
        AreEqual(obj?.MatchId, d?.MatchId);
        AreEqual(obj?.TeamId, d?.TeamId);
        AreEqual(obj?.MatchName, d?.MatchName);
        AreEqual(obj?.TeamName, d?.TeamName);
    }
    [TestMethod] public void MatchIdTest() => IsProperty<int>();
    [TestMethod] public void TeamIdTest() => IsProperty<int>();
    [TestMethod] public void MatchNameTest() => IsProperty<string>();
    [TestMethod] public void TeamNameTest() => IsProperty<string>();
}