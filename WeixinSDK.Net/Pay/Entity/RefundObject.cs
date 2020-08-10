using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinSDK.Net.Pay.Enums;

namespace WeixinSDK.Net.Pay.Entity
{
    public class RefundObject : IMetaEntity
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        /// <value>
        /// The appid.
        /// </value>
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        /// <value>
        /// The MCH identifier.
        /// </value>
        public string mch_id { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <value>
        /// The nonce string.
        /// </value>
        public string nonce_str { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        /// <value>
        /// The out refund no.
        /// </value>
        public string out_refund_no { get; set; }

        /// <summary>
        /// refund_fee
        /// </summary>
        /// <value>
        /// The refund fee.
        /// </value>
        public int refund_fee { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        /// <value>
        /// The total fee.
        /// </value>
        public int total_fee { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        /// <value>
        /// The out trade no.
        /// </value>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 退款货币种类.退款货币类型，需与支付一致，或者不填。符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见
        /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_2
        /// </summary>
        /// <value>
        /// The type of the refund fee.
        /// </value>
        public string refund_fee_type { get; set; }
        /// <summary>
        /// 微信订单号
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public string transaction_id { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        /// <value>
        /// The refund desc.
        /// </value>
        public string refund_desc { get; set; }

        /// <summary>
        /// 退款资金来源
        /// </summary>
        /// <value>
        /// The refund account.
        /// </value>
        public string refund_account { get; set; }

        /// <summary>
        /// 退款结果通知url
        /// </summary>
        /// <value>
        /// The notify URL.
        /// </value>
        public string notify_url { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        /// <value>
        /// The type of the sign.
        /// </value>
        public SignType sign_type { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        /// <value>
        /// The sign.
        /// </value>
        public string sign { get; private set; }

        public void setSign(string s)
        {
            sign = s;
        }
    }
}
