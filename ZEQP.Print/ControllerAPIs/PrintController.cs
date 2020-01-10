using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZEQP.Print.Business;
using ZEQP.Print.Models;

namespace ZEQP.Print.ControllerAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : ControllerBase
    {
        public IPrintModelService ModelSvc { get; set; }
        public IPrintService PrintSvc { get; set; }
        public PrintController(IPrintModelService modelSvc, IPrintService printSvc)
            : base()
        {
            this.PrintSvc = printSvc;
            this.ModelSvc = modelSvc;
        }
        [HttpPost]
        public async Task<ActionResult<ComResult>> Post()
        {
            var model = await this.ModelSvc.GetPrintModel();
            var result = this.PrintSvc.Print(model);
            return result;
        }
        [HttpGet]
        public async Task<ActionResult<ComResult>> Get()
        {
            var model = await this.ModelSvc.GetPrintModel();
            var result = this.PrintSvc.Print(model);
            return result;
        }
    }
}