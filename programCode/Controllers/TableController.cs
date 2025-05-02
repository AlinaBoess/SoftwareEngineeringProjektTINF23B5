using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Services;

namespace RestaurantReservierung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly TableService _tableService;
        private readonly UserService _userService;
        private readonly RestaurantOwnerService _ownerService;

        public TableController(TableService tableService, UserService userService, RestaurantOwnerService ownerService)
        {
            _tableService = tableService;
            _userService = userService;
            _ownerService = ownerService;
        }

        [Authorize(Roles = "RESTAURANT_OWNER,ADMIN")]
        [HttpPost("{restaurantId}")]
        public async Task<IActionResult> CreateTable([FromBody] TableFormModel tableForm, int restaurantId)
        {
            var user = await _userService.GetLoggedInUser();
            if (user == null)
            {
                return BadRequest();
            }

            var restaurant = await _ownerService.GetRestaurantById(restaurantId);
            if (restaurant == null)
            {
                return NotFound(new { Message = "The Restaurant with the id " + restaurantId + " does not exist!" });
            }


            if (restaurant.UserId == user.UserId || user.Role == "ADMIN")
            {
                if (await _tableService.AddTable(tableForm, restaurantId))
                {
                    return Ok(new { Message = "The table has been created successfully!" });
                }
                return BadRequest(new { Message = "Table could not be created!" });
            }
            else
            {
                return Unauthorized(new { Message = "You are not allowed to perform this action!" });
            }

        }

        [HttpGet("{restuarantId}")]
        public async Task<IActionResult> GetTables(int restuarantId)
        {
            var tables = _tableService.GetAllTables(restuarantId);

            return Ok(tables);
        }
    }

    public class TableFormModel
    {

        public int TableNr { get; set; } 

        public int Capacity {  get; set; }

        public string Area { get; set; }
    }
}
