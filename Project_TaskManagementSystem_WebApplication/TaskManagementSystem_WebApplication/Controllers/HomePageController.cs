using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Security;
using System.Web.UI.WebControls;
using TaskManagementSystem_WebApplication.Models;
using TaskManagementSystem_WebApplication.Repository;
using TaskManagementSystem_WebApplication.Utilities;
using static System.Net.WebRequestMethods;

namespace TaskManagementSystem_WebApplication.Controllers
{
    public class HomePageController : Controller
    {
        private readonly SignRepository _signRepository;
        /// <summary>
        /// GET: HomePage
        /// </summary>
        /// <returns></returns>
        public ActionResult MainDashboard()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the MainDashBoard. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// AboutUs
        /// </summary>
        /// <returns></returns>
        public ActionResult AboutUs()
        {
            try
            {
               
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the Aboutus page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// ContactPage
        /// </summary>
        /// <returns></returns>
        public ActionResult ContactPage()
        {
            try
            {
                //throw new Exception("This is a test exception for logging purposes.");
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the Contact page. Please try again later.";
                return View("ErrorView"); 
            }
        }
        /// <summary>
        /// contact us
        /// </summary>
        /// <param name="contactMessages"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ContactPage(ContactMessages contactMessages)
        {
            try
            {
                _signRepository.CreateContactUs(contactMessages);
                return RedirectToAction("ContactPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while submitting the contact form . Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// HomePageController
        /// </summary>
        public HomePageController()
        {
            _signRepository = new SignRepository();
        }
        /// <summary>
        /// GET: HomePage
        /// </summary>
        /// <returns></returns>
        public ActionResult UserDashboard()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the User dashboard. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// GET:AdminDashBoard
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminDashboard()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the User dashboard. Please try again later.";
                return View("ErrorView");
            }
        }

        /// <summary>
        /// GET: /Account/SignIn
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SignInPage()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the Sign in page. Please try again later.";
                return View("ErrorView");
            }
        }

        /// <summary>
        /// POST: SignIn Page
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignInPage(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                // Retrieve user details by email
                UserLogin userLoginDetails = _signRepository.GetUserByEmail(userLogin.EmailAddress);
                if (userLoginDetails != null)
                {
                    string encodedPassword = Base64Encode(userLogin.Password);
                    if (encodedPassword == userLoginDetails.Password)
                    {
                        Session["UserID"] = userLoginDetails.UserID;
                        Session["FirstName"] = userLoginDetails.FirstName;
                        Session["EmailAddress"] = userLoginDetails.EmailAddress;
                        Session["ProfilePhoto"] = userLoginDetails.ProfilePhoto;

                        if (userLoginDetails.UserRole == "Admin")
                        {
                            FormsAuthentication.SetAuthCookie(userLoginDetails.FirstName, false);
                            return RedirectToAction("AdminDashBoard", "AdminPage");
                        }
                        else if (userLoginDetails.UserRole == "User")
                        {
                            FormsAuthentication.SetAuthCookie(userLoginDetails.FirstName, false);
                            return RedirectToAction("UserDashboard", "UserPage");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid user role.");
                            return View(userLogin);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid email address or password.");
                        return View(userLogin);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email address or password.");
                    return View(userLogin);
                }
            }
            ModelState.AddModelError("", "Login failed.Please check your email address and password and try again");
            return View(userLogin);
            }

        // Helper method to Base64 encode a string

            /// <summary>
            /// GET: SignUpPage
            /// </summary>
            /// <returns></returns>
        public ActionResult SignUpPage_demo()
        {
            try
            {
                var model = new UserSignUp
                {
                    States = _signRepository.GetStates(),
                    Cities = new List<SelectListItem>()
                };
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
        /// POST: SignUp/Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        public ActionResult SignUpPage_demo(UserSignUp model)
        {
            model.UserRole = "User";
            if (model.ProfilePhotoFile != null && model.ProfilePhotoFile.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(model.ProfilePhotoFile.InputStream))
                {
                    byte[] imageBytes = binaryReader.ReadBytes(model.ProfilePhotoFile.ContentLength);
                    model.ProfilePhoto = Convert.ToBase64String(imageBytes);
                }
            }
            model.Password = Base64Encode(model.Password);
            _signRepository.UserSignUpCreate(model);

            model.States = _signRepository.GetStates();
            model.Cities = new List<SelectListItem>();
            return RedirectToAction("SignInPage", "HomePage");
        }
        /// <summary>
        /// to encode into base 64 format
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        /// <summary>
        /// GET: AdminSignUpPage
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminSignUpPage()
        {
            try
            {
                var model = new UserSignUp

                {
                    States = _signRepository.GetStates(),
                    Cities = new List<SelectListItem>(),
                };

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading the Admin signup page. Please try again later.";
                return View("ErrorView");
            }
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //admin signuppage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminSignUpPage(UserSignUp model)
        {
            try
            {
                model.UserRole = "Admin";
                if (model.ProfilePhotoFile != null && model.ProfilePhotoFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(model.ProfilePhotoFile.InputStream))
                    {
                        byte[] imageBytes = binaryReader.ReadBytes(model.ProfilePhotoFile.ContentLength);
                        model.ProfilePhoto = Convert.ToBase64String(imageBytes);
                    }
                }

                _signRepository.UserSignUpCreate(model);
                model.States = _signRepository.GetStates();
                model.Cities = new List<SelectListItem>();

                return RedirectToAction("AdminDashBoard", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while submitting admin signup page. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// GET: GetCitiesByStateId
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        public JsonResult GetCities(int stateId)
        {
            var cities = _signRepository.GetCities(stateId);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Delete user account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                var signUp = _signRepository.GetSignUpById(id).FirstOrDefault();
                if (signUp == null)
                {
                    return RedirectToAction("AdminDashBoard");
                }
                return View(signUp);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while getting the user. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// DeleteConfirmation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                _signRepository.DeleteUser(id);
                return RedirectToAction("AdminDashBoard", "AdminPage");
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while deleting. Please try again later.";
                return View("ErrorView");
            }
        }

        /// <summary>
        /// get user account details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Details(int id)
        {
            try
            {
                var signUp = _signRepository.GetSignUpById(id).FirstOrDefault();
                return View(signUp);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                TempData["ErrorMessage"] = "An error occurred while loading details. Please try again later.";
                return View("ErrorView");
            }
        }
        /// <summary>
        /// Update User Acoount
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateUserAcoountPage()
        {
            try
            {
                var userId = (int)Session["UserId"];
                var signUps = _signRepository.GetSignUpById(userId).FirstOrDefault();
                if (signUps == null)
                {
                    return RedirectToAction("MainDashboard");
                }
                if (!int.TryParse(signUps.State, out int stateId))
                {
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
        public ActionResult UpdateUserAcoountPage(UserSignUp signup)
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

     
    }


}
