using System;
using System.Collections.Generic;

namespace UserService.BLL.DTO
{
    public class UserDto
    {
        public UserDto()
        {
            Roles = new List<string>();
        }

        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}