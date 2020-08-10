using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using WeixinSDK.Net.Enums;
using WeixinSDK.Net.Interface;

namespace WeixinSDK.Net.Official
{

    
    public class ComponentAccessToken : ResponseBase
    {
        /// <summary>
        /// 网站授权Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 有效时长
        /// </summary>
        public int Expires { get; set; }

    }


    /// <summary>
    /// AccessToken 请求
    /// </summary>
    internal class ComponentAccessTokenRequest : RequestBase<ComponentAccessToken>
    {
        private class map
        {
            public string component_access_token { get; set; }
            public int expires_in { get; set; }

        }

        public override ComponentAccessToken Post(object param)
        {
            try
            {
                var obj = base.Post((param as Dictionary<string, object>).ToString());
                if (!obj.IsError)
                {
                    var result = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                    if (result != null)
                    {
                        obj.AccessToken = result.component_access_token;
                        obj.Expires = result.expires_in;

                    }
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }
    }

/// <summary>
/// 第三方平台Token
/// </summary>
    public partial class ComponentAppinter
    {
        /// <summary>
        /// 每个令牌是存在有效期（2小时）的，且令牌的调用不是无限制的，请第三方平台做好令牌的管理，在令牌快过期时（比如1小时50分）再进行刷新。
        /// </summary>
        /// <param name="config">接口配置</param>
        /// <param name="ticket">微信服务器定时推送的 verify_ticket</param>
        /// <returns>返回令牌信息</returns>
        public static ComponentAccessToken GetAccessToken(IAppConfig config, string ticket)
        {
            var obj = new ComponentAccessTokenRequest();
            obj.ServiceUrl = BaseConfig.Component_API_URL;
            obj.MethodName = "api_component_token";
            var dict = new Dictionary<string, object>();
            dict.Add("component_appid", config.AppId);
            dict.Add("component_appsecret", config.AppSecret);
            dict.Add("component_verify_ticket", ticket);
            return obj.Post(dict);
        }


        /// <summary>
        /// 生成 【根据 pre_auth_code 获取 auth_code】 的链接。
        /// </summary>
        /// <param name="componentAppid"></param>
        /// <param name="preAuthcode"></param>
        /// <param name="redirectUri"></param>
        public static string GetAuthorizationCode(string componentAppid, string preAuthcode, string redirectUri)
        {
            var url = string.Format("https://mp.weixin.qq.com/cgi-bin/componentloginpage?component_appid={0}&pre_auth_code={1}&redirect_uri={2}",
                componentAppid,
                preAuthcode,
                HttpUtility.UrlEncode(redirectUri));

            return url;
        }


        /// <summary>
        /// 网页扫码登陆地址
        /// </summary>
        /// <param name="appId">公众号 AppId</param>
        /// <param name="redirect_uri">回调地址</param>
        /// <param name="state">自定义参数: 重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字节</param>
        /// <param name="scpoe">授权模式</param>
        /// <returns></returns>
        public static string GetAuthCode(string appId, string redirect_uri, object state, ScopeType scpoe = ScopeType.Snsapi_Base)
        {
            return string.Format(BaseConfig.OAuthUrlWithComponent, new object[]
            {
                appId,
                HttpUtility.UrlEncode(redirect_uri),
                scpoe.ToString().ToLower(),
                state
            });
        }

    }
}
