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
                string[] arr = model.CareworkerName.Split(' ');

                if ((_context.Workers.ToList().Find(x => x.Name == arr[0]) != null) /*&& (_context.Workers.ToList().Find(x => x.Name == arr[0]) != null)*/ && (_context.Zoos.ToList().Find(x => x.Name == model.HomeZooName) != null))
                {
                    _context.Animals.Add(new Animal
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Cost = model.Cost,
                        CareWorker = _context.Workers.ToList().Find(x => x.Name == model.CareworkerName),
                        HomeZoo = _context.Zoos.ToList().Find(x => x.Name == model.HomeZooName)
                    });
                    _context.SaveChanges();
                }
                else
                {
                    return RedirectToAction("ErrorState", new { message = "Such worker or zoo doesn't exist!" });
                }
            }

            return RedirectToAction("ViewAllAnimals");

        }

        public ActionResult ErrorState(string message)
        {
            ViewBag.message = message;
            return View();
        }


        [HttpPost]
        public ActionResult AddWorker(AnimalViewModel model)
        {
            if ((_context.Zoos.ToList().Find(x => x.Name == model.HomeZooName) != null))
            {
                _context.Workers.Add(new Worker
                {
                    Name = model.WorkerName,
                    Surname = model.WorkerSurname,
                    Salary = model.Salary,
                    Age = model.Age,
                    WorkZoo = _context.Zoos.ToList().Find(x => x.Name == model.HomeZooName)
                });
            }
            else
            {
                return RedirectToAction("ErrorState", new { message = "Such zoo doesn't exist!" });
            }
            _context.SaveChanges();
            return RedirectToAction("ViewAllAnimals");
        }

        [HttpPost]
        public ActionResult AddVisitor(AnimalViewModel model)
        {
            if ((_context.Animals.ToList().Find(x => x.Name == model.FavoriteAnimal) != null))
            {
                _context.Visitors.Add(new Visitor
                {
                    Name = model.VisitorName,
                    DateOfVisit = DateTime.Now.ToLongDateString(),
                    FavotiteAnimal = _context.Animals.ToList().Find(x => x.Name == model.FavoriteAnimal)
                });
            }
            else
            {
                return RedirectToAction("ErrorState", new { message = "Such animal doesn't exist!" });
            }
            _context.SaveChanges();
            return RedirectToAction("ViewAllAnimals");
        }

        [HttpPost]
        public ActionResult AddZoo(AnimalViewModel model)
        {
            _context.Zoos.Add(new Zoo
            {
                Name = model.ZooName
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
            wholeModel.Visitors = _context.Visitors.ToList();
            wholeModel.TheMostPopularAnimal = FindTheMostPopularAnimal();
            if (FindTheMostPopularAnimal() != null)
            {
                wholeModel.WorkerOfTheMostPopular = FindTheMostPopularAnimal().CareWorker;
            }
           
            wholeModel.TheMostOftenVisitor = TheMostPopularVisitor();
            wholeModel.TheMostExpensiveAnimal = FindTheMostExpensiveAnimal();
         
            wholeModel.NumberOfVisitors = _context.Visitors.Count();
           
            return View("AdminView", wholeModel);
        }

        public Animal FindTheMostExpensiveAnimal()
        {
            int maxCost = 0;
            foreach (var animal in _context.Animals)
            {
                if (animal.Cost > maxCost)
                {
                    maxCost = animal.Cost;
                }
            }
            return _context.Animals.ToList().Find(x => x.Cost == maxCost);
        }

        public  Animal FindTheMostPopularAnimal()
        {
            Dictionary<int, int> animalDictionary = new Dictionary<int, int>();
            if (_context.Visitors.ToList().Count > 0)
            {
                foreach (var animal in _context.Animals.ToList())
                {
                    animalDictionary.Add(animal.AnimalId, 0);
                }

                foreach (var visitor in _context.Visitors.ToList())
                {
                    animalDictionary[visitor.FavotiteAnimal.AnimalId]++;
                }

                int maxPopularity = 0;
                int maxKey = 0;
              
                foreach (var dictElement in animalDictionary)
                {
                    if (dictElement.Value > maxPopularity)
                    {
                        maxPopularity = dictElement.Value;
                        maxKey = dictElement.Key;
                    }
                }
                return _context.Animals.Find(animalDictionary[maxKey]);
            }
            return null;
        }

        public Visitor TheMostPopularVisitor()
        {
            Dictionary<string, int> visitorsDictionary = new Dictionary<string, int>();
            if (_context.Visitors.ToList().Count() > 0)
            {
                foreach (var visitor in _context.Visitors.ToList())
                {
                    visitorsDictionary.Add(visitor.Name, 1);
                }

                foreach (var visitor in _context.Visitors.ToList())
                {
                    visitorsDictionary[visitor.Name]++;
                }

                int maxPopularity = 0;
                string maxKey = string.Empty;

                foreach (var dictElement in visitorsDictionary)
                {
                    if (dictElement.Value > maxPopularity)
                    {
                        maxPopularity = dictElement.Value;
                        maxKey = dictElement.Key;
                    }
                }
                return _context.Visitors.Find(visitorsDictionary[maxKey]);
            }
            return null;
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

        public ActionResult SearchAnimal(string searchString)
        {

            AnimalViewModel model = new AnimalViewModel();
            model.Workers = _context.Workers.ToList();
            model.Animals = _context.Animals.ToList();
            model.Zoos = _context.Zoos.ToList();
            model.Animals =
                model.Animals.ToList().Where(animal => animal.Name.ToLower().Contains(searchString.ToLower())).ToList();
            return View("AdminView", model);
        }

        public ActionResult SearchVisitor(string searchString)
        {

            AnimalViewModel model = new AnimalViewModel();
            model.Workers = _context.Workers.ToList();
            model.Animals = _context.Animals.ToList();
            model.Zoos = _context.Zoos.ToList();
            model.Visitors =
                model.Visitors.ToList().Where(animal => animal.Name.ToLower().Contains(searchString.ToLower())).ToList();
            return View("AdminView", model);
        }

        public ActionResult SearchWorker(string searchString)
        {
            AnimalViewModel model = new AnimalViewModel();
            model.Workers = _context.Workers.ToList();
            model.Animals = _context.Animals.ToList();
            model.Zoos = _context.Zoos.ToList();
            model.Workers =
                model.Workers.ToList().Where(worker => worker.Name.ToLower().Contains(searchString.ToLower())).ToList();
            return View("AdminView", model);
        }

        public ActionResult SearchZoo(string searchString)
        {
            AnimalViewModel model = new AnimalViewModel();
            model.Workers = _context.Workers.ToList();
            model.Animals = _context.Animals.ToList();
            model.Zoos = _context.Zoos.ToList();
            model.Zoos =
              model.Zoos.ToList().Where(zoo => zoo.Name.ToLower().Contains(searchString.ToLower())).ToList();
            return View("AdminView", model);
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