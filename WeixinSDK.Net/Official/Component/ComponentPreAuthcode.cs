using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WeixinSDK.Net.Interface;

namespace WeixinSDK.Net.Official
{


    /// <summary>
    /// AccessToken 请求
    /// </summary>
    internal class ComponentPreAuthcodeRequest : RequestBase<ComponentAccessToken>
    {
        private class map
        {
            public string pre_auth_code { get; set; }
            public int expires_in { get; set; }

        }

        public override ComponentAccessToken Post(object param)
        {
            try
            {
                var obj = base.Post(param);
                if (!obj.IsError)
                {
                    var result = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                    if (result != null)
                    {
                        obj.AccessToken = result.pre_auth_code;
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
    
    public partial  class ComponentAppinter
    {


        /// <summary>
        /// 获取预授权码pre_auth_code
        /// </summary>
        /// <param name="config">平台的配置信息</param>
        /// <returns></returns>
        public static ComponentAccessToken GetPreAuthcode(IAppConfig config)
        {
            var obj = new ComponentPreAuthcodeRequest();
            obj.ServiceUrl = BaseConfig.Component_API_URL;
            obj.MethodName = "api_create_preauthcode";
            obj.AccessToken = config.AccessToken;
            obj.TokenQueryName = "component_access_token";
            var dict = new Dictionary<string, object>();
            dict.Add("component_appid", config.AppId);
            return obj.Get(dict);
        }


    }
}
