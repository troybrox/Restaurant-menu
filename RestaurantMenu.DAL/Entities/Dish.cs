using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMenu.DAL.Entities
{
    public class Dish
    {
        public int Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AddingDate { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Composition { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(8, 2)")] 
        public decimal Price { get; set; }

        public int Mass { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal CalorieContent { get; set; }

        public int CookingTime { get; set; }
    }
}
