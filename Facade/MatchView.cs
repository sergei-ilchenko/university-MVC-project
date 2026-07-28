using Data;
using System.ComponentModel.DataAnnotations;

namespace Facade;

public sealed class MatchView : EntityView {
       
    internal const string sentenceEx = @"^[A-Z][a-zA-Z\s]*$";
    [RegularExpression(sentenceEx), Required, StringLength(25, MinimumLength = 3)] public string? Title { get; set; }

    [Display(Name = "Start Date"), DataType(DataType.Date), Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        
    public DateTime StartDate { get; set; } = DateTime.Today;
    [RegularExpression(sentenceEx), StringLength(16, MinimumLength = 3)] public string? Sponsor { get; set; }

    [Display(Name = "Participants"), Range(2, 10), Required, RegularExpression(@"^(2|4|6|8|10)$", ErrorMessage = "Matches can only be create 1v1, 2v2 .... 5v5")]
    public int nrParticipants { get; set; }
    [RegularExpression(sentenceEx), StringLength(16, MinimumLength = 3)] public string? Winner { get; set; }
    public Status? Status { get; set; }
}