using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GWvW_Overlay_Location_Server.AnetAPI.Resources
{
    public abstract class Resource<T>
    {
        private const String ApiBase = @"https://api.guildwars2.com/v2/";
        public abstract T GetResource(string apiKey);
        protected String GetJSON(string endPoint, string apiKey = null)
        {
            string s = null;
            using (var client = new WebClient())
            {
                try
                {
                    client.Encoding = Encoding.UTF8;
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
                    s = client.DownloadString(ApiBase + endPoint);
                }

                catch (WebException)
                {
                    return null;
                }
            }

            return s;
        }
    }
}
