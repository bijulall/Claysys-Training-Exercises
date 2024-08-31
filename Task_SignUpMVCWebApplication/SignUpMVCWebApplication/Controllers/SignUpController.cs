using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SignUpMVCWebApplication.DAL;
using SignUpMVCWebApplication.Models;

namespace SignUpMVCWebApplication.Controllers
{
    public class SignUpController : Controller
    {
        SignUp_DAL _signUpDAL =new SignUp_DAL();
        // GET: SignUp
        public ActionResult Index()
        {
            var signUpList = _signUpDAL.GetAllSignUps();
            if(signUpList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently no signup details available";
            }
            return View(signUpList);
        }

        // GET: SignUp/Details/5
        public ActionResult Details(int id)
        {
            var signUp=_signUpDAL.GetSignUpById(id).FirstOrDefault();
            return View(signUp);
        }

        // GET: SignUp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SignUp/Create
        [HttpPost]
        public ActionResult Create(SignUp signup)
        {
            bool IsInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _signUpDAL.InsertSignUp(signup);
                    if (IsInserted)
                        {
                            TempData["InfoMessage"] = "SignUp Completed Successfully";
                        }

                    else
                        {
                            TempData["ErrorMessage"] = "Unable to save the signup  details";
                        }
                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception  ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: SignUp/Edit/5
        public ActionResult Edit(int id)
        {
            var signUps =_signUpDAL.GetSignUpById(id).FirstOrDefault();
            if(signUps ==null)
            {
                TempData["InfoMessage"]="User not available with id" +id.ToString();
                return RedirectToAction("Index");
            }
            return View(signUps);
        }

        // POST: SignUp/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateSignUp(SignUp signup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isUpdated = _signUpDAL.UpdateSignUp(signup);

                    if (isUpdated)
                    {
                        TempData["InfoMessage"] = "Deatils updated successfully";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the data";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View();
                }
            }
        }

        // GET: SignUp/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var signUp = _signUpDAL.GetSignUpById(id).FirstOrDefault();
                if (signUp == null)
                {
                    TempData["InfoMessage"] = "User not available with id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(signUp);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: SignUp/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                bool isDeleted = _signUpDAL.DeleteSignUp(id);
                //string result = _signUpDAL.DeleteSignUp(id);
                if (isDeleted)
                {
                    TempData["InfoMessage"] = "User Deleted successfully";

                }
                else
                {
                    TempData["ErrorMessage"] = "Unable to delete the data";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }
    }
}
