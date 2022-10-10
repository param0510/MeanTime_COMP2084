using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeanTime.Models
{
    public class AppDetail
    {
        //[ForeignKey("AppId")] 
        [Key]
        public int AppId { get; set; }

        [MinLength(2, ErrorMessage = "Owner name must be more than 2 characters in length")]
        [MaxLength(30, ErrorMessage = "Owner name has to be less than 30 characters")]
        [Required]
        public string? Owner { get; set; }

        [MinLength(2, ErrorMessage = "Mode must be more than 2 characters in length")]
        [MaxLength(20, ErrorMessage = "Mode has to be less than 20 characters")]
        public string? Mode { get; set; }

        [MinLength(10, ErrorMessage = "Too short description for the app, keep it more than 10 characters")]
        [MaxLength(250, ErrorMessage = "Too long a description for the app, kindly bring it lower than 250 characters")]
        public string? Description { get; set; }

        [Range(0, 999999, ErrorMessage = "The duration must lie in the range of [0,999999] mins")]
        [DisplayFormat(DataFormatString = "{0:F0} mins")]
        public double Duration { get; set; }

        [Range(0, 999999999999, ErrorMessage = "The data usage must lie in the range of [0,999999999999] Kilobytes")]
        [DisplayFormat(DataFormatString = "{0:F0} Kb")]
        public decimal TotalDataUsage { get; set; }

        [Range(0, 9999999, ErrorMessage = "The average RAM usage must lie in the range of [0,9999999] Kilobytes")]
        [DisplayFormat(DataFormatString = "{0:F0} Kb")]
        public decimal AvgMemoryUsage { get; set; }

        public string? Downloads { get; set; }

        [DisplayName("Install Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]

        public DateTime InstallDate { get; set; }

        // Ref to 1-1 table
        public virtual App? App { get; set; }
    }
}
