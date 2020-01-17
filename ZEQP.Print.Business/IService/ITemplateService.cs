using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZEQP.Print.Entities;
using ZEQP.Print.Models;

namespace ZEQP.Print.Business
{
    public interface ITemplateService
    {
        Task<Template> Get(int id);
        Task<PageResult<Template>> GetPage(PageQuery<TemplateQueryModel> model);
        Task<ComResult<Template>> Modify(Template model);
    }
}
