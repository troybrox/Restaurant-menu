using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.BLL.DTO
{
    public class DishDTO
    {
        public int Id { get; set; }

        public DateTime AddingDate { get; set; }

        public string Name { get; set; }

        public string Composition { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Mass { get; set; }

        public decimal CalorieContent { get; set; }

        public int CookingTime { get; set; }
    }
}
