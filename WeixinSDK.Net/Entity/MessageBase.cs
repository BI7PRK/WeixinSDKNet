using WeixinSDK.Net.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable]
    public class MessageBase : IMessageBase
    {
        /// <summary>
        /// 发送消息目标对象
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送消息来源对象
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long MsgId { get; set; }
    }
}
