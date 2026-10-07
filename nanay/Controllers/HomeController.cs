using Microsoft.AspNetCore.Mvc;
using nanay.Models;
using nanay.Services;

namespace nanay.Controllers;

public class HomeController : Controller
{
    private readonly IMemorialRepository _repo;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IMemorialRepository repo, ILogger<HomeController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [Route("/")]
    [Route("/memorial")]
    [Route("/memorial/{slug}")]
    [HttpGet]
    public async Task<IActionResult> Index(string? slug = null)
    {
        var model = await _repo.GetMemorialViewModelAsync(slug);

        // Check if access password session exists
        if (model.Settings.IsPasswordProtected && !string.IsNullOrEmpty(model.Settings.VisitorPassword))
        {
            var isUnlocked = HttpContext.Session.GetString("MemorialUnlocked") == "true";
            model.IsUnlocked = isUnlocked;
        }
        else
        {
            model.IsUnlocked = true;
        }

        var request = HttpContext.Request;
        model.MemorialUrl = $"{request.Scheme}://{request.Host}/memorial/{model.Settings.MemorialSlug}";

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Unlock(string password)
    {
        var settings = await _repo.GetSettingsAsync();
        if (!string.IsNullOrEmpty(settings.VisitorPassword) && settings.VisitorPassword == password)
        {
            HttpContext.Session.SetString("MemorialUnlocked", "true");
            return Json(new { success = true });
        }

        return Json(new { success = false, message = "Incorrect password. Please try again." });
    }

    [Route("/nfc")]
    [Route("/tap")]
    [HttpGet]
    public async Task<IActionResult> NfcRedirect()
    {
        var settings = await _repo.GetSettingsAsync();
        return Redirect($"/memorial/{settings.MemorialSlug}");
    }
}
