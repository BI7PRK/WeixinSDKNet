using WeixinSDK.Net.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WeixinSDK.Net.Extensions
{

    internal static class XmlString
    {
        /// <summary>
        /// 将实体对象转成XML
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetXmlString(this IXmlFormat entity)
        {
            if (entity == null) return "";
            var objType = entity.GetType();
            var str = new StringBuilder();
            try
            {
                var xml = XmlWriter.Create(str);
                var obj = new XmlSerializer(objType);
                obj.Serialize(xml, entity);
                xml.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            var spName = objType.FullName.Substring(objType.FullName.LastIndexOf(".") + 1);
            var doc = new XmlDocument();
            doc.LoadXml(str.ToString());
            string format = "<xml>";
            format += " " + doc.SelectNodes(spName)[0].InnerXml;
            format += "\r\n</xml>";
            return format;
        }
    }
}
