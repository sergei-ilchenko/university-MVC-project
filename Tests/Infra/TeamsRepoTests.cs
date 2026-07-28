using Data;
using Domain;
using Infra;

namespace Tests.Infra;

[TestClass] public class TeamsRepoTests
    : RepoBaseTests<TeamsRepo, Team, TeamData> {
    protected override Team? createEntity(Func<TeamData> getData)
        => new(getData());
    protected override TeamsRepo CreateObject() => new(dbContext!);
}