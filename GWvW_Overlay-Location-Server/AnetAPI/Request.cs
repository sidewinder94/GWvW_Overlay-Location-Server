using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices.ComTypes;
using GWvW_Overlay_Location_Server.AnetAPI.Resources;

namespace GWvW_Overlay_Location_Server.AnetAPI
{
    public static class Request
    {
        private static readonly HybridDictionary Cache = new HybridDictionary();


        public static T GetResource<T>(string apiKey = null) where T : Resource<T>, new()
        {
            var key = new Tuple<Type, String>(typeof(T), apiKey);
            if (Cache.Contains(key))
            {
                return (T)Cache[key];
            }

            var temp = new T();

            var result = temp.GetResource(apiKey);

            Cache[key] = result;

            return result;
        }
    }
}