using Data;
using Domain;
using Infra;

namespace Tests.Infra;

[TestClass] public class PlayersRepoTests
    : RepoBaseTests<PlayersRepo, Player, PlayerData> {
    protected override Player? createEntity(Func<PlayerData> getData)
        => new(getData());
    protected override PlayersRepo CreateObject() => new(dbContext!);
}