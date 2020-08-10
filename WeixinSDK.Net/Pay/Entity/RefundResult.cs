using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Pay.Entity
{

    public class XMLMemberAttribute : Attribute
    {
        public string Name { get; set; }
        public XMLMemberAttribute()
        {

        }

        public XMLMemberAttribute(string name)
        {
            Name = name;
        }



    }

    public class RefundResult : ResultInfoBase
    {
        public string transaction_id { get; set; }
        public string out_trade_no { get; set; }
        public string out_refund_no { get; set; }


        public string refund_id { get; set; }
        public int refund_fee { get; set; }
        public int settlement_refund_fee { get; set; }

        public int total_fee { get; set; }
        public int settlement_total_fee { get; set; }

        public string fee_type { get; set; }
        public int cash_fee { get; set; }

        public string cash_fee_type { get; set; }

        public int cash_refund_fee { get; set; }
        /// <summary>
        /// 代金券类型 coupon_type_$n 举例：coupon_type_0
        /// </summary>
        /// <value>
        /// The type of the coupon.
        /// </value>
        public string[] coupon_type { get; set; }

        public int coupon_refund_fee { get; set; }
        /// <summary>
        /// 单个代金券退款金额  coupon_refund_fee_$n
        /// </summary>
        /// <value>
        /// The coupon refund fee n.
        /// </value>
        public int coupon_refund_fee_N { get; set; }

        public int coupon_refund_count { get; set; }
        /// <summary>
        /// 退款代金券ID coupon_refund_id_$n  退款代金券ID, $n为下标，从0开始编号
        /// </summary>
        /// <value>
        /// The coupon refund identifier.
        /// </value>
        public string[] coupon_refund_id { get; set; }
    }
}
