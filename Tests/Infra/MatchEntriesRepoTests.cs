using Data;
using Domain;
using Infra;

namespace Tests.Infra;

[TestClass] public class MatchEntriesRepoTests
    : RepoBaseTests<MatchEntriesRepo, MatchEntry, MatchEntryData> {
    protected override MatchEntry? createEntity(Func<MatchEntryData> getData)
        => new(getData());
    protected override MatchEntriesRepo CreateObject() => new(dbContext!);
}