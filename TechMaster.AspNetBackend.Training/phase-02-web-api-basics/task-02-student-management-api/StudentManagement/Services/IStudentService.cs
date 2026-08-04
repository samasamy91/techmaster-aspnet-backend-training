using StudentManagement.DTOs;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public interface IStudentService
    {
        public StudentResponse Create(CreateStudentRequest request);
        public IEnumerable<StudentResponse> GetAll(string? search, string? trackName, bool? isActive, int pageNumber, int pageSize);
        public StudentResponse? GetById(int id);
        public StudentResponse Update(int id, UpdateStudentRequest request);
        public bool UpdateStatus(int id, bool isActive);
        public StudentStatsResponse StudentStats();
        public IEnumerable<StudentResponse> GetByTrack(string trackName);
        public bool Delete(int id);
    }
}
