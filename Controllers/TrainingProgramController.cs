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
                               JOIN EmployeeTraining et ON tp.Id = et.TrainingProgramId
                               JOIN Employee e on et.EmployeeId = e.Id";

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
                               JOIN EmployeeTraining et ON tp.Id = et.TrainingProgramId
                               JOIN Employee e on et.EmployeeId = e.Id
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
    };
}

