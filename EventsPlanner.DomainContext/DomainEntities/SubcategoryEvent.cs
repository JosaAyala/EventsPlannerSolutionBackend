using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.DomainContext.DomainEntities
{
    [Table("SubcategoryEvent")]
    public class SubcategoryEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int SubcategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }
    }
}
