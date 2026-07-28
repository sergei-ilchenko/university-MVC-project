using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra;
public sealed class TourNRepo(DbContext db)
    : Repo<TourN, TourNData>(db, d => new(d)), ITourNRepo { }