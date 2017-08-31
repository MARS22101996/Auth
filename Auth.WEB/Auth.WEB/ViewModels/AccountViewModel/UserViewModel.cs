using System;

namespace Auth.WEB.ViewModels.AccountViewModel
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}