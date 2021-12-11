using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce_Customer_Rating_API.Models
{
    public class Ratings
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingsId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? ItemId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Comment { get; set; }

        [Column(TypeName = "nvarchar(1)")]
        public int Stars { get; set; }
    }
}
