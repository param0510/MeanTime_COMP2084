using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace MeanTime.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        [MinLength(2, ErrorMessage ="Genre type can't be less than 2 characters in length")]
        [MaxLength(40, ErrorMessage="Genre type can't be longer than 40 characters")]
        [Required]
        public string? Type { get; set; }

        [MinLength(8, ErrorMessage ="Too short a meta description for the genre, keep it more than 8 characters")]
        [MaxLength(90, ErrorMessage ="Too long a meta description for the genre, kindly bring it lower than 90 characters")]
        public string? MetaDescription { get; set; }
        // Ref to child table
        public List<App>? Apps { get; set; }

        //public int totalApps;
        //public int TotalApps
        //{
        //    get { return totalApps; }
        //    set 
        //    { 
        //        if(Apps == null)
        //        {
        //            totalApps = 0;
        //        }
        //        else
        //        {
        //            totalApps = Apps.Count;
        //        }

        //    }
        //}
    }
}
