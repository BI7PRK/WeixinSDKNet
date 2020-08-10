using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WeixinSDK.Net.Extensions;
using WeixinSDK.Net.Interface;

namespace WeixinSDK.Net.Official
{
    #region 使用授权码换取公众号的接口调用凭据和授权信息

    public class AuthorizerTokenResponse : ResponseBase
    {
        /// <summary>
        /// 授权方appid
        /// </summary>
        public string Appid { set; get; }
        /// <summary>
        /// 授权方接口调用凭据（在授权的公众号具备API权限时，才有此返回值），也简称为令牌
        /// </summary>
        public string AccessToken { set; get; }
        /// <summary>
        /// 有效期（在授权的公众号具备API权限时，才有此返回值）
        /// </summary>
        public int Expires { set; get; }
        /// <summary>
        /// 接口调用凭据刷新令牌
        /// </summary>
        public string RefreshToken { set; get; }
        /// <summary>
        /// 公众号授权给开发者的权限集列表。ID为1到15时分别代表
        /// </summary>
        public AuthorizerInfo[] FuncInfo { set; get; }
    }
    public class AuthorizerInfo
    {
        public int ID { get; set; }
        //public string Category { get; set; }
    }
    internal class QueryAuthRequest : RequestBase<AuthorizerTokenResponse>
    {
        private class root
        {
            public map authorization_info { get; set; }

        }
        private class map
        {
            public string authorizer_appid { get; set; }
            public string authorizer_access_token { get; set; }
            public int expires_in { get; set; }
            public string authorizer_refresh_token { get; set; }
            public func[] func_info { get; set; }

        }

        internal class func
        {
            public idval funcscope_category { get; set; }
        }

        internal class idval
        {
            public int id { get; set; }
          
        }


        public override AuthorizerTokenResponse Post(object param)
        {
            try
            {
                var obj = base.Post(param);
                if (!obj.IsError)
                {
                    var result = JsonConvert.DeserializeObject<root>(obj.ResonseBody);
                    if (result != null)
                    {
                        obj.AccessToken = result.authorization_info.authorizer_access_token;
                        obj.Appid = result.authorization_info.authorizer_appid;
                        obj.RefreshToken = result.authorization_info.authorizer_refresh_token;
                        obj.Expires = result.authorization_info.expires_in;
                        if (result.authorization_info.func_info != null)
                        {
                            obj.FuncInfo = result.authorization_info.func_info
                                .Select(s=> new AuthorizerInfo { ID = s.funcscope_category.id })
                                .ToArray();
                        }

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

    #endregion

    #region 获取（刷新）授权公众号的接口调用凭据（令牌）

    public class AuthorizerRefreshResponse : ResponseBase
    {
        /// <summary>
        /// 刷新用的令牌
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public int Expires { get; set; }
        /// <summary>
        /// 授权方令牌
        /// </summary>
        public string AuthorizerToken { get; set; }
    }

    internal class AuthorizerRefreshRequest : RequestBase<AuthorizerRefreshResponse>
    {
        private class map
        {
            public string authorizer_access_token { get; set; }
            public int expires_in { get; set; }
            public string authorizer_refresh_token { get; set; }

        }

        public override AuthorizerRefreshResponse Post(object param)
        {
            var obj = base.Post(param);
            if (!obj.IsError)
            {
                var result = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (result != null)
                {
                    obj.AuthorizerToken = result.authorizer_access_token;
                    obj.RefreshToken = result.authorizer_refresh_token;
                    obj.Expires = result.expires_in;
                }
            }
            return obj;
        }
    }
    #endregion

    public partial class ComponentAppinter
    {


        /// <summary>
        /// 使用授权码换取公众号的接口调用凭据和授权信息
        /// </summary>
        /// <param name="componentAppid">第三方平台appid</param>
        /// <param name="authCode">授权code,会在授权成功时返回给第三方平台</param>
        /// <param name="compToken">第三方平台的component_access_token</param>
        /// <returns></returns>
        public static AuthorizerTokenResponse ComponentAuthorizerToken(string componentAppid, string authCode, string compToken)
        {
            var obj = new QueryAuthRequest();
            obj.ServiceUrl = BaseConfig.Component_API_URL;
            obj.MethodName = "api_query_auth";
            obj.AccessToken = compToken;
            obj.TokenQueryName = "component_access_token";
            var dict = new Dictionary<string, object>();
            dict.Add("component_appid", componentAppid);
            dict.Add("authorization_code", authCode);
            return obj.Post(dict);
        }

        /// <summary>
        /// 刷新授权方令牌
        /// </summary>
        /// <param name="component_appid"></param>
        /// <param name="authorizer_appid"></param>
        /// <param name="refresh_token"></param>
        /// <param name="accToken">平台的AccessToken</param>
        /// <returns></returns>
        public static AuthorizerRefreshResponse RefreshAuthorizer(string component_appid, string authorizer_appid, string refresh_token, string accToken)
        {
            var obj = new AuthorizerRefreshRequest();
            obj.ServiceUrl = BaseConfig.Component_API_URL;
            obj.MethodName = "api_authorizer_token";
            obj.AccessToken = accToken;
            obj.TokenQueryName = "component_access_token";
            var dict = new Dictionary<string, object>();
            dict.Add("component_appid", component_appid);
            dict.Add("authorizer_appid", authorizer_appid);
            dict.Add("authorizer_refresh_token", refresh_token);
            return obj.Post(dict);
        }



        /// <summary>
        /// 第三方平台可以使用接口拉取当前所有已授权的帐号基本信息。
        /// https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1419318459&token=d5e7792eec7413194f4da825d568398487ead24d&lang=zh_CN
        /// </summary>
        /// <param name="config">第三方平台配置信息</param>
        /// <param name="offset">偏移位置/起始位置</param>
        /// <param name="count">拉取数量，最大为500</param>
        [Obsolete]
        public static void GetAuthorizerList(IAppConfig config, int offset = 0, int count = 500)
        {
            var obj = new AuthorizerRefreshRequest();
            obj.ServiceUrl = BaseConfig.Component_API_URL;
            obj.MethodName = "api_get_authorizer_list";
            obj.AccessToken = config.AccessToken;
            obj.TokenQueryName = "component_access_token";
            var dict = new Dictionary<string, object>();
            dict.Add("component_appid", config.AppId);
            dict.Add("offset", 0);
            dict.Add("count", count);
            obj.Post(dict);
        }

        /// <summary>
        /// 第三方平台对其所有API调用次数清零（只与第三方平台相关，与公众号无关，接口如api_component_token）
        /// </summary>
        /// <param name="config">调用接口凭据 & 第三方平台APPID</param>
        public static ResponseBase ClearQuotaWithApp(IAppConfig config)
        {

            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.Component_API_URL;
            obj.MethodName = "clear_quota";
            obj.AccessToken = config.AccessToken;
            obj.TokenQueryName = "component_access_token";
            var dict = new Dictionary<string, object>();
            dict.Add("component_appid", config.AppId);
            return obj.Post(dict);
        }
    }
}
