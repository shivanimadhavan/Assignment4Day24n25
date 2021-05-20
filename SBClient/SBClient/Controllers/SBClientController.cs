using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SBClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SBClient.Controllers
{
    public class SBClientController : Controller
    {
        // GET: SBClientController
        public async Task<ActionResult> Index()
        {
            string Baseurl = "http://localhost:61048/";
            var SBAccountInfo = new List<Models.SB>();
            //HttpClient cl = new HttpClient();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/SBAccount");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var SBAccountResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    SBAccountInfo = JsonConvert.DeserializeObject<List<SB>>(SBAccountResponse);

                }
                //returning the employee list to view  
                return View(SBAccountInfo);
            }

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(SB p)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:61048/api/SBAccount", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<SB>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(int id)
        {
            TempData["CustomerId"] = id;
            int bid = Convert.ToInt32(TempData["CustomerId"]);
            SB b = new SB();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:61048/api/SBAccount/" + bid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    b = JsonConvert.DeserializeObject<SB>(apiResponse);
                }
            }
            return View(b);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["CustomerId"] = id;
            SB b = new SB();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:61048/api/SBAccount/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    b = JsonConvert.DeserializeObject<SB>(apiResponse);
                }
            }
            return View(b);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(SB b)
        {
            int bid = Convert.ToInt32(TempData["CustomerId"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:61048/api/SBAccount/" + bid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            TempData["CustomerId"] = id;
            SB b = new SB();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:61048/api/SBAccount/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    b = JsonConvert.DeserializeObject<SB>(apiResponse);
                }
            }
            return View(b);
        }
        [HttpPost]

        public async Task<ActionResult> Edit(SB b)
        {

            int bid = Convert.ToInt32(TempData["CustomerId"]);
            using (var httpClient = new HttpClient())
            {
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:61048/api/SBAccount/" + bid, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    b = JsonConvert.DeserializeObject<SB>(apiResponse);

                }
            }
            return RedirectToAction("Index");
        }
    }
}
