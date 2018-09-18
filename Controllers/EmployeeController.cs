//coded by jenn. This controller provides get, put, post and delete methods for employee.
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DFBangazon.Models;
using Microsoft.AspNetCore.Http;

namespace DFBangazon.Controllers //namespace of controller
{
    [Route("api/[controller]")] //route to API 
    [ApiController]
    public class EmployeeController : ControllerBase //gives employee controller the inheritance of controller base
    {
        private readonly IConfiguration _config; //setting the configuration value to a private readonly _config

        public EmployeeController(IConfiguration config) //a public constructor to set the IConfiguration
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection")); //sets default connection from app settings in Connection
            }
        }

        // GET all Employees
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (IDbConnection conn = Connection)  //setting the connectin to conn
            {

                string sql = $@"SELECT e.id,
                                      e.FirstName,
                                      e.LastName,
                                      e.IsSupervisor,
                                      e.DepartmentId,
                                      e.IsActive,
                                      ec.ComputerId,
                                      ec.Id,
                                      ec.EmployeeId,
                                      ec.DateAssigned,
                                      ec.DateTurnedIn,
                                      c.Id, 
                                      c.Model,
                                      c.DecommissionDate,
                                      c.PurchaseDate,
                                      c.Condition,
                                      d.DeptName,
                                      d.ExpenseBudget,
                                      d.Id
                              FROM Employee e
                              LEFT JOIN EmployeeComputer ec ON ec.EmployeeId = e.Id
                              LEFT JOIN Computer c ON c.Id = ec.ComputerId
                              LEFT JOIN Department d ON d.Id = e.DepartmentId
                              WHERE IsActive = 1; ";

                var fullEmployee = await conn.QueryAsync<Employee, EmployeeComputer, Computer, Department, Employee>(sql, (employee, employeecomputer, computer, department) =>
                {
                    employee.Computer = computer;
                    employee.Department = department;
                    return employee;
                }); //exposes the enumerator 
                return Ok(fullEmployee); // return all employee
            }
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<IActionResult> Get([FromRoute] int id) //gets a single employee by id
        {
            using (IDbConnection conn = Connection)  //setting the connectin to conn
            {
                string sql = $@"SELECT e.id,
                                      e.FirstName,
                                      e.LastName,
                                      e.IsSupervisor,
                                      e.DepartmentId,
                                      e.IsActive,
                                      ec.ComputerId,
                                      ec.Id,
                                      ec.EmployeeId,
                                      ec.DateAssigned,
                                      ec.DateTurnedIn,
                                      c.Id, 
                                      c.Model,
                                      c.DecommissionDate,
                                      c.PurchaseDate,
                                      c.Condition,
                                      d.DeptName,
                                      d.ExpenseBudget,
                                      d.Id
                              FROM Employee e
                              LEFT JOIN EmployeeComputer ec ON ec.EmployeeId = e.Id
                              LEFT JOIN Computer c ON c.Id = ec.ComputerId
                              LEFT JOIN Department d ON d.Id = e.DepartmentId
                              WHERE e.Id = {id}; "; //sql select by id

                var singleEmployee = await conn.QueryAsync<Employee, EmployeeComputer, Computer, Department, Employee>(sql,(employee, employeecomputer, computer, department) =>
                {
                    employee.Computer = computer;
                    employee.Department = department;
                    return employee;
                }); //exposes the enumerator 
                return Ok(singleEmployee); //returns the selected employee
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee) //posts a employee
        {
            string sql = $@"INSERT INTO Employee 
            (FirstName, LastName, IsSupervisor, DepartmentId, IsActive)
            VALUES
            ('{employee.FirstName}', '{employee.LastName}', '{employee.IsSupervisor}', '{employee.DepartmentId}', '{employee.IsActive}');
            select MAX(Id) from Employee"; //returns the last made employee

            using (IDbConnection conn = Connection) //setting connection to conn
            {
                var newEmployeeId = (await conn.QueryAsync<int>(sql)).Single(); //exposes the enumerator and sets to newEmployee
                employee.Id = newEmployeeId;
                return CreatedAtRoute("GetEmployee", new { id = newEmployeeId }, employee); //posts and returns the employee you just added
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Employee employee) //edits an existing employee
        {
            string sql = $@" 
            UPDATE Employee
            SET FirstName = '{employee.FirstName}',
                LastName = '{employee.LastName}', 
                IsSupervisor = '{employee.IsSupervisor}',
                DepartmentId = '{employee.DepartmentId}',
                IsActive = '{employee.IsActive}'
            WHERE Id = {id}"; //edits employee by id

            try
            {
                using (IDbConnection conn = Connection) //sets connection to conn
                {
                    int rowsAffected = await conn.ExecuteAsync(sql); //exposes the enumerator and sets it to rowsAffected
                    if (rowsAffected > 0)
                    {
                        return new StatusCodeResult(StatusCodes.Status204NoContent); //retuns status code 204, success but does not retun product changed
                    }
                    throw new Exception("No rows affected"); //gives alert that no edit took place
                }
            }
            catch (Exception)
            {
                if (!EmployeeExists(id)) //if employee id does not exist,
                {
                    return NotFound(); //return a statement "not found"
                }
                else
                {
                    throw; //otherwise, throw default error statement
                }
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) //deletes a employee by id
        {
            string sql = $@"DELETE FROM Employee WHERE Id = {id}"; //sql command that deletes employee by id

            using (IDbConnection conn = Connection)
            {
                int rowsAffected = await conn.ExecuteAsync(sql); //exposes the enumerator and sets rowsAffected
                if (rowsAffected > 0)
                {
                    return new StatusCodeResult(StatusCodes.Status204NoContent); //if sucess, returns sataus 204, with no comment
                }
                throw new Exception("No rows affected"); //otherwise, returns error statement
            }

        }

        private bool EmployeeExists(int id) //checks if employee exists by id
        {
            string sql = $"SELECT Id, FirstName, LastName, IsSupervisor, DepartmentId, IsActive FROM Employee WHERE Id = {id}";
            using (IDbConnection conn = Connection)
            {
                return conn.Query<Employee>(sql).Count() > 0; //returns employee by id
            }
        }
    }
}
