using Data;
using Domain;
using Facade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soft.Data;
namespace Soft.Controllers;

public sealed class TeamController(ApplicationDbContext c)
    : BaseController<Team, TeamData, TeamView>(c, new TeamViewFactory(), d => new(d)) {
    private async Task UpdateTeamsPlayersCountAsync() {
        var teamRepo = HttpContext?.RequestServices.GetService(typeof(ITeamsRepo)) as ITeamsRepo;
        if (teamRepo != null) {
            await teamRepo.UpdateAllPlayersCount();
        }
    }
    [HttpPost, ValidateAntiForgeryToken]
    public override async Task<IActionResult> Create(TeamView v) {
        if (!ModelState.IsValid) return View(v);

        var teamData = new TeamData {
            Name = v.Name,
            PlayersCount = v.PlayersCount
        };
        var team = new Team(teamData);
        await repo.Add(team);

        var db = HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
        var savedTeam = db.Teams.OrderByDescending(t => t.Id).FirstOrDefault(t => t.Name == v.Name && t.PlayersCount == v.PlayersCount);
        if (savedTeam == null) return View(v);

        var rating = new RatingData {
            TeamId = team.data.Id,
            Value = v.Value,
            UpdatedAt = DateTime.UtcNow
        };
        db.Ratings.Add(rating);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    public override async Task<IActionResult> Edit(int? id)
    {
        var view = await GetTeamViewAsync(id);
        if (view == null) return NotFound();
        return View(view);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public override async Task<IActionResult> Edit(int id, TeamView v)
    {
        if (id != v.Id) return NotFound();

        if (!ModelState.IsValid) {
            var view = await GetTeamViewAsync(id);
            if (view == null) return NotFound();
            view.Name = v.Name;
            view.PlayersCount = v.PlayersCount;
            return View(view);
        }

        var teamToUpdate = await repo.Get(id);
        if (teamToUpdate == null) return NotFound();

        if (teamToUpdate.data != null) {
            teamToUpdate.data.Name = v.Name;
            teamToUpdate.data.PlayersCount = v.PlayersCount;
        }
        await repo.Update(teamToUpdate);

        if (HttpContext?.RequestServices.GetService(typeof(IRatingsRepo)) is IRatingsRepo ratingsRepo)
            await ratingsRepo.UpdateTeamRating(id, v.Value);

        await UpdateTeamsPlayersCountAsync();

        return RedirectToAction(nameof(Index));
    }
    public override async Task<IActionResult> Index(int pageIdx = 0, string? orderBy = null, string? filter = null, int? selectedId = null) {
        ViewBag.PageIdx = pageIdx;
        ViewBag.OrderBy = orderBy;
        ViewBag.Filter = filter;
        ViewBag.SelectedId = selectedId;

        int pageSize = 10;
        var teamViews = await GetTeamViewsWithRatingsAsync(pageIdx, pageSize, orderBy, filter);
        var db = HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
        var countQuery = from team in db.Teams
            join rating in db.Ratings on team.Id equals rating.TeamId into ratings
            from rating in ratings.DefaultIfEmpty()
            select new { team.Name };
        if (!string.IsNullOrWhiteSpace(filter)) countQuery = countQuery.Where(t => t.Name.Contains(filter));
        ViewBag.PageCount = (await countQuery.CountAsync() + pageSize - 1) / pageSize;

        return View(teamViews);
    }
    public override async Task<IActionResult> Details(int? id) {
        var view = await GetTeamViewAsync(id);
        if (view == null) return NotFound();
        return View(view);
    }
    public override async Task<IActionResult> Delete(int? id) {
        var view = await GetTeamViewAsync(id);
        if (view == null) return NotFound();
        return View(view);
    }
    private async Task<List<TeamView>> GetTeamViewsWithRatingsAsync(int pageIdx, int pageSize, string? orderBy = null, string? filter = null) {
        var db = HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
        if (db == null || db.Teams == null) 
            throw new InvalidOperationException("Database or Teams set is not initialized.");
        var query = from team in db.Teams
            join rating in db.Ratings on team.Id equals rating.TeamId into ratings
            from rating in ratings.DefaultIfEmpty()
            select new TeamView {
                Id = team.Id,
                Name = team.Name,
                PlayersCount = team.PlayersCount,
                Value = rating != null ? rating.Value : 0,
                RatingUpdatedAt = rating != null ? rating.UpdatedAt : DateTime.MinValue
            };

        if (!string.IsNullOrWhiteSpace(filter))
            query = query.Where(t => t.Name.Contains(filter));

        query = orderBy switch {
            "Value" => query.OrderBy(t => t.Value),
            "Value_desc" => query.OrderByDescending(t => t.Value),
            "Name" => query.OrderBy(t => t.Name),
            "Name_desc" => query.OrderByDescending(t => t.Name),
            "PlayersCount" => query.OrderBy(t => t.PlayersCount),
            "PlayersCount_desc" => query.OrderByDescending(t => t.PlayersCount),
            "RatingUpdatedAt" => query.OrderBy(t => t.RatingUpdatedAt),
            "RatingUpdatedAt_desc" => query.OrderByDescending(t => t.RatingUpdatedAt),
            _ => query.OrderByDescending(t => t.Value)
        };

        query = query.Skip(pageIdx * pageSize).Take(pageSize);

        return await query.ToListAsync();
    }
    private async Task<TeamView?> GetTeamViewAsync(int? id) {
        var team = await repo.Get(id);
        if (team == null) return null;

        await team.LoadLazy();

        if (team.Rating == null && team.Id.HasValue) {
            if (HttpContext?.RequestServices.GetService(typeof(IRatingsRepo)) is IRatingsRepo ratingsRepo)
            {
                team.Rating = await ratingsRepo.GetByTeamId(team.Id.Value);
            }
        }

        var factory = new TeamViewFactory();
        var view = factory.Create((Team)team);

        if (team.Rating != null) {
            view.Value = team.Rating.Value;
            view.RatingUpdatedAt = team.Rating.UpdatedAt;
        }

        return view;
    }
}