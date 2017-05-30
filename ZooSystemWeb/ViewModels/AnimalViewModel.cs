using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zoo_system.Entities;

namespace ZooSystemWeb.ViewModels
{
  public class AnimalViewModel
  {

    public string Name { get; set; }

    public string Description { get; set; }

    public int Cost { get; set; }

    public int Size { get; set; }

    public string WorkerName { get; set; }

    public string WorkerSurname { get; set; }

    public int Age { get; set; }

    public int Salary { get; set; }

    public string  CareworkerName { get; set; }
    public List<Worker> Workers { get; set; }
    public List<Animal> Animals { get; set; }
  }
}