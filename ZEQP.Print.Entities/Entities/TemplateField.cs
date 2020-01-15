using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZEQP.Print.Entities
{
    /// <summary>
    /// 模板字段
    /// </summary>
    public class TemplateField
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 模板
        /// </summary>
        public int TemplateId { get; set; }

        [ForeignKey(nameof(TemplateId))]
        public Template Template { get; set; }

        /// <summary>
        /// 表名称
        /// 为null时是主表字段
        /// </summary>
        [MaxLength(50)]
        public string TableName { get; set; }

        /// <summary>
        /// 字段名名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 字段编码
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public FieldType FieldType { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [MaxLength(500)]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public ImageType? imgType { get; set; }
        /// <summary>
        /// 图片宽度
        /// </summary>
        public int ImgWidth { get; set; }
        /// <summary>
        /// 图片高度
        /// </summary>
        public int ImgHeight { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifyTime { get; set; }
    }
    /// <summary>
    /// 模板字段类型
    /// </summary>
    public enum FieldType
    { 
        /// <summary>
        /// 文本
        /// </summary>
        Text,
        /// <summary>
        /// 图片
        /// </summary>
        Image
    }
    public enum ImageType
    {
        /// <summary>
        /// 条形码
        /// </summary>
        BarCode,
        /// <summary>
        /// 二维码
        /// </summary>
        QRCode
    }
}
