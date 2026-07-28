using Data;
using Facade;

namespace Tests.Facade;

[TestClass] public class MatchViewFactoryTests : SealedTests<MatchViewFactory, AbstractViewFactory<MatchData, MatchView>> {

    private MatchData? data;
    private MatchView? view;

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
    private MatchView crView()
    {
        var v = new MatchView
        {
            Id = 1,
            Title = "Name",
            StartDate = DateTime.Today,
            Sponsor = "Sponsor",
            nrParticipants = 9,
            Winner = "Winner",
            Status = Status.Unspecified
        };
        return v;
    }
    private MatchData crData()
    {
        var d = new MatchData()
        {
            Id = 1000,
            Title = "Name",
            StartDate = DateTime.Today,
            Sponsor = "Sponsor",
            nrParticipants = 10,
            Winner = "Loser",
            Status = Status.Unspecified
        };
        return d;
    }

    [TestMethod]
    public void CreateViewTest()
    {
        var f = new MatchViewFactory();
        var v = f.CreateView(data);
        IsNotNull(v);
        AreEqual(data?.Id, v.Id);
        AreEqual(data?.Title, v.Title);
        AreEqual(data?.StartDate, v.StartDate);
        AreEqual(data?.Sponsor, v.Sponsor);
        AreEqual(data?.nrParticipants, v.nrParticipants);
        AreEqual(data?.Winner, v.Winner);
        AreEqual(data?.Status, v.Status);
    }
    [TestMethod]
    public void CreateDataTest()
    {
        var f = new MatchViewFactory();
        var d = f.CreateData(view);
        IsNotNull(d);
        AreEqual(view?.Id, d.Id);
        AreEqual(view?.Title, d.Title);
        AreEqual(view?.StartDate, d.StartDate);
        AreEqual(view?.Sponsor, d.Sponsor);
        AreEqual(view?.nrParticipants, d.nrParticipants);
        AreEqual(view?.Winner, d.Winner);
        AreEqual(view?.Status, d.Status);
    }
}