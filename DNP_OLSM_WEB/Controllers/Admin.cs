using DNP_OLSM_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DNP_OLSM_DL;
using DNP_OLSM_DS;
using DNP_OLSM_ENTITY;
using DNP_OLSM_HELPER;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace DNP_OLSM_WEB.Controllers
{
    public class Admin : Controller
    {
        public IActionResult AdminIndex()
        {
            return View();
        }

        public IActionResult AdminLogInResistration(string cf, string cfid, string id, User US, string Command)
        {
            try
            {
                User Usr = new User();
                if (Command == "Save")
                {

                    Usr.Name = US.Name;
                    Usr.Email = US.Email;
                    Usr.Password = US.Password;
                    Usr.IsActive = true;
                    Usr.DOB = US.DOB;
                    Usr.ZipCode = US.ZipCode;
                    Usr.PhoneNo = US.PhoneNo;
                    Usr.UserTypeID = Convert.ToInt32(Common.eUserType.Admin);
                    Usr.UserTypeName = "Admin";

                    string vUserID = cUsers.cAddUser(Usr);

                    if (vUserID != null && vUserID != "0")
                    {
                        TempData["Message"] = $"User {vUserID} Registered Successfully";
                        return RedirectToAction("AdminLogInResistration");
                    }
                    else if (vUserID == "0")
                    {
                        TempData["Message"] = $"Existing User Try With Different Email or LogIn to your Account";
                        return RedirectToAction("LogIn");
                    }



                }
               

            }

            catch (Exception ex)
            {

            }


            return View();
        }

        
        public IActionResult AdminDashBoard()
        {

            string? userId = string.Empty;
            userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                   ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value; 
            var roleId = User.FindFirst("roleId")?.Value;
            if (!User.Identity.IsAuthenticated && role != "Admin" && roleId != "2")
            {
                TempData["Message"] = "User Isn't Authorized";
                return RedirectToAction("AdminLogInResistration");

            }
           
            return View();
        }

        public IActionResult AdminLogOut()
        {
            
            Response.Cookies.Delete("AuthToken"); 
            return RedirectToAction("AdminLogInResistration");
            

        }
    }
}
