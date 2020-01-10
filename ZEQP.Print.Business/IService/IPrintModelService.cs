using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZEQP.Print.Models;

namespace ZEQP.Print.Business
{
    public interface IPrintModelService
    {
        Task<PrintModel> GetPrintModel();
    }
}
