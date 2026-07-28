using Data;
using Facade;

namespace Tests.Facade;

[TestClass] public class TourNViewFactoryTests : SealedTests<TourNViewFactory, AbstractViewFactory<TourNData, TourNView>> {

    private TourNData? data;
    private TourNView? view;

    [TestInitialize] public override void Initialize() {
        
        base.Initialize();
        data = crData();
        view = crView();
    }
    [TestCleanup] public override void Cleanup() {
        base.Cleanup();
        data = null;
        view = null;
    }
    private TourNView crView() { //kasutada Random?
        var v = new TourNView
        {
            Id = 1,
            Title = "viewTitle",
            StartDate = DateTime.Now.AddYears(-10),
            PrizePool = 10M,
            nrParticipants = 2,
            Winner = "viewWinner",
            Status = Status.Live
        };
        return v;
    }
    private TourNData crData() {
        var d = new TourNData()
        {
            Id = 1000,
            Title = "dataTitle",
            StartDate = DateTime.Now.AddYears(-100),
            PrizePool = 1000M,
            nrParticipants = 4,
            Winner = "dataWinner",
            Status = Status.Upcoming
        };
        return d;
    }
    
    [TestMethod] public void CreateViewTest() {
        var f = new TourNViewFactory();
        var v = f.CreateView(data);
        IsNotNull(v);
        AreEqual(data?.Id, v.Id);
        AreEqual(data?.Title, v.Title);
        AreEqual(data?.StartDate, v.StartDate);
        AreEqual(data?.PrizePool, v.PrizePool);
        AreEqual(data?.nrParticipants, v.nrParticipants);
        AreEqual(data?.Winner, v.Winner);
        AreEqual(data?.Status, v.Status);
    }
    [TestMethod] public void CreateDataTest() {
        var f = new TourNViewFactory();
        var d = f.CreateData(view);
        IsNotNull(d);
        AreEqual(view?.Id, d.Id);
        AreEqual(view?.Title, d.Title);
        AreEqual(view?.StartDate, d.StartDate);
        AreEqual(view?.PrizePool, d.PrizePool);
        AreEqual(view?.nrParticipants, d.nrParticipants);
        AreEqual(view?.Winner, d.Winner);
        AreEqual(view?.Status, d.Status);
    }
}
