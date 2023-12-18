using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private static string WebApiUrl = "https://localhost:44319/";
        // GET: Home
        public async Task<ActionResult> Index()
        {
            var tokenBased = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebApiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = await client.GetAsync("Account/ValidLogin?userName=admin&Password=12345678");
                if (responseMessage.IsSuccessStatusCode) 
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    tokenBased = JsonConvert.DeserializeObject<string>(resultMessage);
                    Session["TokenNumber"] = tokenBased;
                    Session["UserName"] = "admin";
                }
            }
            return Content(tokenBased);
        }

        public async Task<ActionResult> GetEmployee()
        {
            string ReturnMessage = string.Empty;
            using (var client = new HttpClient()) 
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebApiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["TokenNumber"].ToString() + ":" + Session["UserName"]);
                var responseMessage = await client.GetAsync("Account/GetEmployee");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    ReturnMessage = JsonConvert.DeserializeObject<string>(resultMessage);
                }

            }
            return Content(ReturnMessage);
        }
    }
}