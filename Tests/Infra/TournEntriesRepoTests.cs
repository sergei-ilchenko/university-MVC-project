using Data;
using Domain;
using Infra;

namespace Tests.Infra;

[TestClass] public class TournEntriesRepoTests
    : RepoBaseTests<TournEntriesRepo, TournEntry, TournEntryData> {
    protected override TournEntry? createEntity(Func<TournEntryData> getData)
        => new(getData());
    protected override TournEntriesRepo CreateObject() => new(dbContext!);
}