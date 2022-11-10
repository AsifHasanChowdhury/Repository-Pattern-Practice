﻿using Microsoft.AspNetCore.Mvc;
using RepositoryPatternPractice.Models;
using RepositoryPatternPractice.Models.Data_Access_Layer.Class;
using RepositoryPatternPractice.Models.Data_Access_Layer;
using System.Diagnostics;

namespace RepositoryPatternPractice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUsersRepository _productRepository;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;

            this._productRepository = new ProductRepository(configuration);
        }

 
        public IActionResult Index()

        {
            return View(_productRepository.GetProducts());
        }

        //GET CREATE
        public IActionResult Create()
        {
            return View(new Product_Table());
        }

        //Post CREATE
        [HttpPost]
        public IActionResult Create(Product_Table product)
        {
            _productRepository.InsertProduct(product);
            return RedirectToAction("Index");
        }

        //Get Update
        public IActionResult Update(int Id)
        {
            return View(_productRepository.GetProductById(Id));
        }

        //Post Update
        [HttpPost]
        public IActionResult Update(Product_Table product)
        {
            _productRepository.UpdateProduct(product);
           // _productRepository.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int Id)
        {
            _productRepository.DeleteProduct(Id);
           // _productRepository.SaveChanges();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}