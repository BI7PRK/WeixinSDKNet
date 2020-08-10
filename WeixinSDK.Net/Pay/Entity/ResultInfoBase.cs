using WeixinSDK.Net.Pay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeixinSDK.Net.Pay.Entity
{
    public class ResultInfoBase : IMetaEntity
    {
        /// <summary>
        /// 返回状态码 SUCCESS/FAIL
        /// 此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
        /// </summary>
        public PayResult return_code { get; set; }

        /// <summary>
        /// 返回信息 如非空，为错误原因:
        /// 签名失败/参数格式/校验错误
        /// </summary>
        public string return_msg { get; set; }
        
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SignType sign_type { get; set; }

        /// <summary>
        /// 业务结果:SUCCESS/FAIL
        /// </summary>
        public PayResult result_code { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public WechatPayError err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }
        

        /// <summary>
        /// 交易类型，取值为：JSAPI，NATIVE，APP等
        /// </summary>
        public TradeType trade_type { get; set; }

        /// <summary>
        /// 为拉起微信支付收银台的中间页面，可通过访问该url来拉起微信客户端，完成支付,mweb_url的有效期为5分钟。
        /// </summary>
        public string mweb_url { get; set; }

        public void setSign(string s)
        {
            sign = s;
        }
    }
}
