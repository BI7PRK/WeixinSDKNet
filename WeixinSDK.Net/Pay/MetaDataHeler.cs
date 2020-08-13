using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using WeixinSDK.Net.Pay.Entity;
using WeixinSDK.Net.Pay.Enums;

namespace WeixinSDK.Net.Pay
{
    public static class MetaDataHeler
    {
        private static BindingFlags bFlags = BindingFlags.Instance
          | BindingFlags.Public
          | BindingFlags.GetProperty
          | BindingFlags.SetProperty;


        public static T ToEntity<T>(string xml) 
            where T: class, new()
        {
            var TEnty = new T();
            var typeOf = TEnty.GetType();

            XmlDocument xmlDoc = new XmlDocument();
            //https://pay.weixin.qq.com/wiki/doc/api/micropay.php?chapter=23_5
            xmlDoc.XmlResolver = null; //杜绝 XXE风险
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            var nodes = xmlNode.ChildNodes.Cast<XmlNode>();

            foreach (var item in typeOf.GetProperties(bFlags))
            {
                var name = item.Name;
                var m = item.GetCustomAttributes<XMLMemberAttribute>().FirstOrDefault();
                if (m != null)
                {
                    name = m.Name;
                }

                var node = nodes.FirstOrDefault(w => w.Name == name);
                if (node != null)
                {
                    var valStr = node.InnerText;
                    if (!string.IsNullOrEmpty(valStr) && item.CanWrite)
                    {
                        try
                        {
                            object val;
                            if (item.PropertyType.IsEnum && Enum.IsDefined(item.PropertyType, valStr))
                            {
                                val = Enum.Parse(item.PropertyType, valStr);
                            }
                            else
                            {
                                val = Convert.ChangeType(valStr, item.PropertyType);
                            }
                            item.SetValue(TEnty, val, null);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"{valStr}=>>{ex.Message}");
                        }
                     
                    }
                }
            }
            
            return TEnty;
        }
        /// <summary>
        /// 签名并转为XML
        /// </summary>
        /// <param name="enty"></param>
        /// <param name="key">API密钥</param>
        /// <returns></returns>
        /// 
        public static string ToXml(this IMetaEntity enty, string key)
        {
            var typeOf = enty.GetType();
            var propers = typeOf.GetProperties(bFlags);

            var keyApped = new List<string>();
            foreach (var item in propers)
            {
                try
                {
                    var name = item.Name;
                    var m = item.GetCustomAttributes<XMLMemberAttribute>().FirstOrDefault();
                    if (m != null)
                    {
                        name = m.Name;
                    }

                    var objValue = item.GetValue(enty, null);
                    if (objValue != null && !string.IsNullOrEmpty(objValue.ToString()))
                    {
                        keyApped.Add(name + "=" + objValue);
                    }
                }
                catch { }
            }

            var str = string.Join("&", keyApped.OrderBy(s => s)) + "&key=" + key;
            byte[] data = Encoding.UTF8.GetBytes(str);
            var sb = new StringBuilder();
            var hash = enty.sign_type == SignType.MD5
                ? MD5.Create().ComputeHash(data)
                : SHA256.Create().ComputeHash(data);
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2")); //大写
            }
            enty.setSign(sb.ToString());

            var xml = new StringBuilder("<xml>");
            foreach (var item in propers)
            {
                try
                {
                    var objValue = item.GetValue(enty, null);
                    if (objValue != null && !string.IsNullOrEmpty(objValue.ToString()))
                    {
                        var custonAttr = item.GetCustomAttributes<DataTypeAttribute>().FirstOrDefault();
                        if (custonAttr != null && custonAttr.DataType == DataType.MultilineText)
                        {
                            xml.AppendFormat("<{0}><![CDATA[{1}]]></{0}>", item.Name, objValue);
                        }
                        else
                        {
                            xml.AppendFormat("<{0}>{1}</{0}>", item.Name, objValue);
                        }
                    }
                }
                catch { }
            }
            xml.Append("</xml>");

            return xml.ToString();
        }

    }
}
