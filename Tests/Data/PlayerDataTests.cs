using Data;
namespace Tests.Data;

[TestClass] public class PlayerDataTests : SealedTests<PlayerData, UserData<PlayerData>>{

    [TestInitialize] public override void Initialize() {
        base.Initialize();
        if (obj == null) return;
        obj.Id = 1;
        obj.Nationality = Nationality.Austria;
        obj.TeamId = 9;
        obj.Born = DateTime.Today;
        obj.Age = 21;
    }
    [TestMethod] public void CloneTest() {
        var d = obj?.Clone();
        IsNotNull(d);
        AreEqual(obj?.Id, d?.Id);
        AreEqual(obj?.Nationality, d?.Nationality);
        AreEqual(obj?.TeamId, d?.TeamId);
        AreEqual(obj?.Born, d?.Born);
        AreEqual(obj?.Age, d?.Age);
    }
    [TestMethod] public void NationalityTest() => IsProperty<Nationality?>();
    [TestMethod] public void TeamIdTest() => IsProperty<int>();
    [TestMethod] public void BornTest() => IsProperty<DateTime>();
    [TestMethod] public void AgeTest() => IsProperty<int>();
}