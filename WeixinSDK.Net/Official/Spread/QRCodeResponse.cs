using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using WeixinSDK.Net.Http;

namespace WeixinSDK.Net.Official
{
    public class CreateQRCodeResponse : ResponseBase
    {
        public string Ticket { get; set; }
        /// <summary>
        /// 过期时长，单位为秒
        /// </summary>
        public int Expire { get; set; }
        public string Url { get; set; }
    }

    internal class CreateQRCodeRequest : RequestBase<CreateQRCodeResponse>
    {
        private class map
        {
            public string ticket { get; set; }
            public int expire_seconds { get; set; }
            public string url { get; set; }
        }

        public override CreateQRCodeResponse Post(object entity)
        {
            var obj = base.Post(entity);
            if (!obj.IsError)
            {
                var result = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (result != null)
                {
                    obj.Url = result.url;
                    obj.Ticket = result.ticket;
                    obj.Expire = result.expire_seconds;
                }
            }
            return obj;
        }
 
    }

    public class ShowQRCodeResponse : ResponseBase
    {
        public Image QRCodeData { get; set; }
    }



    internal class ShowQRCodeRequest : RequestBase<ShowQRCodeResponse>, IDisposable
    {
        private MemoryStream ms = null;
        public void Dispose()
        {
            if (ms != null) ms.Close();
        }

        public override ShowQRCodeResponse Get(Dictionary<string, object> param)
        {
            
            try
            {
                using (var client = new HttpClient())
                {
                    var obj = client.GetStreamAsync(base.QueryUri).Result;
                    return new ShowQRCodeResponse { QRCodeData = Image.FromStream(obj) };
                }
              
            }
            catch (Exception)
            {

            }
            return null;

        }
        
    }
}
