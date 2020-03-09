using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;

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
            if (res.Item1.Succeeded)
                return res.Item2;
            else
                return BadRequest(res.Item1.Message); // -
        }

        // GET: api/Menu/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<DishDTO>> GetDish(int id)
        {
            var res = await _dishService.GetByIDAsync(id);
            if (res.Item1.Succeeded)
                return new ObjectResult(res.Item2);
            else
                return NotFound(res.Item1.Message);
        }

        // POST: api/Menu
        [HttpPost]
        [Route("add-dish")]
        public async Task<ActionResult<DishDTO>> AddDish(DishDTO dto)
        {
            if (dto == null)
                return BadRequest();
            var res = await _dishService.AddNewToDBAsync(dto);
            if (res.Succeeded)
                return Ok(); // return ?? ok(dto) ??
            else
                return BadRequest(res.Message);
        }

        
        // POST: api/Menu
        [HttpPost]
        [Route("delete-dish")]
        public async Task<ActionResult<DishDTO>> DeleteDish(int id) // name ??
        {
            var res = await _dishService.DeleteAsync(id);
            if (res.Succeeded)
                return Ok();
            else
                return BadRequest(res.Message); // NotFound() ??
        }

        // POST: api/Menu
        [HttpPost]
        [Route("edit-dish")]
        public async Task<ActionResult<DishDTO>> EditDish(int id, DishDTO dto)
        {
            var res = await _dishService.EditAsync(id, dto);    // check if(dto == null) ?? 
            if (res.Succeeded)
                return Ok();
            else
                return BadRequest(res.Message);
        }
        
    }
}
