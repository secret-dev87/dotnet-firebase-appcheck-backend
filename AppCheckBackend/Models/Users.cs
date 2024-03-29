using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AppCheckBackend.Models
{
    [Table("users")]
    public class Users : BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        [AllowNull]
        public string Name { get; set; }

        [StringLength(50)]
        [AllowNull]
        public string Email { get; set; }

        [StringLength(50)]
        [AllowNull]
        public string Password { get; set; }
    }
}
