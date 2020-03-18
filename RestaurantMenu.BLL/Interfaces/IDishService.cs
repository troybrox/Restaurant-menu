using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestaurantMenu.BLL.Infrastructure;
using RestaurantMenu.BLL.DTO;

namespace RestaurantMenu.BLL.Interfaces
{
    public interface IDishService
    {
        public Task<OperationDetail<List<DishDTO>>> GetAllFromDBAsync();
        public Task<OperationDetail<List<DishDTO>>> GetSortedListFromDBAsync(string sortParam);
        public Task<OperationDetail<List<DishDTO>>> GetSortedListFromDBAsync_3(string sortParam, string searchString);
        public Task<OperationDetail<DishDTO>> GetByIDAsync(int id);
        public Task<OperationDetail> AddNewToDBAsync(DishDTO dto);
        public Task<OperationDetail> EditAsync(int id, DishDTO dto);
        public Task<OperationDetail> DeleteAsync(int id);
    }
}
