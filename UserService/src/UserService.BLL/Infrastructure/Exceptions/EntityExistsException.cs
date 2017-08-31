namespace UserService.BLL.Infrastructure.Exceptions
{
    public class EntityExistsException : ServiceException
    {
        public EntityExistsException(string message, string target) : base(message, target)
        {
        }
    }
}