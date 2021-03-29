using EMPMGT22.Models;
using EMPMGT22.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EMPMGT22.Controllers
{

    //[Route("Home")]

    //[Route("[Controller]")]

        [Authorize]

    public class HomeController:Controller
    {
        private readonly IEmployeeRpository _employeeRepository;
        private readonly IHostingEnvironment hostingenvironment;
        private readonly ILogger logger;

        //public string index()
        //{

        //    return "hello from mvc";
        //}




        public HomeController(IEmployeeRpository employeeRepository,IHostingEnvironment hostingenvironment,ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            this.hostingenvironment = hostingenvironment;
            this.logger = logger;
        }
        //public JsonResult index()

        //{

        //    return this.Json(new { id = 1, name = "samu" }) ;
        //}

        //public string Index()
        //{
        //   return  _employeeRepository.GetEmployee(1).Name;

        //}
        //[Route("")]
        ////[Route("Index")]
        //[Route("[action]")]
        //[Route("~/")]
        [AllowAnonymous]
        public ViewResult Index()
        {
            //return  _employeeRepository.GetEmployee(1).Name;
            var model =  _employeeRepository.GetAllEmployee();
            return View(model);
        }

        //public JsonResult Details()
        // {

        //    Employee model = _employeeRepository.GetEmployee(1);
        //    return Json(model);

        //}


        //public ObjectResult Details()
        //{

        //    Employee model = _employeeRepository.GetEmployee(1);
        //    return new ObjectResult(model);

        //}


        //public ViewResult Details()
        //{

        //    Employee model = _employeeRepository.GetEmployee(1);
        //    return  View(model);

        //}


        //public ViewResult Details()
        //{

        //    Employee model = _employeeRepository.GetEmployee(1);
        //    ViewData["Employee"] = model;
        //    ViewData["PageTitle"] = "Employee Details";
        //    return View(model);


        //}

        //public ViewResult Details()
        //{

        //    Employee model = _employeeRepository.GetEmployee(1);
        //    ViewBag.Employee = model;
        //    ViewBag.PageTitle = "Employee Details";
        //    return View(model);


        //}

        //public ViewResult Details()
        //{

        //    Employee model = _employeeRepository.GetEmployee(1);
        //    ViewBag.Employee = model;
        //    ViewBag.PageTitle = "Employee Details";
        //    return View();
        //                   }

        //public ViewResult Details()
        //{

        //    Employee model = _employeeRepository.GetEmployee(1);
        //    //ViewBag.Employee = model;
        //    ViewBag.PageTitle = "Employee Details";
        //    return View(model);
        //}


        //public ViewResult Details()
        //{
        //    HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
        //    {
        //        Employee = _employeeRepository.GetEmployee(1),
        //        PageTitle = "Employee Details"

        //    };

        //public ViewResult Details(int id)
        //{
        //    HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
        //    {
        //        //Employee = _employeeRepository.GetEmployee(id),
        //        Employee = _employeeRepository.GetEmployee(1),
        //        PageTitle = "Employee Details"

        //    };
        //    return View(homeDetailsViewModel);
        //}
        //[Route("Details/{id?}")]

        //[Route("[action]/{id?}")]
        //public ViewResult Details( int? id)
        //{
        //    HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
        //    {
        //        //Employee = _employeeRepository.GetEmployee(id),
        //        //Employee = _employeeRepository.GetEmployee(1),
        //        Employee = _employeeRepository.GetEmployee(id??1),
        //        PageTitle = "Employee Details"

        //    };
        //    return View(homeDetailsViewModel);
        //}
        [AllowAnonymous]
        public ViewResult Details(int? id)

        {

            //throw new Exception("Error in Details View");

            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");



            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)

            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound",id.Value);

            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                //Employee = _employeeRepository.GetEmployee(id),
                //Employee = _employeeRepository.GetEmployee(1),
                //Employee = _employeeRepository.GetEmployee(id ?? 1),
                Employee = employee,
                PageTitle = "Employee Details"

            };
            return View(homeDetailsViewModel);


        }

        [HttpGet]
        public ViewResult create()
        {

            return View();

        }


        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath

            };

            return View(employeeEditViewModel);

        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if(model.Photos!=null)
                {

                    if (model.ExistingPhotoPath!=null)
                    {
                        string filepath = Path.Combine(hostingenvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filepath);


                    }
                    //string uniqueFilename = ProcesUploadedFile(model);
                    employee.PhotoPath = ProcesUploadedFile(model);
                }

                

                //Employee newEmployee = new Employee
                //{
                //    Name = model.Name,
                //    Email = model.Email,
                //    Department = model.Department,
                //    PhotoPath = uniqueFilename
                //};
                //_employeeRepository.Update(newEmployee);
                _employeeRepository.Update(employee);
                //return RedirectToAction("details", new { id = newEmployee.Id });
                return RedirectToAction("index");
            }

            return View();
        }

        //private string ProcesUploadedFile(EmployeeEditViewModel model)
        //private string ProcesUploadedFile(EmployeeCreateViewModel model)
        //{
        //    string uniqueFilename = null;



        //    if (model.Photos != null && model.Photos.Count > 0)
        //    {
        //        foreach (IFormFile photo in model.Photos)
        //        {
        //            string uploadsFolder = Path.Combine(hostingenvironment.WebRootPath, "images");
        //            uniqueFilename = Guid.NewGuid().ToString() + "_" + photo.FileName;
        //            string filePath = Path.Combine(uploadsFolder, uniqueFilename);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                model.Photo.CopyTo(fileStream);
        //            }
        //            //photo.CopyTo(new FileStream(filePath, FileMode.Create));
        //        }

        //    }

        //    return uniqueFilename;
        //}
        private string ProcesUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
            {
                foreach (IFormFile photo in model.Photos)
                {
                    string uploadsFolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }

        //public ViewResult create(Employee employee)
        //[HttpPost]
        //public RedirectToActionResult create(Employee employee)
        //public IActionResult create(Employee employee)
        //{
        //    //return View();
        //    if (ModelState.IsValid) {
        //   Employee newemployee= _employeeRepository.Add(employee);
        //    return RedirectToAction("Details", new { id = newemployee.Id });
        //    }

        //    return View();
        //}
        //        [HttpPost]
        //        public IActionResult create(EmployeeCreateViewModel model)
        //        {

        //            if (ModelState.IsValid)
        //            {

        //                string uniqueFilename = null;
        //                //if(model.Photos !=null)
        //                //{
        //                //    string uploadsFolder = Path.Combine(hostingenvironment.WebRootPath, "images");
        //                //    uniqueFilename = Guid.NewGuid().ToString() + "_" + model.Photos.FileName;
        //                //    string filePath = Path.Combine(uploadsFolder, uniqueFilename);
        //                //    model.Photos.CopyTo(new FileStream(filePath, FileMode.Create));

        //                //}
        //                if (model.Photos != null && model.Photos.Count>0)
        //                {
        //                    foreach (IFormFile photo in model.Photos)
        //                    {
        //                        string uploadsFolder = Path.Combine(hostingenvironment.WebRootPath, "images");
        //                        uniqueFilename = Guid.NewGuid().ToString() + "_" + photo.FileName;
        //                        string filePath = Path.Combine(uploadsFolder, uniqueFilename);
        //                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
        //                    }

        //                }

        //                Employee newEmployee = new Employee
        //                {
        //                    Name = model.Name,
        //                    Email = model.Email,
        //                    Department = model.Department,
        //                    // Store the file name in PhotoPath property of the employee object
        //                    // which gets saved to the Employees database table
        //                    PhotoPath = uniqueFilename
        //                };
        //                _employeeRepository.Add(newEmployee);
        //                return RedirectToAction("details", new { id = newEmployee.Id });
        //            }

        //            return View();
        //        }
        //    }
        //}
        [HttpPost]
        public IActionResult create(EmployeeCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFilename = ProcesUploadedFile(model);
                //string uniqueFilename = null;
                ////if(model.Photos !=null)
                ////{
                ////    string uploadsFolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                ////    uniqueFilename = Guid.NewGuid().ToString() + "_" + model.Photos.FileName;
                ////    string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                ////    model.Photos.CopyTo(new FileStream(filePath, FileMode.Create));

                ////}
                //if (model.Photos != null && model.Photos.Count > 0)
                //{
                //    foreach (IFormFile photo in model.Photos)
                //    {
                //        string uploadsFolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                //        uniqueFilename = Guid.NewGuid().ToString() + "_" + photo.FileName;
                //        string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                //        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                //    }

               // }

            Employee newEmployee = new Employee
            {
                Name = model.Name,
                Email = model.Email,
                Department = model.Department,
                // Store the file name in PhotoPath property of the employee object
                // which gets saved to the Employees database table
                PhotoPath = uniqueFilename
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }
    }
}
