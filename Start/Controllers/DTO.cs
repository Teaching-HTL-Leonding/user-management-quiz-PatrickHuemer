using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Controllers
{
    public class DTO
    {
        public record  UserResult(int id, string identifier, string email, string? firstName, string? lastName);
        public record GroupResult(int id, string name);
    }
}
