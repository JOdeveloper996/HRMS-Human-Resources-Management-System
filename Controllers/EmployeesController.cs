using HRMS_Human_Resources_Management_System.DTOS.Employees;
using HRMS_Human_Resources_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace HRMS_Human_Resources_Management_System.Controllers
{
    [Route("api/[controller]")] //define the route for the controller, [controller] will be replaced with the name of the controller (Employees in this case)
    [ApiController] //indicates that this class is an API controller, which means it will handle HTTP requests and return HTTP responses
    public class EmployeesController : ControllerBase
    {
        ////building Endpoints for EmployeesController Endpoints are the methods that will handle the HTTP requests and return HTTP responses
        //[HttpGet("GetAll")]
        //public IActionResult Get()
        //{
        //    return Ok( new { Name = "ahmad", Age = 25 }); // http response data , status code 200 (ok)
        //    //return NotFound("Data Not Found"); // http response status code 404 (not found)
        //    //return BadRequest("Data Not Loaded"); // http response status code 400 (bad request)
        //    //return StatusCode (500, "Internal Server Error"); // http response status code 500 (internal server error)

        //}

        //[HttpGet("GetEmployee")]
        //public IActionResult GetEmployee(int id) { 

        //    return Ok();

        //}
        //Employee Class ==>  Models
        public static List<Employee> employees = new List<Employee>()
        {
            new Employee { Id = 1, FirstName = "Ahmad", LastName = "Khan", Age = 25, Department = "IT", Position = "Developer", Email = "ahmad.khan@example.com", Phone = "123-456-7890", BirthDate = new DateTime(1998, 1, 1), Salary = 50000, HireDate = new DateTime(2020, 1, 1), EndDate = null, IsActive = true },
            new Employee { Id = 2, FirstName = "Sara", LastName = "Ali", Age = 30, Department = "HR", Position = "Manager", Email = "sara.ali@example.com", Phone = "123-456-7891", BirthDate = new DateTime(1993, 2, 2), Salary = 60000, HireDate = new DateTime(2019, 2, 2), EndDate = null, IsActive = true },
            new Employee { Id = 3, FirstName = "Ali", LastName = "Hassan", Age = 28, Department = "Finance", Position = "Analyst", Email = "ali.hassan@example.com", Phone = "123-456-7892", BirthDate = new DateTime(1995, 3, 3), Salary = 55000, HireDate = new DateTime(2021, 3, 3), EndDate = null, IsActive = true },
            new Employee { Id = 4, FirstName = "Lina", LastName = "Ahmed", Age = 32, Department = "Marketing", Position = "Coordinator", Email = "lina.ahmed@example.com", Phone = "123-456-7893", BirthDate = new DateTime(1991, 4, 4), Salary = 52000, HireDate = new DateTime(2018, 4, 4), EndDate = null, IsActive = true },
            new Employee { Id = 5, FirstName = "Omar", LastName = "Yusuf", Age = 27, Department = "Sales", Position = "Representative", Email = "omar.yusuf@example.com", Phone = "123-456-7894", BirthDate = new DateTime(1996, 5, 5), Salary = 48000, HireDate = new DateTime(2022, 5, 5), EndDate = null, IsActive = true  }
        };
        // this function call CRUD operations Create Retrive Update Delete
        [HttpGet("GetByCriteria")]
        public IActionResult GetByCriteria(string? position)
        {
            //query syntax

            var data = from emp in employees
                       where (position == null || emp.Position == position)
                       orderby emp.Id descending
                       // first time we use anonymous type but this method is not recommended because it will return an object that does not have a specific type, which can lead to issues when trying to access the properties of the object
                       // but also model can’t use can’t becuase well retrive all data and fill it in defualt data that mean the haker can use the the innacesery data to hakking website                     
                       select new EmployeesDto { // data transfer object
                           Id = emp.Id,
                           Name = emp.FirstName + " " + emp.LastName,
                           Position = emp.Position,
                           BirthDate = emp.BirthDate
                       };

            return Ok(data);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(long id)
        {
            var data = employees.Select
                (x => new EmployeesDto
                {
                    Id = x.Id,
                    Name = x.FirstName + " " + x.LastName,
                    Position = x.Position,
                    BirthDate = x.BirthDate
                }).FirstOrDefault(x => x.Id == id);




                if (data == null)
            {
                return NotFound("Data Not Found");
            }
            return Ok(data);
        }

        [HttpPost("AddEmployee")]
        public IActionResult Add([FromQuery]long id ,[FromBody]SaveEmployeeDto newEmployee)
        {
            var emp = new Employee()
            {
                Id = (employees.LastOrDefault()?.Id ?? 0 )+ 1,
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                Position = newEmployee.Position,
                BirthDate = newEmployee.BirthDate,
                Salary = newEmployee.Salary,
                Phone = newEmployee.Phone,
                Age = newEmployee.Age,
                HireDate = newEmployee.HireDate,
                EndDate = newEmployee.EndDate,
                IsActive = newEmployee.IsActive,
                Email = newEmployee.Email,

            };
            employees.Add(emp);
            return Ok(emp.Id);
        }

        [HttpPut("UpdatedEmployee")]

            public IActionResult Update(SaveEmployeeDto updatedEmployee)
            {
            var employee = employees.FirstOrDefault(x => x.Id == updatedEmployee.Id);
            if(employee == null)
            {
                return NotFound("Employee Does Not Exist");
            }
            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Position = updatedEmployee.Position;
            employee.BirthDate = updatedEmployee.BirthDate;
            employee.Salary = updatedEmployee.Salary;
            employee.Phone = updatedEmployee.Phone;
            employee.Age = updatedEmployee.Age;
            employee.HireDate = updatedEmployee.HireDate;
            employee.EndDate = updatedEmployee.EndDate;
            employee.IsActive = updatedEmployee.IsActive;
            employee.Email = updatedEmployee.Email;

            return Ok();
            }

            [HttpDelete("DeleteEmployee")]

            public IActionResult DeleteEmployee(long id)
            {
            var employee = employees.FirstOrDefault(x => x.Id == id);
            if(employee == null)
            {
                return NotFound("Employee Does Not Exist");
            }
            employees.Remove(employee);
            return Ok();
            }

        }
    }

// we need swwager to test our API Endpoints, we can use the following command to install swagger in our project

//query parameter you can find parameter in URL simple data type

//request body not appear any data input by user complix data type