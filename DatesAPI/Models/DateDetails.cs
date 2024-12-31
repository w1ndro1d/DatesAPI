using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatesAPI.Models
{
    public class DateDetails
    {
        [Key]
        public int DateId { get; set; }

        [Column(TypeName ="nvarchar(200)")]
        public string Event { get; set; } = "";

        [Column(TypeName = "datetime")]
        public DateTime EventDate { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string EventNote { get; set; } = "";

        [Column(TypeName = "datetime")]
        public DateTime InitialLoggedDate { get; set; }

    }
}
