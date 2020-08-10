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
    public class ReplyVideo : ReplyBase, IXmlFormat
    {
        public ReplyVideo() : base(MessageType.Video) { }
        public ReplyVideoInfo Video { get; set; }

       
    }

    [Serializable]
    public class ReplyVideoInfo
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
        [XmlIgnore]
        public string Title { get; set; }
        [XmlElement("Title")]
        public XmlNode _Title
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.Title);
            }
            set { }
        }
        [XmlIgnore]
        public string Description { get; set; }
        [XmlElement("Description")]
        public XmlNode _Description
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.Description);
            }
            set { }
        }
    }
}
