using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Pay.Entity
{
    public class OrderClose : BaseObject
    {

        /// <summary>
        /// 商户订单号 
        /// </summary>
        public string out_trade_no { get; set; }
        
    }
}
