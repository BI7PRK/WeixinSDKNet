using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeixinSDK.Net.Extensions;
using WeixinSDK.Net.Pay.Enums;

namespace WeixinSDK.Net.Pay.Entity
{
    public interface IBridge
    {
        string prepayid { get; set; }
    }


    /// <summary>
    /// 在微信浏览器里面打开H5网页中执行JS调起支付。接口输入输出数据格式为JSON。
    /// 参数名区分大小，大小写错误签名验证会失败。
    /// </summary>
    public class WechatBridge : IBridge
    {
        /// <summary>
        /// 公众号id。商户注册具有支付权限的公众号成功后即可获得
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 时间戳。当前的时间
        /// </summary>
        public string timeStamp { get { return DateTime.Now.ToUnixSeconds().ToString(); } }

        /// <summary>
        /// 随机字符串。随机字符串，不长于32位
        /// </summary>
        public string nonceStr { get; set; }

        /// <summary>
        /// 订单详情扩展字符串。统一下单接口返回的prepay_id参数值
        /// </summary>
        public string package { get; set; }

        /// <summary>
        /// 签名方式。暂支持MD5
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SignType signType { get; set; }

        /// <summary>
        /// 签名
        /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_3
        /// </summary>
        public string paySign { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TradeType trade_type { get; set; }

        public string prepayid { get; set; }
    }

    public class AppBridge : WechatBridge
    {
        /// <summary>
        /// 暂填写固定值Sign=WXPay
        /// </summary>
        public new string package => "Sign=WXPay";
        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string partnerid { get; set; }
    }

    public class H5Bridge : IBridge
    {
        public string prepayid { get; set; }

        public string mweb_url { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TradeType trade_type => TradeType.MWEB;
    }
}