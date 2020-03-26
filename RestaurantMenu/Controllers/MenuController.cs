using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using RestaurantMenu.Models;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using RestaurantMenu.BLL.Infrastructure;
using RestaurantMenu.BLL.Validation;
using RestaurantMenu.BLL.Exceptions;
using Newtonsoft.Json;

namespace RestaurantMenu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IDishService _dishService;

        public MenuController(IDishService dishService)
        {
            _dishService = dishService;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetDishes()
        {
            var res = await _dishService.GetAllFromDBAsync();
            return Ok(res);
        }

        // GET: api/Menu/5
        [HttpGet("dish/{id}", Name = "Get")]
        public async Task<ActionResult<DishDTO>> GetDish(int id)
        {
            var res = await _dishService.GetByIDAsync(id);
                return Ok(res);
        }

        [HttpPost]
        [Route("filtered-dishes")]
        public async Task<IActionResult> GetDishesAsync([FromBody]MenuRequestModel model)
        {
            var res = await _dishService.GetSortedFilteredListFromDBAsync(model.PageIndex, model.Sort, model.Filters);

            var test = JsonConvert.SerializeObject(res);
            return Ok(res);
        }

        // POST: api/Menu
        [HttpPost]
        [Route("dish/add")]
        public async Task<IActionResult> AddDish(DishModelDTO modelDTO)
        {
            List<string> errorMessages = new List<string>();

            if(modelDTO == null)
            {
                var res = new OperationDetail();
                res.Succeeded = false;
                res.ErrorMessages = new List<string>();
                res.ErrorMessages.Add
                    ("Блюдо имело нулевые значения полей, для добавления блюда в меню заполните значения корректно");
                return Ok(res);
            }

            try
            {
                var res = await _dishService.AddNewToDBAsync(modelDTO);
                return Ok(res);
            }
            catch (Exception e)
            {
                errorMessages.Add(e.Message);
                return Ok(new OperationDetail()
                {
                    Succeeded = false,
                    ErrorMessages = errorMessages
                });

            }

        }

        // POST: api/Menu
        [HttpPost]
        [Route("dish/delete/{id}")]
        public async Task<ActionResult<DishDTO>> DeleteDish(int id)
        {
            var res = await _dishService.DeleteAsync(id);
            return Ok(res);
        }

        // POST: api/Menu
        [HttpPost]
        [Route("dish/edit/{id}")]
        public async Task<ActionResult<DishDTO>> EditDish(int id, DishModelDTO dto)
        {
            if (dto == null)
                return BadRequest();
            var res = await _dishService.EditAsync(id, dto);  
            return Ok(res);
        }
    }
}
