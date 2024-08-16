using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using StudentPortel.Web.Data;
using StudentPortel.Web.Models.Entities;
using System;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace StudentPortel.Web.Controllers
{
    public class Complaint : Controller
    {
        private readonly ApplicationdbContest dbContext;

        public Complaint(ApplicationdbContest dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Addcomplaint model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                model.CreatedDate = DateTime.UtcNow;
                model.EmployeeName = model.EmployeeName;
                model.EmployeeId = HttpContext.Session.GetString("EmployeeId"); 
                model.Department = model.Department;
                model.WorkLocation = model.WorkLocation;
                model.suggestions = model.suggestions;
                model.Status = "Pending"; 
                model.Remark = ""; 

                dbContext.Addcomplaint.Add(model);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index"); ;
            }

            return View(model); 
        }

        [HttpGet]
        public IActionResult List()
        {
            var complaints = dbContext.Addcomplaint.ToList();
            return View(complaints);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var complaint = dbContext.Addcomplaint.FirstOrDefault(c => c.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var complaint = dbContext.Addcomplaint.FirstOrDefault(c => c.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Addcomplaint model)
        {
            //if (ModelState.IsValid)
            {
                var complaint = dbContext.Addcomplaint.FirstOrDefault(c => c.Id == model.Id);
                if (complaint == null)
                {
                    return NotFound();
                }

                // Update the complaint details
                complaint.Remark = model.Remark;
                complaint.Status = model.Status;

                // Save changes to the database
                dbContext.Update(complaint);
                await dbContext.SaveChangesAsync();

                // Redirect to the details page
                return RedirectToAction("Details", new { id = model.Id });
            }

            // If the model state is invalid, return to the edit view with the current model
            return View(model);


       


        }
        //........login

        [HttpGet]
        public IActionResult Login()
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LogIN model)
        {
            // if (ModelState.IsValid)
            {
                var user = dbContext.LogIN
                    .FirstOrDefault(u => u.EmployeeId == model.EmployeeId && u.Password == model.Password);

                if (user != null)
                {
                    // Set session for EmployeeId
                    HttpContext.Session.SetString("EmployeeId", user.EmployeeId);

                    // Use TempData to pass data across redirects
                    TempData["hallo"] = HttpContext.Session.GetString("EmployeeId");

                    if (user.EmployeeId == "EMP004")
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        return RedirectToAction("Add");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult EmployeeComplaints()
        {
            // Get the EmployeeId from session
            var employeeId = HttpContext.Session.GetString("EmployeeId");
            ViewBag.hallo = HttpContext.Session.GetString("EmployeeId");
            var complaints = dbContext.Addcomplaint
                .Where(c => c.EmployeeId == employeeId)
                .ToList();

            return View(complaints);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Redirect to the Login page
            return RedirectToAction("LogIN"); // Adjust the controller and action name as needed
        }

    }
}
