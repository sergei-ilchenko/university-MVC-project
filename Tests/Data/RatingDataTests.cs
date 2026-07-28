using Data;
namespace Tests.Data;

[TestClass] public class RatingDataTests : SealedTests<RatingData, EntityData<RatingData>>{
    [TestInitialize] public override void Initialize() {
        base.Initialize();
        if (obj == null) return;
        obj.Id = 1;
        obj.TeamId = 9;
        obj.UpdatedAt = DateTime.Today;
        obj.Value = 21;
    }
    [TestMethod] public void CloneTest() {
        var d = obj?.Clone();
        IsNotNull(d);
        AreEqual(obj?.Id, d?.Id);
        AreEqual(obj?.TeamId, d?.TeamId);
        AreEqual(obj?.UpdatedAt, d?.UpdatedAt);
        AreEqual(obj?.Value, d?.Value);
    }
    [TestMethod] public void TeamIdTest() => IsProperty<int>();
    [TestMethod] public void UpdatedAtTest() => IsProperty<DateTime>();
    [TestMethod] public void ValueTest() => IsProperty<int>();
}