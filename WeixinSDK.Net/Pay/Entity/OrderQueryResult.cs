using WeixinSDK.Net.Pay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace WeixinSDK.Net.Pay.Entity
{
    public class OrderQueryResult : ResultInfoBase
    {
        /// <summary>
        /// 用户在商户appid下的唯一标识
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
        /// </summary>
        public string is_subscribe { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        /// <value>
        /// The state of the trade.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public TradeState trade_state { get; set; }

        /// <summary>
        /// 银行类型，采用字符串类型的银行标识
        /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_2
        /// </summary>
        public string bank_type { get; set; }

        /// <summary>
        /// 标价金额，单位为分
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 货币种类
        /// </summary>
        public string fee_type { get; set; }

        // <summary>
        /// 现金支付金额
        /// </summary>
        public int cash_fee { get; set; }

        /// <summary>
        /// 现金支付货币类型
        /// </summary>
        public string cash_fee_type { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商家数据包，原样返回
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 支付完成时间，格式为yyyyMMddHHmmss
        /// </summary>
        public string time_end { get; set; }

        /// <summary>
        /// 标价币种与支付币种的兑换比例乘以10的8次方即为此值，例如美元兑换人民币的比例为6.5，则rate=650000000
        /// </summary>
        public string rate { get; set; }
    }
}
