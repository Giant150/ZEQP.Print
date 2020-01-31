using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZEQP.Print.Entities;
using ZEQP.Print.Models;

namespace ZEQP.Print.Business {
    public class TemplateFieldService : ITemplateFieldService {
        public PrintContext DBContext { get; set; }
        public TemplateFieldService (PrintContext context) {
            this.DBContext = context;
        }
        public Task<TemplateField> Get (int Id) {
            return this.DBContext.TemplateFields.SingleOrDefaultAsync (w => w.Id == Id);
        }

        public async Task<ILookup<string, TemplateField>> GetFields (int tempId) {
            var list = await this.DBContext.TemplateFields.AsNoTracking ()
                .Where (w => w.TemplateId == tempId)
                .ToListAsync ();
            return list.ToLookup (keySelector => keySelector.TableName);
        }

        public async Task<ComResult<TemplateField>> Modify (TemplateField model) {
            var result = new ComResult<TemplateField> ();
            if (model.Id == default (int)) {
                model.CreateTime = model.ModifyTime = DateTime.Now;
                var entity = await this.DBContext.TemplateFields.AddAsync (model);
                result.Data = entity.Entity;
            } else {
                var entry = this.DBContext.TemplateFields.Attach (model);
                entry.Property (w => w.ModifyTime).CurrentValue = DateTime.Now;
                entry.State = EntityState.Modified;
                result.Data = entry.Entity;
            }
            var refCount = await this.DBContext.SaveChangesAsync ();
            result.Success = refCount > 0;
            result.Msg = "操作成功";
            result.Code = "0";
            return result;
        }
    }
}