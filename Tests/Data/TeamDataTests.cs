using Data;
namespace Tests.Data;

[TestClass] public class TeamDataTests : SealedTests<TeamData, EntityData<TeamData>>{
    [TestInitialize] public override void Initialize() {
        base.Initialize();
        if (obj == null) return;
        obj.Id = 1;
        obj.Name = "N";
        obj.PlayersCount = 21;
    }
    [TestMethod] public void CloneTest() {
        var d = obj?.Clone();
        IsNotNull(d);
        AreEqual(obj?.Id, d?.Id);
        AreEqual(obj?.Name, d?.Name);
        AreEqual(obj?.PlayersCount, d?.PlayersCount);
    }
    [TestMethod] public void NameTest() => IsProperty<string>();
    [TestMethod] public void PlayersCountTest() => IsProperty<int>();
}