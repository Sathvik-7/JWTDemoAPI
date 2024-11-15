using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models
{
    public class Employee
    {
        [Key]
        public int empId { get; set; }

        public string name { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;
    }
}
