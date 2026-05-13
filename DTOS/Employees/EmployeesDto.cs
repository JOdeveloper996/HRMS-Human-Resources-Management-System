namespace HRMS_Human_Resources_Management_System.DTOS.Employees
{
    public class EmployeesDto
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
