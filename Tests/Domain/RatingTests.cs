using Core;
using Data;
using Domain;
using Random = Aids.Random;

namespace Tests.Domain;

[TestClass]
public class RatingTests : SealedTests<Rating, Entity<RatingData>>
{

    RatingData? data = null;
    protected override Rating CreateObject()
    {
        data = Random.Object<RatingData>();
        return new Rating(data);
    }

    [TestMethod] public void ValueTest() => IsReadOnly(data!.Value);
    [TestMethod] public void UpdatedAtTest() => IsReadOnly(data!.UpdatedAt);
    [TestMethod] public void TeamIdTest() => IsReadOnly(data!.TeamId);
    [TestMethod] public void TeamTest() => IsReadOnly<Team>(null);
    [TestMethod]
    public async Task LoadLazyTest()
    {
        var repo = new MockTeamRepo(obj!.TeamId);
        await Services.addMockRepo<ITeamsRepo, Team>(repo);
        await obj!.LoadLazy();
        AreEqual(obj?.Team?.Id, obj?.TeamId);
    }
}