using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra;

public sealed class RatingsRepo(DbContext db)
    : Repo<Rating, RatingData>(db, d => new(d)), IRatingsRepo {
    public async Task<Rating?> GetByTeamId(int teamId) {
        var data = await set.FirstOrDefaultAsync(x => x.TeamId == teamId);
        return data == null ? null : new Rating(data);
    }
    public async Task UpdateTeamRating(int teamId, int value) {
        var rating = await set.FirstOrDefaultAsync(r => r.TeamId == teamId);
        if (rating != null) {
            rating.Value = value;
            rating.UpdatedAt = DateTime.UtcNow;
        }
        else {
            rating = new RatingData {
                TeamId = teamId,
                Value = value,
                UpdatedAt = DateTime.UtcNow
            };
            await set.AddAsync(rating);
        }
        await db.SaveChangesAsync();
    }
}