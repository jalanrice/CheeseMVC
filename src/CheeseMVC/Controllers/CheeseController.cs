﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Cheese> cheeses = CheeseData.GetAll();

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
                
                CheeseData.Add(addCheeseViewModel.CreateCheese(addCheeseViewModel));

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
            
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                CheeseData.Remove(cheeseId);
            }

            return Redirect("/");
        }
        
        public IActionResult Edit(int cheeseId)
        {
            //cheeseId = 1;
            var cheese = CheeseData.GetById(cheeseId);
            AddEditCheeseViewModel addEditCheeseViewModel = new AddEditCheeseViewModel();
            addEditCheeseViewModel.Name = cheese.Name;
            addEditCheeseViewModel.Description = cheese.Description;
            addEditCheeseViewModel.Type = cheese.Type;
            addEditCheeseViewModel.Rating = cheese.Rating;
            addEditCheeseViewModel.CheeseId = cheese.CheeseId;

            return View(addEditCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddEditCheeseViewModel addEditCheeseViewModel)
        {
            if(ModelState.IsValid)
            {
                var cheese = CheeseData.GetById(addEditCheeseViewModel.CheeseId);
                cheese.Name = addEditCheeseViewModel.Name;
                cheese.Description = addEditCheeseViewModel.Description;
                cheese.Type = addEditCheeseViewModel.Type;
                cheese.Rating = addEditCheeseViewModel.Rating;
            }
            
            

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
