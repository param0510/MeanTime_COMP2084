/**
This model has a 1-1 relation with App model with AppDetail being the dependent side
 */
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeanTime.Models
{
    public class AppDetail
    {
        // Foreign Key 
        [Key]
        public int AppId { get; set; }

        [MinLength(2, ErrorMessage = "Owner name must be more than 2 characters in length")]
        [MaxLength(30, ErrorMessage = "Owner name has to be less than 30 characters")]
        [Required]
        public string? Owner { get; set; }

        public string? Mode { get; set; }

        [MinLength(10, ErrorMessage = "Too short description for the app, keep it more than 10 characters")]
        [MaxLength(250, ErrorMessage = "Too long a description for the app, kindly bring it lower than 250 characters")]
        public string? Description { get; set; }

        [Range(0, 999999, ErrorMessage = "The duration must lie in the range of [0,999999] mins")]
        [DisplayFormat(DataFormatString = "{0:F0} mins")]
        public double Duration { get; set; }

        [Range(0, 999999999999, ErrorMessage = "The data usage must lie in the range of [0,999999999999] Kilobytes")]
        [DisplayName("Total Data Usage")]
        [DisplayFormat(DataFormatString = "{0:F0} Kb")]
        public decimal TotalDataUsage { get; set; }

        [Range(0, 9999999, ErrorMessage = "The average RAM usage must lie in the range of [0,9999999] Kilobytes")]
        [DisplayName("Average Memory Usage")]
        [DisplayFormat(DataFormatString = "{0:F0} Kb")]
        public decimal AvgMemoryUsage { get; set; }

        public string? Downloads { get; set; }

        [DisplayName("Install Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]

        public DateTime InstallDate { get; set; }

        // Reference to 1-1 table, this is one Advanced .NET functionality that I have added based upon independent learning
        public virtual App? App { get; set; }
    }
}
