using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeixinSDK.Net.Pay.Enums
{
    public enum TradeType
    {
        JSAPI,
        NATIVE,
        APP,
        /// <summary>
        /// H5支付的交易类型
        /// </summary>
        MWEB
    }
}