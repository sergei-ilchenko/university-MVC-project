using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core;

public static class Services {
    internal static Dictionary<Type, object> services { get; set; } = [];
    internal static IServiceProvider? sp;
    internal static void init(IServiceCollection c) => sp = c?.BuildServiceProvider();
    public static T? Get<T>() => Get(typeof(T)) is T t ? t : default;
    public static dynamic? Get(Type t) => sp?.GetRequiredService(t)?? fromList(t);
    public static async Task<TItem?> GetItem<TRepo, TItem>(int id)
        where TRepo : IRepo<TItem> where TItem : class, IEntity
    {
        var r = Get<TRepo>();
        if (r is null) return null;
        return await r.Get(id);
    }
    public static async Task<TItem?> GetItem<TRepo, TItem>(string propertyName, int id)
        where TRepo : IRepo<TItem> where TItem : class, IEntity
    {
        var l = await GetList<TRepo, TItem>(propertyName, id);
        return l.FirstOrDefault();
    }
    public static async Task<IEnumerable<TItem>> GetList<TRepo, TItem>(string propertyName, int id)
        where TRepo : IRepo<TItem> where TItem : class, IEntity
    {
        var r = Get<TRepo>();
        if (r is null) return [];
        return await r.Get(propertyName, id);
    }
    public static async Task<IEnumerable<TItem>> GetList<TRepo, TItem>()
        where TRepo : IRepo<TItem> where TItem : class, IEntity
    {
        var r = Get<TRepo>();
        if (r is null) return [];
        return await r.Get();
    }
    public static async Task<SelectList> SelectList<TRepo, TItem>(
        Expression<Func<TItem, dynamic?>> keySelector, int id)
        where TRepo : IRepo<TItem> where TItem : class, IEntity
    {
        var l = await GetList<TRepo, TItem>();
        l = [.. l.OrderBy(keySelector.Compile())];
        return new SelectList(l, nameof(IEntity.Id), getPropName(keySelector), id);
    }
    private static string getPropName<TItem>(Expression<Func<TItem, dynamic?>> expr) where TItem : class, IEntity
    {
        var body = expr.Body;
        var u = body as MemberExpression;
        return u?.Member?.Name ?? nameof(IEntity.Id);
    }
    private static object? fromList(Type t)
    {
        object? result;
        services.TryGetValue(t, out result);
        return result;
    }
    private static readonly object _lock = new object();
    internal static async Task addMockRepo<TRepo, TItem>(TRepo repo) where TRepo : IRepo<TItem>
    {
        sp = null;
        object? r;
        var type = typeof(TRepo);
        lock (_lock)
        {
            services.TryGetValue(type, out r);
            if (r is null)
            {
                services.Add(type, repo);
                return;
            }
        }
        var items = await repo.Get();
        repo = (TRepo)r;
        if (repo is null) return;
        foreach (var i in items) await repo.Add(i);
    }
    public static void Register<T>(T instance) where T : class
    {
        services[typeof(T)] = instance!;
    }
}