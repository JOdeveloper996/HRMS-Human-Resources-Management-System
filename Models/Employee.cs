namespace HRMS_Human_Resources_Management_System.Models
{
    public class Employee
    {

       public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; } // required
        public DateTime? EndDate { get; set; }  // (?) nullable, because the employee may still be working in the company
        public bool IsActive { get; set; }


        }


    }
    

