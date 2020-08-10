using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net
{
    internal sealed class BaseConfig
    {
        /// <summary>
        /// 第三方平台接口
        /// </summary>
        public const string Component_API_URL = "https://api.weixin.qq.com/cgi-bin/component/";

        /// <summary>
        /// 微信网页授权请求地址（非第三方平台代公众号）
        /// </summary>
        public const string OAuthUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect";

        /// <summary>
        /// 第三方使用网站应用授权请求地址
        /// </summary>
        public const string OAuthUrlWithComponent = "https://open.weixin.qq.com/connect/qrconnect?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect";


        /// <summary>
        /// 开放平台接口地址
        /// </summary>
        public const string OPEN_API_URL = "https://open.weixin.qq.com/";
        /// <summary>
        /// 微信接口地址
        /// </summary>
        public const string WECHAT_API_URL = "https://api.weixin.qq.com/";
        /// <summary>
        /// 微信接口CGI地址
        /// </summary>
        public const string WECHAT_CGI_API_URL = "https://api.weixin.qq.com/cgi-bin/";
        /// <summary>
        /// 高级接口的视频发送（另类）
        /// </summary>
        public const string WECHAT_FILE_URL = "http://file.api.weixin.qq.com/cgi-bin/";
        /// <summary>
        /// 微信接口，通过ticket换取二维码服务地址
        /// </summary>
        public const string WECHAT_TICKET_URL = "https://mp.weixin.qq.com/cgi-bin/";
        /// <summary>
        /// 微信小店接口
        /// </summary>
        public const string WECHAT_MERCHANT_URL = " https://api.weixin.qq.com/merchant/";
    }
}
