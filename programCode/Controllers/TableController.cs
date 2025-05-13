using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservierung.Dtos;
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

        /// <summary>
        /// Create a table for a restaurant. The creator must be the restaurant owner or an admin.
        /// </summary>
        /// <param name="tableForm"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        [Authorize(Roles = "RESTAURANT_OWNER,ADMIN")]
        [HttpPost("{restaurantId}")]
        public async Task<IActionResult> CreateTable([FromBody] TableFormModel tableForm, int restaurantId)
        {
            var user = await _userService.GetLoggedInUser();

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

        /// <summary>
        /// Get all tables for an restaurant.
        /// </summary>
        /// <param name="restuarantId"></param>
        /// <returns></returns>
        [HttpGet("{restuarantId}")]
        public async Task<IActionResult> GetTables(int restuarantId)
        {
            var tables = await _tableService.GetAllTables(restuarantId);

            return Ok(TableDto.MapToDtos(tables));
        }


        /// <summary>
        /// Delete Table by id
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        [Authorize(Roles = "RESTAURANT_OWNER,ADMIN")]
        [HttpDelete("{tableId}")]
        public async Task<IActionResult> DeleteTable(int tableId)
        {
            var table = await _tableService.GetTableById(tableId);
            var user = await _userService.GetLoggedInUser();

            if(!await _ownerService.OwnsRestaurant(user, table.RestaurantId) && user.Role != "ADMIN")
                return Unauthorized(new { Message = "You are not owner of the restaurant"});

            if (await _tableService.RemoveTable(table))
                return Ok( new { Messsage = "The Table has been deleted successfully!"});

            return BadRequest( new { Message = "The table could not be deleted"});
        }

        /// <summary>
        /// Update table by id
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = "RESTAURANT_OWNER,ADMIN")]
        [HttpPut("{tableId}")]
        public async Task<IActionResult> UpdateTable(int tableId, TableFormModel model)
        {
            var table = await _tableService.GetTableById(tableId);
            var user = await _userService.GetLoggedInUser();

            if (!await _ownerService.OwnsRestaurant(user, table.RestaurantId) && user.Role != "ADMIN")
                return Unauthorized(new { Message = "You are not owner of the restaurant" });

            if (await _tableService.UpdateTable(table, model))
                return Ok(new { Message = "The Table has been updated successfully!"});
            
            return BadRequest(new { Message = "The table could not be updated" });
        }

    }

    public class TableFormModel
    {

        public int TableNr { get; set; } 

        public int Capacity {  get; set; }

        public string Area { get; set; }
    }
}
