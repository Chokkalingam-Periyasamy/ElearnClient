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
    public class StudentController : Controller


    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(StudentController));
        string Baseurl = "https://localhost:44357/";
        public async Task<ActionResult> Index()
        {
            List<User> StuInfo = new List<User>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Student");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var StuResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    StuInfo = JsonConvert.DeserializeObject<List<User>>(StuResponse);

                }
                //returning the employee list to view  
                return View(StuInfo);
            }
        }
        public async Task<ActionResult> Enroll(int id)
        {
            int myid = (int)HttpContext.Session.GetInt32("userid");
            int courseid = id;
            Usercourse obj = new Usercourse();
            obj.Stuid = myid;
            obj.Coid = courseid;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44357/api/Student/UserEnroll", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //obj = JsonConvert.DeserializeObject<Customers>(apiResponse);
                }
            }
            return RedirectToAction("MyCourse");
        }



        public async Task<IActionResult> MyCourse()
        {

            int uid = (int)HttpContext.Session.GetInt32("userid");
            List<Course> CourseInfo = new List<Course>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                //client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44317/api/Course/GetCourseByUserId?myid=" + uid);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var OrdResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    CourseInfo = JsonConvert.DeserializeObject<List<Course>>(OrdResponse);

                }

                    return View(CourseInfo);

            }
        }
    
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(User e)
        {
            User Studentobj = new User();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44357/api/Student/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Studentobj = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return RedirectToAction("StudentLogin");
        }
        [HttpGet]
        public IActionResult StudentLogin()
        {
            _log4net.Info("Student Logged");
            return View();
        }
        [HttpPost]


        public async Task<IActionResult> StudentLogin(User e)

        {
         
            User Studentobj = new User();
            //var db = new elearnContext();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44377/api/Login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (apiResponse != null)
                    {
                        using (var response1 = await httpClient.PostAsync("https://localhost:44377/api/Login/UserDetail", content))
                        {
                            string api = await response1.Content.ReadAsStringAsync();
                            Studentobj = JsonConvert.DeserializeObject<User>(api);
                        }

                    }

                }

            }
            if (Studentobj != null)
            {

                HttpContext.Session.SetString("email", Studentobj.Email);
                HttpContext.Session.SetInt32("userid", Studentobj.Userid);
                HttpContext.Session.SetString("name", Studentobj.Name);
                string myname = HttpContext.Session.GetString("name");
                _log4net.Info("Logged by " + myname);


                return RedirectToAction("Course2", "Course");


            }
          
            else
            {
               
                return RedirectToAction("StudentLogin");
            }

        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("StudentLogin");
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            User emp = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44357/api/Student/GetByID?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    emp = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return View(emp);
        }

        [HttpPost]
        public async Task<ActionResult> Update(User e)
        {
            User r = new User();

            using (var httpClient = new HttpClient())
            {

                int id = e.Userid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44357/api/Student?id=" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    r = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["userid"] = id;
            User e = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44357/api/Student/GetByID?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return View(e);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> Delete(User e)
        {
            int empid = Convert.ToInt32(TempData["userid"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44357/api/Student?id=" + empid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> GetDetailbyID()
        {
            int id = (int)HttpContext.Session.GetInt32("userid");
            User e = new User();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44357/api/Student/GetByID?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return View(e);
        }


    }
}

