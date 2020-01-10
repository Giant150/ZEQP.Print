using System;
using System.Collections.Generic;
using System.Text;

namespace ZEQP.Print.Models
{
    public class PrintConfigModel
    {
        public string PrintName { get; set; }
        public string Template { get; set; }
        public PrintActionType Action { get; set; }
        public bool IsWait { get; set; }
    }
}
