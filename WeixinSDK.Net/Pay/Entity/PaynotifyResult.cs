using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Pay.Entity
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="WeixinSDK.Net.Pay.Entity.ResultInfoBase" />
    public class PaynotifyResult : ResultInfoBase
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
        /// 银行类型，采用字符串类型的银行标识
        /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_2
        /// </summary>
        public string bank_type { get; set; }

        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 应结订单金额=订单金额-非充值代金券金额，应结订单金额&lt;=订单金额。 
        /// </summary>
        public int settlement_total_fee { get; set; }

        /// <summary>
        /// 货币种类
        /// </summary>
        public string fee_type { get; set; }

        /// <summary>
        /// 现金支付金额
        /// </summary>
        public int cash_fee { get; set; }

        /// <summary>
        /// 现金支付货币类型
        /// </summary>
        public string cash_fee_type { get; set; }

        /// <summary>
        /// 总代金券金额.代金券金额&lt;=订单金额，订单金额-代金券金额=现金支付金额
        /// </summary>
        public int coupon_fee { get; set; }

        /// <summary>
        /// 代金券使用数量
        /// </summary>
        public int coupon_count { get; set; }

        /*
         coupon_type_$n 
         coupon_id_$n
         coupon_fee_$n
        */

        public string[] coupon_types { get; set; }

        public string[] coupon_id { get; set; }

        public string[] coupon_fees { get; set; }

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
    

    }
}
