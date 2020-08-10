using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WeixinSDK.Net.Interface;
using WeixinSDK.Net.OAuth2;

namespace WeixinSDK.Net.Extensions
{
    public static class StringExtension
    {

        /// <summary>
        /// 将字母首字大写化
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToUpperFirst(this string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)) return s;
            string first = s.Substring(0, 1).ToUpper();
            string any = s.Substring(1).ToLower();
            return string.Concat(first, any);
        }


        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSHA1Encrypt(this string str)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            byte[] bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }


        /// <summary>
        /// 生成提交字符串
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static string ToFormString(this IParams entity)
        {
            if (entity == null)
            {
                return "";
            }
            var entyType = entity.GetType();
            List<string> strUrl = new List<string>();
            foreach (var item in entyType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var useValue = item.GetValue(entity, null);
                var name = item.Name;
                var customAttr = (ParamNameAttribute)item.GetCustomAttributes(typeof(ParamNameAttribute), true).FirstOrDefault();
                if (customAttr != null)
                {
                    name = customAttr.Name;
                    var value = customAttr.DefaultValue;
                    if (useValue == null && value != null)
                    {
                        useValue = value;
                    }
                }

                if (!string.IsNullOrEmpty(name) && useValue != null)
                {
                    strUrl.Add(name + "=" + useValue);
                }
            }
            return string.Join("&", strUrl.ToArray());
        }

        internal static string ToFormString(this Dictionary<string, object> dict)
        {
            if (dict == null || dict.Count == 0) return string.Empty;
            return string.Join("&", dict.Select(s =>
            {
                return $"{s.Key}={s.Value}";
            }));
        }
    }
}
