using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra;
public sealed class MatchRepo(DbContext db)
    : Repo<Match, MatchData>(db, d => new(d)), IMatchRepo { }