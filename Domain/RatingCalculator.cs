using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain;
public class RatingCalculator {
    private const int BasePoints = 15;
    private const double Sensitivity = 100.0;
    private const int MinRating = 0;
    private const int MaxRating = 1000;

    public Dictionary<int, int> CalculateNewRatings(Team winner, List<Team> losers) {
        winner.Rating ??= new Rating(new RatingData { TeamId = winner.Id ?? 0, Value = 0 });

        foreach (var loser in losers) {
            loser.Rating ??= new Rating(new RatingData { TeamId = loser.Id ?? 0, Value = 0 });
        }

        int winnerRating = winner.Rating.Value;
        int totalPointsGainedByWinner = 0;
        var result = new Dictionary<int, int>();

        foreach (var loser in losers) {
            int loserRating = loser.Rating.Value;

            double diff = loserRating - winnerRating;
            double pointsLostDouble;

            if (diff > 0) {
                pointsLostDouble = BasePoints * (1.0 + diff / Sensitivity);
            }
            else {
                pointsLostDouble = BasePoints / (1.0 + Math.Abs(diff) / Sensitivity);
            }

            var pointsLostInt = (int)Math.Round(pointsLostDouble);
            if (pointsLostInt < 0) pointsLostInt = 0;

            var newLoserRating = Math.Clamp(loserRating - pointsLostInt, MinRating, MaxRating);
            result[loser.Id ?? 0] = newLoserRating;

            totalPointsGainedByWinner += pointsLostInt;
        }

        var newWinnerRating = Math.Clamp(winnerRating + totalPointsGainedByWinner, MinRating, MaxRating);
        result[winner.Id ?? 0] = newWinnerRating;

        return result;
    }
}

