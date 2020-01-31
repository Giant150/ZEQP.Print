using System;
using System.Linq;
using System.Threading.Tasks;
using ZEQP.Print.Entities;
using ZEQP.Print.Models;

namespace ZEQP.Print.Business
{
    public interface ITemplateFieldService
    {
        Task<TemplateField> Get(int Id);
        Task<ILookup<string,TemplateField>> GetFields(int tempId);
        Task<ComResult<TemplateField>> Modify(TemplateField model);
    }
}