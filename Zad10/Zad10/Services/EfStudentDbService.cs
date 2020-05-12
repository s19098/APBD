using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zad10.Models;

namespace Zad10.Services
{
    public class EfStudentDbService : IStudentDbService
    {
        private readonly s19098Context _dbContext;

        public EfStudentDbService(s19098Context context)
        {
            _dbContext = context;
        }


        public IEnumerable<Student> GetStudents()
        {
            var listStudents = _dbContext.Student.ToList();

            return listStudents;
        }
    }
}