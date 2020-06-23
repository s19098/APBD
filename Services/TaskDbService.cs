using Kolokwium1Poprawa.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1Poprawa.Services
{
    public class TaskDbService : IDbService
    {
        private readonly string _dbConString = "Data Source=db-mssql;Initial Catalog=s19098;Integrated Security=True";

        public TeamMemberResponse GetTeamMember(int idMember)
            {
                var response = new TeamMemberResponse();

                using (var con = new SqlConnection(_dbConString))
                using (var com = con.CreateCommand())
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "SELECT * FROM TeamMember WHERE IdTeamMember = @IdMember";
                    com.Parameters.AddWithValue("IdMember", idMember);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        throw new Exception($"Member with id:{idMember} does not exist");
                    }

                    response.IdTeamMember = Int32.Parse(dr["IdTeamMember"].ToString());
                    response.FirstName = dr["FirstName"].ToString();
                    response.LastName = dr["LastName"].ToString();
                    response.Email = dr["Email"].ToString();

                    response.AssignedTasks = new List<TaskResponse>();
                    response.CreatedTasks = new List<TaskResponse>();

                    dr.Close();

                    com.CommandText = $"SELECT Task.Name, Task.Description, Task.Deadline, Project.Name, TaskType.Name " +
                                        $"FROM Task " +
                                        $"INNER JOIN Project ON Project.IdTeam = Task.IdTeam " +
                                        $"INNER JOIN TaskType ON TaskType.IdTaskType = Task.IdTaskType " +
                                        $"WHERE IdAssignedTo = @IdMember " +
                                        $"ORDER BY Task.Deadline DESC";

                    dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        var task = new TaskResponse();

                        task.Name = dr["Task.Name"].ToString();
                        task.Description = dr["Task.Description"].ToString();
                        task.Deadline = DateTime.Parse(dr["Task.Deadline"].ToString());
                        task.TeamName = dr["Project.Name"].ToString();
                        task.TasktypeName = dr["TaskType.Name"].ToString();

                        response.AssignedTasks.Add(task);
                    }
                    dr.Close();

                    com.CommandText = $"SELECT Task.Name, Task.Description, Task.Deadline, Project.Name, TaskType.Name " +
                                        $"FROM Task " +
                                        $"INNER JOIN Project ON Project.IdTeam = Task.IdTeam " +
                                        $"INNER JOIN TaskType ON TaskType.IdTaskType = Task.IdTaskType " +
                                        $"WHERE IdCreator = @IdMember " +
                                        $"ORDER BY Task.Deadline DESC";

                    dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        var task = new TaskResponse();

                        task.Name = dr["Task.Name"].ToString();
                        task.Description = dr["Task.Description"].ToString();
                        task.Deadline = DateTime.Parse(dr["Task.Deadline"].ToString());
                        task.TeamName = dr["Project.Name"].ToString();
                        task.TasktypeName = dr["TaskType.Name"].ToString();

                        response.CreatedTasks.Add(task);
                    }
                    dr.Close();
                }

                return response;
            }

        public ProjectResponse DeleteProject(int idProject)
        {
            var response = new ProjectResponse();

            using (var con = new SqlConnection(_dbConString))
            using (var com = con.CreateCommand())
            {
                con.Open();
                var tran = con.BeginTransaction();

                com.Connection = con;
                com.Transaction = tran;

                com.CommandText = "SELECT * FROM Task WHERE IdTeam = @idTeam";

                com.Parameters.AddWithValue("idTeam", idProject);//zabezpieczenie

                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    dr.Close();
                    tran.Rollback();
                    throw new Exception($"Task number:{idProject} does not exist !");
                }

                response.IdTeam = Int32.Parse(dr["IdTeam"].ToString());
                response.Name = dr["Name"].ToString();
                response.Deadline = DateTime.Parse(dr["Deadline"].ToString());

                dr.Close();

                com.CommandText = "ALTER TABLE Task NOCHECK CONSTRAINT ALL";
                com.ExecuteNonQuery();

                com.CommandText = "DELETE FROM Task WHERE IdTeam = @idTeam";
                com.ExecuteNonQuery();

                com.CommandText = "DELETE FROM Project WHERE IdTeam = @idTeam";
                com.ExecuteNonQuery();

                com.CommandText = "ALTER TABLE Task WITH CHECK CONSTRAINT ALL";

                tran.Commit();
            }

            return response;
        }

        
    }
}
