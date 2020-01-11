using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Words.Fields;
using Aspose.Words.MailMerging;
using SkiaSharp;
using ZEQP.Print.Models;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.SkiaSharp;

namespace ZEQP.Print.Business {
    public class PrintFieldMergingCallback : IPrintFieldMergingCallback {
        public PrintModel Model { get; set; }
        public PrintFieldMergingCallback () { }
        public void SetPrintModel (PrintModel model) {
            this.Model = model;
        }
        public void FieldMerging (FieldMergingArgs args) { }

        public void ImageFieldMerging (ImageFieldMergingArgs field) {
            var fieldName = field.FieldName;
            if (!this.Model.ImageContent.ContainsKey (fieldName)) return;
            var imageModel = this.Model.ImageContent[fieldName];
            switch (imageModel.Type) {
                case ImageType.BarCode:
                    {
                        var barImage = this.GenerateImageBySkiaSharp (BarcodeFormat.CODE_128, imageModel.Value, imageModel.Width, imageModel.Height);
                        field.ImageStream=barImage;
                        field.ImageWidth.Value = imageModel.Width;
                        field.ImageHeight.Value = imageModel.Height;
                    };
                    break;
                case ImageType.QRCode:
                    {
                        var qrImage = this.GenerateImageBySkiaSharp (BarcodeFormat.QR_CODE, imageModel.Value, imageModel.Width, imageModel.Height);
                        field.ImageStream = qrImage;
                        field.ImageWidth.Value = imageModel.Width;
                        field.ImageHeight.Value = imageModel.Height;
                    };
                    break;
                default:
                    break;
            }
        }
        private MemoryStream GenerateImageBySkiaSharp(BarcodeFormat format, string code, int width, int height)
        {
            var writer = new BarcodeWriter();
            writer.Format = format;
            EncodingOptions options = new EncodingOptions () {
                Width = width,
                Height = height,
                Margin = 2,
                PureBarcode = false
            };
            writer.Options = options;
            if (format == BarcodeFormat.QR_CODE) {
                var qrOption = new QrCodeEncodingOptions () {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height,
                Margin = 2
                };
                writer.Options = qrOption;
            }
            var bitmap = writer.Write (code);
            var ms=new MemoryStream();
            using(SKManagedWStream skStream=new SKManagedWStream(ms))
            {
                SKPixmap.Encode(skStream,bitmap,SKEncodedImageFormat.Jpeg,100);
                //bitmap.Encode(skStream,SKEncodedImageFormat.Jpeg,100);
            }
            return ms;
        }
    }
}