using System;
using System.Collections.Generic;

namespace Auth.WEB.ApiModels
{
    public class UserApiModel
    {
        public UserApiModel()
        {
            Roles = new List<string>();
        }

        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}