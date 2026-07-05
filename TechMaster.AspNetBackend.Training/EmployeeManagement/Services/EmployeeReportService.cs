using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public class EmployeeReportService
    {
        private readonly EmployeeService employeeService;
        public EmployeeReportService(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public decimal AvgSalary()
        {
            List<Employee> employees = employeeService.GetEmployees();
            if (employees.Count == 0)
                return 0;
            return employees.Average(e=>e.Salary);
        }
        public Employee? HighestSalaryEmp()
        {
            List<Employee> employees = employeeService.GetEmployees();
            if (employees.Count == 0)
                return null;
            return employees.OrderByDescending(e=>e.Salary).FirstOrDefault();
        }
        public Employee? LowestSalaryEmp()
        {
            List<Employee> employees = employeeService.GetEmployees();
            if (employees.Count == 0)
                return null;
            return employees.OrderBy(e => e.Salary).FirstOrDefault();
        }
        public decimal TotalPayroll()
        {
            return employeeService.GetEmployees().Sum(e => e.Salary);
        }
        public Dictionary<Department,int> CountByDept()
        {
            return employeeService.GetEmployees().GroupBy(e => e.Department).ToDictionary(d => d.Key, d => d.Count());
            //group all emp same dept together then convert to dict and count number of emp in dept
        }
        public int ActiveEmp()
        {
            return employeeService.GetEmployees().Count(e => e.IsActive);
        }
        public int InactiveEmp()
        {
            return employeeService.GetEmployees().Count(e => !e.IsActive);
        }
    }
}
