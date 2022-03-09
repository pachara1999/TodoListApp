using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using todolist.Models;
using TodoListApp.Models;

namespace todolist.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        List<Todolist> lists = _db.todolist.ToList();
        return View(lists);
    }

    [HttpPost]
    public IActionResult AddList(Todolist todolist)
    {
        try
        {
            _db.todolist.Add(todolist);
            _db.SaveChanges();
            return Redirect("~/Home/Index");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Redirect("~/Home/Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
