using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.BLL.Infrastructure
{
    public class OperationDetail<T> : OperationDetail where T: class
    {
        public T Data { get; set; }
    }

    public class OperationDetail 
    {
        public bool Succeeded { get; set; }
        public List<string> ErrorMessages { get; set; }

        public OperationDetail()
        {
            ErrorMessages = new List<string>();
        }
    }

}
