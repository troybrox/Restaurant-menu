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
    public class DishService : IDishService<OperationDetail>
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

        public async Task<(OperationDetail, List<DishDTO>)> GetAllFromDBAsync()
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
                return (new OperationDetail { Succeeded = true }, dtoList);
            }
            catch(Exception e)
            {
                return (new OperationDetail { Succeeded = false, Message = "Ошибка при получении списка блюд из базы данных:\n" + e.Message }, null);
            }
        }

        public async Task<(OperationDetail, DishDTO)> GetByIDAsync(int id)
        {
            try
            {
                Dish entity = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == id); ;
                return (
                    new OperationDetail { Succeeded = true },
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
                    }
                );
            }
            catch (Exception ex)
            {
                return (new OperationDetail { Succeeded = false, Message = ex.Message }, null);
            }
        }


        private Dish GetEntityFromDTO(DishDTO dto)
        {
            return new Dish
            {
                Name = dto.Name,
                AddingDate = dto.AddingDate,
                Price = dto.Price,
                Composition = dto.Composition,
                Mass = dto.Mass,
                CalorieContent = dto.CalorieContent,
                CookingTime = dto.CookingTime,
                Description = dto.Description
            };
        }
    }
}
