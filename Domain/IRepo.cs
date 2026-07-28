using Core;

namespace Domain;

public interface IMatchRepo : IRepo<Match>;
public interface ITourNRepo : IRepo<TourN>;
public interface ITeamsRepo : IRepo<Team> {
    Task UpdateAllPlayersCount();
}
public interface IPlayersRepo : IRepo<Player>;
public interface ITournEntriesRepo : IRepo<TournEntry>;
public interface IMatchEntriesRepo : IRepo<MatchEntry>;
public interface IRatingsRepo : IRepo<Rating> {
    Task<Rating?> GetByTeamId(int teamId);
    Task UpdateTeamRating(int teamId, int newValue);
}