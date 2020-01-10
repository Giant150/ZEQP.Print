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
        public IPrintModelService PrintSvc { get; set; }
        public IMergeDocService MergeSvc { get; set; }
        public PrintController(IPrintModelService printSvc,IMergeDocService mergeSvc)
            : base()
        {
            this.PrintSvc = printSvc;
            this.MergeSvc = mergeSvc;
        }
        [HttpPost("[Action]")]
        public async Task<ActionResult<PrintModel>> Print()
        {
            var model = await this.PrintSvc.GetPrintModel();
            var xpsStream = this.MergeSvc.Merge(model);

            return result;
        }
    }
}