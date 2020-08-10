using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable]
    public class VerifySuccess : MessageBase
    {
        public int ExpiredTime { get; set; }
    }
}
