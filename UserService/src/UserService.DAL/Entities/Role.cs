namespace UserService.DAL.Entities
{
    public class Role : BaseType
    {
        public string Name { get; set; }

        public override string CollectionName => "roles";
    }
}