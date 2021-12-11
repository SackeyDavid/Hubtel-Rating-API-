using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce_Customer_Rating_API.Models
{
    public class PhoneNumber
    {
        [Column(TypeName = "nvarchar(10)")]
        public string? phoneNumber { get; set; }
    }
}
