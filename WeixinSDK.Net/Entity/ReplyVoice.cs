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
    public class ReplyVoice : ReplyBase, IXmlFormat
    {
        public ReplyVoice() : base(MessageType.Voice) { }
        public ReplyMediaId Voice { get; set; }
        
    }
}
