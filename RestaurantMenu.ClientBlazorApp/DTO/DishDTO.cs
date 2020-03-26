using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestaurantMenu.ClientBlazorApp.DTO
{
    public class DishDTO
    {
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите состав")]
        public string Composition { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Range(1, 99999, ErrorMessage = "Цена должна быть в промежутке от 1 до 999999 рублей")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Введите массу")]
        [Range(1, 99999, ErrorMessage = "Вес порции должен быть в промежутке от 1 до 999999 грамм")]
        public int Mass { get; set; }

        [Required(ErrorMessage = "Введите калорийность")]
        [Range(1, 99999, ErrorMessage = "Значение калорийности блюда должна быть в промежутке от 1 до 999999 калорий")]
        public decimal CalorieContent { get; set; }

        [Required(ErrorMessage = "Введите время приготовления")]
        [Range(1, 99999, ErrorMessage = "Время приготовления должно быть в промежутке от 1 до 99999 минут")]
        public int CookingTime { get; set; }
    }
}
