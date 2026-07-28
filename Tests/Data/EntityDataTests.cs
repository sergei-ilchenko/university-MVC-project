using Data;

namespace Tests.Data;

[TestClass] public class EntityDataTests : AbstractTests<EntityData<TeamData>, EntityData> {
    protected override EntityData<TeamData> CreateObject() => new TeamData();
    [TestMethod] public void IdTest() => IsProperty<int>();
    [TestInitialize] public override void Initialize() {
        base.Initialize();
        if (obj == null) return;
        var o = obj as TeamData;
        o!.Id = 1;
        o!.Name = "Entity";
        o!.PlayersCount = 11;
    }
    [TestMethod] public void CloneTest() {
        var d = obj?.Clone();
        IsNotNull(d);
        AreEqual(1, d?.Id);
        AreEqual("Entity", d?.Name);
        AreEqual(11, d?.PlayersCount);
    }
}