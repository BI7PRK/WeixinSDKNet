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
    public class ReplyMusic : ReplyBase, IXmlFormat
    {
        public ReplyMusic() : base(MessageType.Music) { }
        public ReplyMusicInfo Music { get; set; }
        
    }

    [Serializable]
    public class ReplyMusicInfo
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
        [XmlIgnore]
        public string MusicUrl { get; set; }
        [XmlElement("MusicUrl")]
        public XmlNode _MusicUrl
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.MusicUrl);
            }
            set { }
        }
        [XmlIgnore]
        public string HQMusicUrl { get; set; }
        [XmlElement("HQMusicUrl")]
        public XmlNode _HQMusicUrl
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.HQMusicUrl);
            }
            set { }
        }
        /// <summary>
        /// 缩略图的媒体id，通过上传多媒体文件，得到的id 
        /// </summary>
        [XmlIgnore]
        public string ThumbMediaId { get; set; }
        [XmlElement("ThumbMediaId")]
        public XmlNode _ThumbMediaId
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.ThumbMediaId);
            }
            set { }
        }
    }

}
