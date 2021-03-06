﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GWvW_Overlay_Location_Server.AnetAPI.Resources
{
    class Account : Resource<Account>
    {

        public const String EndPoint = "account";

        public String Id;
        public String Name;
        public int World;
        public String[] Guilds;
        public DateTime Created;

        [JsonConstructor]
        public Account(string id, string name, int world, string[] guilds, DateTime created)
        {
            Id = id;
            Name = name;
            World = world;
            Guilds = guilds;
            Created = created;
        }

        public Account()
        {

        }

        public override Account GetResource(string apiKey)
        {
            if (String.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("Should not be null or empty", "apiKey");
            }


            try
            {
                String json;
                var response = GetJSON(EndPoint, out json, apiKey);
                if (response == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<Account>(json);
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
