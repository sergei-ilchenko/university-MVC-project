using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra;
public sealed class PlayersRepo(DbContext db)
    : Repo<Player, PlayerData>(db, d => new(d)), IPlayersRepo { }