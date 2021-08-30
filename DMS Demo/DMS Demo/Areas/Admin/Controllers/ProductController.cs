using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMS_Demo.Data;
using DMS_Demo.Models;
using DMS_Demo.Services;
using Microsoft.AspNetCore.Authorization;
using DMS_Demo.ViewModel;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace DMS_Demo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IBaseService<Product> productservice;
        private readonly IBaseService<UOM> uomservice;


        public ProductController(ApplicationDbContext db, IBaseService<Product> productservice, IBaseService<UOM> uomservice)
        {
            this.db = db;
            this.productservice = productservice;
            this.uomservice = uomservice;

        }

        // GET: Admin/Product
        public IActionResult Index()
        {
            ViewBag.categ = uomservice.GetAll();

            return View(productservice.GetAll());
        }

        // GET: Admin/Product/Details/5
        public IActionResult Details(int id)
        {
            ViewBag.categ = uomservice.GetAll();
            return View(productservice.GetByID(id));
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            // ViewData["Category_ID"] = new SelectList(categoryservice.GetAll());
            //ViewData["Images_ID"] = new SelectList(imageservice.GetAll());

            ViewBag.categ = uomservice.GetAll();
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Product_ID,Product_Name,Uom_Id,Description,Product_Price,Images,Product_Size,Product_Color,Adding_Date,Stored_Quantity,Discount")] Product product, IFormFile image)
        {
            ViewBag.categ = uomservice.GetAll();
            if (image != null)
            {

                //Set Key Name
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                //Get url To Save
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Products/", ImageName);

                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                product.Images = ImageName;

            }

            try
            {
                productservice.Add(product);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(product);
            }



        }

        // GET: Admin/Product/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.categ = uomservice.GetAll();

            var product = productservice.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.prods = productservice.GetByID(id);

            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Product_ID,Product_Name,Uom_Id,Description,Product_Price,Product_Size,Product_Color,Adding_Date,Popularity,Stored_Quantity")] Product product, IFormFile image)
        {
            ViewBag.categ = uomservice.GetAll();

            if (ModelState.IsValid)
            {

                try
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                    //Get url To Save
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Products/", ImageName);

                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                    product.Images = ImageName;

                    productservice.Update(id, product);

                }
                catch (DbUpdateConcurrencyException)
                {

                    return NotFound();

                }
            }
            return RedirectToAction(nameof(Index));
        }


            // GET: Admin/Product/Delete/5
        public IActionResult Delete(int id)
        {
            ViewBag.categ = uomservice.GetAll();

            var product = productservice.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            productservice.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            var check = false;
            var item = productservice.GetByID(id);
            if (item != null)
            {
                check = true;
            }
            return check;
        }
    }
}
