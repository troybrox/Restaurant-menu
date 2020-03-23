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
                return new OperationDetail { Succeeded = false, Message = "Ошибка при добавлении записи в базу данных:\n" + e.Message };
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
                return new OperationDetail { Succeeded = false, Message = e.Message };
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
                return new OperationDetail { Succeeded = false, Message = e.Message };
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
                return (new OperationDetail<List<DishDTO>> { Succeeded = false, Message = "Ошибка при получении списка блюд из базы данных:\n" + e.Message });
            }
        }

        public async Task<OperationDetail<DishDTO>> GetByIDAsync(int id)
        {
            try
            {
                Dish entity = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == id); 
                if(entity == null)
                    return new OperationDetail<DishDTO> { Succeeded = false, Message = "Блюдо не найдено." };
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
            catch (Exception ex)
            {
                return new OperationDetail<DishDTO> { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<OperationDetail<List<DishDTO>>> GetSortedListFromDBAsync(string sortOrder)
        {
            SortParamContract contract = new SortParamContract();
            try
            {
                var dtoList = new List<DishDTO>();
                var dishes = from d in _context.Dishes select d;
                switch (sortOrder)
                {
                    case "name":
                        dishes = dishes.OrderBy(d => d.Name);
                        break;
                    case "name_desc":
                        dishes = dishes.OrderByDescending(d => d.Name);
                        break;
                    case "description":
                        dishes = dishes.OrderBy(d => d.Description);
                        break;
                    case "description_desc":
                        dishes = dishes.OrderByDescending(d => d.Description);
                        break;
                    case "composition":
                        dishes = dishes.OrderBy(d => d.Composition);
                        break;
                    case "composition_desc":
                        dishes = dishes.OrderByDescending(d => d.Composition);
                        break;
                    case "price":
                        dishes = dishes.OrderBy(d => d.Price);
                        break;
                    case "price_desc":
                        dishes = dishes.OrderByDescending(d => d.Price);
                        break;
                    case "mass":
                        dishes = dishes.OrderBy(d => d.Mass);
                        break;
                    case "mass_desc":
                        dishes = dishes.OrderByDescending(d => d.Mass);
                        break;
                    case "caloriecontent":
                        dishes = dishes.OrderBy(d => d.CalorieContent);
                        break;
                    case "caloriecontent_desc":
                        dishes = dishes.OrderByDescending(d => d.CalorieContent);
                        break;
                    case "cookingtime":
                        dishes = dishes.OrderBy(d => d.CookingTime);
                        break;
                    case "cookingtime_desc":
                        dishes = dishes.OrderByDescending(d => d.CookingTime);
                        break;
                    case "addingdate":
                        dishes = dishes.OrderBy(d => d.AddingDate);
                        break;
                    case "addingdate_desc":
                        dishes = dishes.OrderByDescending(d => d.AddingDate);
                        break;
                }

                foreach (Dish entity in await dishes.AsNoTracking().ToListAsync())
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
            catch (Exception e)
            {
                return (new OperationDetail<List<DishDTO>> { Succeeded = false, Message = "Ошибка при получении отсортированного списка блюд из базы данных:\n" + e.Message });
            }
            throw new NotImplementedException();
        }


        public async Task<OperationDetail<List<DishDTO>>> GetSortedListFromDBAsync_2(string sortOrder)
        {
            try
            {
                var dtoList = new List<DishDTO>();
                var dishes = from d in _context.Dishes select d;

                if (String.IsNullOrEmpty(sortOrder))
                {
                    sortOrder = "Name";
                }

                bool descending = false;

                if (sortOrder.EndsWith("_desc"))
                {
                    sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                    descending = true;
                }

                if (descending)
                {
                    dishes = dishes.OrderByDescending(e => EF.Property<object>(e, sortOrder));
                }
                else
                {
                    dishes = dishes.OrderBy(e => EF.Property<object>(e, sortOrder));
                }

                foreach (Dish entity in await dishes.AsNoTracking().ToListAsync())
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
            catch (Exception e)
            {
                return (new OperationDetail<List<DishDTO>> { Succeeded = false, Message = "Ошибка при получении отсортированного списка блюд из базы данных:\n" + e.Message });
            }
            throw new NotImplementedException();
        }

        public async Task<OperationDetail<List<DishDTO>>> GetSortedListFromDBAsync_3
            (string sortOrder, string searchName, string searchDescrComp, /*string searchDescr, string searchComp,*/ int massMin, int massMax, int timeMin, int timeMax)
        {
            try
            {
                var dtoList = new List<DishDTO>();
                var dishes = from d in _context.Dishes select d;

                //string currentFilter = searchString;
                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    dishes = dishes.Where(d => d.Name.ToUpper().Contains(searchString.ToUpper())
                //                           || d.Description.ToUpper().Contains(searchString.ToUpper())
                //                           || d.Composition.ToUpper().Contains(searchString.ToUpper()));
                //}

                //string currentFilter = searchName;

                //if (!String.IsNullOrEmpty(searchName))
                //{
                //    dishes = dishes.Where(d => d.Name.ToUpper().Contains(searchName.ToUpper()));
                //}
                //if (!String.IsNullOrEmpty(searchDescr))
                //{
                //    dishes = dishes.Where(d => d.Description.ToUpper().Contains(searchDescr.ToUpper()));
                //}
                //if (!String.IsNullOrEmpty(searchComp))
                //{
                //    dishes = dishes.Where(d => d.Composition.ToUpper().Contains(searchComp.ToUpper()));
                //}

                if (!String.IsNullOrEmpty(searchName))
                {
                    dishes = dishes.Where(d => d.Name.ToUpper().Contains(searchName.ToUpper()));
                }
                if (!String.IsNullOrEmpty(searchDescrComp))
                {
                    dishes = dishes.Where(d => d.Description.ToUpper().Contains(searchDescrComp.ToUpper()) 
                                            || d.Composition.ToUpper().Contains(searchDescrComp.ToUpper()));
                }

                if (massMin > 0)
                {
                    dishes = dishes.Where(d => (d.Mass >= massMin));
                }
                if(massMax > 0)
                {
                    dishes = dishes.Where(d => (d.Mass <= massMax));
                }

                if(timeMin > 0)
                {
                    dishes = dishes.Where(d =>(d.CookingTime >= timeMin));
                }
                if(timeMax > 0)
                {
                    dishes = dishes.Where(d => (d.CookingTime <= timeMax));
                }

                if (String.IsNullOrEmpty(sortOrder))
                {
                    sortOrder = "Id";
                }

                bool descending = false;

                if (sortOrder.EndsWith("_desc"))
                {
                    sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                    descending = true;
                }

                if (descending)
                {
                    dishes = dishes.OrderByDescending(e => EF.Property<object>(e, sortOrder));
                }
                else
                {
                    dishes = dishes.OrderBy(e => EF.Property<object>(e, sortOrder));
                }

                foreach (Dish entity in await dishes.AsNoTracking().ToListAsync())
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
            catch (Exception e)
            {
                return (new OperationDetail<List<DishDTO>> { Succeeded = false, Message = "Ошибка при получении отсортированного списка блюд из базы данных:\n" + e.Message });
            }
            throw new NotImplementedException();
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
