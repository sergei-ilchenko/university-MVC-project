using Core;

namespace Tests.Core;

public class DummyEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class DummyRepo : IRepo<DummyEntity>
{
    private readonly List<DummyEntity> _data = new();

    public Task Add(DummyEntity o)
    {
        _data.Add(o);
        return Task.CompletedTask;
    }

    public Task Delete(int id)
    {
        _data.RemoveAll(e => e.Id == id);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<DummyEntity>> Get()
        => Task.FromResult<IEnumerable<DummyEntity>>(_data);

    public Task<IEnumerable<DummyEntity>> Get(int pageIdx, byte pageSize, string? orderBy = null, string? filter = null)
        => Task.FromResult<IEnumerable<DummyEntity>>(_data.Skip(pageIdx * pageSize).Take(pageSize));

    public Task<IEnumerable<DummyEntity>> Get(string propertyName, int idValue)
        => Task.FromResult<IEnumerable<DummyEntity>>(_data.Where(e => propertyName == "Id" && e.Id == idValue));

    public Task<DummyEntity?> Get(int? id)
        => Task.FromResult(_data.FirstOrDefault(e => e.Id == id));

    public Task<int> PageCount(byte pageSize, string? filter)
        => Task.FromResult((_data.Count + pageSize - 1) / pageSize);

    public Task Update(DummyEntity o)
    {
        var idx = _data.FindIndex(e => e.Id == o.Id);
        if (idx >= 0) _data[idx] = o;
        return Task.CompletedTask;
    }
}

[TestClass]
public class IRepoTests : StaticTests
{
    protected override Type? setType() => typeof(IRepo<>);

    [TestMethod]
    public void IsInterface()
    {
        Assert.IsTrue(typeof(IRepo<>).IsInterface);
    }

    [TestMethod]
    public async Task AddAndGet_Works()
    {
        var repo = new DummyRepo();
        var entity = new DummyEntity { Id = 1, Name = "Test" };
        await repo.Add(entity);
        var all = await repo.Get();
        Assert.AreEqual(1, all.Count());
        Assert.AreEqual("Test", all.First().Name);
    }

    [TestMethod]
    public async Task Delete_RemovesEntity()
    {
        var repo = new DummyRepo();
        var entity = new DummyEntity { Id = 2, Name = "ToDelete" };
        await repo.Add(entity);
        await repo.Delete(2);
        var all = await repo.Get();
        Assert.AreEqual(0, all.Count());
    }

    [TestMethod]
    public async Task Update_ChangesEntity()
    {
        var repo = new DummyRepo();
        var entity = new DummyEntity { Id = 3, Name = "Old" };
        await repo.Add(entity);
        entity.Name = "New";
        await repo.Update(entity);
        var updated = (await repo.Get(3))!;
        Assert.AreEqual("New", updated.Name);
    }

    [TestMethod]
    public async Task PageCount_CalculatesCorrectly()
    {
        var repo = new DummyRepo();
        for (int i = 0; i < 10; i++)
            await repo.Add(new DummyEntity { Id = i, Name = $"N{i}" });
        var pages = await repo.PageCount(3, null);
        Assert.AreEqual(4, pages);
    }

    [TestMethod]
    public async Task Get_ById_ReturnsCorrectEntity()
    {
        var repo = new DummyRepo();
        await repo.Add(new DummyEntity { Id = 5, Name = "FindMe" });
        var found = await repo.Get(5);
        Assert.IsNotNull(found);
        Assert.AreEqual("FindMe", found!.Name);
    }

    [TestMethod]
    public async Task Get_ByPropertyName_ReturnsCorrectEntity()
    {
        var repo = new DummyRepo();
        await repo.Add(new DummyEntity { Id = 7, Name = "Special" });
        var found = await repo.Get("Id", 7);
        Assert.AreEqual(1, found.Count());
        Assert.AreEqual("Special", found.First().Name);
    }
}
