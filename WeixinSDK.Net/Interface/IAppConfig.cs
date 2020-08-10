using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinSDK.Net.Enums;

namespace WeixinSDK.Net.Interface
{
    /// <summary>
    /// 公众号API配置接口
    /// </summary>
    public interface IAppConfig
    {
        /// <summary>
        /// AppId 当授权给第三方平台后，返回的可能是第三方平台的ID
        /// </summary>
        string AppId { get; set; }
        /// <summary>
        /// AppSecret
        /// </summary>
        string AppSecret { get; set; }
        /// <summary>
        /// 接口对接令牌
        /// </summary>
        string ApiToken { get; set; }
        /// <summary>
        /// 解密Key
        /// </summary>
        string EncryptKey { get; set; }
        /// <summary>
        /// 加密模式
        /// </summary>
        EncryptType EncryptType { get; set; }
        /// <summary>
        /// 是否已经授权给第三方平台
        /// </summary>
        bool IsAuthorized { set; get; }
        
        /// <summary>
        /// 接口调用令牌
        /// </summary>
        string AccessToken { get; set; }

    }
}
