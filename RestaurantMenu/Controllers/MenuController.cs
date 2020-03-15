using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Net.Http.Headers;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using RestaurantMenu.BLL.Infrastructure;

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
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<DishDTO>> GetDish(int id)
        {
            var res = await _dishService.GetByIDAsync(id);
                return Ok(res);
        }

        // POST: api/Menu
        [HttpPost]
        [Route("dish/add")]
        public async Task<IActionResult> AddDish(DishDTO dto)
        {
            if (dto == null)
                return BadRequest(); 
            var res = await _dishService.AddNewToDBAsync(dto);
            return Ok(res);
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
        public async Task<ActionResult<DishDTO>> EditDish(int id, DishDTO dto)
        {
            if (dto == null)
                return BadRequest();
            var res = await _dishService.EditAsync(id, dto);  
            return Ok(res);
        }
    }
}
