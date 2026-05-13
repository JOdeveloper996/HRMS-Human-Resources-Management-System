using HRMS_Human_Resources_Management_System.DTOS.Department;
using HRMS_Human_Resources_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_Human_Resources_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public static List<Department> departments = new List<Department>()
        {
            new Department { Id = 1, DepartmentName = "IT", Description = "Information Technology Department", FloorNumber = 3 },
            new Department { Id = 2, DepartmentName = "HR", Description = "Human Resources Department", FloorNumber = 2 },
            new Department { Id = 3, DepartmentName = "Finance", Description = "Finance Department", FloorNumber = 4 },
            new Department { Id = 4, DepartmentName = "Marketing", Description = "Marketing Department", FloorNumber = 5 },
            new Department { Id = 5, DepartmentName = "Sales", Description = "Sales Department", FloorNumber = 6 }
        };


        [HttpGet("GetByCriteria")]

        public IActionResult GetByCriteria(string departmentName, int? floorNumber)
        {
            var data = from dept in departments
                       where (departmentName == null || dept.DepartmentName.Contains(departmentName,StringComparison.OrdinalIgnoreCase)) &&
                             (floorNumber == null || dept.FloorNumber == floorNumber)
                       orderby dept.Id descending
                       select new DepartmentDto {                            
                           
                           Id = dept.Id,
                           DepartmentName = dept.DepartmentName,
                           Description = dept.Description,
                           FloorNumber = dept.FloorNumber
                       };

            return Ok(data);

        }


        [HttpGet("{id}")]

        public IActionResult GetById(long id)
        {
            var department = departments.Select(dept => new DepartmentDto
            {
                Id = dept.Id,
                DepartmentName = dept.DepartmentName,
                Description = dept.Description,
                FloorNumber = dept.FloorNumber
            }).FirstOrDefault(dept => dept.Id == id);
            if (department == null)
            {
                return NotFound("Department not found");
            }
           
            return Ok(department);
        }


        [HttpPost()]
        public IActionResult Add([FromBody] SaveDepartmentDto departmentDto)
        {
            var department = new Department
            {
                Id = (departments.LastOrDefault()?.Id ?? 0) + 1,
                DepartmentName = departmentDto.DepartmentName,
                Description = departmentDto.Description,
                FloorNumber = departmentDto.FloorNumber
            };

            departments.Add(department);
            return Ok(department.Id);
        }

        [HttpPut("PutUpdatedData")]
        public IActionResult UpdatedData([FromBody] SaveDepartmentDto departmentDto)
        {
            var department = departments.FirstOrDefault(x => x.Id == departmentDto.Id);
            if( department == null)
            {
                return NotFound("Department Does Not Exist");
            }
            department.DepartmentName = departmentDto.DepartmentName;
                department.Description = departmentDto.Description;
                department.FloorNumber = departmentDto.FloorNumber;

            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteData(long id)
        {
            var department = departments.FirstOrDefault(x => x.Id == id);
            if(department == null)
            {
                return NotFound("Department Does Not Exist");
            
            }

                departments.Remove(department);
                return Ok();
        }

    }
}
