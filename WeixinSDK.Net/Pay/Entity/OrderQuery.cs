using WeixinSDK.Net.Pay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeixinSDK.Net.Pay.Entity
{
    /// <summary>
    /// 订单查询
    /// </summary>
    /// <seealso cref="WeixinSDK.Net.Pay.IMetaEntity" />
    public class OrderQuery : IMetaEntity
    {
        /// <summary>
        /// 公众账号ID 
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号 
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 微信订单号 
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户订单号 
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 随机字符串 
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名 
        /// </summary>
        public string sign { get; private set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SignType sign_type { get; set; }

        public void setSign(string s)
        {
            sign = s;
        }
    }
}
