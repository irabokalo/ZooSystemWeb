using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZooSystemWeb.ViewModels;
using Zoo_system.Concrete;
using Zoo_system.Entities;

namespace ZooSystemWeb.Controllers
{
  public class AdminController : Controller
  {
    private readonly ZooDbContext _context = new ZooDbContext();
    // GET: Admin
    public ActionResult Index()
    {
      return View("AdminView");
    }

    [HttpPost]
    public ActionResult AddAnimal(AnimalViewModel model)
    {
        if (ModelState.IsValid)
        {

            if (_context.Workers.ToList().Find(x => x.Name == model.CareworkerName) != null)
            {
                _context.Animals.Add(new Animal
                {
                    Name = model.Name,
                    Description = model.Description,
                    Cost = model.Cost,
                    CareWorker = _context.Workers.ToList().Find(x => x.Name == model.CareworkerName)

                });
                _context.SaveChanges();
            }
                else
                {
                    return RedirectToAction("ErrorState","Admin","Such worker doesn't exist!");
                }
            }
       
        return RedirectToAction("ViewAllAnimals");

    }

      public ActionResult ErrorState()
      {
          return View();
      }


    [HttpPost]
    public ActionResult AddWorker(AnimalViewModel model)
    {

      _context.Workers.Add(new Worker
      {
        Name = model.WorkerName,
        Surname = model.WorkerSurname,
        Salary = model.Salary,
        Age = model.Age
      });

           

      _context.SaveChanges();


      return RedirectToAction("ViewAllAnimals");
    }

    public ActionResult ViewAllAnimals()
    {
      AnimalViewModel wholeModel = new AnimalViewModel();
      wholeModel.Workers = _context.Workers.ToList();
      wholeModel.Animals = _context.Animals.ToList();
        wholeModel.Zoos = _context.Zoos.ToList();
      return View("AdminView", wholeModel);
    }

    [HttpPost, ActionName("DeleteAnimal")]
    public ActionResult DeleteAnimal(int id)
    {
      if (_context.Animals.Find(id) != null)
      {
        _context.Animals.Remove(_context.Animals.Find(id));
      }
      _context.SaveChanges();
      return RedirectToAction("ViewAllAnimals");
    }

    [HttpPost, ActionName("DeleteWorker")]

    public ActionResult DeleteWorker(int id)
    {
      if (_context.Workers.Find(id) != null)
      {
        _context.Workers.Remove(_context.Workers.Find(id));
      }
      _context.SaveChanges();
      return RedirectToAction("ViewAllAnimals");
    }

        [HttpPost, ActionName("DeleteZoo")]

        public ActionResult DeleteZoo(int id)
        {
            if (_context.Zoos.Find(id) != null)
            {
                _context.Zoos.Remove(_context.Zoos.Find(id));
            }
            _context.SaveChanges();
            return RedirectToAction("ViewAllAnimals");
        }

    }
}