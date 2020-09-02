using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using WebAppConsume.Models;
using System.Text;

namespace WebAppConsume.Controllers
{
    public class EmployeeController : Controller
    {

        Uri baseaddress = new Uri("http://localhost:13896/api");

        HttpClient client;
        public EmployeeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;
        }
        // GET: Employee
        public ActionResult Index()
        {
            List<EmployeeViewModel> modelList = new List<EmployeeViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress +"/employee").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList=JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data);

            }
            return View(modelList);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/employee", content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            EmployeeViewModel model = new EmployeeViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/employee/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<EmployeeViewModel>(data);

            }
            return View("Create",model);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/employee/"+model.EmployeeId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create",model);
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/employee/" +id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}