using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra;
public sealed class TeamsRepo(DbContext db)
    : Repo<Team, TeamData>(db, d => new(d)), ITeamsRepo {
    public async Task UpdateAllPlayersCount()
    {
        var allTeams = await Get();
        foreach (var teamObj in allTeams)
        {
            if (teamObj is Team team)
            {
                await team.LoadLazy();
                var playersCount = await team.GetPlayersCount();
                var teamData = team.data;
                teamData.PlayersCount = playersCount;
                await Update(new Team(teamData));
            }
        }
    }
}