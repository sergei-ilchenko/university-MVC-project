using Data;
using Domain;
using Core;
using Random = Aids.Random;

namespace Tests.Domain;
//test

internal class MockPlayerRepo(int id) : MockRepo<Player>(id), IPlayersRepo
{
    protected override EntityData createData() => Random.Object<TourNData>();
    protected override Player createObject(EntityData d) => new(d as PlayerData);
    public override async Task<IEnumerable<Player>> Get(string propertyName, int idValue) {
        await Task.CompletedTask;
        if (propertyName == nameof(Player.TeamId))
            return list.Where(x => (x as Player)?.TeamId == idValue).ToList();
        return [];
    }
}
internal class MockRatingRepo(int id) : MockRepo<Rating>(id), IRatingsRepo
{
    protected override EntityData createData() => Random.Object<RatingData>();
    protected override Rating createObject(EntityData d) => new(d as RatingData);
    public Task<Rating?> GetByTeamId(int teamId)
    {
        var rating = new Rating(new RatingData { TeamId = teamId, Value = 100, UpdatedAt = DateTime.UtcNow });
        return Task.FromResult<Rating?>(rating);
    }

    public Task UpdateTeamRating(int teamId, int newValue)
    {
        throw new NotImplementedException();
    }
}
internal class MockTourNRepo(int id) : MockRepo<TourN>(id), ITourNRepo
{
    protected override EntityData createData() => Random.Object<TourNData>();
    protected override TourN createObject(EntityData d) => new(d as TourNData);
}
internal class MockTournEntryRepo(int id) : MockRepo<TournEntry>(id), ITournEntriesRepo
{
    protected override EntityData createData() => Random.Object<TournEntryData>();
    protected override TournEntry createObject(EntityData d) => new(d as TournEntryData);
}
internal class MockMatchRepo(int id) : MockRepo<Match>(id), IMatchRepo
{
    protected override EntityData createData() => Random.Object<MatchData>();
    protected override Match createObject(EntityData d) => new(d as MatchData);
}
internal class MockMatchEntriesRepo(int id) : MockRepo<MatchEntry>(id), IMatchEntriesRepo
{
    protected override EntityData createData() => Random.Object<MatchEntryData>();
    protected override MatchEntry createObject(EntityData d) => new(d as MatchEntryData);
}
internal class MockTeamRepo(int id) : MockRepo<Team>(id), ITeamsRepo
{
    protected override EntityData createData() => Random.Object<TeamData>();
    protected override Team createObject(EntityData d) => new(d as TeamData);

    Task ITeamsRepo.UpdateAllPlayersCount()
    {
        throw new NotImplementedException();
    }
}
internal abstract class MockRepo<TObject>() : IRepo<TObject> where TObject : IEntity
{
    internal List<TObject> list { get; set; } = [];

    protected MockRepo(int id)
        : this()
    {
        byte count = Random.Uint8(5, 10);
        byte idx = Random.Uint8(0, count);
        for (var i = 0; i < count; i++)
        {
            EntityData d = createData();
            if (i == idx) d.Id = id;
            var o = createObject(d);
            list.Add(o);
        }
    }

    protected abstract TObject createObject(EntityData d);
    protected abstract EntityData createData();
    public async Task Add(TObject o)
    {
        await Task.CompletedTask;
        list.Add(o);
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TObject>> Get(int pageIdx, byte pageSize, string? orderBy = null, string? filter = null)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IEnumerable<TObject>> Get(string propertyName, int idValue)
        => await Get();

    public async Task<IEnumerable<TObject>> Get()
    {
        await Task.CompletedTask;
        return [.. list];
    }

    public async Task<TObject?> Get(int? id)
    {
        await Task.CompletedTask;
        return list.FirstOrDefault(x => x.Id == id);
    }

    public Task<int> PageCount(byte pageSize, string? filter)
    {
        throw new NotImplementedException();
    }

    public Task Update(TObject o)
    {
        throw new NotImplementedException();
    }
}