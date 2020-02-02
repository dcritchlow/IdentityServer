using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc.Models;
using Newtonsoft.Json.Linq;

namespace Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index() => View();

        public IActionResult Logout() => SignOut("Cookies", "oidc");

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CallApiAsUser()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = _httpClientFactory.CreateClient("user_client");
            var content = await client.GetStringAsync("identity");

            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }

        // [AllowAnonymous]
        // public async Task<IActionResult> CallApiAsClient()
        // {
        //     var client = _httpClientFactory.CreateClient("client");

        //     var response = await client.GetStringAsync("identity");
        //     ViewBag.Json = JArray.Parse(response).ToString();

        //     return View("json");
        // }
    }
}
