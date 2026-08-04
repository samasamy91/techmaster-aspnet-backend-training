using EmployeeManagement.Helpers;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public class EmployeeService
    {
        private List<Employee> employees;
        public EmployeeService()
        {
            employees=EmployeeSeeder.SeedEmployees();
        }
        public List<Employee> GetEmployees()
        {
            return employees;
        }
        private string GenerateEmployeeId()
        {
            int nextNumber = employees.Count + 1;
            return $"Emo-{nextNumber:000}";
        }
        public bool AddEmployee(Employee employee)
        {
            if (!ValidationHelper.IsRequired(employee.FullName))
                return false;
            if (!ValidationHelper.IsValidEmail(employee.Email))
                return false;
            if (!ValidationHelper.IsRequired(employee.Position))
                return false;
            if (!ValidationHelper.ValidSalary(employee.Salary))
                return false;
            if (!ValidationHelper.IsValidHireDate(employee.HireDate))
                return false;
            employee.EmployeeId= GenerateEmployeeId();
            employee.IsActive = true;
            employees.Add(employee);
            return true;
        }
        public Employee? FindEmpById(string empId)
        {
            return employees.FirstOrDefault(e => e.EmployeeId == empId);
        }
        public bool UpdateEmployee(string empId,string email,Department dept,string position,decimal salary)
        {
            Employee? employee = FindEmpById(empId);
            if(employee == null)
                return false;
            if(!ValidationHelper.IsValidEmail(email))
                return false;
            if(!ValidationHelper.IsRequired(position))
                return false;
            if (!ValidationHelper.ValidSalary(salary))
                return false;
            employee.Email= email;
            employee.Position= position;
            employee.Salary= salary;
            employee.Department= dept;
            return true;
        }
        public bool DeactivateEmp(string empId)
        {
            Employee? employee = FindEmpById(empId);
            if (employee == null)
                return false;
            employee.IsActive= false;
            return true;
        }
        public List<Employee> SearchEmp(string name)
        {
            return employees.Where(e=>e.FullName.Contains(name,StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Employee> FilterByDept(Department dept)
        {
            return employees.Where(e=>e.Department==dept && e.IsActive).ToList();
        }
        public List<Employee> SortByName()
        {
            return employees.OrderBy(e=>e.FullName).ToList();
        }
        public List<Employee> SortBySalaryAsc()
        {
            return employees.OrderBy(e=>e.Salary).ToList();
        }
        public List<Employee> SortBySalaryDesc()
        {
            return employees.OrderByDescending(e=>e.Salary).ToList();
        }
        public List<Employee> SortByHireAsc()
        {
            return employees.OrderBy(e=>e.HireDate).ToList();
        }
        public List<Employee> SortByHireDesc()
        {
            return employees.OrderByDescending(e=>e.HireDate).ToList();
        }
    }
}
