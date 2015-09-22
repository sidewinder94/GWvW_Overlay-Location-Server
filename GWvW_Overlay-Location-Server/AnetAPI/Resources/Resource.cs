using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GWvW_Overlay_Location_Server.AnetAPI.Resources
{
    public abstract class Resource<T>
    {

        private const String ApiBase = @"https://api.guildwars2.com/v2/";
        public abstract T GetResource(string apiKey);
        protected HttpStatusCode GetJSON(string endPoint, out string requestResult, string apiKey = null)
        {
            HttpStatusCode result;
            var client = WebRequest.CreateHttp(ApiBase + endPoint);

            if (apiKey != null)
            {
                client.Headers = new WebHeaderCollection()
                    {
                        new NameValueCollection()
                        {
                            {"Authorization", "Bearer " + apiKey}
                        }
                    };
            }
            using (var response = (HttpWebResponse)client.GetResponse())
            {
                result = response.StatusCode;
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        requestResult = sr.ReadToEnd();
                    }
                }
            }

            return result;
        }

    }
}
