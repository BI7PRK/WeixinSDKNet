using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable]
    internal class ResponseHandler
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// 验证时的回应
        /// </summary>
        public string EchoStr { get; set; }

        /// <summary>
        /// 消息签名
        /// </summary>
        public string Msg_signature { get; set; }

        
        /// <summary>
        /// 加密类型
        /// </summary>
        public string Encrypt_type { get; set; }
    }
}
