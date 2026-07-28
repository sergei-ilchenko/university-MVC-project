using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Soft.Data;
using Random = Aids.Random;

namespace Tests;

public abstract class DbBaseTests<TClass, TBaseClass, TObject, TData> :
    BaseClassTests<TClass, TBaseClass>
    where TClass : class
    where TBaseClass : class
    where TObject : Entity<TData>
    where TData : EntityData<TData>, new() {
    
    protected ApplicationDbContext? dbContext;
    protected DbSet<TData?> dbSet;
    protected TObject? entity;
    internal byte lastId;
    internal byte nextId => ++lastId;
    protected internal TData createData() {
        var d = Random.Object<TData>();
        d.Id = nextId;
        return d;
    }
    protected abstract TObject? createEntity(Func<TData> getData);
    protected TObject? createEntity() => entity = createEntity(createData);
    [TestInitialize] public override void Initialize()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        dbContext = new ApplicationDbContext(options);
        dbSet = dbContext!.Set<TData>();
        seedData();
        base.Initialize();
    }
    [TestCleanup] public override void Cleanup() {
        base.Cleanup();
        dbContext = null;
        dbSet = null;
        entity = null;
    }
    [TestMethod] public virtual void IsSealedTest() => IsTrue(typeof(TClass).IsSealed);
    [TestMethod] public void CanCreateDbContextTest() => IsNotNull(dbContext);
    [TestMethod] public void HasDbSetTest() => IsNotNull(dbSet);
    [TestMethod] public void DbSetHasDataTest() => IsTrue(dbSet!.Any());
    protected internal void seedData() {
        var l = new List<TData>();
        for (var i = 0; i < Random.Uint8(20, 30); i++) l.Add(createData());
        dbSet?.AddRange(l);
        dbContext?.SaveChanges();
        dbContext?.ChangeTracker.Clear();
    }
    protected internal void addToSet(TData d) {
        dbSet?.Add(d);
        dbContext?.SaveChanges();
        dbContext?.ChangeTracker.Clear();
    }
}