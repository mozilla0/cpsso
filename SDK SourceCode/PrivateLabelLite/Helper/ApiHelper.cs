using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PrivateLabelLite.Models;
namespace PrivateLabelLite.Helper
{
    public class ApiHelper
    {
        async static public Task<T> PostApi<T>(string uri, List<KeyValuePair<string, string>> headerItems, List<KeyValuePair<string, string>> bodyItems)
        {
            HttpContent queryParms = new FormUrlEncodedContent(bodyItems);
            using (HttpClient client = new HttpClient())
            {
                foreach (var header in headerItems)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                using (HttpResponseMessage response = await client.PostAsync(uri, queryParms))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<T>(stringData);
                        return (T)Convert.ChangeType(result, typeof(T));
                    }
                }
            }
            return (T)Convert.ChangeType(new object(), typeof(T));
        }
        
        //public static AccessToken GenerateToken(string url, )
        //{
        //    var headers = new List<KeyValuePair<string, string>>();
        //    var bodyItems = new List<KeyValuePair<string, string>>();
        //    var res = PostApi<AccessToken>(url,
        //    return s;
        //}
    }
}