using DNP_OLSM_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DNP_OLSM_DL;
using DNP_OLSM_DS;
using DNP_OLSM_ENTITY;
using DNP_OLSM_HELPER;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace DNP_OLSM_WEB.Controllers
{
    public class LogInController : Controller
    {

        private readonly IConfiguration _configuration;

        public LogInController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string AuthenticateUser(string email, string password)
        {
            User Us = new User();
            Us.Email = email;
            Us.Password = password;
            Us.GetUserData = cUsers.VerifyUser(Us);

            if (Us.GetUserData != null && Us.GetUserData.Tables.Count > 0 && Us.GetUserData.Tables[0].Rows.Count > 0)
            {
                DataRow row = Us.GetUserData.Tables[0].Rows[0];
                var userId = row["UserID"].ToString();
                var userName = row["UserName"].ToString();
                var email2 = row["EmailID"].ToString();
                var role = row["Role"].ToString();
                Int32 roleID = (Int32)row["RoleID"];
               
                return GenerateJwtToken(userId, userName, email2, role, roleID);
            }
            
           return string.Empty;
        }

        public string GenerateJwtToken(string userId, string Name, string email, string role, Int32 roleId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTConfig:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Name, Name),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role), // Adding role claim
            new Claim("roleId", roleId.ToString()) // Adding roleId claim (Custom claim)
        };

            var token = new JwtSecurityToken(
                _configuration["JWTConfig:Issuer"],
                _configuration["JWTConfig:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWTConfig:TokenValidityMins")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
        public IActionResult LogIn(string cf, string cfid, string id, User US, string Command)
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
                    Usr.UserTypeID = Convert.ToInt32(Common.eUserType.User);
                    Usr.UserTypeName = "User";
                    Usr.DOB = null;

                    string vUserID = cUsers.cAddUser(Usr);

                    if (vUserID != null && vUserID != "0")
                    {
                        TempData["Message"] = $"User {vUserID} Registered Successfully";
                        return RedirectToAction("LogIn");
                    }
                    else if(vUserID == "0")
                    {
                        TempData["Message"] = $"Existing User Try With Different Email or LogIn to your Account";
                        return RedirectToAction("LogIn");
                    }

                    

                }
                else if (Command == "LogIn")
                {
                    string token = AuthenticateUser(US.Email, US.Password);
                    if (!string.IsNullOrEmpty(token))
                    {
                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true, // Secure, JavaScript can't access
                            Secure = false,  // Send only over HTTPS
                            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWTConfig:TokenValidityMins"))
                        };

                        Response.Cookies.Append("AuthToken", token, cookieOptions);
                        
                        TempData["Token"] = token;
                       
                        var handler = new JwtSecurityTokenHandler();
                        var jwtSecurityToken = handler.ReadJwtToken(token);
                        var roleIdClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "roleId");

                        if (roleIdClaim != null && roleIdClaim.Value == "1")
                        {
                            return RedirectToAction("Index", "Home");
                        } 
                        else if(roleIdClaim != null && roleIdClaim.Value == "2")
                        {
                            return RedirectToAction("AdminDashBoard", "Admin");
                        }
                            
                    }
                    else
                    {
                        TempData["Message"] = "Invalid Credentials";
                        return RedirectToAction("LogIn");
                    }
                }

            }

            catch (Exception ex)
            { 

            }
           
            return View();
        }
    }
}
