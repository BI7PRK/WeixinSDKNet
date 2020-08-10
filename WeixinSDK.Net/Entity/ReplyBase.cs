using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using WeixinSDK.Net.Enums;

namespace WeixinSDK.Net.Entity
{
    [Serializable()]
    public abstract class ReplyBase
    {
        public ReplyBase() { }
        public ReplyBase(MessageType s)
        {
            this.MsgType = s;
        }
        private MessageType MsgType;
        [XmlElement("MsgType")]
        public XmlNode _MsgType
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.MsgType.ToString().ToLower());
            }
            set { }
        }

        [XmlIgnore]
        public string ToUserName { get; set; }
        [XmlElement("ToUserName")]
        public XmlNode ToUser
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.ToUserName);
            }
            set { }
        }
        [XmlIgnore]
        public string FromUserName { get; set; }
        [XmlElement("FromUserName")]
        public XmlNode FromUser
        {
            get
            {
                var dom = new XmlDocument();
                return dom.CreateCDataSection(this.FromUserName);
            }
            set { }
        }

        public int CreateTime { get; set; }

        //public long MsgId
        //{
        //    get
        //    {
        //        return DateTime.Now.Ticks;
        //    }
        //    set { } 
        //}



    }

}
