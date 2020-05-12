using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zad10.Models;

namespace Zad10.Services
{
   public interface IStudentDbService
   {
       public IEnumerable<Student> GetStudents();
   }
}