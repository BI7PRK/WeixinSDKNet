using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{

    [Serializable]
    /// <summary>
    /// 公众号授权消息实体
    /// </summary>
    public class AuthorizedMessage
    {
        /// <summary>
        /// 平台AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 授权的公众号Appid
        /// </summary>
        public string AuthorizerAppid { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        public string AuthorizationCode { get; set; }


        /// <summary>
        /// 授权码过期时间
        /// </summary>
        public int AuthorizationCodeExpiredTime { get; set; }

        /// <summary>
        /// 授权时间
        /// </summary>
        public int CreateTime { get; set; }
    }
}
