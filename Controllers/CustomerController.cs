//Author: Natasha Cox
//Handles everything related to Customer Model

using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using DFBangazon.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DFBangazon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CustomerController(IConfiguration config)
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
        // GET api/customer
        [HttpGet]
        public async Task<IActionResult> Get(string q, string _include, string active)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT * FROM Customer";

                //GET api/customer is active
                if (active == "false")
                {
                    sql = $@"SELECT * FROM Customer 
LEFT JOIN Orders ON Orders.CustomerId = Customer.Id WHERE OrderDate IS NULL";

                    var customerNoOrders = await conn.QueryAsync<Customer>(sql);
                    return Ok(customerNoOrders);

                }
                
                //GET api/customer?q=
                if (q != null)
                {
                    sql = ($@"SELECT * FROM 
                        Customer WHERE 
                        FirstName LIKE '%{q}' 
                        OR LastName LIKE '%{q}'");
                }

                // GET api/customer?_include=product
                if (_include == "products")
                {
                    Dictionary<int, Customer> customerProducts = new Dictionary<int, Customer>();

                    sql = @"
                SELECT
                    c.Id,
                    c.FirstName,
                    c.LastName,
                    c.AccountCreated,
                    c.LastLogin,
                    p.Id,
                    p.Price,
                    p.Title,
                    p.ProdDesc,
                    p.Quantity,
                    p.SellerId,
                    p.ProductTypeId
                FROM Customer c
                JOIN Product p ON c.Id = p.SellerId";

                    var customerss = await conn.QueryAsync<Customer, Product, Customer>(
                    sql,
                    (customer, product) =>
                    {
                        if (!customerProducts.ContainsKey(customer.Id))
                        {
                            customerProducts[customer.Id] = customer;
                        }
                        customerProducts[customer.Id].Products.Add(product);
                        return customer;
                    }
                    );
                    return Ok(customerss);
                }else if(_include == "paymenttypes")
                {
                    Dictionary<int, Customer> customerPaymentTypes = new Dictionary<int, Customer>();

                    sql = @"SELECT
                            c.Id,
                            c.FirstName,
                            c.LastName,
                            c.AccountCreated,
                            c.LastLogin,
                            p.Id,
                            p.CustomerId,
                            p.AccountNo,
                            p.AccType,
                            p.Nickname,
                            p.IsActive
                    FROM Customer c
                    JOIN PaymentType p ON c.Id = p.CustomerId";

                    var custPayTypes = await conn.QueryAsync<Customer, PaymentType, Customer>(
                        sql,
                        (customers, paymenttypes) =>
                        {
                            if (!customerPaymentTypes.ContainsKey(customers.Id))
                            {
                                customerPaymentTypes[customers.Id] = customers;
                            }
                            customerPaymentTypes[customers.Id].PaymentTypes.Add(paymenttypes);
                            return customers;
                        });
                    return Ok(custPayTypes);
                }
                else
                {
                    var allCustomers = (await conn.QueryAsync<Customer>(sql));
                    return Ok(allCustomers);
                }
            }
        }

        // GET api/customer/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = $"SELECT * FROM Customer WHERE Id = {id}";

                var theSingleCustomer = (await conn.QueryAsync<Customer>(sql)).Single();
                return Ok(theSingleCustomer);
            }
        }

        // POST api/customer
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            string sql = $@"INSERT INTO Customer
            (FirstName, LastName, AccountCreated, LastLogin)
            VALUES
            ('{customer.FirstName}', '{customer.LastName}', '{customer.AccountCreated}', '{customer.LastLogin}')
            select MAX(Id) from Customer";

            using (IDbConnection conn = Connection)
            {
                var newCustomerId = (await conn.QueryAsync<int>(sql)).Single();
                customer.Id = newCustomerId;
                return CreatedAtRoute("GetCustomer", new { id = newCustomerId }, customer);
            }
        }

        // PUT api/customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Customer customer)
        {
            string sql = $@"
            UPDATE Customer
            SET FirstName = '{customer.FirstName}',
                LastName = '{customer.LastName}',
                AccountCreated = '{customer.AccountCreated}',
                LastLogin = '{customer.LastLogin}'
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
                if (!ExerciseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ExerciseExists(int id)
        {
            string sql = $"SELECT Id, FirstName, LastName, AccountCreated, LastLogin FROM Customer WHERE Id = {id}";
            using (IDbConnection conn = Connection)
            {
                return conn.Query<Customer>(sql).Count() > 0;
            }
        }
    }
}

