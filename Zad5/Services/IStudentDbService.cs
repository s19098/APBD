using Zad5.Models;
using Zad5.Models.DTOs.Requests;

namespace Zad5.Services
{
    public interface IStudentDbService
    {
        public Enrollment EnrollStudent(EnrollStudentRequest request);
        public Enrollment PromoteStudents(PromoteStudentRequest request);
    }
}