using Microsoft.AspNetCore.Mvc;

namespace HiddenVilla_Web_Api.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}