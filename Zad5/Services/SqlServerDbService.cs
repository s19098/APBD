using System;
using System.Data;
using System.Data.SqlClient;
using Zad5.Models;
using Zad5.Models.DTOs.Requests;

namespace Zad5.Services
{
    public class SqlServerDbService : IStudentDbService
    {
        private const string connecionString = "Data Source=db-mssql.pjwstk.edu.pl;" +
                                               "Initial Catalog=s18478;" +
                                               "Integrated Security=True";

        public Enrollment EnrollStudent(EnrollStudentRequest request)
        {
            Enrollment enrollment = new Enrollment();

            using (var conn = new SqlConnection(connecionString))
            using (var comm = new SqlCommand())
            {
                conn.Open();
                comm.Connection = conn;
                SqlTransaction tran = conn.BeginTransaction();
                comm.Transaction = tran;

                try
                {
                    SqlDataReader sdr;

                    comm.CommandText = "SELECT IdStudy FROM Studies WHERE Name = @name";
                    comm.Parameters.AddWithValue("name", request.Studies);
                    sdr = comm.ExecuteReader();

                    if (!sdr.Read())
                    {
                        sdr.Close();
                        tran.Rollback();
                        throw new Exception("Studies does not exist !");
                    }

                    enrollment.IdStudy = (int) sdr["IdStudy"];
                    sdr.Close();

                  
                    comm.CommandText =
                        "SELECT IdEnrollment, StartDate FROM Enrollment WHERE IdStudy = @idStudy AND Semester = 1";
                    comm.Parameters.AddWithValue("idStudy", enrollment.IdStudy);
                    sdr = comm.ExecuteReader();

                  
                    if (!sdr.Read())
                    {
                       
                        comm.CommandText = "SELECT MAX(IdEnrollment) FROM Enrollment";
                        sdr.Close();
                        sdr = comm.ExecuteReader();

                        int id = 1;
                        if (sdr.Read() && !DBNull.Value.Equals(sdr[0]))
                            id += (int) sdr[0];

                        comm.CommandText = "INSERT INTO Enrollment VALUES (@id, 1, @idStudy, @startDate)";
                        comm.Parameters.AddWithValue("id", id);
                        comm.Parameters.AddWithValue("startDate", DateTime.Now.Date);
                        sdr.Close();
                        comm.ExecuteNonQuery();

                        enrollment.IdEnrollment = id;
                        enrollment.StartDate = DateTime.Now;
                    }
                    else
                    {
                        enrollment.IdEnrollment = (int) sdr["IdEnrollment"];
                        enrollment.StartDate = (DateTime) sdr["StartDate"];
                    }

                    enrollment.Semester = 1;

                    
                    comm.CommandText = "SELECT IndexNumber FROM Student WHERE IndexNumber = @index";
                    comm.Parameters.AddWithValue("index", request.IndexNumber);
                    sdr.Close();
                    sdr = comm.ExecuteReader();

                    if (sdr.Read())
                    {
                        sdr.Close();
                        tran.Rollback();
                        throw new Exception("Index exists in database!");
                    }

                    comm.CommandText = "INSERT INTO Student VALUES (@index, @fname, @lname, @birthdate, @idEnroll)";
                    comm.Parameters.AddWithValue("fname", request.FirstName);
                    comm.Parameters.AddWithValue("lname", request.LastName);
                    comm.Parameters.AddWithValue("birthdate", DateTime.Parse(request.BirthDate));
                    comm.Parameters.AddWithValue("idEnroll", enrollment.IdEnrollment);
                    sdr.Close();
                    comm.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                    throw new Exception(exc.Message);
                }
            }
            return enrollment;
        }


        public Enrollment PromoteStudents(PromoteStudentRequest request)
        {
            using (var conn = new SqlConnection(connecionString))
            using (var comm = new SqlCommand())
            {
                conn.Open();
                comm.Connection = conn;
          
                comm.CommandText = "SELECT IdEnrollment FROM Enrollment " +
                                   "INNER JOIN Studies ON Enrollment.IdStudy = Studies.IdStudy " +
                                   "WHERE Name = @Studies AND Semester = @Semester;";
                comm.Parameters.AddWithValue("Studies", request.Studies);
                comm.Parameters.AddWithValue("Semester", request.Semester);
                
                var sdr = comm.ExecuteReader();

                if (!sdr.Read())
                    throw new Exception("Incorrect studies or semester!");

              
                comm.CommandText = "promoteStudents"; 
                comm.CommandType = CommandType.StoredProcedure;
                sdr.Close(); 
                comm.ExecuteNonQuery();
                    
              
                comm.CommandText = "SELECT IdEnrollment, Semester, Enrollment.IdStudy, StartDate FROM Enrollment " +
                                       "INNER JOIN Studies ON Enrollment.IdStudy = Studies.IdStudy " +
                                       "WHERE Name = @Studies AND Semester = @newSemester;"; 
                comm.CommandType = CommandType.Text; 
                comm.Parameters.AddWithValue("newSemester", request.Semester + 1); 
                sdr = comm.ExecuteReader();
                
                Enrollment enrollment = new Enrollment();
                
                if (sdr.Read()) 
                {
                    enrollment.IdEnrollment = (int) sdr["IdEnrollment"];
                    enrollment.Semester = (int) sdr["Semester"];
                    enrollment.IdStudy = (int) sdr["IdStudy"];
                    enrollment.StartDate = (DateTime) sdr["StartDate"];
                }
                
                return enrollment;;
            }
        }
    }
}