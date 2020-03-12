using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using RestaurantMenu.BLL.Infrastructure;

namespace RestaurantMenu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IDishService<OperationDetail> _dishService;

        public MenuController(IDishService<OperationDetail> dishService)
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
                return Ok(res.Item1); 
        }

        // GET: api/Menu/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<DishDTO>> GetDish(int id)
        {
            var res = await _dishService.GetByIDAsync(id);
            if (res.Item1.Succeeded)
                return res.Item2;
            else
                return Ok(res.Item1);
        }

        // POST: api/Menu
        [HttpPost]
        [Route("add-dish")]
        public async Task<ActionResult<DishDTO>> AddDish(DishDTO dto)
        {
            if (dto == null)
                return BadRequest(); // ?
            var res = await _dishService.AddNewToDBAsync(dto);
            return Ok(res);
        }

        
        // POST: api/Menu
        [HttpPost]
        [Route("delete-dish")]
        public async Task<ActionResult<DishDTO>> DeleteDish(int id)
        {
            var res = await _dishService.DeleteAsync(id);
            return Ok(res);
        }

        // POST: api/Menu
        [HttpPost]
        [Route("edit-dish")]
        public async Task<ActionResult<DishDTO>> EditDish(int id, DishDTO dto)
        {
            var res = await _dishService.EditAsync(id, dto);    // check if(dto == null) ?? 
            return Ok(res);
        }
    }
}
