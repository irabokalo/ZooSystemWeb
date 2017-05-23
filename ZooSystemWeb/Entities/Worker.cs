using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_system.Entities
{
    public class Worker: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkerId { get; set; }
        public virtual List<Animal> CaredAnimals { get; set; }
        public int Salary { get; set; } 
        public virtual Zoo WorkZoo { get; set; }
    }
}
