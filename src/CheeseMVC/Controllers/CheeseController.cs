using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Cheese> cheeses = context.Cheeses.ToList();

            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if(ModelState.IsValid)
            {
                //Cheese newCheese = new Cheese
                //{
                //    Name = addCheeseViewModel.Name,
                //    Description = addCheeseViewModel.Description,
                //    Type = addCheeseViewModel.Type
                //};
                
                context.Cheeses.Add(addCheeseViewModel.CreateCheese(addCheeseViewModel));
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
            
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);
            }

            context.SaveChanges();
            return Redirect("/");
        }
        
        public IActionResult Edit(int ID)
        {
            //cheeseId = 1;
            Cheese theCheese = context.Cheeses.Single(c => c.ID == ID);
            AddEditCheeseViewModel addEditCheeseViewModel = new AddEditCheeseViewModel();
            addEditCheeseViewModel.Name = theCheese.Name;
            addEditCheeseViewModel.Description = theCheese.Description;
            addEditCheeseViewModel.Type = theCheese.Type;
            addEditCheeseViewModel.Rating = theCheese.Rating;
            addEditCheeseViewModel.ID = theCheese.ID;

            return View(addEditCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddEditCheeseViewModel addEditCheeseViewModel)
        {
            if(ModelState.IsValid)
            {
                Cheese cheese = context.Cheeses.Single(c => c.ID == addEditCheeseViewModel.ID);
                cheese.Name = addEditCheeseViewModel.Name;
                cheese.Description = addEditCheeseViewModel.Description;
                cheese.Type = addEditCheeseViewModel.Type;
                cheese.Rating = addEditCheeseViewModel.Rating;
            }


            context.SaveChanges();
            return Redirect("/");
        }

        //public IActionResult Edit(int cheeseId)
        //{
        //    ViewBag.cheese = CheeseData.GetById(cheeseId);

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Edit(int cheeseId, string name, string description)
        //{
        //    var cheese = CheeseData.GetById(cheeseId);
        //    cheese.Name = name;
        //    cheese.Description = description;

        //    return Redirect("/");
        //}

    }
}
