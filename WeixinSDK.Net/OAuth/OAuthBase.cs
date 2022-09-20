﻿using System;
using System.Threading.Tasks;
using WeixinSDK.Net.Extensions;
using WeixinSDK.Net.Http;
using WeixinSDK.Net.Interface;

namespace WeixinSDK.Net.OAuth2
{
    /// <summary>
    /// 基本的Authorization请求功能，需要继承类使用
    /// </summary>
    public abstract class OAuthBase
    {
        /// <summary>
        /// 生成获取授权的地址
        /// </summary>
        /// <param name="url">OAuth地址</param>
        /// <param name="dict">传递参数</param>
        /// <returns>返回完整的请求地址，成功授权后，接口带相关重定向到指定网址</returns>
        public virtual string GetOAuthCodeLink(string url, OAuthParams dict)
        {
            return string.Concat(url.TrimEnd('?'), "?", dict.ToFormString());
        }

        /// <summary>
        /// Post提交，根据代码获取AccessToken
        /// </summary>
        /// <param name="url">AccessToken接口地址</param>
        /// <param name="dict">在重写向而获取返回的Code，换取AccessToken</param>
        /// <returns></returns>
        public virtual async Task<string> GetAccessToken(string url, ToketParamsBase dict)
        {
            var res = await HttpProxy.PostAsync(url, dict.ToFormString());
            return res.IsSuccess ? res.Body : "";
        }

        /// <summary>
        /// 刷新access_token（如果需要）
        /// 由于access_token拥有较短的有效期，当access_token超时后，可以使用refresh_token进行刷新
        /// efresh_token拥有较长的有效期（7天、30天、60天、90天），当refresh_token失效的后，需要用户重新授权。 
        /// </summary>
        /// <param name="url">AccessToken接口地址</param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public virtual async Task<string> RefreshOAuthToke(string url, IParams dict)
        {
            var res = await HttpProxy.PostAsync(url, dict.ToFormString());
            return res.IsSuccess ? res.Body : "";
        }
        /// <summary>
        /// 根据access_token获取用户信息
        /// </summary>
        /// <param name="url">接口地址</param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public virtual async Task<string> GetFromAuthToket(string url, OpenIdBaseParams dict)
        {
            var res = await HttpProxy.PostAsync(url, dict.ToFormString());
            return res.IsSuccess ? res.Body : "";
        }
    }
}
