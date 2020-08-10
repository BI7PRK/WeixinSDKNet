using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using WeixinSDK.Net;
using WeixinSDK.Net.Interface;
using WeixinSDK.Net.Enums;

namespace WeixinSDK.Net.Official
{
    /// <summary>
    /// AccessToken响应
    /// </summary>
    public class AccessTokenResponse : ResponseBase
    {
        /// <summary>
        /// 用于授权的Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 有效时长
        /// </summary>
        public int Expires { get; set; }
        /// <summary>
        /// 用于刷新的 Token
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 网站授权作用域
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// 用户统一标识
        /// </summary>
        public string UnionID { get; set; }
    }

    /// <summary>
    /// AccessToken 请求
    /// </summary>
    internal class AccessTokenRequest : RequestBase<AccessTokenResponse>
    {
        private class map
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
            public string openid { get; set; }
            public string scope { get; set; }
            public string unionid { get; set; }
        }

        public override AccessTokenResponse Get(Dictionary<string, object> param)
        {
            var obj = base.Get(param);
            if (!obj.IsError)
            {
                var result = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (result != null)
                {
                    obj.AccessToken = result.access_token;
                    obj.Expires = result.expires_in;
                    obj.OpenId = result.openid;
                    obj.RefreshToken = result.refresh_token;
                    obj.Scope = result.scope;
                    obj.UnionID = result.unionid;
                }
            }
            return obj;
        }
    }


    /// <summary>
    /// 公众号的授权方式
    /// </summary>
    public sealed class TokenAPI
    {

        /// <summary>
        /// （第一步：用户同意授权，获取code）生成一个认证网址
        /// </summary>
        /// <param name="appId">公众号 AppId</param>
        /// <param name="redirect_uri">回调地址</param>
        /// <param name="state">自定义参数: 重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字节</param>
        /// <param name="scpoe">授权模式</param>
        /// <returns></returns>
        public static string GetAuthCode(string appId, string redirect_uri, object state, ScopeType scpoe = ScopeType.Snsapi_Base)
        {
            return string.Format(BaseConfig.OAuthUrl, new object[]
            {
                appId,
                HttpUtility.UrlEncode(redirect_uri),
                scpoe.ToString().ToLower(),
                state
            });
        }

        /// <summary>
        /// 第二步：通过code换取网页授权access_token
        /// </summary>
        /// <param name="config">公众号配置(AppId、AppSecret)</param>
        /// <param name="code">填写第一步获取的code参数 </param>
        /// <returns>返令牌信息</returns>
        public static AccessTokenResponse GetAuthToken(IAppConfig config, string code)
        {
            var obj = new AccessTokenRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "sns/oauth2/access_token";
            var dict = new Dictionary<string, object>();
            dict.Add("appid", config.AppId);
            dict.Add("secret", config.AppSecret);
            dict.Add("code", code);
            dict.Add("grant_type", "authorization_code");
            return obj.Get(dict);
        }

        /// <summary>
        /// 第三步：刷新access_token（如果需要）
        /// 由于access_token拥有较短的有效期，当access_token超时后，可以使用refresh_token进行刷新
        /// refresh_token拥有较长的有效期（7天、30天、60天、90天），当refresh_token失效的后，需要用户重新授权。 
        /// </summary>
        /// <param name="appId">公众号 AppId</param>
        /// <param name="refresh_token">填写通过access_token获取到的refresh_token参数</param>
        /// <returns></returns>
        public static AccessTokenResponse RefreshOAuthToke(string appId, string refresh_token)
        {
            var obj = new AccessTokenRequest();
            obj.ServiceUrl = BaseConfig.OPEN_API_URL;
            obj.MethodName = "sns/oauth2/refresh_token";
            var dict = new Dictionary<string, object>();
            dict.Add("appid", appId);
            dict.Add("grant_type", "refresh_token");
            dict.Add("refresh_token", refresh_token);
            return obj.Get(dict);
        }



        /// <summary>
        /// 全局唯一票据，公众号调用各接口时都需使用。
        /// 要做好保存机制
        /// </summary>
        /// <param name="config">公众号配置(AppId、AppSecret)</param>
        /// <returns></returns>
        public static AccessTokenResponse OverallAccessToken(IAppConfig config)
        {
            var obj = new AccessTokenRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "token";
            var dict = new Dictionary<string, object>();
            dict.Add("grant_type", "client_credential");
            dict.Add("appid", config.AppId);
            dict.Add("secret", config.AppSecret);
            return obj.Get(dict);
        }
    }
}
