using System.ComponentModel.DataAnnotations.Schema;

namespace AppCheckBackend.Models
{
    [Table("departments")]
    public class Department
    {
        [Column("id")]
        public int DepartmentId { get; set; }

        [Column("name")]
        public string DepartmentName { get; set; }
    }
}
