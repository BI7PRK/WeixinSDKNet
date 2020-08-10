using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Pay.Enums
{
    public class BillTypes 
    {
        /// <summary>
        /// 返回当日所有订单信息，默认值 
        /// </summary>
        public const string ALL = "ALL";
        /// <summary>
        /// 返回当日成功支付的订单 
        /// </summary>
        public const string SUCCESS = "SUCCESS";
        /// <summary>
        /// 返回当日退款订单
        /// </summary>
        public const string REFUND = "REFUND";
        /// <summary>
        /// 返回当日充值退款订单
        /// </summary>
        public const string RECHARGE_REFUND = "RECHARGE_REFUND";
    }
}
