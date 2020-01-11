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
using System.Collections.Concurrent;
using System.Linq;

namespace ZEQP.Print.Business
{
    public class MergeDocService : IMergeDocService, IDisposable
    {
        private IServiceProvider SvcProvider { get; set; }
        private IHostEnvironment HostEnv { get; set; }
        private ConcurrentDictionary<string,FileStream> DicDoc { get; set; }
        public MergeDocService(IServiceProvider svcProvider, IHostEnvironment hostEnv)
        {
            this.DicDoc = new ConcurrentDictionary<string, FileStream>();
            this.SvcProvider = svcProvider;
            this.HostEnv = hostEnv;
        }
        public MemoryStream Merge(PrintModel model)
        {
            FileStream tempStream = this.DicDoc.GetOrAdd(model.Template, (template) =>
            {
                var tempPath = $"{this.HostEnv.ContentRootPath}\\wwwroot\\template\\{template}";
                if (!System.IO.File.Exists(tempPath)) throw new Exception($"{template}模板文件不存在");
                return File.OpenRead(tempPath);
            });
            var doc = new Document(tempStream);
            IPrintFieldMergingCallback callback;
            using (var scope= this.SvcProvider.CreateScope())
            {
                callback = scope.ServiceProvider.GetRequiredService<IPrintFieldMergingCallback>();
            } 
            callback.SetPrintModel(model);

            doc.MailMerge.FieldMergingCallback = callback;
            var fieldNames=model.FieldCotent.Keys.Concat(model.ImageContent.Keys).ToArray();
            var fieldValues=model.FieldCotent.Values.Concat(model.ImageContent.Values.Select(s => s.Value)).ToArray();
            if (model.FieldCotent.Count > 0)
                doc.MailMerge.Execute(fieldNames, fieldValues);
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

        public void Dispose()
        {
            foreach (var item in this.DicDoc)
            {
                item.Value.Close();
                item.Value.Dispose();
            }
        }
    }
}
