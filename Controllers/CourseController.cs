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
    public class CourseController : Controller
    {
        string Baseurl = "https://localhost:44317/";
        public async Task<ActionResult> CourseIndex()
        {
            List<Course> CourseInfo = new List<Course>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Course");
                if (Res.IsSuccessStatusCode)
                {
                    var CourResponse = Res.Content.ReadAsStringAsync().Result;
                    CourseInfo = JsonConvert.DeserializeObject<List<Course>>(CourResponse);
                }
                return View(CourseInfo);
            }
        }
        public async Task<ActionResult> Course2()
        {
            List<Course> CourseInfo = new List<Course>();

            using (var client = new HttpClient())
            {
                int myid = (int)HttpContext.Session.GetInt32("userid");
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44317/api/Course/GetStudentCourse?id=" + myid);
                if (Res.IsSuccessStatusCode)
                {
                    var CourResponse = Res.Content.ReadAsStringAsync().Result;
                    CourseInfo = JsonConvert.DeserializeObject<List<Course>>(CourResponse);
                }
                return View(CourseInfo);
            }
        }
        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddCourse(Course e)
        {
            Course Courseobj = new Course();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44317/api/Course/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Courseobj = JsonConvert.DeserializeObject<Course>(apiResponse);
                }
            }
            return RedirectToAction("CourseIndex");
        }
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            Course c = new Course();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Course/GetByCourseID?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    c = JsonConvert.DeserializeObject<Course>(apiResponse);
                }
            }
            return View(c);
        }

        [HttpPost]
        public async Task<ActionResult> Update(Course e)
        {
            Course r = new Course();

            using (var httpClient = new HttpClient())
            {

                int id = e.Courseid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44317/api/Course?id=" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    r = JsonConvert.DeserializeObject<Course>(apiResponse);
                }
            }
            return RedirectToAction("CourseIndex");
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["courseid"] = id;
            Course e = new Course();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Course/GetByCourseID?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<Course>(apiResponse);
                    
                }
               
            }
            return View(e);

        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> Delete(Course e)
        {
            int cid = e.Courseid;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44317/api/Course?id=" + cid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("CourseIndex");
        }

       
        public async Task<IActionResult> Payment(int id)
        {
            Course e = new Course();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44317/api/Course/GetByCourseID?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<Course>(apiResponse);
                }
            }
            return View(e);
        }


    }
}
