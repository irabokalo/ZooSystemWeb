using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo_system.Entities
{
    public class Zoo: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ZooId { get; set; }
        public string Name { get; set; }
        public virtual List<Animal> Animals { get; set; }
        public virtual List<Visitor> Visitors { get; set; }
        public virtual List<Worker> Workers { get; set; }

    }
}
