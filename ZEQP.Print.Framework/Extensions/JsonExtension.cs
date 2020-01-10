using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ZEQP.Print.Framework
{
    public static class JsonExtension
    {
        public static T ToObject<T>(this string source)
        {
            return JsonConvert.DeserializeObject<T>(source);
        }
        public static string ToJson(this object model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
