using System.ComponentModel.DataAnnotations;

namespace StudentPortel.Web.Models.Entities
{
    public class Addcomplaint
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string WorkLocation { get; set; }
        public string suggestions { get; set; }
        public string? Status { get; set; }
        public string? Remark { get; set; }
    }
}
