using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Infrastructure;
using RestaurantMenu.BLL.Interfaces;
using RestaurantMenu.DAL.Data;
using RestaurantMenu.DAL.Entities;

namespace RestaurantMenu.BLL.Services
{
    public class DishService : IDishService
    {
        private MenuDBContext _context;

        public DishService(MenuDBContext context)
        {
            _context = context;
        }

        public async Task<OperationDetail> AddNewToDBAsync(DishDTO dto)
        {
            try
            {
                await _context.Dishes.AddAsync(GetEntityFromDTO(dto));
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            } 
            catch(Exception e)
            {
                List<string> errorList = new List<string>();
                errorList.Add("Ошибка при добавлении записи в базу данных: " + e.Message);
                return new OperationDetail { Succeeded = false, ErrorMessages = errorList };
            }
        }

        public async Task<OperationDetail> DeleteAsync(int id)
        {
            try
            {
                Dish entity = await _context.Dishes.FindAsync(id);
                _context.Dishes.Remove(entity);
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch(Exception e)
            {
                List<string> errorList = new List<string>();
                errorList.Add(e.Message);
                return new OperationDetail { Succeeded = false, ErrorMessages = errorList };
            }
        }

        public async Task<OperationDetail> EditAsync(int id, DishDTO dto)
        {
            try
            {
                Dish entity = await _context.Dishes.FindAsync(id);
                entity.Name = dto.Name;
                entity.AddingDate = dto.AddingDate;
                entity.Price = dto.Price;
                entity.Composition = dto.Composition;
                entity.Mass = dto.Mass;
                entity.CalorieContent = dto.CalorieContent;
                entity.CookingTime = dto.CookingTime;
                entity.Description = dto.Description;
                _context.Dishes.Update(entity);
                await _context.SaveChangesAsync();
                return new OperationDetail { Succeeded = true };
            }
            catch (Exception e)
            {
                List<string> errorList = new List<string>();
                errorList.Add(e.Message);
                return new OperationDetail { Succeeded = false, ErrorMessages = errorList };
            }
        }

        public async Task<OperationDetail<List<DishDTO>>> GetAllFromDBAsync()
        {
            try
            {
                var dtoList = new List<DishDTO>();
                var entityList = await _context.Dishes.AsNoTracking().ToListAsync();
                foreach(Dish entity in entityList)
                {
                    dtoList.Add(
                        new DishDTO
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
                        });
                }
                return (new OperationDetail<List<DishDTO>> { Succeeded = true, Data = dtoList });
            }
            catch(Exception e)
            {
                List<string> errorList = new List<string>();
                errorList.Add(e.Message + e.Message);
                return (new OperationDetail<List<DishDTO>> { Succeeded = false, ErrorMessages = errorList });
            }
        }

        public async Task<OperationDetail<DishDTO>> GetByIDAsync(int id)
        {
            try
            {
                Dish entity = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == id); 
                if(entity == null) 
                {
                    return new OperationDetail<DishDTO> { Succeeded = false, ErrorMessages = new List<string>() { "Блюдо не найдено" } };
                }
                    
                return (
                    new OperationDetail<DishDTO>
                    { 
                        Succeeded = true,
                        Data = new DishDTO
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
                        }
                    }
                );
            }
            catch (Exception e)
            {
                List<string> errorList = new List<string>();
                errorList.Add(e.Message);
                return new OperationDetail<DishDTO> { Succeeded = false, ErrorMessages = errorList };
            }
        }


        public async Task<OperationDetail<PaginatedList<DishDTO>>> GetSortedFilteredListFromDBAsync
            (int? pageIndex, SortDefinition sort, FilterDefinition[] filters = null)
        {
            int pageSize = 20;

            try
            {
                var dtoList = new List<DishDTO>();
                var dishes = from d in _context.Dishes select d;

                if (sort == null)
                {
                    sort.Name = "Id";
                    sort.IsAscending = true;
                }

                if (sort.IsAscending)
                {
                    dishes = dishes.OrderBy(e => EF.Property<object>(e, sort.Name));
                }
                else
                {
                    dishes = dishes.OrderByDescending(e => EF.Property<object>(e, sort.Name));
                }

                if(filters != null)
                {
                    foreach(var filter in filters)
                    {
                        switch (filter.Name)
                        {
                            case "Name":
                                if (!String.IsNullOrEmpty(filter.Value))
                                {
                                    dishes = dishes.Where(d => d.Name.ToUpper().Contains(filter.Value.ToUpper()));
                                }
                                break;
                            case "DescrComp":
                                if (!String.IsNullOrEmpty(filter.Value))
                                {
                                    dishes = dishes.Where(d => d.Description.ToUpper().Contains(filter.Value.ToUpper())
                                            || d.Composition.ToUpper().Contains(filter.Value.ToUpper()));
                                }
                                break;
                            case "MinMass":
                                int minMass;
                                if ((minMass = Convert.ToInt32(filter.Value)) > 0)
                                {
                                    dishes = dishes.Where(d => (d.Mass >= minMass));
                                }
                                break;
                            case "MaxMass":
                                int maxMass;
                                if ((maxMass = Convert.ToInt32(filter.Value)) > 0)
                                {
                                    dishes = dishes.Where(d => (d.Mass <= maxMass));
                                }
                                break;
                            case "MinTime":
                                int minTime;
                                if ((minTime = Convert.ToInt32(filter.Value)) > 0)
                                {
                                    dishes = dishes.Where(d => (d.CookingTime >= minTime));
                                }
                                break;
                            case "MaxTime":
                                int maxTime;
                                if ((maxTime = Convert.ToInt32(filter.Value)) > 0)
                                {
                                    dishes = dishes.Where(d => (d.CookingTime <= maxTime));
                                }
                                break;
                        }
                    }
                }
                //PaginatedList<Dish> menuDishes = await PaginatedList<Dish>.CreateAsync(dishes.AsNoTracking(), pageIndex ?? 1, pageSize);
                PaginatedList<DishDTO> menuDishes = await PaginatedList<DishDTO>.CreateAsync<Dish>(dishes.AsNoTracking(), pageIndex ?? 1, pageSize, DishDTO.Map);

                return (new ListOparationDetail<DishDTO> { Succeeded = true, Data = menuDishes });
            }

            catch (Exception e)
            {
                List<string> errorList = new List<string>();
                errorList.Add(e.Message + e.Message);
                return (new ListOparationDetail<DishDTO> { Succeeded = false, ErrorMessages = errorList });
            }
        }

        private Dish GetEntityFromDTO(DishDTO dto)
        {
            return new Dish
            {
                Name = dto.Name,
                Price = dto.Price,
                Composition = dto.Composition,
                Mass = dto.Mass,
                CalorieContent = dto.CalorieContent,
                CookingTime = dto.CookingTime,
                Description = dto.Description,
                AddingDate = DateTime.Now
            };
        }
    }
}
