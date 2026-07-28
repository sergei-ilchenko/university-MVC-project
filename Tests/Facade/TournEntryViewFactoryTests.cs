using Data;
using Facade;

namespace Tests.Facade;

[TestClass] public class TournEntryViewFactoryTests : SealedTests<TournEntryViewFactory, AbstractViewFactory<TournEntryData, TournEntryView>> {

    private TournEntryData? data;
    private TournEntryView? view;

    [TestInitialize]
    public override void Initialize()
    {

        base.Initialize();
        data = crData();
        view = crView();
    }
    [TestCleanup]
    public override void Cleanup(){
        base.Cleanup();
        data = null;
        view = null;
    }
    private TournEntryView crView(){ //kasutada Random?
        var v = new TournEntryView
        {
            Id = 1,
            TourNId = 6,
            TeamId = 9,
        };
        return v;
    }
    private TournEntryData crData()
    {
        var d = new TournEntryData()
        {
            Id = 1000,
            TourNId = 7,
            TeamId = 10,
        };
        return d;
    }

    [TestMethod]
    public void CreateViewTest()
    {
        var f = new TournEntryViewFactory();
        var v = f.CreateView(data);
        IsNotNull(v);
        AreEqual(data?.Id, v.Id);
        AreEqual(data?.TourNId, v.TourNId);
        AreEqual(data?.TeamId, v.TeamId);
    }
    [TestMethod]
    public void CreateDataTest()
    {
        var f = new TournEntryViewFactory();
        var d = f.CreateData(view);
        IsNotNull(d);
        AreEqual(view?.Id, d.Id);
        AreEqual(view?.TourNId, d.TourNId);
        AreEqual(view?.TeamId, d.TeamId);
    }
}
