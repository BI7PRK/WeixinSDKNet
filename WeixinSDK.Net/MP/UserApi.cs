using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinSDK.Net.Interface;
using WeixinSDK.Net.Official;

namespace WeixinSDK.Net.MP
{
    public sealed class UserApi
    {

        class sessionmap
        {
            public string OpenId { get; set; }
            [JsonProperty("session_key")]
            public string SessionKey { get; set; }
            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }
        }

        public class UserInfo
        {
            public string openId { get; set; }
            public string nickName { get; set; }
            /// <summary>
            /// 性别 0：未知、1：男、2：女
            /// </summary>
            /// <value>
            /// The gender.
            /// </value>
            public int gender { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string country { get; set; }
            public string avatarUrl { get; set; }
            public string unionId { get; set; }
        }

        public class DecodeUserRequest : ResponseBase
        {
            public UserInfo Data { get; set; } = new UserInfo();
        }


        /// <summary>
        /// 小程序用户信息解密
        /// </summary>
        /// <param name="jcode">The jcode.</param>
        /// <param name="encryptedData">The encrypted data.</param>
        /// <param name="iv">The iv.</param>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public static DecodeUserRequest DecodeUserInfo(string jcode, string encryptedData, string iv, IAppConfig config)
        {
            var obj = new RequestBase<DecodeUserRequest>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "sns/jscode2session";
            var dict = new Dictionary<string, object>();
            dict.Add("appid", config.AppId);
            dict.Add("secret", config.AppSecret);
            dict.Add("js_code", jcode);
            dict.Add("grant_type", "authorization_code");
            var result = obj.Get(dict);
            if (!result.IsError)
            {
                var keyInfo = JsonConvert.DeserializeObject<sessionmap>(result.ResonseBody);
                if (!string.IsNullOrEmpty(encryptedData))
                {
                    byte[] toEncryptArray = Convert.FromBase64String(encryptedData);
                    System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
                    {
                        Key = Convert.FromBase64String(keyInfo.SessionKey),
                        IV = Convert.FromBase64String(iv),
                        Mode = System.Security.Cryptography.CipherMode.CBC,
                        Padding = System.Security.Cryptography.PaddingMode.PKCS7
                    };
                    System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    string jsonStr = Encoding.UTF8.GetString(resultArray);
                    result.Data = JsonConvert.DeserializeObject<UserInfo>(jsonStr);
                }
                else
                {
                    result.Data.openId = keyInfo.OpenId;
                }
            }

            return result;
        }
    }
}
