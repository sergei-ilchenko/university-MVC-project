using System.ComponentModel.DataAnnotations;

namespace Facade;

public sealed class RatingView : EntityView
{
    [Display(Name = "Team ID")]
    public int TeamId { get; set; }

    [Required]
    [Display(Name = "Rating Value")]
    [Range(0, 1000)]
    public int Value { get; set; }
        
    [Display(Name = "Last Updated")]
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; }
}