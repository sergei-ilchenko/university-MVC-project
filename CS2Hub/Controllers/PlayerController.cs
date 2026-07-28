using Data;
using Domain;
using Facade;
using Microsoft.AspNetCore.Mvc;
using Soft.Data;

namespace Soft.Controllers;

public sealed class PlayerController(ApplicationDbContext c)
    : BaseController<Player, PlayerData, PlayerView>(c, new PlayerViewFactory(), d => new(d)) {
    private async Task UpdateTeamsPlayersCountAsync() {
        var teamRepo = HttpContext?.RequestServices.GetService(typeof(ITeamsRepo)) as ITeamsRepo;
        if (teamRepo != null) {
            await teamRepo.UpdateAllPlayersCount();
        }
    }
    [HttpPost, ValidateAntiForgeryToken]
    public override async Task<IActionResult> Create(PlayerView v) {
        await base.Create(v);
        await UpdateTeamsPlayersCountAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public override async Task<IActionResult> Edit(int id, PlayerView v) {
        await base.Edit(id, v);
        await UpdateTeamsPlayersCountAsync();
        if (id != v.Id) return NotFound();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public override async Task<IActionResult> DeleteConfirmed(int id) {
        await DeleteEntity(id);
        await UpdateTeamsPlayersCountAsync();
        return RedirectToAction(nameof(Index));
    }
    private async Task DeleteEntity(int id) {
        await base.DeleteConfirmed(id);
    }
}