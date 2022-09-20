using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Http
{

    internal class HttpResult
    {
        public string Message { get; set; } 
        public bool IsSuccess { get; set; }
        public string Body { get; set; }

        public static HttpResult Error(string message)
        {
            return new HttpResult
            {
                Message = message
            };
        } 
    }
    internal class HttpProxy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<HttpResult> PostAsync(string url, string  data)
        {
            try
            {
                if (data == null) return  HttpResult.Error("提交数据不能为空");
#if DEBUG
            Console.WriteLine(data);
#endif
                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 5);
                    var res = await client.PostAsync(url, new StringContent(data));
                    return new HttpResult
                    {
                        IsSuccess = res.IsSuccessStatusCode,
                        Message = res.IsSuccessStatusCode ? "OK" : res.ReasonPhrase,
                        Body = await res.Content.ReadAsStringAsync()
                    };
                }
            }
            catch (Exception ex)
            {

                return HttpResult.Error(ex.Message);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static async Task<string> GetStringAsync(string url, Dictionary<string, object> dict = null)
        {
            if (dict != null)
            {
                url += url.LastIndexOf('?') == -1 ? "?" : "&";
                url += string.Join("&", dict.Select(s =>
                {
                    return $"{s.Key}={s.Value}";
                }));
            }
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 30);
                return await client.GetAsync(url)
                    .Result
                    .Content
                    .ReadAsStringAsync();
            }
        }
        

        #region 表单模拟
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        private static int DefaultTimeout = 20 * 1000; //20秒

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }

        private static WebProxy SetWebProxy
        {
            get
            {
                return new WebProxy
                {
                    UseDefaultCredentials = false
                };
            }
        }
        private static HttpWebRequest CreateRequest(string url, string method)
        {
            HttpWebRequest requst = (HttpWebRequest)HttpWebRequest.Create(url);
            requst.Proxy = new WebProxy
            {
                UseDefaultCredentials = false
            };
            requst.UserAgent = DefaultUserAgent;
            requst.KeepAlive = true;
            requst.Timeout = 100000;
            requst.Method = method;
            return requst;
        }

        /// <summary>
        /// 模拟表单提交的数据实体
        /// </summary>
        public class FormPostData
        {
            /// <summary>
            /// 提交地址
            /// </summary>
            public string Url { get; set; }
            /// <summary>
            /// 文件项提交
            /// </summary>
            public Dictionary<string, string> FileField { get; set; }
            /// <summary>
            /// 普通项提交
            /// </summary>
            public Dictionary<string, string> FormField { get; set; }
        }

        private static KeyValuePair<FileStream, byte[]> FileFieldHeaderBuufer(KeyValuePair<string, string> dict, string block, out long sumlen)
        {
            Encoding ascii = Encoding.ASCII;
            StringBuilder sb = new StringBuilder();
            sb.Append(block);
            sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type:application/octet-stream",
                new object[] { dict.Key, Path.GetFileName(dict.Value) });
            sb.Append("\r\n");
            sb.Append("\r\n");
            byte[] header = ascii.GetBytes(sb.ToString());
            FileStream stream = new FileStream(dict.Value, FileMode.Open, FileAccess.Read);
            sumlen = header.Length + stream.Length;
            return new KeyValuePair<FileStream, byte[]>(stream, header);
        }

        private static byte[] FormFieldHeaderBuufer(KeyValuePair<string, string> dict, string block)
        {
            Encoding ascii = Encoding.ASCII;
            StringBuilder sb = new StringBuilder();
            sb.Append(block);
            sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", new object[] { dict.Key, dict.Value });
            return ascii.GetBytes(sb.ToString());
        }
        /// <summary>
        /// 模拟表单上传文件(支持表单数据及多文件)
        /// </summary>
        /// <param name="data">提交的数据</param>
        /// <returns>返回服务器响应</returns>
        public static string FormPostFile(FormPostData data)
        {
            Encoding ascii = Encoding.ASCII;
            string itemBlock = "-----------------------------" + DateTime.Now.Ticks.ToString("x") + "\r\n";
            byte[] newLineBuff = ascii.GetBytes("\r\n");
            byte[] endBuff = ascii.GetBytes(itemBlock);
            var fileArr = new List<KeyValuePair<FileStream, byte[]>>();
            long fsize = 0;
            if (data.FileField != null && data.FileField.Count > 0)
            {
                foreach (var item in data.FileField)
                {
                    var s = 0L;
                    fileArr.Add(FileFieldHeaderBuufer(item, itemBlock, out s));
                    fsize += s;
                }
            }
            MemoryStream ms = new MemoryStream();
            byte[] buffItem;
            if (data.FormField != null && data.FormField.Count > 0)
            {
                foreach (var item in data.FormField)
                {
                    buffItem = FormFieldHeaderBuufer(item, itemBlock);
                    ms.Write(buffItem, 0, buffItem.Length);
                }
            }
            var headerBuff = ms.ToArray();
            ms.Close();

            try
            {
                HttpWebRequest requst = CreateRequest(data.Url, "POST");
                requst.ContentType = "multipart/form-data; boundary=" + itemBlock.Substring(2);
                //requst.ContentLength = fsize + headerBuff.Length + endBuff.Length;
                using (var oRequestStream = requst.GetRequestStream())
                {
                    oRequestStream.Write(headerBuff, 0, headerBuff.Length);

                    foreach (var item in fileArr)
                    {
                        oRequestStream.Write(item.Value, 0, item.Value.Length);
                        var reader = item.Key;
                        if (reader.Length == 0)
                            continue;
                        byte[] readBuff = new byte[checked((int)Math.Min(4096, reader.Length))];
                        int read = (int)reader.Length;
                        while ((read = reader.Read(readBuff, 0, readBuff.Length)) > 0)
                        {
                            oRequestStream.Write(readBuff, 0, readBuff.Length);
                        }
                        reader.Close();
                        oRequestStream.Write(newLineBuff, 0, newLineBuff.Length);
                    }
                    oRequestStream.Write(endBuff, 0, endBuff.Length);
                }

                using (var oWResponse = requst.GetResponse())
                {
                    Stream s = oWResponse.GetResponseStream();
                    return new StreamReader(s).ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        #endregion
    }
}
