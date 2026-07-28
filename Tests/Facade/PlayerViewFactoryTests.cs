using Data;
using Facade;

namespace Tests.Facade;

[TestClass] public class PlayerViewFactoryTests : SealedTests<PlayerViewFactory, UserViewFactory<PlayerData, PlayerView>> {

    private PlayerData? data;
    private PlayerView? view;

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
    private PlayerView crView()
    {
        var v = new PlayerView
        {
            Id = 1,
            Nationality = Nationality.Austria,
            Born = DateTime.Today,
            TeamId = 2
        };
        return v;
    }
    private PlayerData crData()
    {
        var d = new PlayerData()
        {
            Id = 77,
            Nationality = Nationality.Belgium,
            Born = DateTime.Today,
            TeamId = 1
        };
        return d;
    }

    [TestMethod]
    public void CreateViewTest()
    {
        var f = new PlayerViewFactory();
        var v = f.CreateView(data);
        IsNotNull(v);
        AreEqual(data?.Id, v.Id);
        AreEqual(data?.Nationality, v.Nationality);
        AreEqual(data?.Born, v.Born);
        AreEqual(data?.TeamId, v.TeamId);
    }
    [TestMethod]
    public void CreateDataTest()
    {
        var f = new PlayerViewFactory();
        var d = f.CreateData(view);
        IsNotNull(d);
        AreEqual(view?.Id, d.Id);
        AreEqual(view?.Nationality, d.Nationality);
        AreEqual(view?.Born, d.Born);
        AreEqual(view?.TeamId, d.TeamId);
    }
}