using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem_WebApplication.Repository;
using TaskManagementSystem_WebApplication.Models;
using System.Reflection;
using System.Web.Security;
using System.Text;
using TaskManagementSystem_WebApplication.Utilities;

namespace TaskManagementSystem_WebApplication.Controllers
{
    [Authorize]
    public class AdminPageController : Controller
    {
        private readonly AdminRepository _adminRepository;
        private readonly SignRepository _signRepository;

        public AdminPageController()
        {
            _adminRepository = new AdminRepository();
            _signRepository = new SignRepository();
        }

        /// <summary>
        /// GET: AdminPage
        /// </summary>
        /// <returns></returns>

        
        public ActionResult AdminDashBoard()
        {
            try
            {
                string firstName = Session["FirstName"] != null ? Session["FirstName"].ToString() : "Guest";

                // Retrieve the list of users from the repository
                var signUpList = _adminRepository.GetAllUsers();
                string profilePhoto = Session["ProfilePhoto"] as string;
                // Pass data to the view using ViewBag
                ViewBag.ProfilePhoto = profilePhoto;

                ViewBag.FirstName = firstName;
                ViewBag.SignUpList = signUpList;

                return View(signUpList); // Return the existing view with the list
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the Contact page. Please try again later.";
                return View("ErrorView");
            }
        }
        //Task
        /// <summary>
        /// create Tasks
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateTask()
        {
            try
            {
                var model = new Tasks();

                // Fetch all user details
                var allUsers = _adminRepository.GetAllUsers(); // Existing method that fetches full user details
                var allProjects = _adminRepository.GetAllProjects();
                var allClients = _adminRepository.GetAllClients();

                // Project only the essential details for dropdown
                var userSelectList = allUsers.Select(details => new
                {
                    UserID = details.UserID,
                    Username = details.Username,
                    EmailAddress = details.EmailAddress
                }).ToList();

                var projectSelectList = allProjects.Select(project => new
                {
                    ProjectID = project.ProjectID,
                    ProjectName = project.ProjectName
                }).ToList();

                var clientSelectList = allClients.Select(client => new
                {
                    ClientID = client.ClientID,
                    ClientName = client.ClientName
                }).ToList();
                ViewBag.Users = userSelectList;
                ViewBag.Projects = projectSelectList;
                ViewBag.Clients = clientSelectList;
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the task data Please try again later.";
                return View("ErrorView");
            }
        }

        /// <summary>
        /// POST: AdminPage/Create Task
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateTask(Tasks tasks)
        {
            try
            {
                var userId = Session["UserID"] != null ? (int)Session["UserID"] : 0;
                tasks.CreatedBy = userId;

                _adminRepository.CreateTask(tasks);


                return RedirectToAction("GetAllTasks");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while task creation. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// get all tasks
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllTasks()
        {
            try
            {
                var projectList = _adminRepository.GetAllTasks();
                string profilePhoto = Session["ProfilePhoto"] as string;
                ViewBag.ProfilePhoto = profilePhoto;
                return View(projectList);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// GetAllReports
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllReports()
        {
            try
            {
                var reportList = _adminRepository.GetAllReports();
                string profilePhoto = Session["ProfilePhoto"] as string;
                ViewBag.ProfilePhoto = profilePhoto;
                return View(reportList);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }

        /// <summary>
        /// GetTasktByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetTask(int id)
        {
            try
            {
                var task = _adminRepository.GetTaskAllDataByID(id).FirstOrDefault();
                return View(task);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// update Tasks
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateTask(int id)
        {
            try
            {

                var task = _adminRepository.GetTaskByID(id).FirstOrDefault();
                var allUsers = _adminRepository.GetAllUsers();
                var allProjects = _adminRepository.GetAllProjects();
                var allClients = _adminRepository.GetAllClients();
                var userSelectList = allUsers.Select(details => new
                {
                    UserID = details.UserID,
                    Username = details.Username,
                    EmailAddress = details.EmailAddress
                }).ToList();
                var projectSelectList = allProjects.Select(project => new
                {
                    ProjectID = project.ProjectID,
                    ProjectName = project.ProjectName
                }).ToList();
                var clientSelectList = allClients.Select(client => new
                {
                    ClientID = client.ClientID,
                    ClientName = client.ClientName
                }).ToList();
                // Pass simplified user list to the view
                ViewBag.Users = userSelectList;
                ViewBag.Projects = projectSelectList;
                ViewBag.Clients = clientSelectList;
                return View(task);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the Contact page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateTask(Tasks tasks)
        {
            try
            {
                var userId = Session["UserID"] != null ? (int)Session["UserID"] : 0;
                tasks.CreatedBy = userId;
                _adminRepository.UpdateTask(tasks);
                return RedirectToAction("GetAllTasks", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while updating the task details.Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteTask(int id)
        {
            try
            {
                var task = _adminRepository.GetTaskByID(id).FirstOrDefault();
                if (task == null)
                {
                    return RedirectToAction("GetAllTasks");
                }
                return View(task);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the Contact page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// DeleteTask
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteTask(int id, FormCollection collection)
        {
            try
            {
                _adminRepository.DeleteTask(id);
                return RedirectToAction("GetAllTasks", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while Deleting the data. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// GET: AdminPage/Delete/5
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateProject()
        {
            try
            {
                var model = new Projects();
                var allUsers = _adminRepository.GetAllUsers();
                var allProjects = _adminRepository.GetAllProjects();
                var allClients = _adminRepository.GetAllClients();
                var clientSelectList = allClients.Select(client => new
                {
                    ClientID = client.ClientID,
                    ClientName = client.ClientName
                }).ToList();

                ViewBag.Clients = clientSelectList;

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading  page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// POST: AdminPage/Create Project
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateProject(Projects projects)
        {
            try
            {
                var userId = Session["UserID"] != null ? (int)Session["UserID"] : 0;
                projects.CreatedBy = userId;

                _adminRepository.CreateProject(projects);
                return RedirectToAction("GetProjects");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while project creation. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// Project
        /// </summary>
        /// <returns></returns>
        // GET: Projects
        public ActionResult GetProjects()
        {
            try
            {
                var projectList = _adminRepository.GetAllProjects();
                string profilePhoto = Session["ProfilePhoto"] as string;
                ViewBag.ProfilePhoto = profilePhoto;
                return View(projectList);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the Contact page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// get projectbyid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetProjectByID(int id)
        {
            try
            {
                var project = _adminRepository.GetProjectByID(id).FirstOrDefault();
                return View(project);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// delete project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteProject(int id)
        {
            try
            {
                var project = _adminRepository.GetProjectByID(id).FirstOrDefault();
                if (project == null)
                {
                    return RedirectToAction("AdminDashboard");
                }
                return View(project);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        [HttpPost ]
        public ActionResult DeleteProject(int id, FormCollection collection)
        {
            try
            {
                _adminRepository.DeleteProject(id);
                return RedirectToAction("GetProjects", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while deleting data. Please try again later.";
                return View("ErrorView");
            }
        }

        /// <summary>
        /// update project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateProject(int id)
        {
            try
            {
                var allClients = _adminRepository.GetAllClients();
                var clientSelectList = allClients.Select(client => new
                {
                    ClientID = client.ClientID,
                    ClientName = client.ClientName
                }).ToList();

                ViewBag.Clients = clientSelectList;
                var signUps = _adminRepository.GetProjectByID(id).FirstOrDefault();
                if (signUps == null)
                {
                    return RedirectToAction("AdminDashboard");
                }
                return View(signUps);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// UpdateProjec
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProject(Projects projects)
        {
            try
            {
                _adminRepository.UpdateProject(projects);
                return RedirectToAction("GetProjects", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while updating data. Please try again later.";
                return View("ErrorView");
            }
        }

        /// <summary>
        /// GET All clients
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllClients()
        {
            try
            {
                var projectList = _adminRepository.GetAllClients();
                string profilePhoto = Session["ProfilePhoto"] as string;
                ViewBag.ProfilePhoto = profilePhoto;
                return View(projectList);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }

        /// <summary>
        /// Create Client
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateClient()
        {
            try
            {
                var model = new Clients();
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the Contact page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// POST: AdminPage/Create Ptoject
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateClient(Clients clients)
        {
            try
            {
                var userId = Session["UserID"] != null ? (int)Session["UserID"] : 0;
                clients.CreatedBy = userId;

                _adminRepository.CreateClient(clients);


                return RedirectToAction("GetAllClients");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while client creation. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// UpdateClient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateClient(int id)
        {
            try
            {
                var client = _adminRepository.GetClientByID(id).FirstOrDefault();
                return View(client);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        ///updateClient
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateClient(Clients clients)
        {
            try
            {
                var userId = Session["UserID"] != null ? (int)Session["UserID"] : 0;
                clients.CreatedBy = userId;
                _adminRepository.UpdateClient(clients);
                return RedirectToAction("GetAllClients", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while client updation. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// get details of client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult GetClient(int id)
        {
            try
            {
                var client = _adminRepository.GetClientByID(id).FirstOrDefault();
                return View(client);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the Contact page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// Delete Client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteClient(int id)
        {
            try
            {
                var client = _adminRepository.GetClientByID(id).FirstOrDefault();
                if (client == null)
                {
                    return RedirectToAction("AdminDashboard");
                }
                return View(client);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteClient(int id, FormCollection collection)
        {
            try
            {
                _adminRepository.DeleteClient(id);
                return RedirectToAction("GetAllClients", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while client deletion. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// reports
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateReport()
        {
            try
            {
                var model = new Reports();
                var allTasks = _adminRepository.GetAllTasks();
                var taskSelectList = allTasks.Select(task => new
                {
                    TaskID = task.TaskID,
                    TaskTitle = task.TaskTitle
                }).ToList();
                ViewBag.Projects = taskSelectList;
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// POST: AdminPage/Create Ptoject
        /// </summary>
        /// <param name="reports"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReport(Reports reports)
        {
            try
            {
                var userId = Session["UserID"] != null ? (int)Session["UserID"] : 0;
                reports.GeneratedBy = userId;
                reports.GeneratedOn = DateTime.Now;


                if (reports.ReportImageFile != null && reports.ReportImageFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(reports.ReportImageFile.InputStream))
                    {
                        byte[] imageBytes = binaryReader.ReadBytes(reports.ReportImageFile.ContentLength);
                        reports.ReportImage = Convert.ToBase64String(imageBytes);
                    }
                }
                _adminRepository.CreateReport(reports);

                return RedirectToAction("GetAllReports");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while report creation Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// GetReport
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetReport(int id)
        {
            try
            {
                var report = _adminRepository.GetReportByID(id).FirstOrDefault();
                return View(report);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// UpdateReport
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateReport(int id)
        {
            try
            {
                var report = _adminRepository.GetReportByID(id).FirstOrDefault();
                var allTasks = _adminRepository.GetAllTasks();
                var taskSelectList = allTasks.Select(task => new
                {
                    TaskID = task.TaskID,
                    TaskTitle = task.TaskTitle
                }).ToList();
                ViewBag.Projects = taskSelectList;
                return View(report);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while record updation. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// UpdateReport
        /// </summary>
        /// <param name="reports"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateReport(Reports reports)
        {
            try
            {
                var userId = Session["UserID"] != null ? (int)Session["UserID"] : 0;
                reports.GeneratedBy = userId;
                _adminRepository.UpdateReport(reports);
                return RedirectToAction("GetAllReports", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while record updation. Please try again later.";
                return View("ErrorView");
            }
        }

        /// <summary>
        /// delete report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteReport(int id)
        {
            try
            {
                var task = _adminRepository.GetReportByID(id).FirstOrDefault();
                if (task == null)
                {
                    return RedirectToAction("AdminDashboard");
                }
                return View(task);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// DeleteReport
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteReport(int id, FormCollection collection)
        {
            try
            {
                _adminRepository.DeleteReport(id);
                return RedirectToAction("GetAllReports", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while report deletion. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// password reset
        /// </summary>
        /// <returns></returns>
        public ActionResult PasswordReset()
        {
            try
            {
                string emailAddress = Session["EmailAddress"] as string;
                var model = new PasswordReset
                {
                    EmailAddress = emailAddress,
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// PasswordReset
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PasswordReset(PasswordReset model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _adminRepository.GetUserByEmail(model.EmailAddress);
                    string encodedOldPassword = Base64Encode(model.Password);
                    string encodedNewPassword = Base64Encode(model.NewPassword);

                    if (user == null)
                    {
                        ModelState.AddModelError("", "User not found.");
                    }
                    else if (user.Password != encodedOldPassword)
                    //else if (user.Password != model.Password)
                    {
                        ModelState.AddModelError("", "The old password is incorrect.");
                    }
                    else
                    {
                        _adminRepository.UpdatePassword(model.EmailAddress, encodedNewPassword);
                        //_adminRepository.UpdatePassword(model.EmailAddress, model.NewPassword);
                        return RedirectToAction("AdminDashboard");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while resetting password. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// Base64Encode
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        /// <summary>
        /// list all comments
        /// </summary>
        /// <returns></returns>
        public ActionResult GetComments()
        {
            try
            {
                string profilePhoto = Session["ProfilePhoto"] as string;
                ViewBag.ProfilePhoto = profilePhoto;
                var commentList = _adminRepository.GetAllMessages();
                return View(commentList);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// get meassages by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetCommentByID(int id)
        {
            try
            {
                var project = _adminRepository.GetCommentByID(id).FirstOrDefault();
                return View(project);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// Delete message
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteComment(int id)
        {
            try
            {
                var task = _adminRepository.GetCommentByID(id).FirstOrDefault();
                if (task == null)
                {
                    return RedirectToAction("GetComments");
                }
                return View(task);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }

        }
        /// <summary>
        /// DeleteReport
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteComment(int id, FormCollection collection)
        {
            try
            {
                _adminRepository.DeleteMessage(id);
                return RedirectToAction("GetComments", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while message deletion. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// Update User Acoount
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateUserAccountPage(int id)
        {
            try
            {
                var signUps = _signRepository.GetSignUpById(id).FirstOrDefault();
                if (signUps == null)
                {
                    return RedirectToAction("MainDashboard");
                }
                if (!int.TryParse(signUps.State, out int stateId))
                {
                    // Handle the case if StateId is not convertible to an int
                    TempData["ErrorMessage"] = "Invalid State ID.";
                    return View("ErrorView");
                }
                signUps.States = _signRepository.GetStates();

                signUps.Cities = _signRepository.GetCities(stateId);
                         

                return View(signUps);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the Contact page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// update user account page
        /// </summary>
        /// <param name="signup"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult UpdateUserAccountPage(UserSignUp signup)
        {
            try
            {
                if (signup.ProfilePhotoFile != null && signup.ProfilePhotoFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(signup.ProfilePhotoFile.InputStream))
                    {
                        byte[] imageBytes = binaryReader.ReadBytes(signup.ProfilePhotoFile.ContentLength);
                        signup.ProfilePhoto = Convert.ToBase64String(imageBytes);
                    }
                }
                _signRepository.UpdateSignUp(signup);
                return RedirectToAction("AdminDashBoard", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while updating the data Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// Sign Out
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            try
            {
                Session.Abandon();
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("SignInPage", "HomePage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while signout. Please try again later.";
                return View("ErrorView");
            }
        }
    }
}








