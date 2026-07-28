using Core;
using Data;
using Domain;
using Random = Aids.Random;

namespace Tests.Domain;

[TestClass]
public class TeamTests : SealedTests<Team, Entity<TeamData>>
{

    TeamData? data = null;
    protected override Team CreateObject()
    {
        data = Random.Object<TeamData>();
        return new Team(data);
    }
    [TestMethod] public void NameTest() => IsReadOnly(data!.Name);
    [TestMethod] public void PlayersCountTest() => IsReadOnly(data!.PlayersCount);
    [TestMethod] public void RatingTest() => IsReadOnly<Rating>(null);
    [TestMethod] public void PlayersTest() => IsOfType(obj!.Players, typeof(List<Player>));
    [TestMethod] public async Task GetPlayersCountTest() {
        
        var team = CreateObject();
        dynamic mockPlayersRepo = new MockPlayerRepo(team.Id ?? 0);
        await mockPlayersRepo.Add(new Player(new PlayerData { TeamId = team.Id ?? 0 }));
        await mockPlayersRepo.Add(new Player(new PlayerData { TeamId = team.Id ?? 0 }));
        Services.addMockRepo<IPlayersRepo, Player>(mockPlayersRepo);
        var count = await team.GetPlayersCount();
        AreEqual(2, count);
        AreEqual(2, team.Players.Count);
    }
}