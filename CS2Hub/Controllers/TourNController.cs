using Data;
using Domain;
using Facade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Soft.Data;
namespace Soft.Controllers;

public sealed class TourNController(ApplicationDbContext c)
    : BaseController<TourN, TourNData, TourNView>(c, new TourNViewFactory(), d => new(d)) {
    public async Task<IActionResult> RandomLiveTournamentsPartial()
    {
        var liveTournaments = (await repo.Get("Status", (int)Status.Live)).ToList();
        var random = new Random();
        var randomTournaments = liveTournaments.OrderBy(x => random.Next()).Take(3).ToList();
        return PartialView("_RandomLiveTournaments", randomTournaments);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id) {
        var tournament = await GetLoadedTournamentOrNotFound(id);
        if (tournament == null) return NotFound();
        var view = new TourNViewFactory().Create(tournament);
        SetTeamsViewBag(tournament);

        return View(view);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public override async Task<IActionResult> Edit(int id, TourNView view) {
        if (id != view.Id) return NotFound();

        var tournament = await GetLoadedTournamentOrNotFound(id);
        if (tournament == null) return NotFound();

        if (!ModelState.IsValid)
        {
            SetTeamsViewBag(tournament);
            return View(view);
        }

        if (tournament.data != null) {
            tournament.data.Title = view.Title;
            tournament.data.StartDate = view.StartDate;
            tournament.data.PrizePool = view.PrizePool;
            tournament.data.Sponsor = view.Sponsor;
            tournament.data.nrParticipants = view.nrParticipants;
            tournament.data.Status = view.Status;
            if (view.Status == Status.Past && string.IsNullOrEmpty(view.Winner))
            {
                ModelState.AddModelError("Winner", "Choose winner");
                SetTeamsViewBag(tournament);
                return View(view);
            }

            if (view.Status == Status.Past) {
                tournament.data.Winner = view.Winner;

                var teams = tournament.Teams.Where(t => t != null).ToList();
                foreach (var t in teams)
                    await t!.LoadLazy();

                var winnerTeam = teams.FirstOrDefault(t => t?.Name == view.Winner);
                var losers = teams.Where(t => t != null && t.Name != view.Winner).ToList();

                if (winnerTeam != null && losers.Count > 0) {
                    var calculator = new RatingCalculator();
                    var newRatings = calculator.CalculateNewRatings(winnerTeam, losers);

                    foreach (var kvp in newRatings) {
                        var ratingData = await c.Ratings.FirstOrDefaultAsync(r => r.TeamId == kvp.Key);
                        if (ratingData != null) {
                            ratingData.Value = kvp.Value;
                            ratingData.UpdatedAt = DateTime.UtcNow;
                        }
                        else {
                            c.Ratings.Add(new RatingData {
                                TeamId = kvp.Key,
                                Value = kvp.Value,
                                UpdatedAt = DateTime.UtcNow
                            });
                        }
                    }
                    await c.SaveChangesAsync();
                }
            }
            else {
                tournament.data.Winner = null;
            }
        }
        await repo.Update(tournament);
        return RedirectToAction("Index");
    }
    private async Task<TourN?> GetLoadedTournamentOrNotFound(int id)
    {
        var tournament = await repo.Get(id);
        if (tournament == null) return null;
        await tournament.LoadLazy();
        return tournament;
    }
    private void SetTeamsViewBag(TourN tournament) =>
        ViewBag.Teams = tournament.Teams.Where(t => t != null).ToList();
}