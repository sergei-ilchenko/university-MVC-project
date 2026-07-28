using Data;
using Domain;
using Infra;

namespace Tests.Infra;

[TestClass] public class MatchRepoTests
    : RepoBaseTests<MatchRepo, Match, MatchData> {
    protected override Match? createEntity(Func<MatchData> getData)
        => new(getData());
    protected override MatchRepo CreateObject() => new(dbContext!);
}