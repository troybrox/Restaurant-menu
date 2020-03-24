using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            var res = await _dishService.GetSortedFilteredListFromDBAsync(sortOrder, null, null, minMass, maxMass, minTime, maxTime);
            return Ok(res);
        }

        // searchName 
        //GET: api/Menu/sort:Name/search_name:весенний/min_mass:100/max_mass:300/min_time:4/max_time:30
        [HttpGet("sort:{sortOrder}/search_name:{searchName}/min_mass:{minMass}/max_mass:{maxMass}/min_time:{minTime}/max_time:{maxTime}")] 
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetFilteredDishes(string sortOrder, string searchName, int minMass, int maxMass, int minTime, int maxTime)
        {
            var res = await _dishService.GetSortedFilteredListFromDBAsync(sortOrder, searchName, null, minMass, maxMass, minTime, maxTime);
            return Ok(res);
        }

        // searchDescrComp
        //GET: api/Menu/sort:Name/search_descr_comp:салат/min_mass:100/max_mass:300/min_time:4/max_time:30
        [HttpGet("sort:{sortOrder}/search_descr_comp:{searchDescrComp}/min_mass:{minMass}/max_mass:{maxMass}/min_time:{minTime}/max_time:{maxTime}")]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetSortedFilteredByDecCompDishes(string sortOrder, string searchDescrComp, int minMass, int maxMass, int minTime, int maxTime)
        {
            var res = await _dishService.GetSortedFilteredListFromDBAsync(sortOrder, null, searchDescrComp, minMass, maxMass, minTime, maxTime);
            return Ok(res);
        }

        // all filters
        //GET: api/Menu/sort:Name/search_name:салат/search_descr_comp:весенний/min_mass:100/max_mass:300/min_time:4/max_time:30
        [HttpGet("sort:{sortOrder}/search_name:{searchName}/search_descr_comp:{searchDescrComp}/min_mass:{minMass}/max_mass:{maxMass}/min_time:{minTime}/max_time:{maxTime}")]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetSortedFiltered
            (string sortOrder, string searchName, string searchDescrComp, int minMass, int maxMass, int minTime, int maxTime)
        {
            var res = await _dishService.GetSortedFilteredListFromDBAsync(sortOrder, searchName, searchDescrComp, minMass, maxMass, minTime, maxTime);
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
        public async Task<IActionResult> GetDishesAsync(int page = 1, SortDefinition sort = null, FilterDefinition[] filters = null)
        {
            var res = new OperationDetail();
            //var res = await _dishService.GetSortedFilteredListFromDBAsync(int page = 1, FilterDefinition[] filters, SortDefinition[] sorts);
            return Ok(res);
        }


        // POST: api/Menu
        [HttpPost]
        [Route("dish/add")]
        public async Task<IActionResult> AddDish(DishDTO dto)
        {
            if (dto == null)
                return BadRequest();
            var res = new OperationDetail();
            if (ModelState.IsValid)
            {
                res = await _dishService.AddNewToDBAsync(dto);
            }
            else
            {
                res.Succeeded = false;
                foreach (ModelStateEntry modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        res.ErrorMessages.Add(error.ErrorMessage);
                    }
                }
            }
            
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
