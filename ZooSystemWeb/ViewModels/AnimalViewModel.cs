using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zoo_system.Entities;

namespace ZooSystemWeb.ViewModels
{
    public class AnimalViewModel
    {

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Cost { get; set; }

        public int Size { get; set; }

        public string ZooName { get; set; }
        public string WorkerName { get; set; }

        public string WorkerSurname { get; set; }

        public int Age { get; set; }

        public int Salary { get; set; }
        public string VisitorName { get; set; }

        public string CareworkerName { get; set; }
        public string HomeZooName { get; set; }
        public string FavoriteAnimal { get; set; }
        public List<Worker> Workers { get; set; }
        public List<Animal> Animals { get; set; }
        public List<Zoo> Zoos { get; set; }
        public List<Visitor> Visitors { get; set; }
        public Animal TheMostPopularAnimal { get; set; }
        public Worker WorkerOfTheMostPopular { get; set; }
        public Visitor TheMostOftenVisitor { get; set; }
        public Animal TheMostExpensiveAnimal { get; set; }
        public  int NumberOfVisitors { get; set; }
        public int MoneyForAnimal { get; set; }
        public int MoneyForLastWeek { get; set; }
        public int MoneyFromCurrentDayToNow { get; set; }
    }
}