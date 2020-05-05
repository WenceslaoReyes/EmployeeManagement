using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeeManagement.Controllers
{
    /*Adding route attribute on class to cambine it with action method attribute routes */
    //[Route("Home")]

    /*Adding route attribute on class to cambine it with action method attribute routes BUT with tokens */
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly ILogger _logger;

        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostingEnvironment, ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            _webHostingEnvironment = webHostingEnvironment;
            this._logger = logger;
        }

        /*Absolute routes*/
        //[Route("")]
        //[Route("Home")] 
        //[Route("Home/Index")]

        /*Combining attribute route with class attribute route*/
        //[Route("")]
        //[Route("~/Home")]
        //[Route("~/")]
        public ViewResult Index()
        {
            IEnumerable<Employee> model = _employeeRepository.GetEmployees();
            return View(model);
        }

        //[Route("Home/Details/{id?}")]
        //[Route("{id?}")]
        public ViewResult Details(int? id)
        {
            Employee model = _employeeRepository.GetEmployee(id ?? 1);
            //throw new Exception("Customed error");
            _logger.LogInformation("Information log");
            _logger.LogWarning("Error log");
            _logger.LogCritical("Critical log");
            _logger.LogDebug("Debug log");
            _logger.LogTrace("Trace log");
            if(model == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeWasNotFound", id.Value);
            }

            HomeDetailsViewModel ViewModel = new HomeDetailsViewModel()
            {
                Employee = model,
                Title = "Details page"
            };

            /*Using ViewData to send data to the View */
            //ViewData["Title-Page"] = "Details page";
            //ViewData["model"] = model;
            /*-----------------------------------------*/

            /*Using ViewBag to send data to the View */
            //ViewBag.title = "Details page";
            //ViewBag.model = model;
            /*-----------------------------------------*/


            /* View() and View(model) take the action method name as View name.
               Controller name will be taken as the folder name of the view or Shared folder.
               All of it is in View folder.
            */
            return View(ViewModel);


            /*
                Overloaded version of View() you can add a string a as a View name (it takes Views folder and controller name as folders).
                Absolute paths are available:
                ~/Views/Home/Test.cshtml
                /Views/Home/Test.cshtml
                Views/Home/Test.cshtml
             */
            //return View("~/Views/Home/Test.cshtml");


            /*
                For relative path or folder inside Views folder you don't have to use extension
            */
            //return View("../Test/Test");

        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = GetUniqueName(model);

                Employee employee = new Employee
                {
                    Name = model.Name,
                    Department = model.Department,
                    Email = model.Email,
                    PhotoPath = uniqueFileName

                };
                Employee emp = _employeeRepository.CreateEmployee(employee);
                //Redirect to another controller action method
                return RedirectToAction("Details", new { id = emp.Id });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee oldEmployee = _employeeRepository.GetEmployee(id);
            EditEmployeeViewModel editEmployee = null;
            if (oldEmployee != null)
            {
                editEmployee = new EditEmployeeViewModel
                {
                    Id = oldEmployee.Id,
                    Name = oldEmployee.Name,
                    Department = oldEmployee.Department,
                    Email = oldEmployee.Email,
                    ExistingPath = oldEmployee.PhotoPath,
                };
            }
            return View(editEmployee);
        }

        [HttpPost]
        public IActionResult Edit(EditEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee emp = _employeeRepository.GetEmployee(model.Id);
                emp.Name = model.Name;
                emp.Department = model.Department;
                emp.Email = model.Email;
                emp.PhotoPath = GetUniqueName(model, model.ExistingPath);


                Employee employee = _employeeRepository.Upadate(emp);
                //Redirect to another controller action method
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private string GetUniqueName(CreateEmployeeViewModel model, string existingFileName = null)
        {
            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
            {
                foreach (var photo in model.Photos)
                {
                    string uploadDiroctory = Path.Combine(_webHostingEnvironment.WebRootPath, "img");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadDiroctory, uniqueFileName);
                    using(var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                    
                }
                if (existingFileName != null)
                {
                    string fileDir = Path.Combine(_webHostingEnvironment.WebRootPath, "img", existingFileName);
                    System.IO.File.Delete(fileDir);
                }
            }
            else
            {
                uniqueFileName = existingFileName;
            }

            return uniqueFileName;
        }
    }
}