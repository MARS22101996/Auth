using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.DAL.Entities
{
    [BsonIgnoreExtraElements]
    public class User : BaseType
    {
        public User()
        {
            Roles = new List<string>();
        }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public override string CollectionName => "users";
    }
}