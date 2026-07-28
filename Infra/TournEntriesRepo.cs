using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra;
public sealed class TournEntriesRepo(DbContext db)
    : Repo<TournEntry, TournEntryData>(db, d => new(d)), ITournEntriesRepo { }