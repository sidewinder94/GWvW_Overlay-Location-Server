using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GWvW_Overlay_Location_Server.AnetAPI.Resources
{
    class TokenInfo : Resource<TokenInfo>
    {

        public const String EndPoint = "tokeninfo";

        public String Id;
        public String Name;
        public String[] Permissions;


        [JsonConstructor]
        public TokenInfo(string id, string name, string[] permissions)
        {
            Id = id;
            Name = name;
            Permissions = permissions;
        }

        public TokenInfo()
        {

        }

        public override TokenInfo GetResource(string apiKey)
        {
            if (String.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("Should not be null or empty", "apiKey");
            }


            try
            {
                String json;
                var response = GetJSON(EndPoint, out json, apiKey);
                Console.WriteLine("Response HTTP Code : {0}", response);
                Console.WriteLine("Response : {0}", json);
                if (response == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<TokenInfo>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
