using System.Linq.Dynamic.Core;
using Core;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra;

public class Repo<TObject, TData>(DbContext c, Func<TData?, TObject> f) : 
    IRepo<TObject> where TObject : Entity<TData> where TData : EntityData<TData> {

    private readonly DbContext db = c;
    protected readonly DbSet<TData> set = c.Set<TData>();
    private static bool isAsc(string s) => !s.EndsWith("_desc");
    private static string propName(string s) => s.Replace("_desc", "");
    private IQueryable<TData> ordered(string? orderBy = null, string? filter = null)
        => (orderBy is null) ? filtered(filter) : isAsc(orderBy)
            ? filtered(filter).OrderBy(propName(orderBy))
            : filtered(filter).OrderBy(propName(orderBy) + " descending");
    private ParsingConfig config = new() { AllowEqualsAndToStringMethodsOnObject = true };
    private IQueryable<TData> filtered(string? filter = null)
        => (filter is null)
            ? set
            : set.Where(config, whereExpr(), filter);
    private IQueryable<TData> filtered(string propertyName, int idValue)
    {
        var predicate = whereExpr(propertyName);
        if (string.IsNullOrWhiteSpace(predicate))
            throw new ArgumentException($"No int property '{propertyName}' found on type '{typeof(TData).Name}'.", nameof(propertyName));
        return set.Where(predicate, idValue);
    }
    private string whereExpr(string propertyName)
    {
        var filters = new List<string>();
        foreach (var p in typeof(TData).GetProperties())
        {
            if (p.Name != propertyName) continue;
            if (p.PropertyType == typeof(int) || p.PropertyType.IsEnum ||
                (Nullable.GetUnderlyingType(p.PropertyType)?.IsEnum ?? false))
            {
                filters.Add($"({p.Name}==@0)");
            }
        }
        return string.Join(" OR ", filters);
    }
    private string whereExpr()
    {
        var filters = new List<string>();
        foreach (var p in typeof(TData).GetProperties())
        {
            if (p.PropertyType == typeof(string)) filters.Add($"({p.Name} != null && {p.Name}.Contains(@0))");
            else filters.Add($"({p.Name}.ToString().Contains(@0))");
        }
        return string.Join(" OR ", filters);
    }
    public async Task<int> PageCount(byte pageSize, string? filter) {
        var cnt = await filtered(filter).CountAsync();
        return cnt % pageSize == 0 ? cnt / pageSize : cnt / pageSize + 1;
    }
    public async Task<IEnumerable<TObject>> Get(string propertyName, int idValue)
        => (await filtered(propertyName, idValue).ToListAsync()).Select(f);
    public async Task<IEnumerable<TObject>> Get(int pageIdx, byte pageSize
        , string? orderBy = null, string? filter = null)
        => (await ordered(orderBy, filter)
            .Skip(pageIdx * pageSize).Take(pageSize).ToListAsync()).Select(f);
    public async Task<IEnumerable<TObject>> Get() => (await set.ToListAsync()).Select(f);
    public async Task<TObject?> Get(int? id) => (id == null) ? null : f(await set.FindAsync(id));
    public async Task Delete(int id) {
        var x = await set.FindAsync(id);
        if (x is null) return;
        set.Remove(x);
        await db.SaveChangesAsync();
    }
    public async Task Update(TObject o)
    {
        var d = o?.data;
        if (d is null) return;
        var tracked = db.ChangeTracker.Entries<TData>()
            .FirstOrDefault(e => e.Entity.Id == d.Id);
        if (tracked != null) {
               
            tracked.CurrentValues.SetValues(d);
        }
        else {
            db.Update(d);
        }
        await db.SaveChangesAsync();
    }
    public async Task Add(TObject o) {
        var d = o?.data;
        if (d is null) return;
        try {
            set.Add(d);
            await db.SaveChangesAsync();
        }
        catch (DbUpdateException ex) {
            if (ex.InnerException?.Message.Contains("UNIQUE") == true) {
                throw new InvalidOperationException("A record with the same unique values already exists.", ex);
            }
            throw; 
        }
    }
}