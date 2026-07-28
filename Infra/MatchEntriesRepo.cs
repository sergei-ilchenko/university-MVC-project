using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra;
public sealed class MatchEntriesRepo(DbContext db)
    : Repo<MatchEntry, MatchEntryData>(db, d => new(d)), IMatchEntriesRepo { }