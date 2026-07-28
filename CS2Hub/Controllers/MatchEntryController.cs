using Data;
using Domain;
using Facade;
using Soft.Data;
using Microsoft.AspNetCore.Mvc;

namespace Soft.Controllers;

public sealed class MatchEntryController(ApplicationDbContext c)
    : BaseController<MatchEntry, MatchEntryData, MatchEntryView>(c, new MatchEntryViewFactory(), d => new(d))
{
    private async Task UpdateTeamsAsync()
    {
        var teamRepo = HttpContext?.RequestServices.GetService(typeof(ITeamsRepo)) as ITeamsRepo;
        if (teamRepo != null)
        {
            await teamRepo.UpdateAllPlayersCount();
        }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public override async Task<IActionResult> Create(MatchEntryView v)
    {
        await base.Create(v);
        await UpdateTeamsAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public override async Task<IActionResult> Edit(int id, MatchEntryView v)
    {
        await base.Edit(id, v);
        await UpdateTeamsAsync();
        if (id != v.Id) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public override async Task<IActionResult> DeleteConfirmed(int id)
    {
        await DeleteEntity(id);
        await UpdateTeamsAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task DeleteEntity(int id)
    {
        await base.DeleteConfirmed(id);
    }
}