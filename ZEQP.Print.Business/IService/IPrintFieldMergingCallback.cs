using Aspose.Words.MailMerging;
using System;
using System.Collections.Generic;
using System.Text;
using ZEQP.Print.Models;

namespace ZEQP.Print.Business
{
    public interface IPrintFieldMergingCallback: IFieldMergingCallback
    {
        void SetPrintModel(PrintModel model);
    }
}
