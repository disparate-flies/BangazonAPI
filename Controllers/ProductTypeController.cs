//Controller for ProductType Table to GET, GET one, POST, PUT, and DELETE
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
    public class ProductTypeController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProductTypeController(IConfiguration config)
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

        // GET api/values
        //Defines GET method for GET all from ProductType table
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT * FROM ProductType";

                var fullProductType = await conn.QueryAsync<ProductType>(sql);
                return Ok(fullProductType);
            };
        }

        //Defines GET method for GET one specific item from ProductType table
        [HttpGet("{id}", Name = "GetProductType")]
        public async Task<IActionResult> Get(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = $"SELECT * FROM ProductType WHERE Id = {id}";

                var OneProductType = (await conn.QueryAsync<ProductType>(sql)).Single();
                return Ok(OneProductType);
            };
        }

        // POST api/values
        // Defines POST method to add an item to the ProductType table
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductType productType)
        {
            string sql = $@"INSERT INTO ProductType
            (Name)
            VALUES
            ('{productType.Name}');
            select MAX(Id) from ProductType";

            using (IDbConnection conn = Connection)
            {
                //Returns the object that was just created

                var newProductTypeId = (await conn.QueryAsync<int>(sql)).Single();
                productType.Id = newProductTypeId;
                return CreatedAtRoute("GetProductType", new { id = newProductTypeId }, productType);
            }
        }

        // PUT api/values/5
        //Defines PUT method to allow changes to be made to exisiting item in ProductType table
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ProductType productType)
        {
            string sql = $@"
            UPDATE ProductType
            SET Name = '{productType.Name}'
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
                if (!ProductTypeExists(id))
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            string sql = $@"DELETE FROM ProductType WHERE Id = {id}";

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

        private bool ProductTypeExists(int id)
        {
            string sql = $"SELECT Id, Name FROM ProductType WHERE Id = {id}";
            using (IDbConnection conn = Connection)
            {
                return conn.Query<ProductType>(sql).Count() > 0;
            }
        }


    }
}

