using Core;
using Data;
using Domain;
using Random = Aids.Random;

namespace Tests.Domain;

[TestClass] public class MatchEntryTests : SealedTests<MatchEntry, Entity<MatchEntryData>> {

    MatchEntryData? data = null;
    protected override MatchEntry CreateObject()
    {
        data = Random.Object<MatchEntryData>();
        return new MatchEntry(data);
    }
    [TestMethod] public void MatchIdTest() => IsReadOnly(data!.MatchId);
    [TestMethod] public void TeamIdTest() => IsReadOnly(data!.TeamId);
    [TestMethod] public void MatchTest() => IsReadOnly<Match>(null);
    [TestMethod] public void TeamTest() => IsReadOnly<Team>(null);
    [TestMethod] public async Task LoadLazyTest() {
        var matchRepo = new MockMatchRepo(obj!.MatchId);
        await Services.addMockRepo<IMatchRepo, Match>(matchRepo);
        var teamRepo = new MockTeamRepo(obj.TeamId);
        await Services.addMockRepo<ITeamsRepo, Team>(teamRepo);
        await obj.LoadLazy(); 
        AreEqual(obj.Match?.Id, obj.MatchId); 
        AreEqual(obj.Team?.Id, obj.TeamId);
    }
    [TestMethod]
    public void Constructor_SetsDataCorrectly()
    {
        var d = Random.Object<MatchEntryData>();
        var entry = new MatchEntry(d);
        AreEqual(d.MatchId, entry.MatchId);
        AreEqual(d.TeamId, entry.TeamId);
    }

    [TestMethod]
    public void DefaultConstructor_SetsDefaults()
    {
        var entry = new MatchEntry();
        AreEqual(0, entry.MatchId);
        AreEqual(0, entry.TeamId);
        IsNull(entry.Match);
        IsNull(entry.Team);
    }

    [TestMethod]
    public async Task LoadLazy_DoesNotThrow_WhenReposAreMissing()
    {
        var entry = CreateObject();
    
        await entry.LoadLazy();
      
        IsNull(entry.Match);
        IsNull(entry.Team);
    }
   
}