using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZEQP.Print.Entities
{
    public class Template
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 模板版本
        /// </summary>
        [MaxLength(50)]
        public string Verstion { get; set; }

        /// <summary>
        /// 模板路径
        /// </summary>
        [MaxLength(1000)]
        public string Path { get; set; }

        /// <summary>
        /// 保存打印文件
        /// </summary>
        public bool SaveToFile { get; set; }
        /// <summary>
        /// 打印次数
        /// </summary>
        public int PrintCount { get; set; }

        public TemplateStatus Status { get; set; }

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
    /// 模板状态
    /// </summary>
    public enum TemplateStatus
    {
        /// <summary>
        /// 启用
        /// </summary>
        Enable = 1,
        /// <summary>
        /// 停用
        /// </summary>
        Disable = 2
    }
}
