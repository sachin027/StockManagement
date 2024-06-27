using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Sachin_452.WebHelper
{
    public class WHelpers
    {
        public static async Task<string> HttpRequestResponce(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var token = HttpContext.Current.Request.Cookies["jwt"]?.Value;
                    client.BaseAddress = new Uri("http://localhost:53645/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    if (token != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}