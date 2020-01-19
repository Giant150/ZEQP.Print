using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ZEQP.Print.Controllers
{
    public class FieldController : Controller
    {
        public IActionResult Index(int tempId)
        {
            ViewBag.TemplateId = tempId;
            return View();
        }
    }
}