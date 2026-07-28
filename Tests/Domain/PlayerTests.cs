using Core;
using Data;
using Domain;
using Random = Aids.Random;

namespace Tests.Domain;

[TestClass] public class PlayerTests : SealedTests<Player, User<PlayerData>> {

    PlayerData? data = null;
    protected override Player CreateObject() {
        data = Random.Object<PlayerData>();
        return new Player(data);
    }
    [TestMethod] public void NationalityTest() => IsReadOnly(data!.Nationality);
    [TestMethod] public void BornTest() => IsReadOnly(data!.Born);
    [TestMethod] public void AgeTest() => IsReadOnly(data!.Age);
    [TestMethod] public void TeamIdTest() => IsReadOnly(data!.TeamId);
    [TestMethod] public void TeamTest() => IsReadOnly<Team>(null);
    [TestMethod] public async Task LoadLazyTest() {
        dynamic repo = new MockTeamRepo(obj!.TeamId);
        await Services.addMockRepo<ITeamsRepo, Team>(repo);
        await obj!.LoadLazy();
        AreEqual(obj?.Team?.Id, obj?.TeamId);
    }
}