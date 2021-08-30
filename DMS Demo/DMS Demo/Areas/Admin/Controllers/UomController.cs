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

namespace DMS_Demo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UomController : Controller
    {
        private readonly IBaseService<UOM> uomservice;

        public UomController(IBaseService<UOM> uomservice)
        {
            this.uomservice = uomservice;
        }

        // GET: Admin/Category
        public IActionResult Index()
        {
           
            return View( uomservice.GetAll());
        }

        // GET: Admin/Category/Details/5
        public IActionResult Details(int id)
        {
            return View(uomservice.GetByID(id));
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description")] UOM uom)
        {
            
            
            if (!ModelState.IsValid)
                return View(uom);
            try
            {
                uomservice.Add(uom);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(uom);
            }
        }

        // GET: Admin/Category/Edit/5
        public IActionResult Edit(int id)
        {
            var uom = uomservice.GetByID(id);
            if (uom == null)
            {
                return NotFound();
            }
            ViewBag.categories_Names = uomservice.GetByID(id);
            return View(uom);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description")] UOM uom)
        {
            if (id != uom.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    uomservice.Update(id, uom);
                    //await _context.SaveChangesAsync();
                    //return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(uom.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction( nameof(Index));

            }
            return View(uom);
        }

            // GET: Admin/Category/Delete/5
            public IActionResult Delete(int id)
        {
            var uom = uomservice.GetByID(id);
            if (uom == null)
            {
                return NotFound();
            }

            // categoryservice.Delete(id);
            return View(uom);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            uomservice.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            var check = false;
            var item = uomservice.GetByID(id);
            if (item != null)
            {
                check = true;
            }
            return check;
        }

        public IActionResult CategorySearch(string name)
        {
            List<UOM> categ = uomservice.Search(name);
            return View(categ);
        }
    }
}
