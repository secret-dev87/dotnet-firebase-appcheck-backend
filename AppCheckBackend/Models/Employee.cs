using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCheckBackend.Models
{
    [Table("employees")]
    public class Employee
    {
        [Column("id")]
        public int EmployeeId { get; set; }

        [Column("name")]
        [Required]
        public required string EmployeeName { get; set; }

        [Column("department")]
        [Required]
        public required string Department { get; set; }

        [Column("joineddate")]
        public DateTime? DateOfJoining { get; set; }

        [Column("photofile")]
        public string? PhotoFileName { get; set; }
    }
}
