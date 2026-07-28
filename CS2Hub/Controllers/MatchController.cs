using Data;
using Domain;
using Facade;
using Soft.Data;
using Microsoft.AspNetCore.Mvc;

namespace Soft.Controllers;

public sealed class MatchController(ApplicationDbContext c)
    : BaseController<Match, MatchData, MatchView>(c, new MatchViewFactory(), d => new(d)) {
    private async Task UpdateMatchEntriesAsync()
    {
        var matchEntryRepo = HttpContext?.RequestServices.GetService(typeof(IMatchEntriesRepo)) as IMatchEntriesRepo;
        if (matchEntryRepo != null)
        {
            var allEntries = await matchEntryRepo.Get();
            foreach (var entry in allEntries)
            {
                await entry.LoadLazy();
            }
        }
    }
    [HttpPost, ValidateAntiForgeryToken]
    public override async Task<IActionResult> Create(MatchView v) {
        await base.Create(v);
        await UpdateMatchEntriesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public override async Task<IActionResult> Edit(int id, MatchView v) {
        await base.Edit(id, v);
        await UpdateMatchEntriesAsync();
        if (id != v.Id) return NotFound();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public override async Task<IActionResult> DeleteConfirmed(int id) {
        await DeleteEntity(id);
        await UpdateMatchEntriesAsync();
        return RedirectToAction(nameof(Index));
    }
    private async Task DeleteEntity(int id) {
        await base.DeleteConfirmed(id);
    }
}