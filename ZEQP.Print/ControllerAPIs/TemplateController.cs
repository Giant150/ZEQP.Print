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

namespace ZEQP.Print.ControllerAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        public ITemplateService TempSvc { get; set; }
        public IHostEnvironment HostEnv { get; set; }
        public TemplateController(ITemplateService tempSvc, IHostEnvironment hostEnv)
        {
            this.TempSvc = tempSvc;
            this.HostEnv = hostEnv;
        }

        [HttpGet("[action]/{id}")]
        public Task<Template> Get(int id)
        {
            return this.TempSvc.Get(id);
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

        [HttpPost("[action]")]
        public async Task<ComResult> Upload(IFormFile tempFile)
        {
            if (!tempFile.FileName.EndsWith(".docx", StringComparison.CurrentCultureIgnoreCase))
                return new ComResult() { Code = "1", Data = "文件格式不正确", Success = false, Msg = "只能上传Word文档(docx)" };
            var tempPath = Path.Combine(this.HostEnv.ContentRootPath, "template", DateTime.Now.ToString("yyyyMM"));
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.docx";
            var filePath = Path.Combine(tempPath, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await tempFile.CopyToAsync(stream);
            }
            var fileVPath = filePath.Replace(this.HostEnv.ContentRootPath, "");
            return new ComResult() { Code = "0", Data = fileVPath, Success = true, Msg = "上传成功" };
        }
    }
}