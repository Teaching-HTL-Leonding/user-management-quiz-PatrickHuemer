using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UserManagement.Data;
using static UserManagement.Controllers.DTO;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "administrator")]
    public class GroupsController : ControllerBase
    {
        public readonly UserManagementDataContext dc;
        public GroupsController(UserManagementDataContext dc)
        {
            this.dc = dc;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupResult>> GetById(int id)
        {
            var group = await dc.Groups.Where(g => g.Id == id).FirstOrDefaultAsync();

            if (group == null) return NotFound();

            var g = new GroupResult(group.Id, group.Name);
            return Ok(g);
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupResult>>> GetGroups()
        {

            var groups = await dc.Groups.ToListAsync();
            var resultGroup = new List<GroupResult>();

            foreach (var g in groups)
            {
                resultGroup.Add(new GroupResult(g.Id, g.Name));
            }

            return Ok(resultGroup);
        }
    }
}
