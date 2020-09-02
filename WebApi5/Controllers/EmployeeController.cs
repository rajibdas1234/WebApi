using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi5.Models;

namespace WebApi5.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeDbEntities db = new EmployeDbEntities();
        public IEnumerable<EmployeeData> GetEmployeeDatas()
        {
            return db.EmployeeDatas.ToList();
        }
        public   EmployeeData GetEmployeeData(int id)
        {
            return db.EmployeeDatas.Find(id);
        }

        //add/employee
        [HttpPost]
        public HttpResponseMessage AddEmployee(EmployeeData model)
        {
            try
            {
                db.EmployeeDatas.Add(model);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }
            catch(Exception e)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        //update/emp
        [HttpPut]
        public HttpResponseMessage UpdateEmployee(int id,EmployeeData model)
        {
            try
            {
                if (id == model.EmployeeId)
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
        public HttpResponseMessage DeleteEmp(int id)
        {
            EmployeeData emp = db.EmployeeDatas.Find(id);
            if(emp != null)
            {
                db.EmployeeDatas.Remove(emp);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
    }
}
