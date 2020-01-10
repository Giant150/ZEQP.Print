using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Words.Fields;
using Aspose.Words.MailMerging;
using ZEQP.Print.Models;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

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
                        var barImage = this.GenerateImage (BarcodeFormat.CODE_128, imageModel.Value, imageModel.Width, imageModel.Height);
                        field.ImageStream=barImage;
                        field.ImageWidth.Value = imageModel.Width;
                        field.ImageHeight.Value = imageModel.Height;
                    };
                    break;
                case ImageType.QRCode:
                    {
                        var qrImage = this.GenerateImage (BarcodeFormat.QR_CODE, imageModel.Value, imageModel.Width, imageModel.Height);
                        field.ImageStream = qrImage;
                        field.ImageWidth.Value = imageModel.Width;
                        field.ImageHeight.Value = imageModel.Height;
                    };
                    break;
                default:
                    break;
            }
        }
        private MemoryStream GenerateImage (BarcodeFormat format, string code, int width, int height) {
            var writer = new BarcodeWriterPixelData ();
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
            var pixelData = writer.Write (code);
            var bitmap = new System.Drawing.Bitmap (pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            var ms = new MemoryStream ();
            var bitmapData = bitmap.LockBits (new System.Drawing.Rectangle (0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            try {
                System.Runtime.InteropServices.Marshal.Copy (pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            } finally {
                bitmap.UnlockBits (bitmapData);
            }
            bitmap.Save (ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms;
        }

    }
}