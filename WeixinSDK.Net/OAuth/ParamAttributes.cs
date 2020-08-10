using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinSDK.Net.OAuth2
{
    /// <summary>
    /// 定义Uri传递值的参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ParamNameAttribute : Attribute
    {
        /// <summary>
        /// 请求参数名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 请求参数默认值
        /// </summary>
        public object DefaultValue { get; private set; }
        /// <summary>
        /// 定义传递值的参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">默认值</param>
        public ParamNameAttribute(string name, object value = null)
        {
            this.Name = name;
            this.DefaultValue = value;
        }
    }
}
