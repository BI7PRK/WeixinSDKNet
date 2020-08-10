using System;
using System.Collections.Generic;
using WeixinSDK.Net.Interface;

namespace WeixinSDK.Net.Official
{

    public class DowloadMediaResponse : ResponseBase
    {
        public byte[] Buffer { get; set; }
    }

    internal class DowloadMediaRequest : RequestBase<DowloadMediaResponse>
    {

        public override DowloadMediaResponse Get(Dictionary<string, object> param)
        {
            var obj = base.Get(param);
            try
            {
                obj.Buffer = Convert.FromBase64String(obj.ResonseBody);
                obj.setError(false);
                obj.Message = "OK";
                return obj;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
            }
            return obj;

        }
    }

    public sealed partial class MediaAPI
    {

        #region 文件传输
        /// <summary>
        /// 下载临时素材
        /// </summary>
        /// <param name="config"></param>
        /// <param name="mediaId">媒体ID</param>
        /// <returns>返回保存路径</returns>
        public static DowloadMediaResponse DownloadFile(IAppConfig config, string mediaId)
        {
            var obj = new DowloadMediaRequest();
            obj.MethodName = "media/get";
            obj.ServiceUrl = BaseConfig.WECHAT_FILE_URL;
            obj.AccessToken = config.AccessToken;
            var dict = new Dictionary<string, object>();
            dict.Add("media_id", mediaId);
            return obj.Get(dict);
        }
        
        #endregion


    }
}
