using Data;
using Facade;

namespace Tests.Facade;

[TestClass] public class TeamViewFactoryTests : SealedTests<TeamViewFactory, AbstractViewFactory<TeamData, TeamView>> {

    private TeamData? data;
    private TeamView? view;

    [TestInitialize]
    public override void Initialize() {

        base.Initialize();
        data = crData();
        view = crView();
    }
    [TestCleanup]
    public override void Cleanup() {
        base.Cleanup();
        data = null;
        view = null;
    }
    private TeamView crView() {
        var v = new TeamView
        {
            Id = 1,
            Name = "Name",
            PlayersCount = 9,
            Value = 100
        };
        return v;
    }
    private TeamData crData() {
        var d = new TeamData()
        {
            Id = 1000,
            Name = "Name",
            PlayersCount = 10,
            //Value = 99
        };
        return d;
    }

    [TestMethod]
    public void CreateViewTest()
    {
        var f = new TeamViewFactory();
        var v = f.CreateView(data);
        IsNotNull(v);
        AreEqual(data?.Id, v.Id);
        AreEqual(data?.Name, v.Name);
        AreEqual(data?.PlayersCount, v.PlayersCount);
        AreEqual(0, v.Value);
    }
    [TestMethod]
    public void CreateDataTest()
    {
        var f = new TeamViewFactory();
        var d = f.CreateData(view);
        IsNotNull(d);
        AreEqual(view?.Id, d.Id);
        AreEqual(view?.Name, d.Name);
        AreEqual(view?.PlayersCount, d.PlayersCount);
        AreEqual(view?.Value, 100);
    }
}