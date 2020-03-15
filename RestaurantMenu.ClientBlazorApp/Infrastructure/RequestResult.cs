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

    public class RequestResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
