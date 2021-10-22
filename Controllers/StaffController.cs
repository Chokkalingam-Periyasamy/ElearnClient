using Elearn.ElearnModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Elearn.Controllers
{
    public class StaffController : Controller
    {
        public async Task<ActionResult> ViewAll()
        {
            List<staff> StaffInfo = new List<staff>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44319/api/Staff");
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    StaffInfo = JsonConvert.DeserializeObject<List<staff>>(Response);
                }
                return View(StaffInfo);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(staff e)
        {
            staff staffObj = new staff();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44319/api/Staff", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    staffObj = JsonConvert.DeserializeObject<staff>(apiResponse);
                }
            }
            return RedirectToAction("StaffLogin");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            staff stf = new staff();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44319/api/Staff/GetStaffByID?Id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    stf = JsonConvert.DeserializeObject<staff>(apiResponse);
                }
            }
            return View(stf);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(staff e)
        {
            staff receivedstf = new staff();

            using (var httpClient = new HttpClient())
            {
                int id = e.Staffid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44319/api/Staff?Id=" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedstf = JsonConvert.DeserializeObject<staff>(apiResponse);
                }
            }
            return RedirectToAction("ViewAll");
        }

        [HttpGet]

        public async Task<ActionResult> Delete(int id)
        {
            TempData["staffId"] = id;
            staff e = new staff();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44319/api/Staff/GetStaffByID?Id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<staff>(apiResponse);
                }
            }
            return View(e);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> Delete(staff e)
        {
            int staffId = Convert.ToInt32(TempData["staffId"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44319/api/Staff?Id=" + staffId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("ViewAll");

        }
        [HttpGet]
        public IActionResult StaffLogin()
        {
            return View();
        }
        [HttpPost]


        public async Task<IActionResult> StaffLogin(staff e)
        {

            staff Staffobj = new staff();
            //var db = new elearnContext();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44319/api/Staff/StaffLogin/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Staffobj = JsonConvert.DeserializeObject<staff>(apiResponse);

                }

            }
            if (Staffobj != null)
            {

                HttpContext.Session.SetString("email", Staffobj.Email);
                HttpContext.Session.SetInt32("userid", Staffobj.Staffid);
                HttpContext.Session.SetString("name", Staffobj.Name);

                return RedirectToAction("CourseIndex", "Course");


            }
            else
            {
                return RedirectToAction("StaffLogin");
            }

        }
    }
}
