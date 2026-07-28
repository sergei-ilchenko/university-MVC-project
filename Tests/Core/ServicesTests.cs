using Microsoft.Extensions.DependencyInjection;
using Core;
using System.Reflection;

namespace Tests.Core;
[DoNotParallelize]
[TestClass]
public class ServicesTests : StaticTests
{
    protected override Type? setType() => typeof(Services);

    [TestInitialize]
    public override void Initialize()
    {
        base.Initialize();
        ResetServicesStaticState();
    }

    [TestCleanup]
    public override void Cleanup()
    {
        base.Cleanup();
        ResetServicesStaticState();
    }

    private static void ResetServicesStaticState()
    {
        var type = typeof(Services);
        var spField = type.GetField("sp", BindingFlags.Static | BindingFlags.NonPublic);
        var servicesField = type.GetField("services", BindingFlags.Static | BindingFlags.NonPublic);
        spField?.SetValue(null, null);
        servicesField?.SetValue(null, new Dictionary<Type, object>());
    }
    private class DummyEntity : IEntity
    {
        public int? Id { get; set; }
        public Task LoadLazy() => Task.CompletedTask;
    }

    private class DummyRepo : IRepo<DummyEntity>
    {
        private readonly List<DummyEntity> _items = new();
        public Task Add(DummyEntity e) { _items.Add(e); return Task.CompletedTask; }
        public Task<DummyEntity?> Get(int? id) => Task.FromResult(_items.Find(x => x.Id == id));
        public Task<IEnumerable<DummyEntity>> Get() => Task.FromResult<IEnumerable<DummyEntity>>(_items);
        public Task<IEnumerable<DummyEntity>> Get(string propertyName, int id) => Task.FromResult<IEnumerable<DummyEntity>>(_items.FindAll(x => x.Id == id));
        public Task<IEnumerable<DummyEntity>> Get(int pageIdx, byte pageSize, string? orderBy = null, string? filter = null) => Task.FromResult<IEnumerable<DummyEntity>>(_items);
        public Task<int> PageCount(byte pageSize, string? filter) => Task.FromResult(_items.Count);
        public Task Update(DummyEntity o) => Task.CompletedTask;
        public Task Delete(int id) => Task.CompletedTask;
    }

    [TestMethod]
    public void Init_And_Get_Service()
    {
        var services = new ServiceCollection();
        var repo = new DummyRepo();
        services.AddSingleton<IRepo<DummyEntity>>(repo);

        Services.init(services);

        var resolved = Services.Get<IRepo<DummyEntity>>();
        IsNotNull(resolved);
        AreSame(repo, resolved);
    }

    [TestMethod]
    public async Task GetItem_Returns_Entity()
    {
        var services = new ServiceCollection();
        var repo = new DummyRepo();
        services.AddSingleton<IRepo<DummyEntity>>(repo);
        Services.init(services);

        var entity = new DummyEntity { Id = 42 };
        await repo.Add(entity);

        var result = await Services.GetItem<IRepo<DummyEntity>, DummyEntity>(42);
        IsNotNull(result);
        AreEqual(42, result.Id);
    }

    [TestMethod]
    public async Task GetList_Returns_All_Entities()
    {
        var services = new ServiceCollection();
        var repo = new DummyRepo();
        services.AddSingleton<IRepo<DummyEntity>>(repo);
        Services.init(services);

        await repo.Add(new DummyEntity { Id = 1 });
        await repo.Add(new DummyEntity { Id = 2 });

        var result = await Services.GetList<IRepo<DummyEntity>, DummyEntity>();
        IsNotNull(result);
        AreEqual(2, new List<DummyEntity>(result).Count);
    }

    [TestMethod]
    public async Task GetItem_Returns_Null_If_Not_Found()
    {
        var services = new ServiceCollection();
        var repo = new DummyRepo();
        services.AddSingleton<IRepo<DummyEntity>>(repo);
        Services.init(services);

        var result = await Services.GetItem<IRepo<DummyEntity>, DummyEntity>(999);
        IsNull(result);
    }
}