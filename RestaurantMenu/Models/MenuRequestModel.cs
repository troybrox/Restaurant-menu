using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantMenu.BLL.Infrastructure;

namespace RestaurantMenu.Models
{
    public class MenuRequestModel
    {
        public int PageIndex { get; set; }

        public SortDefinition Sort { get; set; }

        public FilterDefinition[] Filters { get; set; }
    }
}
