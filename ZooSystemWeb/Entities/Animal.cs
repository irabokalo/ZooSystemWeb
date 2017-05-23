using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo_system.Entities
{
    public class Animal : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public int Populatiry { get; set; }
        public int Cost { get; set; }
        public virtual Zoo HomeZoo { get; set; }
        public virtual Worker CareWorker { get; set; }
    }
}
