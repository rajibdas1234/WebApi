using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppConsume.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }
        public int? CompanyId { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }

    }
}