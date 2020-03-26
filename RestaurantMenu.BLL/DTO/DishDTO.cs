﻿using System;
using System.ComponentModel.DataAnnotations;
using RestaurantMenu.DAL.Entities;

namespace RestaurantMenu.BLL.DTO
{
    public class DishModelDTO
    {
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите состав")]
        public string Composition { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = "Введите вес порции")]
        [Range(1, 99999, ErrorMessage = "Вес порции должен быть в промежутке от 1 до 999999 грамм")]
        public int Mass { get; set; }

        //[Required(ErrorMessage = "Введите значение калорийности")]
        [Range(1, 99999, ErrorMessage = "Значение калорийности блюда должна быть в промежутке от 1 до 999999 калорий")]
        public decimal CalorieContent { get; set; }

        //[Required(ErrorMessage = "Введите время приготовления")]
        [Range(1, 99999, ErrorMessage = "Время приготовления должно быть в промежутке от 1 до 99999 минут")]
        public int CookingTime { get; set; }
    }

    public class DishDTO : DishModelDTO
    {
        public int Id { get; set; }

        public DateTime AddingDate { get; set; }

        //[Required(ErrorMessage = "Введите название")]
        //public string Name { get; set; }

        //[Required(ErrorMessage = "Введите состав")]
        //public string Composition { get; set; }

        //[Required(ErrorMessage = "Введите описание")]
        //public string Description { get; set; }

        //[Required(ErrorMessage = "Введите цену")]
        //public decimal Price { get; set; }

        //[Required(ErrorMessage = "Введите вес порции")]
        //[Range(1, 99999, ErrorMessage = "Вес порции должен быть в промежутке от 1 до 999999 грамм")]
        //public int Mass { get; set; }

        //[Required(ErrorMessage = "Введите значение калорийности")]
        //[Range(1, 99999, ErrorMessage = "Значение калорийности блюда должна быть в промежутке от 1 до 999999 калорий")]
        //public decimal CalorieContent { get; set; }

        //[Required(ErrorMessage = "Введите время приготовления")]
        //[Range(1, 99999, ErrorMessage = "Время приготовления должно быть в промежутке от 1 до 99999 минут")]
        //public int CookingTime { get; set; }


        public static DishDTO Map(Dish entity)  => new DishDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            AddingDate = entity.AddingDate,
            Price = entity.Price,
            Composition = entity.Composition,
            Mass = entity.Mass,
            CalorieContent = entity.CalorieContent,
            CookingTime = entity.CookingTime,
            Description = entity.Description
        };
    }
}
