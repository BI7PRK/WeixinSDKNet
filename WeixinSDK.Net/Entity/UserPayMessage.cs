using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable]
    public class UserPayMessage : MessageBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        /// <value>
        /// The card identifier.
        /// </value>
        public string CardId { get; set; }

        /// <summary>
        /// 卡券Code码
        /// </summary>
        /// <value>
        /// The user card code.
        /// </value>
        public string UserCardCode { get; set; }

        /// <summary>
        /// 微信支付交易订单号（只有使用买单功能核销的卡券才会出现）
        /// </summary>
        /// <value>
        /// The trans identifier.
        /// </value>
        public string TransId { get; set; }

        /// <summary>
        /// 门店名称，当前卡券核销的门店名称（只有通过卡券商户助手和买单核销时才会出现）
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }

        /// <summary>
        /// 实付金额，单位为分
        /// </summary>
        /// <value>
        /// The fee.
        /// </value>
        public int Fee { get; set; }

        /// <summary>
        /// 应付金额，单位为分
        /// </summary>
        /// <value>
        /// The original fee.
        /// </value>
        public int OriginalFee { get; set; }
    }
}
