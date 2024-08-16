using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortel.Web.Data;
using StudentPortel.Web.Models;
using StudentPortel.Web.Models.Entities;

namespace StudentPortel.Web.Controllers
{
    public class StudentsController : Controller
    {
        // Controller connection
        private readonly ApplicationdbContest dbContext;
        public StudentsController(ApplicationdbContest dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        

        [HttpGet]
        public IActionResult AddTest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)  //AddStudentViewModel model for data store
        {
            if (ModelState.IsValid)
            {
                var Student = new Student //student Entity
                {
                    Name = viewModel.Name,
                    Phone = viewModel.Phone,
                    Subscribed = viewModel.Subscribed,
                    Job = viewModel.Job,
                };
                await dbContext.Students.AddAsync(Student);
                await dbContext.SaveChangesAsync();
                return View();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students
                                          //.Where(s => s.Name == "mohammed")
                                          .ToListAsync();

            var employees = await dbContext.Employees.ToListAsync();

            var viewModel = new StudentEmployeeViewModel
            {
                Students = students,
                Employees = employees
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var Student = await dbContext.Students.FindAsync(id);
            return View(Student);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Student Viewmodel)
        {
            var Student = await dbContext.Students.FindAsync(Viewmodel.Id);
            if (Student is not null)
            {
                Student.Name = Viewmodel.Name;
                Student.Phone = Viewmodel.Phone;
                Student.Subscribed = Viewmodel.Subscribed;
                Student.Job = Viewmodel.Job;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
        [HttpGet]
        public async Task<IActionResult> PostList(string searchString)
        {
            // Get all students and employees initially
            var studentsQuery = dbContext.Students.AsQueryable();
            var employeesQuery = dbContext.Employees.AsQueryable();

            // Filter students based on search string
            if (!string.IsNullOrEmpty(searchString))
            {
                studentsQuery = studentsQuery.Where(s =>
                    s.Name.Contains(searchString) ||
                    s.Job.Contains(searchString));
            }

            var students = await studentsQuery.ToListAsync();
            var employees = await employeesQuery.ToListAsync();

            var viewModel = new StudentEmployeeViewModel
            {
                Students = students,
                Employees = employees
            };

            ViewData["CurrentFilter"] = searchString;

            return View("List", viewModel); // Ensure "PostList.cshtml" is rendered with the updated viewModel
        }


    }
}
