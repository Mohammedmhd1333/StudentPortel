
using System.ComponentModel.DataAnnotations;

namespace StudentPortel.Web.Models.Entities
{
    public class LogIN
    {
        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public string Place { get; set; }
    }
}
