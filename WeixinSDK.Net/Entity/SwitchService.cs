using WeixinSDK.Net.Enums;
using WeixinSDK.Net.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable()]
    public class SwitchService : ReplyBase, IXmlFormat
    {
        public SwitchService() : base(MessageType.Transfer_customer_service) { }
       
    }

    [Serializable()]
    public class SelectServicer : ReplyBase, IXmlFormat
    {
        public SelectServicer()
            : base(MessageType.Transfer_customer_service)
        {
        }

        public TransInfo TransInfo { get; set; }

    }
    [Serializable()]
    public class TransInfo
    {
        public string KfAccount { get; set; }
    }
}
