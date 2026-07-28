using Data;
using Facade;

namespace Tests.Facade;

[TestClass] public class RatingViewFactoryTests : SealedTests<RatingViewFactory, AbstractViewFactory<RatingData, RatingView>> {

    private RatingData? data;
    private RatingView? view;

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
    private RatingView crView()
    {
        var v = new RatingView
        {
            Id = 1,
            Value = 666,
            UpdatedAt = DateTime.Today,
            TeamId = 2
        };
        return v;
    }
    private RatingData crData()
    {
        var d = new RatingData()
        {
            Id = 77,
            Value = 500,
            UpdatedAt = DateTime.Today,
            TeamId = 1
        };
        return d;
    }

    [TestMethod]
    public void CreateViewTest()
    {
        var f = new RatingViewFactory();
        var v = f.CreateView(data);
        IsNotNull(v);
        AreEqual(data?.Id, v.Id);
        AreEqual(data?.Value, v.Value);
        AreEqual(data?.UpdatedAt, v.UpdatedAt);
        AreEqual(data?.TeamId, v.TeamId);
    }
    [TestMethod]
    public void CreateDataTest()
    {
        var f = new RatingViewFactory();
        var d = f.CreateData(view);
        IsNotNull(d);
        AreEqual(view?.Id, d.Id);
        AreEqual(view?.Value, d.Value);
        AreEqual(view?.UpdatedAt, d.UpdatedAt);
        AreEqual(view?.TeamId, d.TeamId);
    }
}