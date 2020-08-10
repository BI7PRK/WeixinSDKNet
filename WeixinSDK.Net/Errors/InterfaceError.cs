using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinSDK.Net.Errors
{
    public class InterfaceError : ErrorInfo
    {

        public InterfaceError(string msg, int code)
            : base(msg, code)
        {

        }

    }
}
