using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SrcChess2
{
    public class WebAPIHelper
    {
        #region Singleton
        public WebAPIHelper() { }
        private static readonly WebAPIHelper singleton = new WebAPIHelper();
        public static WebAPIHelper Singleton { get { return singleton; } } 
        #endregion
        /// <summary>
        /// Perfrom Http POST to ASP.NET WEB API on url.        
        /// Try converting result to TResponse
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="postdata">The postdata.</param>
        /// <param name="postUrl">The post URL.</param>
        /// <returns></returns>
        public TResponse Post<TResponse, TRequest>(TRequest postdata, string postUrl)            
        {
            using (HttpClient client = new HttpClient())
            {
                var postdataJson = JsonConvert.SerializeObject(postdata);
                var postdataString = new StringContent(postdataJson, new UTF8Encoding(), "application/json");
                var responseMessage = client.PostAsync(postUrl, postdataString).Result;
                var responseString = responseMessage.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TResponse>(responseString);
            }
        }

        /// <summary>
        /// Perform HTTP Get the specified post ASP.NET WEB API URL.
        /// Try to convert response to TResponse
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="postUrl">The post URL.</param>
        /// <returns></returns>
        public TResponse Get<TResponse>(string postUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                var responseMessage = client.GetStringAsync(postUrl).Result;
                return JsonConvert.DeserializeObject<TResponse>(responseMessage);
            }
        }
    }
}
