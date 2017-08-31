using System;
using System.Collections.Generic;

namespace UserService.WEB.Models.AccountApiModels
{
    public class UserApiModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}