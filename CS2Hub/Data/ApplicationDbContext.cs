using Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Soft.Data;
public class ApplicationDbContext : IdentityDbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<TourNData> Tournaments { get; set; } = default!;
    public DbSet<TeamData> Teams { get; set; } = default!;
    public DbSet<PlayerData> Players { get; set; } = default!;
    public DbSet<TournEntryData> TournamentEntries { get; set; } = default!;
    public DbSet<MatchData> Matches { get; set; } = default!;
    public DbSet<MatchEntryData> MatchEntries { get; set; } = default!;
    public DbSet<RatingData> Ratings { get; set; } = default!;
}