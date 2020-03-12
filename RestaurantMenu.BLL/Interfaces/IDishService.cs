using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestaurantMenu.BLL.Infrastructure;
using RestaurantMenu.BLL.DTO;

namespace RestaurantMenu.BLL.Interfaces
{
    public interface IDishService<T>
    {
        public Task<(T, List<DishDTO>)> GetAllFromDBAsync();
        public Task<(T, DishDTO)> GetByIDAsync(int id);
        public Task<T> AddNewToDBAsync(DishDTO dto);
        public Task<T> EditAsync(int id, DishDTO dto);
        public Task<T> DeleteAsync(int id);
    }
}
