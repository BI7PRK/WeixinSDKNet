using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeixinSDK.Net.Enums;
using WeixinSDK.Net.Extensions;

namespace WeixinSDK.Net.Official
{
    public class UploadMediaResponse : ResponseBase
    {
        public MediaType MediaType { get; set; }
        public string MediaId { get; set; }
        public DateTime CreateTime { get; set; }
    }


    internal class UploadMediaRequest : RequestBase<UploadMediaResponse>
    {
        class map
        {
            public string type { get; set; }
            public string media_id { get; set; }
            public int created_at { get; set; }


        }

        public override UploadMediaResponse Upload(string file)
        {
            var obj = base.Upload(file);
            if (!obj.IsError)
            {
                var result = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (result != null)
                {
                    MediaType mt = MediaType.Image;
                    if (Enum.TryParse<MediaType>(result.type.ToUpperFirst(), out mt))
                    {
                        obj.MediaType = mt;
                    }
                    obj.MediaId = result.media_id;
                    obj.CreateTime = result.created_at.FromUnixSeconds();
                }
            }
            return obj;
        }
    }


    #region 上传图片
    public class UploadImageResponse : ResponseBase
    {
        public string Url { get; set; }
    }


    internal class UploadImageRequest : RequestBase<UploadImageResponse>
    {
        class map
        {
            public string url { get; set; }


        }

        public override UploadImageResponse Upload(string file)
        {
            var obj = base.Upload(file);
            if (!obj.IsError)
            {
                var result = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (result != null)
                {
                    obj.Url = result.url;
                }
            }
            return obj;
        }
    }
    #endregion

    public sealed partial class MediaAPI
    {

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static UploadImageResponse UploadImage(string file, string token)
        {
            var obj = new UploadImageRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "media/uploadimg";
            obj.AccessToken = token;
            return obj.Upload(file);
        }



        /// <summary>
        /// 上传消息媒体文件。此上传接口为临时素材接口
        /// <para>图片（image）: 1M，支持JPG格式 </para>
        /// <para>语音（voice）：2M，播放长度不超过60s，支持AMR\MP3格式 </para>
        /// <para>视频（video）：10MB，支持MP4格式 </para>
        /// <para>缩略图（thumb）：64KB，支持JPG格式 </para>
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="file">要上传的文件</param>
        /// <param name="type">文件类型</param>
        /// <returns></returns>
        public static UploadMediaResponse UploadMessageMedia(string accessToken, string file, MediaType type)
        {

            var err = new UploadMediaResponse();
            err.StateCode = 40004L;
            var fileInfo = new FileInfo(file);
            if (!fileInfo.Exists)
            {
                err.Message = "未找到文件:" + file;
                return err;
            }
            long maxLength = 0L;
            switch (type)
            {
                case MediaType.Image: //图片（image）: 1M
                    if (!Regex.IsMatch(fileInfo.Extension, "\\.(bmp|png|jpeg|jpg|gif)", RegexOptions.IgnoreCase))
                    {
                        err.Message = "不支持“" + fileInfo.Extension + "”图片格式";
                        return err;
                    }
                    maxLength = 1024 * 1024L;
                    err.StateCode = 40009L;
                    break;
                case MediaType.Voice: //语音（voice）：2M，播放长度不超过60s，支持AMR\MP3格式 
                    if (!Regex.IsMatch(fileInfo.Extension, "\\.(mp3|amr)", RegexOptions.IgnoreCase))
                    {
                        err.Message = "不支持“" + fileInfo.Extension + "”语音格式";
                        return err;
                    }
                    maxLength = 1024 * 2 * 1024L;
                    err.StateCode = 40010L;
                    break;
                case MediaType.Video: //视频（video）：10MB，支持MP4格式 
                    {
                        if (!Regex.IsMatch(fileInfo.Extension, "\\.(mp4)", RegexOptions.IgnoreCase))
                        {
                            err.Message = "不支持“" + fileInfo.Extension + "”视频格式";
                            return err;
                        }
                    }
                    maxLength = 10 * 1024 * 1024L;
                    err.StateCode = 40011L;
                    break;
                case MediaType.Thumb: //缩略图（thumb）：64KB，支持JPG格式 
                    maxLength = 64 * 1024L;
                    err.StateCode = 40012L;
                    break;
            }

            if (fileInfo.Length > maxLength)
            {
                err.Message = string.Format("文件大小不能超过{0}KB", maxLength / 1024L);
                return err;
            }
            var obj = new UploadMediaRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "media/upload?type=" + type.ToString().ToLower();
            obj.AccessToken = accessToken;
            return obj.Upload(file);
        }

        

        
    }
}
