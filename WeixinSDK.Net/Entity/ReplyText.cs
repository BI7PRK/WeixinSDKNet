using WeixinSDK.Net.Enums;
using WeixinSDK.Net.Extensions;
using WeixinSDK.Net.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WeixinSDK.Net.Entity
{
    [Serializable()]
    public class ReplyText : ReplyBase, IXmlFormat
    {
        public ReplyText() : base(MessageType.Text) { }

        [XmlIgnore]
        public string Content { get; set; }
        [XmlElement("Content")]
        public XmlNode _Content
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.Content);
            }
            set { }
        }
        public string ToXmlString()
        {
            return this.GetXmlString();
        }
    }

}
