using Drill02_OneToOneStudentProfile.Data;
using Microsoft.EntityFrameworkCore;
using RefactoredEnrollment.Common;
using RefactoredEnrollment.DTOs;
using RefactoredEnrollment.Services.IServices;

namespace RefactoredEnrollment.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext context;
        public EnrollmentService(AppDbContext context)
        {
            this.context = context;
        }
    }
}
