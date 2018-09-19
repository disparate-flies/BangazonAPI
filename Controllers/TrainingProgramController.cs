//Controller for TraingingProgram Table to GET, GET one, POST, PUT, and DELETE
//Written by Robert Leedy

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DFBangazon.Models;

namespace DFBangazon.Controllers
{
    //Define route and set controller base to database
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingProgramController : ControllerBase
    {
        private readonly IConfiguration _config;
        public TrainingProgramController(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET api/value
        //Defines GET method for GET all from ProductType table
        [HttpGet]
        public async Task<IActionResult> Get(string _completed)
        {
            using (IDbConnection conn = Connection)
            {
                Dictionary<int, TrainingProgram> EmpsInTraining = new Dictionary<int, TrainingProgram>();

                string sql = @"SELECT tp.ProgName,
                                      tp.Id,
                                      tp.StartDate,
                                      tp.EndDate,
                                      tp.MaxAttendees,
                                      et.Id,
                                      et.EmployeeId,
                                      et.TrainingProgramId,
                                      e.Id,
                                      e.IsSupervisor,
                                      e.DepartmentId,
                                      e.FirstName,
                                      e.LastName
                               FROM TrainingProgram tp
                               LEFT JOIN EmployeeTraining et ON tp.Id = et.TrainingProgramId
                               LEFT JOIN Employee e on et.EmployeeId = e.Id";

                if (_completed == "false")
                {
                    sql += $" WHERE tp.StartDate <= GETDATE()";
                    var fullTrainingProgram =
                        await conn.QueryAsync<TrainingProgram, EmployeeTraining, Employee, TrainingProgram>(
                        sql,
                        (trainingprogram, employeetraining, employee) =>
                        {
                            if (!EmpsInTraining.ContainsKey(trainingprogram.Id))
                            {
                                EmpsInTraining[trainingprogram.Id] = trainingprogram;
                            }
                            EmpsInTraining[trainingprogram.Id].Employee.Add(employee);
                            return trainingprogram;
                        }
                            );

                    return Ok(EmpsInTraining.Values);
                }


                else
                {
                    var fullTrainingProgram =
                        await conn.QueryAsync<TrainingProgram, EmployeeTraining, Employee, TrainingProgram>(
                        sql,
                        (trainingprogram, employeetraining, employee) =>
                        {
                            if (!EmpsInTraining.ContainsKey(trainingprogram.Id))
                            {
                                EmpsInTraining[trainingprogram.Id] = trainingprogram;
                            }
                            EmpsInTraining[trainingprogram.Id].Employee.Add(employee);
                            return trainingprogram;
                        }
                            );

                    return Ok(EmpsInTraining.Values);
                };
            }
        }

        [HttpGet("{Id}", Name = "GetTrainingProgram")]
        public async Task<IActionResult> Get(int id)
        {
            using (IDbConnection conn = Connection)
            {
                Dictionary<int, TrainingProgram> EmpsInTraining = new Dictionary<int, TrainingProgram>();

                string sql = $@"SELECT tp.ProgName,
                                       tp.Id,
                                       tp.StartDate,
                                       tp.EndDate,
                                       tp.MaxAttendees,
                                       et.Id,
                                       et.EmployeeId,
                                       et.TrainingProgramId,
                                       e.Id,
                                       e.IsSupervisor,
                                       e.DepartmentId,
                                       e.FirstName,
                                       e.LastName
                               FROM TrainingProgram tp
                               LEFT JOIN EmployeeTraining et ON tp.Id = et.TrainingProgramId
                               LEFT JOIN Employee e on et.EmployeeId = e.Id
                               WHERE tp.Id = {id}";

                await conn.QueryAsync<TrainingProgram, EmployeeTraining, Employee, TrainingProgram>(
                        sql, (trainingprogram, employeetraining, employee) =>
                        {
                            if (!EmpsInTraining.ContainsKey(trainingprogram.Id))
                            {
                                EmpsInTraining[trainingprogram.Id] = trainingprogram;
                            }
                            EmpsInTraining[trainingprogram.Id].Employee.Add(employee);
                            return trainingprogram;
                        }
                        );
                return Ok(EmpsInTraining.Values);
            };
        }
        // Defines POST method to add an item to the ProductType table
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TrainingProgram trainingProgram)
        {
            string sql = $@"INSERT INTO TrainingProgram
            (ProgName, StartDate, EndDate, MaxAttendees)
            VALUES
            ('{trainingProgram.ProgName}', '{trainingProgram.StartDate}', '{trainingProgram.EndDate}', '{trainingProgram.MaxAttendees}');
            select MAX(Id) from TrainingProgram";

            using (IDbConnection conn = Connection)
            {
                //Returns the object that was just created

                var newTrainingProgramId = (await conn.QueryAsync<int>(sql)).Single();
                trainingProgram.Id = newTrainingProgramId;
                return CreatedAtRoute("GetTrainingProgram", new { id = newTrainingProgramId }, trainingProgram);
            }
        }

        // PUT api/values/5
        //Defines PUT method to allow changes to be made to exisiting item in ProductType table
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] TrainingProgram trainingProgram)
        {
            string sql = $@"
            UPDATE TrainingProgram
            SET ProgName = '{trainingProgram.ProgName}',
                StartDate = '{trainingProgram.StartDate}',
                EndDate = '{trainingProgram.EndDate}',
                MaxAttendees = '{trainingProgram.MaxAttendees}'
            WHERE Id = {id}";

            try
            {
                using (IDbConnection conn = Connection)
                {
                    int rowsAffected = await conn.ExecuteAsync(sql);
                    if (rowsAffected > 0)
                    {
                        return new StatusCodeResult(StatusCodes.Status204NoContent);
                    }
                    throw new Exception("No rows affected");
                }
            }
            catch (Exception)
            {
                if (!TrainingProgramExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE api/values/5
        // Defines DELETE method to remove an item from ProductType Table
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    string sql = $"DELETE FROM TrainingProgram WHERE Id = {id} AND StartDate <= GETDATE()";

        //    try
        //    {
        //        using (IDbConnection conn = Connection)
        //        {
        //            int rowsAffected = await conn.ExecuteAsync(sql);
        //            if (rowsAffected > 0)
        //            {
        //                return new StatusCodeResult(StatusCodes.Status204NoContent);
        //            }
        //            throw new Exception("No rows affected");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        if (!TrainingProgramExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}

           

        private bool TrainingProgramExists(int id)
        {
            string sql = $"SELECT Id, ProgName, StartDate, EndDate, MaxAttendees FROM TrainingProgram WHERE Id = {id}";
            using (IDbConnection conn = Connection)
            {
                return conn.Query<TrainingProgram>(sql).Count() > 0;
            }
        }
    };
}

