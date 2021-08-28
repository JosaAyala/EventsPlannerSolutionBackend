using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.DomainContext.DomainEntities
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleDescription { get; set; }
    }
}
