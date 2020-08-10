using WeixinSDK.Net.Enums;
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
    public class ReplyImage : ReplyBase, IXmlFormat
    {
        public ReplyImage() : base(MessageType.Image) { }
        public ReplyMediaId Image { get; set; }
        
    }

    [Serializable]
    public class ReplyMediaId
    {
        [XmlIgnore]
        public string MediaId { get; set; }
        [XmlElement("MediaId")]
        public XmlNode _MediaId
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.MediaId);
            }
            set { }
        }
    }

}
