using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZEQP.Print.Business;
using ZEQP.Print.Entities;
using ZEQP.Print.Models;

namespace ZEQP.Print.ControllerAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        public ITemplateService TempSvc { get; set; }
        public TemplateController(ITemplateService tempSvc)
        {
            this.TempSvc = tempSvc;
        }

        [HttpPost("[action]")]
        public Task<PageResult<Template>> GetPage(PageQuery<TemplateQueryModel> model)
        {
            return this.TempSvc.GetPage(model);
        }

        [HttpPost("[action]")]
        public Task<ComResult<Template>> Modify(Template model)
        {
            return this.TempSvc.Modify(model);
        }
    }
}