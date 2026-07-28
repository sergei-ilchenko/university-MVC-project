using Data;
using Facade;
using Domain;
using Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Soft.Controllers;

public abstract class BaseController<TObject, TData, TView> (DbContext c, 
    AbstractViewFactory<TData, TView> f, Func<TData?, TObject> CreateObject) : Controller 
    where TObject : Entity<TData>
    where TData: EntityData<TData>, new()
    where TView : EntityView, new() {
    private const byte pageSize = 10;

    protected readonly Repo<TObject, TData> repo = new(c, CreateObject);
    private async Task<IActionResult> showAsync(string? viewName, int? id) {
        var o = await repo.Get(id);
        var v = await f.CreateView(o?.data, true);
        return (o == null)? NotFound() : View(viewName, v);
    }

    public virtual async Task<IActionResult> Index(int pageIdx = 0, string? orderBy = null, string? filter = null, 
        int? selectedId = null) {
        ViewBag.PageIdx = pageIdx;
        ViewBag.PageCount = await repo.PageCount(pageSize, filter);
        ViewBag.OrderBy = orderBy;
        ViewBag.Filter = filter;
        ViewBag.SelectedId = selectedId;
        return View((await repo.Get(pageIdx, pageSize, orderBy, filter)).Select(x => f.CreateView(x?.data)));
    }
    public virtual async Task<IActionResult> Details(int? id) => await showAsync(nameof(Details), id);
    public IActionResult Create() => View(new TView());
        
    [HttpPost, ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> Create(TView v)
    {
        if (!ModelState.IsValid) return View(v);
        var d = f.CreateData(v);
        await repo.Add(CreateObject(d));
        return RedirectToAction(nameof(Index));
    }

    public virtual async Task<IActionResult> Edit(int? id) => await showAsync(nameof(Edit), id);

    [HttpPost, ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> Edit(int id, TView v) {
        if (id != v.Id) return NotFound();
        if (!ModelState.IsValid) return View(v);
        var d = f.CreateData(v);
        await repo.Update(CreateObject(d));
        return RedirectToAction(nameof(Index));
    }
    public virtual async Task<IActionResult> Delete(int? id) => await showAsync(nameof(Delete), id);
        
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public virtual async Task<IActionResult> DeleteConfirmed(int id) {
        await repo.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}