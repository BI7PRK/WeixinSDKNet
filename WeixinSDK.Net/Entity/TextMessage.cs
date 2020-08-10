using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable]
    public class TextMessage :MessageBase
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
    }
}
