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

        // sort only
        // GET: api/Menu/Name
        [HttpGet("sort:{sortOrder}/min_mass:{minMass}/max_mass:{maxMass}/min_time:{minTime}/max_time:{maxTime}")] 
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetSortedDishes(string sortOrder, int minMass, int maxMass, int minTime, int maxTime)
        {
            //todo: bll-sort method for getting list
            var res = await _dishService.GetSortedListFromDBAsync_3(sortOrder, null, null, minMass, maxMass, minTime, maxTime);
            return Ok(res);
        }

        // searchName 
        [HttpGet("sort:{sortOrder}/search_name:{searchName}/min_mass:{minMass}/max_mass:{maxMass}/min_time:{minTime}/max_time:{maxTime}")] 
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetFilteredDishes(string sortOrder, string searchName, int minMass, int maxMass, int minTime, int maxTime)
        {
            var res = await _dishService.GetSortedListFromDBAsync_3(sortOrder, searchName, null, minMass, maxMass, minTime, maxTime);
            return Ok(res);
        }

        // searchDescrComp
        //GET: api/Menu/sort:Name/search_name:салат
        [HttpGet("sort:{sortOrder}/search_descr_comp:{searchDescrComp}/min_mass:{minMass}/max_mass:{maxMass}/min_time:{minTime}/max_time:{maxTime}")]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetSortedFilteredByDecCompDishes(string sortOrder, string searchDescrComp, int minMass, int maxMass, int minTime, int maxTime)
        {
            var res = await _dishService.GetSortedListFromDBAsync_3(sortOrder, null, searchDescrComp, minMass, maxMass, minTime, maxTime);
            return Ok(res);
        }

        // searchName & searchDescrComp
        //GET: api/Menu/sort:Name/search_name:салат/search_descr_comp:огурец
        [HttpGet("sort:{sortOrder}/search_name:{searchName}/search_descr_comp:{searchDescrComp}/min_mass:{minMass}/max_mass:{maxMass}/min_time:{minTime}/max_time:{maxTime}")]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetSortedFilteredByNameDecCompDishes(string sortOrder, string searchName, string searchDescrComp, int minMass, int maxMass, int minTime, int maxTime)
        {
            var res = await _dishService.GetSortedListFromDBAsync_3(sortOrder, searchName, searchDescrComp, minMass, maxMass, minTime, maxTime);
            return Ok(res);
        }

        // sort & searchName & searchDescrComp & Mass
        //GET: api/Menu/sort:Name/search_name:салат/search_descr_comp:суп/min_mass:50/max_mass:120
        [HttpGet("sort:{sortOrder}/search_name:{searchName}/search_descr_comp:{searchDescrComp}/min_mass:{minMass}/max_mass:{maxMass}")]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetSortedFilteredByNameDecCompDishes
            (string sortOrder, string searchName, string searchDescrComp, int minMass, int maxMass)
        {
            var res = await _dishService.GetSortedListFromDBAsync_3(sortOrder, searchName, searchDescrComp, minMass, maxMass, 0, 0);
            return Ok(res);
        }

        // all filters
        //GET: api/Menu/sort:Name/search_name:салат/search_descr_comp:весенний/min_mass:100/max_mass:300/min_time:4/max_time:30
        [HttpGet("sort:{sortOrder}/search_name:{searchName}/search_descr_comp:{searchDescrComp}/min_mass:{minMass}/max_mass:{maxMass}/min_time:{minTime}/max_time:{maxTime}")]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetSortedFiltered
            (string sortOrder, string searchName, string searchDescrComp, int minMass, int maxMass, int minTime, int maxTime)
        {
            var res = await _dishService.GetSortedListFromDBAsync_3(sortOrder, searchName, searchDescrComp, minMass, maxMass, minTime, maxTime);
            return Ok(res);
        }

        // GET: api/Menu/5
        [HttpGet("dish/{id}", Name = "Get")]
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
