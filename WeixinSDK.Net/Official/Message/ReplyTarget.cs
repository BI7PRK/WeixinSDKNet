using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Official
{
    public class ReplyTarget
    {
        /// <summary>
        /// 发送消息目标对象
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送消息来源对象
        /// </summary>
        public string FromUserName { get; set; }
    }
}
