using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Pay
{
    public interface IMetaEntity
    {
        void setSign(string s);
        string sign { get; }
    }
}
