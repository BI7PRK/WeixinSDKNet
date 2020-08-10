using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using WeixinSDK.Net.Interface;
using WeixinSDK.Net.Entity;
using WeixinSDK.Net.Enums;
using WeixinSDK.Net.Extensions;
using System.Web;
using System.Web.Security;

namespace WeixinSDK.Net
{
    /// <summary>
    /// 接收微信服务器消息
    /// </summary>
    public class ReceiveMessage
    {
        private IAppConfig _Config;
        private ResponseHandler _Responst;
        private XmlElement rootElement;
        /// <summary>
        /// 接收微信服务器消息
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <param name="config">公众号接口配置</param>
        public ReceiveMessage(HttpContext context, IAppConfig config)
        {
            _Config = config;

            var stream = context.Request.InputStream;

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            SourceBody = Encoding.UTF8.GetString(bytes);
            try
            {
                _Responst = FormToEntityHelper<ResponseHandler>.DeserializeNameValue(context.Request.Params);

                if (CheckSignature)
                {
                    if (!string.IsNullOrEmpty(_Responst.EchoStr))
                    {
                        context.Response.Write(_Responst.EchoStr);
                        Message = _Responst.EchoStr;
                        context.Response.End();
                        return;
                    }
                    else
                    {

                        //平台设置了加密
                        if (_Responst.Encrypt_type == "aes")
                        {
                            var encryMsg = "";
                            var encry = MessageEncryptHelper.MessageDecrypt(
                                config, _Responst.Msg_signature,
                                _Responst.Timestamp.ToString(),
                                _Responst.Nonce, SourceBody, ref encryMsg);

                            if (encry != MessageEncryptHelper.CryptErrorCode.CryptErrorCode_OK)
                            {
                                Message = encry.GetDescription();
                                return;
                            }
                            else
                            {
                                Message = encryMsg;
                            }
                        }
                        else
                        {
                            Message = SourceBody;
                        }

                        var xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(Message);
                        rootElement = xmlDoc.DocumentElement;
                        var msgTypeNode = rootElement.SelectSingleNode("InfoType")
                            ?? rootElement.SelectSingleNode("MsgType");
                        if (msgTypeNode != null)
                        {
                            try
                            {
                                MessageType = (MessageType)Enum.Parse(typeof(MessageType), msgTypeNode.InnerText.ToUpperFirst());
                            }
                            catch { }
                          
                        }

                        var eventType = SelectNodeText("Event");
                        if (!string.IsNullOrEmpty(eventType))
                        {
                            try
                            {
                                EventType = (EventType)Enum.Parse(typeof(EventType), eventType.ToUpperFirst());
                            }
                            catch { }
                           
                        }

                        AppId = SelectNodeText("AuthorizerAppid");
                        ToUserName = SelectNodeText("ToUserName");
                        FromUserName = SelectNodeText("FromUserName");
                    }

                    IsSuccess = true;
                }
                else
                {
                    Message = "真实性验证失败";
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        /// <summary>
        /// 消息是否可用
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// 授权方公众号AppId
        /// </summary>
        public string AppId { get; private set; }

        /// <summary>
        /// 来源都微信号原始ID
        /// </summary>
        public string ToUserName { get; private set; }

        /// <summary>
        /// 微信公众号原始ID
        /// </summary>
        public string FromUserName { get; private set; }

        /// <summary>
        /// 接收的原始内容
        /// </summary>
        public string SourceBody { get; private set; }

        /// <summary>
        /// 已经解密的内容
        /// </summary>
        public string Message
        {
            get; private set;
        }
    
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; private set; }
        /// <summary>
        /// 推送事件类型
        /// </summary>
        public EventType EventType { get; private set; }

        /// <summary>
        /// 获取消息体的某个节点内容
        /// </summary>
        /// <param name="node">节点标签名称</param>
        /// <returns></returns>
        public string SelectNodeText(string node)
        {
            if (rootElement != null)
            {
                var modeElet = rootElement.SelectSingleNode(node);
                if (modeElet != null)
                {
                    return modeElet.InnerText;
                } 
            }
            return string.Empty;
        }
        
        /// <summary>
        /// 验证真实性
        /// </summary>
        private bool CheckSignature
        {
            get
            {
                //微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。
                var arr = new string[] { _Config.ApiToken, _Responst.Timestamp.ToString(), _Responst.Nonce };
                Array.Sort(arr);　　 //字典排序 
                var arrString = string.Join("", arr);
                var sha1 = System.Security.Cryptography.SHA1.Create();
                var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
                StringBuilder sb = new StringBuilder();
                foreach (var b in sha1Arr)
                {
                    sb.AppendFormat("{0:x2}", b);
                }

                return sb.ToString().Equals(_Responst.Signature, StringComparison.InvariantCultureIgnoreCase);
            }
        }


        /// <summary>
        /// 将消息反序列化为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal T MessageDeserialize<T>()
            where  T : class
        {
            var result = default(T);
            var objType = typeof(T);
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(Message);
                var rootElement = xmlDoc.DocumentElement;

                var _newDoc = new XmlDocument();
                _newDoc.AppendChild(_newDoc.CreateXmlDeclaration("1.0", "utf-16", null));
                var _rootEle = _newDoc.CreateElement("", objType.Name, null);//加入一个根元素

                var format = rootElement.InnerXml;
                //需要处理一下大小写
                var reg = new Regex(@"<MsgType><!\[CDATA\[(.+?)\]\]></MsgType>");
                if (reg.IsMatch(Message))
                {
                    format = reg.Replace(rootElement.InnerXml, new MatchEvaluator(s =>
                    {
                        if (!s.Success) return s.Value;
                        return @"<MsgType><![CDATA[" + s.Groups[1].Value.ToUpperFirst() + "]]></MsgType>";
                    }));
                    
                }
                reg = null;

                _rootEle.InnerXml = format;
                _newDoc.AppendChild(_rootEle);

                using (StringReader rdr = new StringReader(_newDoc.OuterXml))
                {
                    result = (T)(new XmlSerializer(objType).Deserialize(rdr));
                }

            }
            catch (Exception ex)
            {
               
            }
            return result;
        }
    }
}
