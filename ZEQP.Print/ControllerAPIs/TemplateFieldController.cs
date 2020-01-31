using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ZEQP.Print.Business;
using ZEQP.Print.Entities;
using ZEQP.Print.Models;

namespace ZEQP.Print.ControllerAPIs {
    [Route ("api/[controller]")]
    [ApiController]
    public class TemplateFieldController : ControllerBase {
        public ITemplateFieldService FieldSvc { get; set; }
        public IHostEnvironment HostEnv { get; set; }
        public TemplateFieldController (ITemplateFieldService fieldSvc, IHostEnvironment hostEnv) {
            this.FieldSvc = fieldSvc;
            this.HostEnv = hostEnv;
        }

        [HttpGet ("[action]/{id}")]
        public Task<TemplateField> Get (int id) {
            return this.FieldSvc.Get (id);
        }

        [HttpGet ("[action]/{templateId}")]
        public Task<ILookup<string, TemplateField>> GetFields (int templateId) {
            return this.FieldSvc.GetFields (templateId);
        }

        [HttpPost ("[action]")]
        public Task<ComResult<TemplateField>> Modify (TemplateField model) {
            return this.FieldSvc.Modify (model);
        }
    }
}