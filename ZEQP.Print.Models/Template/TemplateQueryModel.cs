using System;
using System.Collections.Generic;
using System.Text;
using ZEQP.Print.Entities;

namespace ZEQP.Print.Models
{
    public class TemplateQueryModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public TemplateStatus? Status { get; set; }
    }
}
