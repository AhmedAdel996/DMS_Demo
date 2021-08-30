using DMS_Demo.Data;
using DMS_Demo.Models;
using DMS_Demo.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Demo.Services
{
    public class FilterProducts : IFilterService<Product>
    {
        private readonly ApplicationDbContext context;

        public FilterProducts(ApplicationDbContext context)
        {
            this.context = context;
        }
       
        public List<Product> FilterByPrice(int id)
        {
            List<Product> products;
            switch (id)
            {
                case (1):
                    products = context.Products.Where(model => model.Product_Price > 0 && model.Product_Price <= 100).ToList();
                    break;
                case (2):
                    products = context.Products.Where(model => model.Product_Price > 100 && model.Product_Price <= 150).ToList();
                    break;
                case (3):
                    products = context.Products.Where(model => model.Product_Price > 150 && model.Product_Price <= 200).ToList();
                    break;
                case (4):
                    products = context.Products.Where(model => model.Product_Price > 200).ToList();
                    break;
                default:
                    products = context.Products.ToList();
                    break;
            }
            return products;
        }
       
        public List<Product> SortingItems(int id)
        {
            List<Product> products;
            switch (id)
            {
                case (1):
                    products = context.Products
                            .OrderByDescending(model => model.Product_Price).ToList();
                    break;
                case (2):
                    products = context.Products
                            .OrderByDescending(model => model.Adding_Date).ToList();
                    break;
                case (3):
                    products = context.Products
                            .OrderBy(model => model.Product_Price).ToList();
                    break;
                
                default:
                    products = context.Products.ToList();
                    break;
            }
            return products;
        }
    }
}
