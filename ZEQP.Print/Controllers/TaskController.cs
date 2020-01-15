using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ZEQP.Print.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}