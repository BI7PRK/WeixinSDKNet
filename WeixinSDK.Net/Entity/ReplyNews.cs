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
    [Serializable]
    public class ReplyNews : ReplyBase, IXmlFormat
    {
        public ReplyNews() : base(MessageType.News) { }
        public int ArticleCount { get; set; }
        public List<item> Articles { get; set; }

    }

    [Serializable]
    public class item
    {
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
        /// <summary>
        /// 图片链接
        /// </summary>
        [XmlIgnore]
        public string PicUrl { get; set; }
        [XmlElement("PicUrl")]
        public XmlNode _PicUrl
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.PicUrl);
            }
            set { }
        }
        [XmlIgnore]
        public string Url { get; set; }
        [XmlElement("Url")]
        public XmlNode _Url
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.Url);
            }
            set { }
        }

    }
}
