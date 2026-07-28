using System.Reflection;
using Data;
using Domain;
using Infra;
using Core;
using Random = Aids.Random;

namespace Tests.Infra;
public abstract class RepoBaseTests<TRepo, TObject, TData> :
    DbBaseTests<TRepo, Repo<TObject, TData>, TObject, TData>
    where TRepo : class, IRepo<TObject>
    where TObject : Entity<TData>
    where TData : EntityData<TData>, new()
{
    [TestMethod] public async Task AddTest() {
        createEntity();
        IsNull(dbSet!.Find(entity!.Id));
        await obj!.Add(entity!);
        IsNotNull(dbSet!.Find(entity!.Id));
    }
    [TestMethod] public async Task UpdateTest() {
        var d1 = createData();
        var d2 = createData();
        d2.Id = d1.Id;
        addToSet(d1);
        await obj!.Update(createEntity(() => d2)!);
        var o = dbSet!.Find(d1!.Id);
        validateData(o, d2);
    }
    private void validateData(TData? d1, TData? d2) {
        foreach (var pi in d1!.GetType().GetProperties()) {
            var actual = pi.GetValue(d1);
            var expected = pi.GetValue(d2);
            AreEqual(actual, expected);
        }
    }
    [TestMethod] public async Task DeleteTest() {
        await AddTest();
        IsNotNull(dbSet!.Find(entity!.Id));
        await obj!.Delete(entity!.Id ?? 0);
        IsNull(dbSet!.Find(entity!.Id));
    }
    [TestMethod] public async Task GetAsyncByIdTest() {
        async Task validate(bool dataIsNull = true)
        {
            var o = await obj!.Get(entity!.Id);
            IsNotNull(o);
            if (dataIsNull) IsNull(o!.data);
            else {
                IsNotNull(o!.data);
                validateData(entity.data, o.data);
            }
        }
        createEntity();
        await validate();
        await obj!.Add(entity!);
        await validate(false);
    }
    [TestMethod] public async Task GetAsyncAllTest() {
        var l = await obj!.Get();
        AreEqual(dbSet!.Count(), l.Count());
    }
    private List<TData> list = [];
    private async Task get(int pageIdx, byte pageSize,
        string? orderBy = null, string? filter = null)
    {
        list = [.. (await obj!.Get(pageIdx, pageSize, orderBy, filter))
            .Where(x => x.data is not null).Select(x => x.data)];
        if (filter is null) AreEqual(pageSize, list.Count);
        else IsTrue(list.Count > 0);
    }
    private void validate(PropertyInfo pi, bool isDesc = false) {
        var actual = pi!.GetValue(list[0]);
        var expected = pi!.GetValue(list[1]);
        var comparer = System.Collections.Comparer.Default;
        var result = comparer.Compare(actual, expected);
        if (isDesc) IsTrue(result >= 0, $"{actual} < {expected} for {pi.Name}");
        else IsTrue(result <= 0, $"{actual} > {expected} for {pi.Name}");
    }
    [TestMethod] public async Task GetTest() {
        await get(0, 10);
        foreach (var pi in typeof(TData).GetProperties())
        {
            await get(3, 5, pi.Name);
            validate(pi);
            await get(2, 5, pi.Name + "_desc");
            validate(pi, true);
            var filter = pi!.GetValue(list[0])!.ToString();
            await get(0, 5, pi.Name, filter);
        }
    }
    [TestMethod] public async Task PageCountTest() {
        var itemsInPage = Random.Uint8(5, 10);
        var items = dbSet!.Count();
        var pages = items / itemsInPage;
        pages = (items % itemsInPage) == 0 ? pages : pages + 1;
        AreEqual(pages, await obj!.PageCount(itemsInPage, null));
    }
}