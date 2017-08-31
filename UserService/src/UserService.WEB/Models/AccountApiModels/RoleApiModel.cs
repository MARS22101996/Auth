using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.WEB.Models.AccountApiModels
{
    public class RoleApiModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}