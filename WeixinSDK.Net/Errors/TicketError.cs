using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinSDK.Net.Errors
{
    public class TicketError : ErrorInfo
    {
        public TicketError(string msg, int code)
            :base(msg, code)
        {
        
        }
    }
}
