using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MeanTime.Models
{
    public class App
    {
        public int AppId { get; set; }

        [MinLength(2, ErrorMessage = "Hey! This way too short!")]
        [MaxLength(70, ErrorMessage = "Too long name for an app")]
        [Required]
        public string? Name { get; set; }
       
        public string? Image { get; set; }

        [Range(0.00,999,ErrorMessage = "The price must belong in the range of [0,999]")]

        // This one does not seem to work yet.... please do it with JavaScript display Free instead of $0
        [DisplayFormat(NullDisplayText ="Free", DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        [Range(2, 9999999, ErrorMessage = "The size must belong in the range of [2,9999999] Kilobytes")]
        [DisplayFormat(DataFormatString ="{0} Kb")]
        [Required]
        public decimal Size { get; set; }

        [MinLength(3, ErrorMessage ="Too short for a meta tag, try making it more than 3 characters in length")]
        [MaxLength(25, ErrorMessage ="Too long for a meta tag, keep it less than 25 characters")]
        [Display(Name ="Meta Tag")]
        public string? MetaTag { get; set; }

        [Range(0,5,ErrorMessage ="App rating must be in the range of [0,5] stars")]
        [DisplayFormat(DataFormatString ="{0:F1}")]
        public float Rating { get; set; }

        public bool Status { get; set; }

        public int GenreId { get; set; }    

        // Ref to parent table
        public Genre? Genre { get; set; }
        // Ref to 1-1 table
        public virtual AppDetail? AppDetail { get; set; }
    }
}
