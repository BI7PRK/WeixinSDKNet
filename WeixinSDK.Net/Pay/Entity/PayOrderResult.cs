using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Pay.Entity
{
    public class PayOrderResult : ResultInfoBase
    {
        /// <summary>
        /// 微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时
        /// </summary>
        public string prepay_id { get; set; }

        /// <summary>
        /// 二维码链接
        /// </summary>
        public string code_url { get; set; }

    }
}
