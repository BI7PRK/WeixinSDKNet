using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Pay.Enums
{
    public enum TradeState
    {
        /// <summary>
        /// 支付失败
        /// </summary>
        [Description("支付失败")]
        PAYERROR,

        /// <summary>
        /// 用户支付中
        /// </summary>
        [Description("用户支付中")]
        USERPAYING,

        /// <summary>
        /// 已撤销/刷卡支付
        /// </summary>
        [Description("已撤销/刷卡支付")]
        REVOKED,

        /// <summary>
        /// 已关闭
        /// </summary>
        [Description("已关闭")]
        CLOSED,

        /// <summary>
        /// 未支付
        /// </summary>
        [Description("未支付")]
        NOTPAY,

        /// <summary>
        /// 转入退款
        /// </summary>
        [Description("转入退款")]
        REFUND,

        /// <summary>
        /// 支付成功
        /// </summary>
        [Description("支付成功")]
        SUCCESS
    }
}
