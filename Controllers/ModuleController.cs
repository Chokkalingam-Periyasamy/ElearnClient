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
    public class ModuleController : Controller
    {
        public async Task<ActionResult> ViewAllModules()
        {
            List<Module> ModuleInfo = new List<Module>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44320/api/Module");
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    ModuleInfo = JsonConvert.DeserializeObject<List<Module>>(Response);
                }
                return View(ModuleInfo);
            }
        }

        public ActionResult Create(int id)
        {
            HttpContext.Session.SetInt32("CourseId", id);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Module m)
        {

            Module ModuleObj = new Module();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44320/api/Module", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ModuleObj = JsonConvert.DeserializeObject<Module>(apiResponse);
                }
            }
            return View(m);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            Module mod = new Module();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44320/api/Module/GetModuleByID?Id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    mod = JsonConvert.DeserializeObject<Module>(apiResponse);
                }
            }
            return View(mod);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Module m)
        {
            Module receivedmod = new Module();

            using (var httpClient = new HttpClient())
            {
                int id = m.Moduleid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44320/api/Module?Id=" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedmod = JsonConvert.DeserializeObject<Module>(apiResponse);
                }
            }
            return RedirectToAction("StaffModule");
        }

        [HttpGet]

        public async Task<ActionResult> Delete(int id)
        {
            TempData["ModuleId"] = id;
            Module m = new Module();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44320/api/Module/GetModuleByID?Id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    m = JsonConvert.DeserializeObject<Module>(apiResponse);
                }
            }
            return View(m);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> Delete(Module m)
        {
            int ModuleId = Convert.ToInt32(TempData["ModuleId"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44320/api/Module?Id=" + ModuleId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("StaffModule");

        }
        public async Task<IActionResult> StaffModule()
        {
            //HttpContext.Session.SetInt32("CourseId", id);

            int uid = (int)HttpContext.Session.GetInt32("CourseId");
            List<Module> ModuleInfo = new List<Module>();
            using (var client = new HttpClient())
            {
                

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

               
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44320/api/Module/GetModuleCourseById?myid=" + uid);

                
                if (Res.IsSuccessStatusCode)
                {
   
                    var OrdResponse = Res.Content.ReadAsStringAsync().Result;
                    
                    ModuleInfo = JsonConvert.DeserializeObject<List<Module>>(OrdResponse);

                }
                if(ModuleInfo.Count == 0)
                {
                    return RedirectToAction("Failure");
                   
                }
                return View(ModuleInfo);
            }
        }
        public IActionResult Failure()
        {
            return View();
        }
        public async Task<IActionResult> StudentModule(int id)
        {
            //HttpContext.Session.SetInt32("CourseId", id);

            //uid = (int)HttpContext.Session.GetInt32("CourseId");
            List<Module> ModuleInfo = new List<Module>();
            using (var client = new HttpClient())
            {


                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage Res = await client.GetAsync("https://localhost:44320/api/Module/GetModuleCourseById?myid=" + id);

                //"https://localhost:44320/api/Module/GetModuleCourseStuId?myid="
                if (Res.IsSuccessStatusCode)
                {

                    var OrdResponse = Res.Content.ReadAsStringAsync().Result;

                    ModuleInfo = JsonConvert.DeserializeObject<List<Module>>(OrdResponse);

                }
                if (ModuleInfo.Count == 0)
                {
                    return RedirectToAction("Failure");

                }
                return View(ModuleInfo);

            }
        }
    }
}
