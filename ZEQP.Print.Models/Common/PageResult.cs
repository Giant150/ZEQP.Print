using System;
using System.Collections.Generic;
using System.Text;

namespace ZEQP.Print.Models
{
    /// <summary>
    /// 返回的页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 当前页数据
        /// </summary>
        public List<T> Data { get; set; }
    }
}
