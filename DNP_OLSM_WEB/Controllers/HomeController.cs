using DNP_OLSM_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DNP_OLSM_DL;
using DNP_OLSM_DS;
using DNP_OLSM_ENTITY;
using DNP_OLSM_HELPER;
using Microsoft.AspNetCore.Authorization;

namespace DNP_OLSM_WEB.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
