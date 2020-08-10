using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable]
    /// <summary>
    /// 公众号取消授权消息实体
    /// </summary>
    public class UnAuthorizedMessage
    {
        /// <summary>
        /// 平台AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 取消授权的公众号Appid
        /// </summary>
        public string AuthorizerAppid { get; set; }
    }
}
