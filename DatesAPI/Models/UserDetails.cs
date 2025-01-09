using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DatesAPI.Models
{
    public class UserDetails
    {
        [Key]
        public int UserID { get; set; }

        [Column(TypeName ="nvarchar(256)")]
        public string Email { get; set; } = "";

        [Column(TypeName = "nvarchar(max)")]
        [JsonPropertyName("password")]
        public string PasswordHash { get; set; } = "";

        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        
    }
}
