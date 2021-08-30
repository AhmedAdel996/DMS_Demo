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
    public class ProductService : IBaseService<Product>
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(Product model)
        {
            context.Products.Add(model);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Product product = context.Products.FirstOrDefault(prod => prod.Product_ID == id);
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            List<Product> products = context.Products.ToList();
             
            
            return products;
        }
        public Product GetByID(int id)
        {
            Product product = context.Products.FirstOrDefault(prod => prod.Product_ID == id);
           
            return product;
        }

        public List<Product> Search(string name)
        {
            List<Product> products = context.Products.Where(prod => prod.Product_Name.Contains(name)).ToList();
            return products;
        }

        public void Update(int id, Product model)
        {
            Product product = context.Products.FirstOrDefault(prod => prod.Product_ID == id);
            product.Uom_Id = model.Uom_Id;

            product.Description = model.Description;
            product.Product_Color = model.Product_Color;
            product.Product_Name = model.Product_Name;
            product.Product_Price = model.Product_Price;
            product.Images = model.Images;
            product.Product_Size = model.Product_Size;
            product.Stored_Quantity = model.Stored_Quantity;
            product.Discount = model.Discount;
           
            context.SaveChanges();
        }
        
    }
}
