using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Web_AppointmentSystem.MVC.Services.Implementations
{
    public class TokenFilter:IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Cookies["token"];
            if (token == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", new { area = "Admin" });
            }

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                bool isAdmin = jwtToken.Claims
                         .Where(c => c.Type == ClaimTypes.Role)
                         .Any(c => c.Value == "Admin");

                if (!isAdmin) context.Result = new RedirectToActionResult("Login", "Auth", new { area = "Admin" });
            }
        }
    }
}
