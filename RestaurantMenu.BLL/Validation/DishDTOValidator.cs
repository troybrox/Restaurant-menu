using System;
using System.Collections.Generic;
using System.Text;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Exceptions;

namespace RestaurantMenu.BLL.Validation
{
    public class DishDTOValidator
    {
        public static void Validate(DishModelDTO dto)
        {
            if (String.IsNullOrEmpty(dto.Name))
                throw new ValidationException("Укажите корректно название блюда");

            if (String.IsNullOrEmpty(dto.Description))
                throw new ValidationException("Укажите корректно описание блюда");

            if (String.IsNullOrEmpty(dto.Composition))
                throw new ValidationException("Укажите корректно состав блюда");

            if (dto.CalorieContent <= 0 || dto.CalorieContent > 999999)
                throw new ValidationException("Укажите корректно калорийность блюда, диапазон от 1 до 999999 кал");

            if (dto.CookingTime <= 0 || dto.CookingTime >= 99999)
                throw new ValidationException("Укажите корректно время приготовления блюда, диапазон от 1 до 99999 мин");

            if (dto.CalorieContent <= 0 || dto.CalorieContent >= 99999)
                throw new ValidationException("Укажите корректно калорийность на 100г блюда, диапазон от 1 до 99999 г");

            if (dto.Price <= 0 || dto.Price >= 999999)
                throw new ValidationException("Укажите корректно цену блюда, диапазон от 1 до 999999 руб");
        }

        public static bool Validate(DishModelDTO dto, out List<string> messages)
        {
            bool res = true;
            messages = new List<string>();

            if (String.IsNullOrEmpty(dto.Name))
            {
                messages.Add("Укажите корректно название блюда");
                res = false;
            }

            if (String.IsNullOrEmpty(dto.Description))
            {
                messages.Add("Укажите корректно описание блюда");
                res = false;
            }

            if (String.IsNullOrEmpty(dto.Composition))
            {
                messages.Add("Укажите корректно состав блюда");
                res = false;
            }

            if (dto.CalorieContent <= 0 || dto.CalorieContent > 999999)
            {
                messages.Add("Укажите корректно калорийность блюда, диапазон от 1 до 999999 кал");
                res = false;
            }

            if (dto.CookingTime <= 0 || dto.CookingTime >= 99999)
            {
                messages.Add("Укажите корректно время приготовления блюда, диапазон от 1 до 99999 мин");
                res = false;
            }

            if (dto.CalorieContent <= 0 || dto.CalorieContent >= 99999)
            {
                messages.Add("Укажите корректно калорийность на 100г блюда, диапазон от 1 до 99999 г");
                res = false;
            }

            if (dto.Price <= 0 || dto.Price >= 999999)
            {
                messages.Add("Укажите корректно цену блюда, диапазон от 1 до 999999 руб");
                res = false;
            }

            return res;
        }
    }
}
