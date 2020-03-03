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
        public async Task<ActionResult<IEnumerable<DishDTO>>> Get()
        {
            var res = await _dishService.GetAllFromDBAsync();
            if (res.Item1.Succeeded)
                return res.Item2;
            else
                return BadRequest(res.Item1.Message);
        }

        // GET: api/Menu/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<DishDTO>> Get(int id)
        {
            var res = await _dishService.GetByIDAsync(id);
            if (res.Item1.Succeeded)
                return new ObjectResult(res.Item2);
            else
                return NotFound(res.Item1);
        }

        // POST: api/Menu
        [HttpPost]
        public async Task<ActionResult<DishDTO>> Post(DishDTO dto)
        {
            if (dto == null)
                return BadRequest();
            var res = await _dishService.AddNewToDBAsync(dto);
            if (res.Succeeded)
                return Ok(dto);
            else
                return BadRequest(res.Message);
        }
    }
}
