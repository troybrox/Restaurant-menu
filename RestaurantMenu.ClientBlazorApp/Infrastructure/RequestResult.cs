using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantMenu.ClientBlazorApp.ViewModels;

namespace RestaurantMenu.ClientBlazorApp.Infrastructure
{
    public class RequestResult<T> : RequestResult where T : class
    {
        public T Data { get; set; }
    }

    public class ListRequestResult<T> : RequestResult<T> where T : class
    {
        public int TotalPages { get; set; }

        public int PageIndex { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }

    public class RequestResult
    {
        public bool Succeeded { get; set; }

        public List<string> ErrorMessages { get; set; }
    }
}
