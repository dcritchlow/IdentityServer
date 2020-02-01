using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers
{
    public class LogOutController : Controller
    {
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}