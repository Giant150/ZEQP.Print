using System;
using System.Collections.Generic;
using System.Text;
using ZEQP.Print.Models;

namespace ZEQP.Print.Business
{
    public interface IPrintService
    {
        ComResult Print(PrintModel model);
    }
}
