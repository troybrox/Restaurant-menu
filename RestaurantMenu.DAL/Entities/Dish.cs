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

        [Column(TypeName = "varchar(1000)")]
        public string Composition { get; set; }

        [Column(TypeName = "varchar(1000)")]
        public string Description { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal Price { get; set; }

        public int Mass { get; set; }

        public int CaloricContent { get; set; }

        public int CookingTime { get; set; }
    }
}
