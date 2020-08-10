using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WeixinSDK.Net.Interface;

namespace WeixinSDK.Net.Official
{

    public class AuthorizerInfoResponse : ResponseBase
    {
        /// <summary>
        /// 授权方AppId
        /// </summary>
        public string Appid { set; get; }
        ///<summary>
        ///<para>授权方昵称</para>
        ///</summary>
    
        public string NickName { set; get; }

        ///<summary>
        ///<para>授权方头像</para>
        ///</summary>
        public string HeadImg { set; get; }
        
        ///<summary>
        ///<para>授权方公众号的原始ID</para>
        ///</summary>
        public string UserName { set; get; }
        ///<summary>
        ///<para>授权方公众号类型，0代表订阅号，1代表由历史老帐号升级后的订阅号，2代表服务号</para>
        ///</summary>
        public int UserType { set; get; }
        ///<summary>
        ///<para>授权方认证类型，-1代表未认证，0代表微信认证，1代表新浪微博认证，2代表腾讯微博认证，3代表已资质认证通过但还未通过名称认证，4代表已资质认证通过、还未通过名称认证，但通过了新浪微博认证，5代表已资质认证通过、还未通过名称认证，但通过了腾讯微博认证</para>
        ///</summary>
        public int VerifyType { set; get; }

        ///<summary>
        ///<para>公众号的主体名称</para>
        ///</summary>
        public string PrincipalName { set; get; }
        ///<summary>
        ///<para></para>
        ///</summary>
        public string AliasName { set; get; }

        /// <summary>
        /// 功能开通状况（0代表未开通，1代表已开通）
        ///  open_store:是否开通微信门店功能
        ///  open_scan:是否开通微信扫商品功能
        ///  open_pay:是否开通微信支付功能
        ///  open_card:是否开通微信卡券功能
        ///  open_shake:是否开通微信摇一摇功能
        /// </summary>
        public string Business { set; get; }

        /// <summary>
        /// 集权信息
        /// </summary>
        public AuthorizerInfo[] FuncInfo { set; get; }
    }

    /// <summary>
    /// AccessToken 请求
    /// </summary>
    internal class ComponentAuthorizerInfoRequest : RequestBase<AuthorizerInfoResponse>
    {
        private class map
        {
            public info authorizer_info { get; set; }
            public auth authorization_info { get; set; }

        }
        private class auth
        {
            public string authorizer_appid { get; set; }
            public QueryAuthRequest.func[] func_info { get; set; }
        }

        private class info
        {
            public string nick_name { get; set; }
            public string head_img { get; set; }
            public QueryAuthRequest.idval service_type_info { get; set; }
            public QueryAuthRequest.idval verify_type_info { get; set; }
            public string user_name { get; set; }
            public string principal_name { get; set; }
            public opt business_info { get; set; }
            public string alias { get; set; }
            public string qrcode_url { get; set; }
           
        }



        
        private class opt
        {
            public int open_store { get; set; }
            public int open_scan { get; set; }
            public int open_pay { get; set; }
            public int open_card { get; set; }
            public int open_shake { get; set; }
        }

        public override AuthorizerInfoResponse Post(object param)
        {
            try
            {
                var obj = base.Post(param);
                if (!obj.IsError)
                {
                    var result = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                    if (result != null)
                    {
                        List<string> str = new List<string>();
                        var busin = result.authorizer_info.business_info;
                        if (busin.open_card == 1)
                            str.Add("OpenCard");
                        if (busin.open_pay == 1)
                            str.Add("OpenPay");
                        if (busin.open_scan == 1)
                            str.Add("OpenScan");
                        if (busin.open_shake == 1)
                            str.Add("OpenShake");
                        if (busin.open_store == 1)
                            str.Add("OpenStore");

                        obj.AliasName = result.authorizer_info.alias;
                        obj.Appid = result.authorization_info.authorizer_appid;
                        obj.Business = string.Join(",", str);
                        obj.FuncInfo = result.authorization_info.func_info.Select(s => new AuthorizerInfo
                        {
                            ID = s.funcscope_category.id
                            //Category = s.funcscope_category.name
                        }).ToArray();
                        obj.HeadImg = result.authorizer_info.head_img;
                        obj.NickName = result.authorizer_info.nick_name;
                        obj.PrincipalName = result.authorizer_info.principal_name;
                        obj.UserName = result.authorizer_info.user_name;
                        obj.UserType = result.authorizer_info.service_type_info.id;
                        obj.VerifyType  = result.authorizer_info.verify_type_info.id;
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



    public class QueryAuthorizerOptionResponse : ResponseBase
    {
        /// <summary>
        /// 授权公众号appid
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 选项名称
        /// </summary>
        public string OptionName { get; set; }
        /// <summary>
        /// 选项值
        /// </summary>
        public string OptionValue { get; set; }
    }

    internal class QueryAuthorizerOptionRequest : RequestBase<QueryAuthorizerOptionResponse>
    {
        private class map
        {
            public string authorizer_appid { get; set; }
            public string option_name { get; set; }
            public string option_value { get; set; }
        }

        public override QueryAuthorizerOptionResponse Post(object param)
        {
            var obj = base.Post(param);
            if (!obj.IsError)
            {
                var result = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (result != null)
                {
                    obj.AppId = result.authorizer_appid;
                    obj.OptionName = result.option_name;
                    obj.OptionValue = result.option_value;
                }
            }

            return obj;
        }
    }

   
    /// <summary>
    /// 第三方平台的 AccessToken
    /// </summary>
    public partial  class ComponentAppinter
    {


        /// <summary>
        /// 获取预授权码授权方令牌
        /// </summary>
        /// <param name="config">第三方平台接口配置.包括第三方平台access_token</param>
        /// <param name="authorizer_appid">授权方appid</param>
        /// <returns></returns>
        public static AuthorizerInfoResponse GetAuthorizerUser(IAppConfig config, string authorizer_appid)
        {
            var obj = new ComponentAuthorizerInfoRequest();
            obj.ServiceUrl = BaseConfig.Component_API_URL;
            obj.MethodName = "api_get_authorizer_info";
            obj.AccessToken = config.AccessToken;
            obj.TokenQueryName = "component_access_token";
            var dict = new Dictionary<string, object>();
            dict.Add("component_appid", config.AppId);
            dict.Add("authorizer_appid", authorizer_appid);
            return obj.Post(dict);
        }



        /// <summary>
        /// 获取授权方的选项设置信息
        /// 如：地理位置上报，语音识别开关，多客服开关。注意，获取各项选项设置信息，需要有授权方的授权，详见权限集说明。
        /// </summary>
        /// <param name="config">第三方平台接口配置</param>
        /// <param name="authorizer_appid">授权方appid</param>
        /// <param name="option_name">选项名称</param>
        public static QueryAuthorizerOptionResponse QueryAuthorizerOption(IAppConfig config, string authorizer_appid, string option_name)
        {
            var obj = new QueryAuthorizerOptionRequest();
            obj.ServiceUrl = BaseConfig.Component_API_URL;
            obj.MethodName = "api_get_authorizer_option";
            obj.AccessToken = config.AccessToken;
            obj.TokenQueryName = "component_access_token";
            var dict = new Dictionary<string, object>();
            dict.Add("component_appid", config.AppId);
            dict.Add("authorizer_appid", authorizer_appid);
            dict.Add("option_name", option_name);
            return obj.Post(dict);
        }

        /// <summary>
        /// 设置授权方的选项信息
        /// 如：地理位置上报，语音识别开关，多客服开关。注意，设置各项选项设置信息，需要有授权方的授权，详见权限集说明。
        /// </summary>
        /// <param name="config">第三方平台接口配置</param>
        /// <param name="authorizer_appid">授权方appid</param>
        /// <param name="name">选项名称</param>
        /// <param name="value">选项值</param>
        /// <returns></returns>
        public static ResponseBase SetAuthorizerOption(IAppConfig config, string authorizer_appid, string name, string value)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.Component_API_URL;
            obj.MethodName = "api_set_authorizer_option";
            obj.AccessToken = config.AccessToken;
            obj.TokenQueryName = "component_access_token";
            var dict = new Dictionary<string, object>();
            dict.Add("component_appid", config.AppId);
            dict.Add("authorizer_appid", authorizer_appid);
            dict.Add("option_name", name);
            dict.Add("option_value", value);
            return obj.Post(dict);
        }
    }
}
