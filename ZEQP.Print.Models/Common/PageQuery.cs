using System;
using System.Collections.Generic;
using System.Text;

namespace ZEQP.Print.Models
{
    /// <summary>
    /// 分页相询条件实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageQuery<T>
        where T : class, new()
    {
        /// <summary>
        /// 页大小
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 排序方式 AES,DESC
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 模糊相配查询
        /// </summary>
        public string Match { get; set; }
        /// <summary>
        /// 相询实体
        /// </summary>
        public T Query { get; set; }
    }
}
