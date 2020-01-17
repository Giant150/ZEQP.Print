using System;
using System.Collections.Generic;
using System.Text;
using ZEQP.Print.Entities;
using ZEQP.Print.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ZEQP.Print.Business
{
    public class TemplateService : ITemplateService
    {
        public PrintContext DBContext { get; set; }
        public TemplateService(PrintContext context)
        {
            this.DBContext = context;
        }
        public async Task<PageResult<Template>> GetPage(PageQuery<TemplateQueryModel> model)
        {
            var queryable = this.DBContext.Templates.AsNoTracking();
            if (!String.IsNullOrEmpty(model.Match))
                queryable = queryable.Where(w => w.Name.Contains(model.Match) || w.Code.Contains(model.Match));
            if (model.Query.Status.HasValue)
                queryable = queryable.Where(w => w.Status == model.Query.Status.Value);
            var result = new PageResult<Template>();
            result.Count = await queryable.CountAsync();
            queryable = queryable.OrderByDescending(o => o.ModifyTime);
            var skip = (model.Page - 1) * model.Size;
            queryable = queryable.Skip(skip).Take(model.Size);
            result.Page = model.Page;
            result.Data = await queryable.ToListAsync();
            return result;
        }
        public async Task<ComResult<Template>> Modify(Template model)
        {
            var result = new ComResult<Template>();
            if (model.Id == default(int))
            {
                model.CreateTime = model.ModifyTime = DateTime.Now;
                model.PrintCount = 0;
                var entity = await this.DBContext.Templates.AddAsync(model);
                result.Data = entity.Entity;
            }
            else
            {
                model.ModifyTime = DateTime.Now;
                var entry = this.DBContext.Templates.Attach(model);
                entry.State = EntityState.Modified;
                result.Data = entry.Entity;
            }
            var refCount= await this.DBContext.SaveChangesAsync();
            result.Success = refCount > 0;
            result.Msg = "操作成功";
            result.Code = "0";
            return result;
        }
    }
}
