
using System.ComponentModel.DataAnnotations;

namespace StudentPortel.Web.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Business Unit Name is required.")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Subjet { get; set; }
        public string Job { get; set; }
    }
}
