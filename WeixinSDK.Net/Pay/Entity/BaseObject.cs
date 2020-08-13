using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinSDK.Net.Pay.Enums;

namespace WeixinSDK.Net.Pay.Entity
{
    public class BaseObject : IMetaEntity
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }

  

        /// <summary>
        /// 随机字符串 
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// Gets or sets the sign.
        /// </summary>
        /// <value>
        /// The sign.
        /// </value>
        public string sign
        {
            get;
            private set;
        }
        /// <summary>
        /// 签名类型
        /// </summary>
        public SignType sign_type { get; set; }

        public void setSign(string s)
        {
            sign = s;
        }
    }
}
