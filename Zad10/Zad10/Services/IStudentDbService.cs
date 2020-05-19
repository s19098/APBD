using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zad10.Models;
using Zad10.DTOs.Requests;

namespace Zad10.Services
{
   public interface IStudentDbService
   {
       public IEnumerable<Student> GetStudents();
	   public string UpdateStudent(UpdateStudentRequest request);
       public string DeleteStudent(string index);
       public Enrollment CreateStudent(EnrollStudentRequest studentRequest);
       public Enrollment PromoteStudent(PromoteStudentRequest studentRequest);
   }
}