using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Helpers
{
    public class ValidationHelper
    {
        public static bool IsRequired (string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
        public static bool ValidSalary(decimal salary)
        {
            return salary>0;
        }
        public static bool IsValidHireDate(DateTime hireDate)
        {
            return hireDate <= DateTime.Today;
        }
        public static bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email)&&email.Contains("@")&&email.Contains(".");
        }
    }
}
