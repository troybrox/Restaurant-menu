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

        public Task<OperationDetail<PaginatedList<DishDTO>>> GetSortedFilteredListFromDBAsync
            (SortDefinition sort, int? pageIndex, FilterDefinition[] filters = null);

        public Task<OperationDetail<DishDTO>> GetByIDAsync(int id);

        public Task<OperationDetail> AddNewToDBAsync(DishDTO dto);

        public Task<OperationDetail> AddNewToDBAsync(DishModelDTO dto);

        public Task<OperationDetail> EditAsync(int id, DishModelDTO dto);

        public Task<OperationDetail> DeleteAsync(int id);
    }
}
