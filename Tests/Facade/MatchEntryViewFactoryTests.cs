using Data;
using Facade;

namespace Tests.Facade;

[TestClass]
public class MatchEntryViewFactoryTests : SealedTests<MatchEntryViewFactory, AbstractViewFactory<MatchEntryData, MatchEntryView>>
{

    private MatchEntryData? data;
    private MatchEntryView? view;

    [TestInitialize]
    public override void Initialize()
    {

        base.Initialize();
        data = crData();
        view = crView();
    }
    [TestCleanup]
    public override void Cleanup()
    {
        base.Cleanup();
        data = null;
        view = null;
    }
    private MatchEntryView crView()
    {
        var v = new MatchEntryView
        {
            Id = 1,
            MatchId = 6,
            TeamId = 9,
            MatchName = "MatchName",
            TeamName = "TeamName"
        };
        return v;
    }
    private MatchEntryData crData()
    {
        var d = new MatchEntryData()
        {
            Id = 1000,
            MatchId = 7,
            TeamId = 10,
            MatchName = "MatchName",
            TeamName = "TeamName"
        };
        return d;
    }

    [TestMethod]
    public void CreateViewTest()
    {
        var f = new MatchEntryViewFactory();
        var v = f.CreateView(data);
        IsNotNull(v);
        AreEqual(data?.Id, v.Id);
        AreEqual(data?.MatchId, v.MatchId);
        AreEqual(data?.TeamId, v.TeamId);
        AreEqual(data?.MatchName, v.MatchName);
        AreEqual(data?.TeamName, v.TeamName);
    }
    [TestMethod]
    public void CreateDataTest()
    {
        var f = new MatchEntryViewFactory();
        var d = f.CreateData(view);
        IsNotNull(d);
        AreEqual(view?.Id, d.Id);
        AreEqual(view?.MatchId, d.MatchId);
        AreEqual(view?.TeamId, d.TeamId);
        AreEqual(view?.MatchName, d.MatchName);
        AreEqual(view?.TeamName, d.TeamName);
    }
}