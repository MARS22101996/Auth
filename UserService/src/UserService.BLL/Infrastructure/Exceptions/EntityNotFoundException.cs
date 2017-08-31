namespace UserService.BLL.Infrastructure.Exceptions
{
    public class EntityNotFoundException : ServiceException
    {
        public EntityNotFoundException(string message, string entity) : base(message, entity)
        {
        }
    }
}