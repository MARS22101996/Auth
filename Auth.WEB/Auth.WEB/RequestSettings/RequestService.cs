using System;
using System.Threading.Tasks;
using Auth.WEB.RequestSettings.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Auth.WEB.RequestSettings
{
    public class RequestService
    {
		//private readonly CommunicationOptions _configuration;

	    public RequestService()
	    {
		    //_configuration = communicationOptions.Value;
	    }

	    public async Task<TResponse> PostAsync<TResponse, TRequest>(
			string requestPath,
			TRequest data,
			IHeaderDictionary headers = null,
			string urlPrefix = "default")
		{
			string responseText;

			//var requestUrl = "http://localhost:54027/" + requestPath;

			var requestUrl = "http://localhost:5000/UserService/" + requestPath;

			try
			{
				responseText = await RequestSender.SendRequestAsync(requestUrl, "Post", data, headers);
			}
			catch (Exception ex)
			{
				var msg = $"Error occured during request.  Message: {ex.Message}";

				throw new Exception(msg);
			}

			var result = DeserializeResponse<TResponse>(responseText);

			return result;
		}

		public async Task<string> PostAsync(
		   string requestPath,
		   object data,
		   IHeaderDictionary headers = null,
		   string urlPrefix = "default")
		{
			string responseText;

			//var requestUrl = "http://localhost:54027/" + requestPath;
		    var requestUrl = "http://localhost:5000/UserService/" + requestPath;

			try
			{
				responseText = await RequestSender.SendRequestAsync(requestUrl, "Post", data, headers);
			}
			catch (WebRequestException ex)
			{
				var msg = $"Error occured during request. Url: {ex.Url}. Message: {ex.Message}";

				throw new ServiceCommunicationException(msg);
			}

			return responseText;
		}
		private T DeserializeResponse<T>(string response)
		{
			try
			{
				var result = JsonConvert.DeserializeObject<T>(response);

				return result;
			}
			catch (JsonException ex)
			{
				var msg = $"Cannot deserialize response into {typeof(T).Name}. Error message: {ex.Message}";

				throw new Exception(msg);
			}
		}

		//private string FormRequestUrl(string requestPath, string urlPrefix, Dictionary<string, string> urlParameters = null)
		//{
		//	if (string.IsNullOrEmpty(urlPrefix) || urlPrefix.Equals("default"))
		//	{
		//		urlPrefix = _configuration.DefaultPrefix;
		//	}

		//	requestPath = requestPath.Trim('/');
		//	urlPrefix = urlPrefix.Trim('/');
		//	var hostAddress = _configuration.HostAddress.Trim('/');

		//	var requestUrl = hostAddress + '/' + urlPrefix + '/' + requestPath;

		//	if (urlParameters != null)
		//	{
		//		requestUrl += ParseUrlParameters(urlParameters);
		//	}

		//	return requestUrl;
		//}
	}
}
