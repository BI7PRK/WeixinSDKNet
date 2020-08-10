using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Pay.Entity
{
    public class DataResult
    {
        public bool IsError { get; set; }

        public string Message { get; set; }

        public IBridge Result { get; set; }
    }
}
