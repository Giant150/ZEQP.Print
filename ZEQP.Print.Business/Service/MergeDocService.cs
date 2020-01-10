using Aspose.Words;
using Aspose.Words.Fields;
using Aspose.Words.MailMerging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZEQP.Print.Models;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.SkiaSharp;
using System.Collections.Concurrent;
using System.Linq;

namespace ZEQP.Print.Business
{
    public class MergeDocService : IMergeDocService
    {
        private IServiceProvider SvcProvider { get; set; }
        private IHostEnvironment HostEnv { get; set; }
        private ConcurrentDictionary<string,Document> DicDoc { get; set; }
        public MergeDocService(IServiceProvider svcProvider, IHostEnvironment hostEnv)
        {
            this.SvcProvider = svcProvider;
            this.HostEnv = hostEnv;
        }
        public MemoryStream Merge(PrintModel model)
        {
            Document doc = this.DicDoc.GetOrAdd(model.Template, (template) =>
            {
                var tempPath = $"{this.HostEnv}\\wwwroot\\template\\{template}";
                if (!System.IO.File.Exists(tempPath)) throw new Exception($"{template}模板文件不存在");
                return new Document(tempPath);
            });
            
            var callback = this.SvcProvider.GetRequiredService<IPrintFieldMergingCallback>();
            callback.SetPrintModel(model);

            doc.MailMerge.FieldMergingCallback = callback;
            if (model.FieldCotent.Count > 0)
                doc.MailMerge.Execute(model.FieldCotent.Keys.ToArray(), model.FieldCotent.Values.ToArray());
            if (model.ImageContent.Count > 0)
            {
                doc.MailMerge.Execute(model.ImageContent.Keys.ToArray(), model.ImageContent.Values.Select(s => s.Value).ToArray());
            };
            if (model.TableContent.Count > 0)
            {
                foreach (var item in model.TableContent)
                {
                    var table = item.Value;
                    table.TableName = item.Key;
                    doc.MailMerge.ExecuteWithRegions(table);
                }
            }
            doc.UpdateFields();
            
            var ms = new MemoryStream();
            doc.Save(ms, SaveFormat.Xps);
            return ms;
        }
    }
}
