using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ZEQP.Print.Models
{
    public class PrintModel
    {
        /// <summary>
        /// 打印机名称
        /// </summary>
        public string PrintName { get; set; }

        //打印份数
        public int Copies { get; set; }

        /// <summary>
        /// 是否等待打印完成
        /// </summary>
        public bool IsWait { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// 执行动作
        /// </summary>
        public PrintActionType Action { get; set; }

        /// <summary>
        /// 文本字段数据源
        /// </summary>
        public Dictionary<string, string> FieldCotent { get; set; }

        /// <summary>
        /// 图片数据源
        /// </summary>
        public Dictionary<string, ImageContentModel> ImageContent { get; set; }

        /// <summary>
        /// 表格数据源
        /// </summary>
        public Dictionary<string, DataTable> TableContent { get; set; }
        public PrintModel()
        {
            this.Copies = 1;
            this.FieldCotent = new Dictionary<string, string>();
            this.ImageContent = new Dictionary<string, ImageContentModel>();
            this.TableContent = new Dictionary<string, DataTable>();
        }
    }
    public class ImageContentModel
    {
        public ImageType Type { get; set; }
        public string Value { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
    public enum ImageType
    {
        /// <summary>
        /// 本地图片
        /// </summary>
        Local,
        /// <summary>
        /// 网络图片
        /// </summary>
        Network,
        /// <summary>
        /// 条形码
        /// </summary>
        BarCode,
        /// <summary>
        /// 二维码
        /// </summary>
        QRCode
    }
    public enum PrintActionType
    {
        /// <summary>
        /// 打印
        /// </summary>
        Print = 0,
        /// <summary>
        /// 输出成文件
        /// </summary>
        File = 1,
        /// <summary>
        /// 打印并输出成文件
        /// </summary>
        PrintAndFile = 2
    }
}
