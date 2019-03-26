using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchTooolbox.EF
{
    [Table("Titles")]
    public partial class Title
    {
        [Key]
        [StringLength(12)]
        public string Code { get; set; }

        [Required]
        [StringLength(20)]
        public string TitleType { get; set; }

        [Required]
        [StringLength(1000)]
        public string PrimaryTitle { get; set; }

        [Required]
        [StringLength(1000)]
        public string OriginalTitle { get; set; }

        public bool IsAdult { get; set; }

        public short? StartYear { get; set; }

        public short? EndYear { get; set; }

        public int? RuntimeMinutes { get; set; }

        [Required]
        [StringLength(50)]
        public string Genres { get; set; }
    }
}
