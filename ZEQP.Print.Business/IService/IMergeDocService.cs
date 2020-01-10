using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZEQP.Print.Models;

namespace ZEQP.Print.Business
{
    public interface IMergeDocService
    {
        /// <summary>
        /// 合并文档
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        MemoryStream Merge(PrintModel model);
    }
}
