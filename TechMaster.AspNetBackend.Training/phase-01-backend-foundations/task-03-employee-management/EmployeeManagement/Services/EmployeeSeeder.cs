using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    public static class EmployeeSeeder
    {
        public static List<Employee> SeedEmployees()
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee
            {
                EmployeeId = "EMP-001",
                FullName = "Mohamed Ayman",
                Email = "mohamed@test.com",
                Department = Department.IT,
                Position = "Backend Developer",
                Salary = 20000,
                HireDate = new DateTime(2025, 1, 10),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-002",
                FullName = "Sara Adel",
                Email = "sara@test.com",
                Department = Department.HR,
                Position = "HR Specialist",
                Salary = 12000,
                HireDate = new DateTime(2024, 5, 15),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-003",
                FullName = "Ahmed Tarek",
                Email = "ahmed@test.com",
                Department = Department.IT,
                Position = "Junior Developer",
                Salary = 9000,
                HireDate = new DateTime(2026, 1, 1),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-004",
                FullName = "Omar Samir",
                Email = "omar@test.com",
                Department = Department.Sales,
                Position = "Sales Executive",
                Salary = 14000,
                HireDate = new DateTime(2023, 11, 20),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-005",
                FullName = "Mariam Hassan",
                Email = "mariam@test.com",
                Department = Department.Finance,
                Position = "Accountant",
                Salary = 11000,
                HireDate = new DateTime(2022, 9, 11),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-006",
                FullName = "Khaled Ali",
                Email = "khaled@test.com",
                Department = Department.IT,
                Position = "DevOps Trainee",
                Salary = 10000,
                HireDate = new DateTime(2026, 2, 1),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-007",
                FullName = "Nour Emad",
                Email = "nour@test.com",
                Department = Department.Marketing,
                Position = "Content Specialist",
                Salary = 9500,
                HireDate = new DateTime(2025, 7, 8),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-008",
                FullName = "Youssef Nabil",
                Email = "youssef@test.com",
                Department = Department.Sales,
                Position = "Sales Manager",
                Salary = 18000,
                HireDate = new DateTime(2021, 3, 17),
                IsActive = false
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-009",
                FullName = "Dina Farouk",
                Email = "dina@test.com",
                Department = Department.HR,
                Position = "Recruiter",
                Salary = 10500,
                HireDate = new DateTime(2024, 2, 13),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-010",
                FullName = "Hady Mahmoud",
                Email = "hady@test.com",
                Department = Department.IT,
                Position = "QA Engineer",
                Salary = 13000,
                HireDate = new DateTime(2025, 10, 1),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-011",
                FullName = "Salma Taha",
                Email = "salma@test.com",
                Department = Department.Finance,
                Position = "Finance Manager",
                Salary = 26000,
                HireDate = new DateTime(2020, 12, 12),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-012",
                FullName = "Ali Mostafa",
                Email = "ali@test.com",
                Department = Department.Support,
                Position = "Support Agent",
                Salary = 8000,
                HireDate = new DateTime(2026, 3, 5),
                IsActive = true
            });

            return employees;
        }
    }
}
