using Data;
using Domain;
using Infra;

namespace Tests.Infra;
[TestClass] public class RepoTests
    : RepoBaseTests<Repo<Team, TeamData>, Team, TeamData> {
    protected override Team? createEntity(Func<TeamData> getData)
        => new(getData());
    protected override Repo<Team, TeamData> CreateObject()
        => new TeamsRepo(dbContext!);
    [TestMethod] public override void IsSealedTest() =>
        IsFalse(typeof(Repo<Team, TeamData>).IsSealed);
    [TestMethod] public void IsAbstractTest() =>
        IsFalse(typeof(Repo<Team, TeamData>).IsAbstract);
    [TestMethod] public override void IsBaseTypeTest() =>
        AreEqual(typeof(Repo<Team, TeamData>).BaseType, typeof(object));
}