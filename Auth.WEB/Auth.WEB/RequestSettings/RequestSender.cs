using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Auth.WEB.RequestSettings.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Auth.WEB.RequestSettings
{
    internal static class RequestSender
    {
        /// <summary>
        /// Sends the request asynchronous.
        /// </summary>
        /// <param name="requestUrl">The request URL.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="data">The data.</param>
        /// <param name="headers">The headers.</param>
        /// <returns>Task&lt;string&gt;</returns>
        /// <exception cref="WebRequestException">
        /// Cannot get response
        /// or
        /// 403. Forbidden
        /// or
        /// 401. Unauthorized
        /// or
        /// </exception>
        /// <exception cref="BadRequestException"></exception>
        public static async Task<string> SendRequestAsync(string requestUrl, string httpMethod, object data = null, IHeaderDictionary headers = null)
        {
            string responseText;

            try
            {
                var request = await FormRequestAsync(requestUrl, httpMethod, data, headers);

                var response = await request.GetResponseAsync() as HttpWebResponse;

                if (response == null)
                {
                    throw new WebRequestException("Cannot get response", requestUrl);
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new BadRequestException(requestUrl);
                }

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new WebRequestException("403. Forbidden", requestUrl);
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new WebRequestException("401. Unauthorized", requestUrl);
                }

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new WebRequestException(ex.Message, requestUrl);
            }

            return responseText;
        }

        /// <summary>
        /// Forms the request asynchronous.
        /// </summary>
        /// <param name="requestUrl">The request URL.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="data">The data.</param>
        /// <param name="headers">The headers.</param>
        /// <returns>Task&lt;HttpWebRequest&gt;</returns>
        /// This text was inserted by Ievhenii_Tkachenko. 3/28/2017.3:45 PM
        private static async Task<HttpWebRequest> FormRequestAsync(string requestUrl, string httpMethod, object data, IHeaderDictionary headers)
        {
            var request = WebRequest.CreateHttp(requestUrl);
            request.Method = httpMethod;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers[header.Key] = header.Value;
                }
            }

            bool isForm = false;

            if (!request.Headers.AllKeys.Contains("Content-Type"))
            {
                request.ContentType = "application/json";
            }
            else
            {
                isForm = request.Headers["Content-Type"].Equals("application/x-www-form-urlencoded");
            }

            if (data != null)
            {
                data = isForm ? (string)data : JsonConvert.SerializeObject(data);

                using (var requestStream = new StreamWriter(await request.GetRequestStreamAsync()))
                {
                    await requestStream.WriteAsync((string)data);
                }
            }

            return request;
        }
    }
}