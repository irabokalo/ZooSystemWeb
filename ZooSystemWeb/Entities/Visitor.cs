using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_system.Entities
{
    public class Visitor : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitorId { get; set; }

        public string Name { get; set; }
        public virtual Animal FavotiteAnimal { get; set; }
        public virtual List<Zoo> Zoos { get; set; }
    }
}
