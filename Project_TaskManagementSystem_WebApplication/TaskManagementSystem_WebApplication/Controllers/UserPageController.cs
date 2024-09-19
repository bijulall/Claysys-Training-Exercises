using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem_WebApplication.Repository;
using TaskManagementSystem_WebApplication.Models;
using System.IO;
using TaskManagementSystem_WebApplication.Utilities;

namespace TaskManagementSystem_WebApplication.Controllers
{
    [Authorize]
    public class UserPageController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly AdminRepository _adminRepository;
        public UserPageController()
        {
            _userRepository = new UserRepository();
            _adminRepository = new AdminRepository();
        }
        /// <summary>
        /// GET: UserPage
        /// </summary>
        /// <returns></returns>
        public ActionResult UserDashBoard()
        {
            try
            {
                string profilePhoto = Session["ProfilePhoto"] as string;
                ViewBag.ProfilePhoto = profilePhoto;
                var userId = Session["UserID"] != null ? (int)Session["UserID"] : 0;
                var taskList = _userRepository.GetTaskDetailsByID(userId);
                return View(taskList);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        // GET: UserPage/Details/5
        //public ActionResult TaskDetails(int id)
        //{
        //    var task = _userRepository.GetTaskDetailsByID(id).FirstOrDefault();
        //    return View(task);
        //}
        public ActionResult TaskDeatils(int id)
        {
            try
            {
                var task = _userRepository.GetTaskDetailsByTask(id).FirstOrDefault();
                TempData["TaskID"] = id;
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
        /// GET: UserPage/Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>lient
        public ActionResult UpdateTaskProgress(int id)
        {
            try
            {
                TaskProgress taskProgress = _userRepository.GetTaskDetailsUpdate(id).FirstOrDefault();
                TempData["TaskID"] = id;
                return View(taskProgress);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// UpdateTaskProgress
        /// </summary>
        /// <param name="taskProgress"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateTaskProgress(TaskProgress taskProgress)
        {
            try
            {
                taskProgress.TaskID = (int)TempData["TaskID"];
                if (taskProgress.TaskProgressFile != null && taskProgress.TaskProgressFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(taskProgress.TaskProgressFile.InputStream))
                    {
                        byte[] imageBytes = binaryReader.ReadBytes(taskProgress.TaskProgressFile.ContentLength);
                        taskProgress.TaskFile = Convert.ToBase64String(imageBytes);
                    }
                }
                _userRepository.UpdateTaskDetailsByUser(taskProgress);
                return RedirectToAction("UserDashBoard");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while updating the task progress. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// DeleteTaskbyUser/// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteTaskbyUser(int id)
        {
            try
            {
                var task = _userRepository.GetTaskDetailsByTask(id).FirstOrDefault();
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
        /// DeleteTaskbyUser        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult DeleteTaskbyUser(int id, FormCollection collection)
        {
            try
            {
                _userRepository.DeleteTask(id);
                return RedirectToAction("AdminDashBoard", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while task deletion. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// create task
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateTaskForUser()
        {
            try
            {
                var model = new Tasks();
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
                ViewBag.Users = userSelectList;
                ViewBag.Projects = projectSelectList;
                ViewBag.Clients = clientSelectList;

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
        /// POST: AdminPage/Create Task
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateTaskForUser(Tasks tasks)
        {
            try
            {
                var userId = Session["UserID"] != null ? (int)Session["UserID"] : 0;
                tasks.CreatedBy = userId;
                _adminRepository.CreateTask(tasks);
                return RedirectToAction("AdminDashboard");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while task creation. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// get report by user id
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReportByUSER()
        {
            try
            {
                var userId = Session["UserID"] != null ? (int)Session["UserID"] : 0;

                var reportby = _userRepository.GetReportByUserID(userId);
                return View(reportby);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// get report by task id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetReportByID(int id)
        {
            try
            {
                var reportby = _userRepository.GetReportByID(id);
                return View(reportby);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading report. Please try again later.";
                return View("ErrorView");
            }
        }
    }
}
