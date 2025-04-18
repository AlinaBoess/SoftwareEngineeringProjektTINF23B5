﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservierung.Models;
using RestaurantReservierung.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantReservierung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) {
            _userService = userService;

        }

        /// <summary>
        /// Returns all Users that exist. Only Admins can use this Endpoint.
        /// </summary>
        /// <returns>All Users</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        /// <summary>
        /// Deletes a User specified by ID. Only Admins can use this Endpoint.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletion Status</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserByIdAsync(int id) {
            if (await _userService.DeleteUserAsync(await _userService.getUserByIdAsync(id)))
            {
                return Ok(new {Message = "User deleted successfully"});
            }
            else { 
                return NotFound(new { Message = "User not found" });
            }
        }

        /// <summary>
        /// Deletes the User who is currently logged in.
        /// </summary>
        /// <returns>Deletion Status</returns>
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteCurrentUser()
        {
            // TODO
            return Ok();
        }

        /// <summary>
        /// Allows Admins to create new Users without the Authentication Process.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Deletion Status</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public ActionResult CreateUser([FromBody] User model)
        {
            // TODO
            return Ok();
        }


        /// <summary>
        /// Updates the Role of an user. Only Admins can use this endpoint
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newRole"></param>
        /// <returns></returns>
        [Route("Role")]
        [Authorize(Roles = "ADMIN")]
        [HttpPut]
        public async Task<ActionResult> UpdateRole(string email, string newRole)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user != null)
            {
                if(await _userService.UpdateRoleAsync(user, newRole))
                {
                    return Ok(new {Message = "Role has been updated successfully"});
                }
                return BadRequest(new { Message = "Role could not be updated!" });
            }
            return BadRequest(new { Message = "The user with the email " + email + " does not exist" });
        }
    }
  
}
