using DMS_Demo.Data;
using DMS_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Demo.Services
{
    public class UomService : IBaseService<UOM>
    {
        private readonly ApplicationDbContext context;

        public UomService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public void Add(UOM model)
        {
            context.Uoms.Add(model);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            UOM uom = context.Uoms.FirstOrDefault(u => u.Id == id);
            context.Uoms.Remove(uom);
            context.SaveChanges();
        }

        public List<UOM> GetAll()
        {
            List<UOM> uoms = context.Uoms.ToList();
            return uoms;
        }
        public UOM GetByID(int id)
        {
            UOM uom = context.Uoms.FirstOrDefault(u => u.Id == id);
            return uom;
        }

        public List<UOM> Search(string name)
        {
            List<UOM> uoms = context.Uoms.Where(u => u.Name.Contains(name)).ToList();
            return uoms;
        }

        public void Update(int id, UOM model)
        {
            UOM uom = context.Uoms.FirstOrDefault(u => u.Id == id);
            uom.Name = model.Name;
            uom.Description = model.Description;
            context.SaveChanges();
        }

    }
}



