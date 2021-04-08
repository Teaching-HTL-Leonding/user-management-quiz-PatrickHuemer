using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data;
using UserManagement.Controllers;
using System.Security.Claims;
using static UserManagement.Controllers.DTO;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly UserManagementDataContext dc;
        public UsersController(UserManagementDataContext dc)
        {
            this.dc = dc;
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserResult>> GetUser()
        {
            var uId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var u = await dc.Users.Where(u => u.NameIdentifier == uId).FirstOrDefaultAsync();
            var resultUser = new UserResult( u.Id, u.NameIdentifier, u.Email, u.FirstName, u.LastName );

            return Ok(resultUser);

        }

        [HttpGet]
        public async Task<ActionResult<List<UserResult>>> GetUsers()
        {

            var users = await dc.Users.ToListAsync();
            var resultUsers = new List<UserResult>();

            foreach (var u in users)
            {
                resultUsers.Add(new UserResult(u.Id, u.NameIdentifier, u.Email, u.FirstName, u.LastName));
            }

            return Ok(resultUsers);
        }
    }
}
