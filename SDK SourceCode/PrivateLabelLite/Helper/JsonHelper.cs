using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivateLabelLite.Helper
{
    public static class JsonHelper
    {
        public static string ToJson(this HtmlHelper helper, object obj)
        {
            return GetCamelCaseJson(obj);
        }
        public static string GetCamelCaseJson(object obj)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var content = JsonConvert.SerializeObject(obj, jsonSerializerSettings);
            return content;
        }
    }
}