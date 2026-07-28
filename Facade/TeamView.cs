using System.ComponentModel.DataAnnotations;

namespace Facade;

public sealed class TeamView : EntityView {
    
    internal const string sentenceEx = @"^[A-Z][a-zA-Z\s]*$";
    [RegularExpression(sentenceEx), Required, StringLength(16, MinimumLength = 3)] public string? Name { get; set; }
    [Display(Name = "Player Count")] public int PlayersCount { get; set; }
    [Display(Name = "Rating Value"), Range(0, 1000, ErrorMessage = "Rating must be between 0 and 1000.")] public int Value { get; set; }
    [Display(Name = "Rating Updated At")] [DataType(DataType.DateTime)] public DateTime RatingUpdatedAt { get; set; }
}