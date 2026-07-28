using Data;
using Domain;

namespace Facade;

public sealed class TeamViewFactory : AbstractViewFactory<TeamData, TeamView> {
    public TeamView Create(Team team)
    {
        var view = new TeamView
        {
            Id = team.Id ?? 0,
            Name = team.Name,
            PlayersCount = team.PlayersCount,
            Value = team.Rating?.Value ?? 0,
            RatingUpdatedAt = team.Rating?.UpdatedAt ?? DateTime.MinValue

        };
        Console.WriteLine($"TeamView {view.Id}: Mapped Value = {view.Value}");
        return view;
    }
}