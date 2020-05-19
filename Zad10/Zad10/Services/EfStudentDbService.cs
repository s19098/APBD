using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zad10.Models;
using Zad10.DTOs.Requests;

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
		
		public string UpdateStudent(UpdateStudentRequest request)
        { 
            var student = _dbContext.Student.FirstOrDefault(student => student.IndexNumber.Equals(request.IndexNumber));
            if (student == null)
                return "Not Found";

            student.FirstName = request.FirstName != null ? request.FirstName : student.FirstName;
            student.LastName = request.LastName != null ? request.LastName : student.LastName;
            student.BirthDate = request.BirthDate != null ? request.BirthDate : student.BirthDate;

            _dbContext.Update(student);
            _dbContext.SaveChanges();
            return "OK";
        }
		
		public string DeleteStudent(string index)
        {
            if (index == null)
                return "Bad Request";

            var student = _dbContext.Student.FirstOrDefault(student => student.IndexNumber.Equals(index));
            if (student == null)
                return "Not Found";

            _dbContext.Remove(student);
            _dbContext.SaveChanges();
            return "OK";
        }
		
		 public Enrollment CreateStudent(EnrollStudentRequest studentRequest)
        {
            using (var tran = _dbContext.Database.BeginTransaction())
            {
                if (_dbContext.Studies.FirstOrDefault(st => st.Name == studentRequest.Studies) == null)
                    return null;

                int idStudy = _dbContext.Studies
                .Where(st => st.Name == studentRequest.Studies)
                .Select(st => st.IdStudy).SingleOrDefault();

                Enrollment enrollment = _dbContext.Enrollment
                    .FirstOrDefault(e => (e.IdStudy == idStudy) && (e.Semester == 1));

                if (enrollment == null)
                {
                    int maxId = _dbContext.Enrollment.Max(e => e.IdEnrollment);

                    enrollment = new Enrollment();
                    enrollment.IdEnrollment = maxId + 1;
                    enrollment.Semester = 1;
                    enrollment.IdStudy = idStudy;
                    enrollment.StartDate = DateTime.Now;
                    _dbContext.Enrollment.Add(enrollment);
                }

                var isStudentExist = _dbContext.Student.FirstOrDefault(student => student.IndexNumber.Equals(studentRequest.IndexNumber));
                if (isStudentExist != null)
                    return null;

                string strDateFormat = "dd-MM-yyyy";
                DateTime BirthDate = DateTime.ParseExact(studentRequest.BirthDate.ToString(), strDateFormat, CultureInfo.InvariantCulture);

                Student student = new Student();
                student.IndexNumber = studentRequest.IndexNumber;
                student.FirstName = studentRequest.FirstName;
                student.LastName = studentRequest.LastName;
                student.BirthDate = BirthDate;
                student.IdEnrollment = enrollment.IdEnrollment;
                _dbContext.Student.Add(student);

                _dbContext.SaveChanges();
                tran.Commit();

                return enrollment;
            }
			
		public Enrollment PromoteStudent(PromoteStudentRequest studentRequest)
		{
			using (var tran = _dbContext.Database.BeginTransaction())
            {
                int idStudy = _dbContext.Studies
                .Where(st => st.Name == studentRequest.Studies)
                .Select(st => st.IdStudy).SingleOrDefault();

                Enrollment enrollment = _dbContext.Enrollment
                        .FirstOrDefault(e => e.IdStudy == idStudy && e.Semester == studentRequest.Semester);

                if (enrollment == null)
                    return null;

                int oldIdEnrollment = enrollment.IdEnrollment;
                enrollment = _dbContext.Enrollment
                        .FirstOrDefault(e => e.IdStudy == idStudy && e.Semester == studentRequest.Semester + 1);

                if (enrollment == null)
                {
                    int maxId = _dbContext.Enrollment.Max(e => e.IdEnrollment);

                    enrollment = new Enrollment();
                    enrollment.IdEnrollment = maxId + 1;
                    enrollment.Semester = studentRequest.Semester + 1;
                    enrollment.IdStudy = idStudy;
                    enrollment.StartDate = DateTime.Now;
                    _dbContext.Enrollment.Add(enrollment);
                }

                var students = _dbContext.Student.Where(s => s.IdEnrollment == oldIdEnrollment).ToList();

                foreach(Student student in students)
                {
                    student.IdEnrollment = enrollment.IdEnrollment;
                }

                _dbContext.SaveChanges();
                tran.Commit();
                return enrollmen
			}
		}
    }
}