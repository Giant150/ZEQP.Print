using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZEQP.Print.Entities;
using ZEQP.Print.Models;

namespace ZEQP.Print.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public PrintContext DbContext { get; set; }
        public HomeController(ILogger<HomeController> logger, PrintContext dbContext)
        {
            _logger = logger;
            this.DbContext = dbContext;
        }

        public IActionResult Index()
        {
            var list = this.DbContext.Templates.ToList();
            return View();
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
}
