using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantMenu.ClientBlazorApp.Infrastructure
{
    public class MenuRequestObject
    {
        public int PageIndex { get; set; }

        public SortDefinition Sort { get; set; }

        public List<FilterDefinition> Filters { get; set; }


        public MenuRequestObject()
        {
            Filters = new List<FilterDefinition>();
        }
    }
}
