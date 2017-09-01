using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Auth.WEB.RequestSettings.Inerfaces
{
    public interface IRequestService
    {
	   Task<TResponse> PostAsync<TResponse, TRequest>(
		    string requestPath,
		    TRequest data,
		    IHeaderDictionary headers = null);

	    Task<string> PostAsync(
		    string requestPath,
		    object data,
		    IHeaderDictionary headers = null);
    }
}
