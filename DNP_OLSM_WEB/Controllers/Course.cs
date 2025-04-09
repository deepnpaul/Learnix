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
using System.Data;

namespace DNP_OLSM_WEB.Controllers
{
    public class Course : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageCourses(CourseModel CS)
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

            CourseModel vCS = new CourseModel();
            vCS.Createdby = userId;
            CS.GetCourseInfo = cCourse.CGetCourseInformation(vCS);

            var courseList = CS.GetCourseInfo.AsEnumerable().Select(row => new CourseModel
            {
                CourseID = row["CourseID"] != DBNull.Value ? Convert.ToInt32(row["CourseID"]) : (int?)null,
                CourseName = row["CourseName"] != DBNull.Value ? row["CourseName"].ToString() : null,
                CourseDesc = row["CourseDesc"] != DBNull.Value ? row["CourseDesc"].ToString() : null,                           
                CreatedDate = row["DateEntered"] != DBNull.Value ? Convert.ToDateTime(row["DateEntered"]) : DateTime.MinValue,
                IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(Convert.ToInt32(row["IsActive"])) : false
            }).ToList();

            ViewBag.ManageCourse = courseList;

            return View(CS);
        }

        public IActionResult AddEditCourseInfo(CourseModel CM, string Command, int id)
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

            CourseModel obj = new CourseModel();
            if(Command == "Save")
            {
                obj.CourseName = CM.CourseName;
                obj.CourseDesc = CM.CourseDesc;
                obj.CreatedDate = CM.CreatedDate;
                obj.Createdby = userId;
            }

            return View(CM); 
        }

    }
}
