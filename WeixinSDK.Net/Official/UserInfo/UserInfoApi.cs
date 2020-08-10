using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WeixinSDK.Net.Entity;
using WeixinSDK.Net.Enums;
using WeixinSDK.Net.Interface;

namespace WeixinSDK.Net.Official
{
    /// <summary>
    /// 用户信息响应
    /// </summary>
    public class UserInfoResponse : ResponseBase
    {
        /// <summary>
        /// 返回用户信息
        /// </summary>
        public UserInfo UserInfo { get; set; }
    }

    public sealed class UserInfoAPI
    {

        /// <summary>
        /// 第四步：拉取用户信息(需scope为 snsapi_userinfo)
        /// </summary>
        /// <param name="token">此令牌是网站授权令牌，与基础令牌不同</param>
        /// <param name="openid"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static UserInfoResponse GetUserInfoWithAuthToket(string token, string openid, LangType lang = LangType.zh_CN)
        {
            var obj = new RequestBase<UserInfoResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "sns/userinfo";
            var dict = new Dictionary<string, object>();
            dict.Add("access_token", token);
            dict.Add("openid", openid);
            dict.Add("lang", lang);
            var result = obj.Get(dict);
            if (!result.IsError)
            {
                result.UserInfo = JsonConvert.DeserializeObject<UserInfo>(result.ResonseBody);
            }
        
            return result;
        }

    }
}
