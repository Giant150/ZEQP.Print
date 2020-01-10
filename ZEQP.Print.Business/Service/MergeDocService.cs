using Aspose.Words.MailMerging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZEQP.Print.Models;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.SkiaSharp;

namespace ZEQP.Print.Business
{
    public class MergeDocService : IMergeDocService
    {
        public Stream Merge(PrintModel model)
        {
            throw new NotImplementedException();
            //ActivatorUtilities.CreateInstance<IFieldMergingCallback>()
        }
    }
    public class PrintFieldMergingCallback : IFieldMergingCallback
    {
        public PrintModel Model { get; set; }
        public PrintFieldMergingCallback(PrintModel model)
        {
            this.Model = model;
        }
        public void FieldMerging(FieldMergingArgs args)
        {
        }

        public void ImageFieldMerging(ImageFieldMergingArgs field)
        {
            var fieldName = field.FieldName;
            if (!this.Model.ImageContent.ContainsKey(fieldName)) return;
            var imageModel = this.Model.ImageContent[fieldName];
            switch (imageModel.Type)
            {
                case ImageType.BarCode:
                    {
                        var barImage = this.GenerateImage(BarcodeFormat.CODE_128, imageModel.Value, imageModel.Width, imageModel.Height);
                        field.Image = barImage;
                    }; break;
                case ImageType.QRCode:
                    {
                        var qrImage = this.GenerateImage(BarcodeFormat.QR_CODE, imageModel.Value, imageModel.Width, imageModel.Height);
                        field.Image = qrImage;
                    }; break;
                default: break;
            }
        }
        private SkiaSharp.SKBitmap GenerateImage(BarcodeFormat format, string code, int width, int height)
        {
            var writer = new BarcodeWriter();
            writer.Format = format;
            EncodingOptions options = new EncodingOptions()
            {
                Width = width,
                Height = height,
                Margin = 2,
                PureBarcode = false
            };
            writer.Options = options;
            if (format == BarcodeFormat.QR_CODE)
            {
                var qrOption = new QrCodeEncodingOptions()
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = width,
                    Height = height,
                    Margin = 2
                };
                writer.Options = qrOption;
            }
            var codeimg = writer.Write(code);
            return codeimg;
        }
        
    }
}
