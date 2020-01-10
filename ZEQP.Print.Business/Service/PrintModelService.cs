using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using ZEQP.Print.Models;
using System.Linq;
using ZEQP.Print.Framework;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace ZEQP.Print.Business
{
    public class PrintModelService : IPrintModelService
    {
        private IHttpContextAccessor ContextAccessor { get; set; }
        private IConfiguration Config { get; set; }
        public PrintModelService(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            this.ContextAccessor = contextAccessor;
            this.Config = configuration;
        }
        public async Task<PrintModel> GetPrintModel()
        {
            var result = new PrintModel();
            var printConfig = this.Config.GetSection("PrintDefault").Get<PrintConfigModel>();
            result.PrintName = printConfig.PrintName;
            result.Template = printConfig.Template;
            result.Action = printConfig.Action;
            result.IsWait = printConfig.IsWait;

            var httpContext = this.ContextAccessor.HttpContext;
            var query = httpContext.Request.Query;
            if (query.ContainsKey(nameof(result.PrintName))) result.PrintName = query[nameof(result.PrintName)];
            if (query.ContainsKey(nameof(result.Copies))) result.Copies = int.Parse(query[nameof(result.Copies)]);
            if (query.ContainsKey(nameof(result.Template))) result.Template = query[nameof(result.Template)];
            if (query.ContainsKey(nameof(result.Action))) result.Action = (PrintActionType)Enum.Parse(typeof(PrintActionType), query[nameof(result.Action)]);
            if (query.ContainsKey(nameof(result.IsWait))) result.IsWait = new string[] { "True", "1", "Y", "ON", "true", "1", "y", "on" }.Contains(query[nameof(result.IsWait)].ToString());
            foreach (var item in query)
            {
                if (item.Key.StartsWith("Image:"))
                {
                    var keyName = item.Key.Replace("Image:", "");
                    if (result.ImageContent.ContainsKey(keyName)) continue;
                    var imageModel = item.Value.ToString().ToObject<ImageContentModel>();
                    result.ImageContent.Add(keyName, imageModel);
                    continue;
                }
                if (item.Key.StartsWith("Table:"))
                {
                    var keyName = item.Key.Replace("Table:", "");
                    if (result.TableContent.ContainsKey(keyName)) continue;
                    var table = item.Value.ToString().ToObject<DataTable>();
                    table.TableName = keyName;
                    result.TableContent.Add(keyName, table);
                    continue;
                }
                if (result.FieldCotent.ContainsKey(item.Key)) continue;
                result.FieldCotent.Add(item.Key, item.Value);
            }
            if (httpContext.Request.Method.Equals("POST", StringComparison.CurrentCultureIgnoreCase))
            {
                var body = httpContext.Request.Body;
                var encoding = Encoding.UTF8;
                var reader = new StreamReader(body, encoding);
                var bodyContent = await reader.ReadToEndAsync();

                var bodyModel = bodyContent.ToObject<Dictionary<string, object>>();
                foreach (var item in bodyModel)
                {
                    if (item.Key.StartsWith("Image:"))
                    {
                        var imageModel = item.Value.ToJson().ToObject<ImageContentModel>();
                        var keyName = item.Key.Replace("Image:", "");
                        if (result.ImageContent.ContainsKey(keyName))
                            result.ImageContent[keyName] = imageModel;
                        else
                            result.ImageContent.Add(keyName, imageModel);
                        continue;
                    }
                    if (item.Key.StartsWith("Table:"))
                    {
                        var table = item.Value.ToJson().ToObject<DataTable>();
                        var keyName = item.Key.Replace("Table:", "");
                        table.TableName = keyName;
                        if (result.TableContent.ContainsKey(keyName))
                            result.TableContent[keyName] = table;
                        else
                            result.TableContent.Add(keyName, table);
                        continue;
                    }
                    if (result.FieldCotent.ContainsKey(item.Key))
                        result.FieldCotent[item.Key] = item.Value.ToString();
                    else
                        result.FieldCotent.Add(item.Key, item.Value.ToString());
                }
            }
            return result;
        }
    }
}
