using System;
using System.Collections.Generic;
using System.Text;

namespace ZEQP.Print.Models
{
    public class ComResult<T>
    {
        public string Code { get; set; }
        public bool Success { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }
    public class ComResult : ComResult<string>
    {
    }
}
