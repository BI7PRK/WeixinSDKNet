using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WeixinSDK.Net.Extensions;

namespace WeixinSDK.Net.Official
{
    public class JsApiTicketResponse : ResponseBase
    {
        [JsonProperty("ticket")]
        public string Ticket { get; set; }

        [JsonProperty("expires_in")]
        public int Expires { get; set; }
    }


    internal class JsApiTicketRequest : RequestBase<JsApiTicketResponse>
    {

    }


    public sealed class JsSdkAPI
    {
        /// <summary>
        /// 获取api_ticket
        /// api_ticket 是用于调用微信卡券JS API的临时票据，有效期为7200 秒，通过access_token 来获取。 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static JsApiTicketResponse GetJsApiTicket(string token)
        {
            var obj = new JsApiTicketRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "ticket/getticket";
            var dict = new Dictionary<string, object>();
            dict.Add("type", "jsapi");
            obj.AccessToken = token;
            var result = obj.Get(dict);
            if (!result.IsError)
            {
                var format = JsonConvert.DeserializeObject<JsApiTicketResponse>(result.ResonseBody);
                if (format != null)
                {
                    result.Expires = format.Expires;
                    result.Ticket = format.Ticket;
                }
            }
            return result;
        }


        /// <summary>
        /// 创建微信JS接口的关键配置
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ticket">已有缓存的 ticket。可以通过 GetJsApiTicket 获得</param>
        /// <param name="_appid">授权方的APPID</param>
        /// <param name="jsApiList">需要使用的JS接口列表，所有JS接口列表</param>
        /// <returns></returns>
        public static string CreateJsapiConfig(string url, string ticket, string _appid, string[] jsApiList = null)
        {

            string _nonceStr = Guid.NewGuid().ToString("N");
            int _timestamp = DateTime.Now.ToUnixSeconds();
            var strArr = new string[]
            {
                string.Format("jsapi_ticket={0}", ticket),
                string.Format("noncestr={0}", _nonceStr),
                string.Format("timestamp={0}", _timestamp),
                string.Format("url={0}", url)
            };
            if (jsApiList == null || jsApiList.Length == 0)
            {
                jsApiList = new string[] { "onMenuShareTimeline", "onMenuShareAppMessage", "onMenuShareQQ", "onMenuShareWeibo", "onMenuShareQZone", "startRecord", "stopRecord", "onVoiceRecordEnd", "playVoice", "pauseVoice", "stopVoice", "onVoicePlayEnd", "uploadVoice", "downloadVoice", "chooseImage", "previewImage", "uploadImage", "downloadImage", "translateVoice", "getNetworkType", "openLocation", "getLocation", "hideOptionMenu", "showOptionMenu", "hideMenuItems", "showMenuItems", "hideAllNonBaseMenuItem", "showAllNonBaseMenuItem", "closeWindow", "scanQRCode", "chooseWXPay", "openProductSpecificView", "addCard", "chooseCard", "openCard" };
            }

            var string1 = string.Join("&", strArr.OrderBy(z => z));
            string _signature = string1.ToSHA1Encrypt();//SHA1加密
            
            return JsonConvert.SerializeObject(new
            {
                appId = _appid,
                timestamp = _timestamp,
                nonceStr = _nonceStr,
                signature = _signature,
                jsApiList
            });
        }
    }
}
