using DNP_OLSM_ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DNP_OLSM_WEB.Controllers
{

    public class Common : Controller
    {
        
        public enum eUserType
        {
            User = 1,
            Admin = 2
        }

      


    }
}

