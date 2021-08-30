using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Demo.Services
{
    public interface IFilterService<T>
    {
      

        

        List<T> FilterByPrice(int price);

        List<T> SortingItems(int id);
    }
}
