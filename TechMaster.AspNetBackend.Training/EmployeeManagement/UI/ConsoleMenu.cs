using EmployeeManagement.Models;
using EmployeeManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.UI
{
    public class ConsoleMenu
    {
        private readonly EmployeeService employeeService;
        private readonly EmployeeReportService employeeReportService;
        public ConsoleMenu()
        {
            employeeService = new EmployeeService();
            employeeReportService = new EmployeeReportService(employeeService);
        }
        public void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("====== Employee Management System ======");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Update Employee");
                Console.WriteLine("3. Deactivate Employee");
                Console.WriteLine("4. Search Employee");
                Console.WriteLine("5. Filter by Department");
                Console.WriteLine("6. Sort Employees");
                Console.WriteLine("7. Show Salary Reports");
                Console.WriteLine("8. View All Employees");
                Console.WriteLine("9. Exit");
                Console.Write("Choose Option: ");
                string choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        UpdateEmployee();
                        break;
                    case "3":
                        DeactivateEmployee();
                        break;
                    case "4":
                        SearchEmployee();
                        break;
                    case "5":
                        FilterByDepartment();
                        break;
                    case "6":
                        SortEmployees();
                        break;
                    case "7":
                        ShowReports();
                        break;
                    case "8":
                        ViewAllEmployees();
                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Option.");
                        Pause();
                        break;
                }
            }
            
        }
        private void Pause()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        private Department? ChooseDepartment()
        {
            Console.WriteLine("Departments");
            Console.WriteLine("1. IT");
            Console.WriteLine("2. HR");
            Console.WriteLine("3. Finance");
            Console.WriteLine("4. Sales");
            Console.WriteLine("5. Marketing");
            Console.WriteLine("6. Support");
            Console.Write("Choose Department: ");
            switch (Console.ReadLine())
            {
                case "1":
                    return Department.IT;
                case "2":
                    return Department.HR;
                case "3":
                    return Department.Finance;
                case "4":
                    return Department.Sales;
                case "5":
                    return Department.Marketing;
                case "6":
                    return Department.Support;
                default:
                    return null;
            }
        }
        private void AddEmployee()
        {
            Console.WriteLine("===== Add Employee =====");
            Console.Write("Full Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Position: ");
            string position = Console.ReadLine();
            Console.Write("Salary: ");
            decimal salary;
            if (!decimal.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Invalid salary.");
                Pause();
                return;
            }
            Console.Write("Hire Date (yyyy-MM-dd): ");
            DateTime hireDate;
            if (!DateTime.TryParse(Console.ReadLine(), out hireDate))
            {
                Console.WriteLine("Invalid hire date.");
                Pause();
                return;
            }
            Department? department = ChooseDepartment();

            if (department == null)
            {
                Console.WriteLine("Invalid department.");
                Pause();
                return;
            }
            Employee employee = new Employee
            {
                FullName = name,
                Email = email,
                Position = position,
                Salary = salary,
                HireDate = hireDate,
                Department = department.Value
            };
            bool added = employeeService.AddEmployee(employee);
            if (added)
            {
                Console.WriteLine("Employee added successfully.");
                Console.WriteLine("Employee ID: " + employee.EmployeeId);
            }
            else
            {
                Console.WriteLine("Failed to add employee.");
            }
            Pause();
        }
        private void DisplayEmployees(List<Employee> employees)
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found.");
                return;
            }
            foreach (Employee employee in employees)
            {
                Console.WriteLine("ID: " + employee.EmployeeId);
                Console.WriteLine("Name: " + employee.FullName);
                Console.WriteLine("Department: " + employee.Department);
                Console.WriteLine("Position: " + employee.Position);
                Console.WriteLine("Salary: " + employee.Salary);
                Console.WriteLine("Status: " + (employee.IsActive ? "Active" : "Inactive"));
            }
        }
        private void UpdateEmployee()
        {
            Console.WriteLine("===== Update Employee =====");
            Console.Write("Enter Employee ID: ");
            string employeeId = Console.ReadLine();
            Employee employee = employeeService.FindEmpById(employeeId);
            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                Pause();
                return;
            }
            Console.Write("New Email: ");
            string email = Console.ReadLine();
            Console.Write("New Position: ");
            string position = Console.ReadLine();
            Console.Write("New Salary: ");
            decimal salary;
            if (!decimal.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Invalid salary.");
                Pause();
                return;
            }
            Department? department = ChooseDepartment();

            if (department == null)
            {
                Console.WriteLine("Invalid department.");
                Pause();
                return;
            }
            bool updated = employeeService.UpdateEmployee(
                employeeId,
                email,
                department.Value,
                position,
                salary);
            if (updated)
            {
                Console.WriteLine("Employee updated successfully.");
            }
            else
            {
                Console.WriteLine("Update failed.");
            }
            Pause();
        }
        private void DeactivateEmployee()
        {
            Console.WriteLine("===== Deactivate Employee =====");
            Console.Write("Enter Employee ID: ");
            string employeeId = Console.ReadLine();
            bool deactivated = employeeService.DeactivateEmp(employeeId);
            if (deactivated)
            {
                Console.WriteLine("Employee deactivated successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
            Pause();
        }
        private void SearchEmployee()
        {
            Console.WriteLine("===== Search Employee =====");
            Console.WriteLine("1. Search by ID");
            Console.WriteLine("2. Search by Name");
            Console.Write("Choose: ");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Write("Employee ID: ");
                string id = Console.ReadLine();
                Employee employee = employeeService.FindEmpById(id);
                if (employee == null)
                {
                    Console.WriteLine("Employee not found.");
                }
                else
                {
                    DisplayEmployees(new List<Employee> { employee });
                }
            }
            else if (choice == "2")
            {
                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                List<Employee> employees = employeeService.SearchEmp(name);

                DisplayEmployees(employees);
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
            Pause();
        }
        private void FilterByDepartment()
        {
            Console.WriteLine("===== Filter by Department =====");
            Department? department = ChooseDepartment();
            if (department == null)
            {
                Console.WriteLine("Invalid department.");
                Pause();
                return;
            }
            List<Employee> employees = employeeService.FilterByDept(department.Value);
            DisplayEmployees(employees);
            Pause();
        }
        private void SortEmployees()
        {
            Console.WriteLine("===== Sort Employees =====");
            Console.WriteLine("1. Salary (Low to High)");
            Console.WriteLine("2. Salary (High to Low)");
            Console.WriteLine("3. Hire Date (Oldest)");
            Console.WriteLine("4. Hire Date (Newest)");
            Console.WriteLine("5. Name (A-Z)");
            Console.Write("Choose: ");
            string choice = Console.ReadLine();
            List<Employee> employees = new List<Employee>();
            switch (choice)
            {
                case "1":
                    employees = employeeService.SortBySalaryAsc();
                    break;
                case "2":
                    employees = employeeService.SortBySalaryDesc();
                    break;
                case "3":
                    employees = employeeService.SortByHireAsc();
                    break;
                case "4":
                    employees = employeeService.SortByHireDesc();
                    break;
                case "5":
                    employees = employeeService.SortByName();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    return;
            }
            DisplayEmployees(employees);
            Pause();
        }
        private void ShowReports()
        {
            Console.WriteLine("===== Salary Reports =====");
            Console.WriteLine($"Average Salary : {employeeReportService.AvgSalary()}");
            Employee highest = employeeReportService.HighestSalaryEmp();
            Console.WriteLine($"Highest Salary : {highest.FullName} ({highest.Salary})");
            Employee lowest = employeeReportService.LowestSalaryEmp();
            Console.WriteLine($"Lowest Salary : {lowest.FullName} ({lowest.Salary})");
            Console.WriteLine($"Total Payroll : {employeeReportService.TotalPayroll()}");
            Console.WriteLine("Employees By Department");
            foreach (var item in employeeReportService.CountByDept())
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }
            Console.WriteLine($"Active Employees : {employeeReportService.ActiveEmp()}");
            Console.WriteLine($"Inactive Employees : {employeeReportService.InactiveEmp()}");
            Pause();
        }
        private void ViewAllEmployees()
        {
            List<Employee> employees = employeeService.GetEmployees();
            DisplayEmployees(employees);
            Pause();
        }
    }
}
