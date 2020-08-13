using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinSDK.Net.Pay.Enums;

namespace WeixinSDK.Net.Pay
{
    public interface IMetaEntity
    {
        void setSign(string s);
        string sign { get; }
        SignType sign_type { get; set; }
    }
}
