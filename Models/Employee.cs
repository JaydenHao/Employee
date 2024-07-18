using EmployeeApplication.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace EmployeeApplication.Models
{
    public class Employee
    {
        [Key]
        [Column("EmployeeId")]
        public long Id { get; set; }
        public string Name { get; set; }
        public string EmpNo { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string ICNo { get; set; }
        public string Department { get; set; }
        public string Occupation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HiredDate { get; set; }

 

    }
}
