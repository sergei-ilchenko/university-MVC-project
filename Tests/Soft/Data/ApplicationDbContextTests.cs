using Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Soft.Data;

namespace Tests.Soft.Data;

[TestClass] public class ApplicationDbContextTests :
    BaseClassTests<ApplicationDbContext, IdentityDbContext> {
    protected override ApplicationDbContext CreateObject() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }
    [TestMethod] public void TournamentsTest() => IsOfType(obj!.Tournaments, typeof(DbSet<TourNData>));
    [TestMethod] public void TeamsTest() => IsOfType(obj!.Teams, typeof(DbSet<TeamData>));
    [TestMethod] public void PlayersTest() => IsOfType(obj!.Players, typeof(DbSet<PlayerData>));
    [TestMethod] public void TournamentEntriesTest() => IsOfType(obj!.TournamentEntries, typeof(DbSet<TournEntryData>));
    [TestMethod] public void MatchesTest() => IsOfType(obj!.Matches, typeof(DbSet<MatchData>));
    [TestMethod] public void MatchEntriesTest() => IsOfType(obj!.MatchEntries, typeof(DbSet<MatchEntryData>));
    [TestMethod] public void RatingsTest() => IsOfType(obj!.Ratings, typeof(DbSet<RatingData>));
}