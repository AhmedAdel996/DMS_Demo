using DMS_Demo.Data;
using DMS_Demo.Models;
using DMS_Demo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Demo.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBaseService<Product> baseService;
        
        private readonly IFilterService<Product> filterService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;

        public ProductController(IBaseService<Product> baseService, IFilterService<Product> filterService, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            this.baseService = baseService;
            
            this.filterService = filterService;
            this.userManager = userManager;
            this.context = context;
        }


        public IActionResult Index()
        {
            
            
            var products = baseService.GetAll();

            foreach (var item in products)
            {
                item.Product_Price = item.Product_Price - item.Discount;
            }

            return View(products);
        }
       
        public IActionResult SortProducts(int id)
        {
           
            var products = filterService.SortingItems(id);
           
            return View("Index", products);
        }
        public IActionResult ProductsWithPrice(int id)
        {
            
            var products = filterService.FilterByPrice(id);
           
            return View("Index", products);
        }
      
        public IActionResult ProductSearch(string name)
        {
           
            var products = baseService.Search(name);
           
            return View("Index", products);
        }
        public IActionResult Details(int id)
        {
           
            Product product = baseService.GetByID(id);
            
            context.SaveChanges();

           

            return View(product);
        }
       
       


    }
}
