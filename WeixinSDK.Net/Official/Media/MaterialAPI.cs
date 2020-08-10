using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeixinSDK.Net.Entity;
using WeixinSDK.Net.Enums;
using WeixinSDK.Net.Extensions;
using WeixinSDK.Net.Interface;

namespace WeixinSDK.Net.Official
{

    #region AddMaterialResponse
    public class AddMaterialResponse : ResponseBase
    {
        public string MediaId { get; set; }
        public string Url { get; set; }
    }

    internal class AddMaterialRequest : RequestBase<AddMaterialResponse>
    {
        class map
        {
            public string media_id { get; set; }
            public string url { get; set; }
        }

        public override AddMaterialResponse FormPost(Dictionary<string, string> formdata, Dictionary<string, string> file)
        {
            var obj = base.FormUpload(formdata, file);
            if (!obj.IsError)
            {
                var result = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (result != null)
                {
                    obj.MediaId = result.media_id;
                    obj.Url = result.url;
                }
            }
            return obj;
        }

    }
    #endregion


    #region MaterialTypeCountResponse
    public class MaterialTypeCountResponse : ResponseBase
    {

        public int VideoCount { get; set; }
        public int VoiceCount { get; set; }
        public int ImageCount { get; set; }
        public int NewsCount { get; set; }
    }

    internal class MaterialTypeCountRequest : RequestBase<MaterialTypeCountResponse>
    {

        private class map
        {
            public int voice_count { get; set; }
            public int video_count { get; set; }
            public int image_count { get; set; }
            public int news_count { get; set; }
        }

        public override MaterialTypeCountResponse Get(Dictionary<string, object> param = null)
        {
            var obj = base.Get();
            if (!obj.IsError)
            {
                var result = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (result != null)
                {
                    obj.ImageCount = result.image_count;
                    obj.NewsCount = result.news_count;
                    obj.VideoCount = result.video_count;
                    obj.VoiceCount = result.voice_count;
                }
            }
            return obj;
        }
        
    }
    #endregion

    #region MaterialListResponse
    public class MaterialListResponse : ResponseBase
    {
       
        /// <summary>
        /// 图文素材列表
        /// </summary>
        public MaterList MaterList { get; set; }
    }

    internal class MaterialListRequest : RequestBase<MaterialListResponse>
    {
        public override MaterialListResponse Post(object entity)
        {
            var obj = base.Post(entity);
            if (!obj.IsError)
            {
                try
                {
                    obj.MaterList = JsonConvert.DeserializeObject<MaterList>(obj.ResonseBody);
                }
                catch
                {

                }

            }
            return obj;
        }
        
    }

    #endregion


    public sealed class MaterialAPI
    {
        #region 素材管理

        /// <summary>
        /// 新增其他类型永久素材
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="file"></param>
        /// <param name="mtype"></param>
        /// <param name="title">在上传视频时，这里必须填写标题</param>
        /// <param name="introduction">在上传视频时，这里必须填写素材的描述信息</param>
        /// <returns></returns>
        public static AddMaterialResponse UploadMaterialMedia(string accessToken, string file, MediaType mtype, string title = "", string introduction = "")
        {
            var err = new AddMaterialResponse();
            var fileInfo = new FileInfo(file);
            if (!fileInfo.Exists)
            {
                return err;
            }
            err.StateCode = 40004L;
            long maxLength = 0L;
            switch (mtype)
            {
                case MediaType.Image: //图片（image）: 2M，支持JPG格式 
                    if (!Regex.IsMatch(fileInfo.Extension, "\\.(bmp|png|jpeg|jpg|gif)", RegexOptions.IgnoreCase))
                    {
                        err.Message = "不支持“" + fileInfo.Extension + "”图片格式";
                        return err;
                    }
                    maxLength = 1024 * 2 * 1024L;
                    err.StateCode = 40009L;
                    break;
                case MediaType.Voice: //语音（voice）：5M，播放长度不超过60s，支持AMR\MP3格式 
                    if (!Regex.IsMatch(fileInfo.Extension, "\\.(mp3|wma|wav|amr)", RegexOptions.IgnoreCase))
                    {
                        err.Message = "不支持“" + fileInfo.Extension + "”语音格式";
                        return err;
                    }
                    maxLength = 1024 * 5 * 1024L;
                    err.StateCode = 40010L;
                    break;
                case MediaType.Video: //视频（video）：10MB，支持MP4格式 
                    {
                        if (!Regex.IsMatch(fileInfo.Extension, "\\.(mp4|flv)", RegexOptions.IgnoreCase))
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
            
            var obj = new AddMaterialRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.AccessToken = accessToken;
            obj.MethodName = "material/add_material?type=" + mtype.ToString().ToLower();
            
            var formdata = new Dictionary<string, string>();
            if (mtype == MediaType.Video)
            {
  
                formdata.Add("description", JsonConvert.SerializeObject(new { title, introduction }));
            }

            var files = new Dictionary<string, string>
            {
                { "media",  file }
            };

            return obj.FormPost(formdata, files);
        }

        /// <summary>
        /// 上传图文信息（永久素材）
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="newList">文章列表，最多10条</param>
        /// <param name="showCover">是否在文中显示主题图片</param>
        /// <returns></returns>
        private static List<object> FormaetArticles(string accessToken, List<UploadNewEnity> newList, bool showCover)
        {
            if (newList.Count > 10)
            {
                newList.RemoveRange(10, newList.Count - 10);
            }

            var midList = new List<object>();
            foreach (var item in newList)
            {
                var objUp = UploadMaterialMedia(accessToken, item.Thumbmid, MediaType.Image);
                if (!string.IsNullOrEmpty(objUp.MediaId))
                {
                    midList.Add(new
                    {
                        thumb_media_id = objUp.MediaId,
                        author = item.Author,
                        title = item.Title,
                        content_source_url = item.SourceUrl,
                        content = item.Content,
                        digest = item.Digest,
                        show_cover_pic = (showCover ? 1 : 0)
                    });
                }
            }
            return midList;
        }


        /// <summary>
        /// 新增永久图文素材
        /// </summary>
        /// <param name="accessToken">文章列表，最多10条</param>
        /// <param name="newList">文章列表，最多10条</param>
        /// <param name="showCover">是否在文中显示主题图片</param>
        /// <returns></returns>
        public static UploadMediaResponse AddArticles(string accessToken, List<UploadNewEnity> newList, bool showCover)
        {

            var list = FormaetArticles(accessToken, newList, showCover);

            if (list.Count == 0)
            {
                return new UploadMediaResponse()
                {
                    Message = "媒体文件上传失败",
                    StateCode = -1
                };
            }
            var obj = new UploadMediaRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "material/add_news";
            obj.AccessToken = accessToken;
            return obj.Post(new { articles = list });
        }
        /// <summary>
        /// 更新永久图文消息
        /// </summary>
        /// <param name="accessToken">文章列表，最多10条</param>
        /// <param name="mid">图文素材ID</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0 </param>
        /// <param name="newList"></param>
        /// <param name="showCover">是否在内容显示图片</param>
        /// <returns></returns>
        public ResponseBase UpldateArticles(string accessToken, string mid, int index, List<UploadNewEnity> newList, bool showCover)
        {
            var list = FormaetArticles(accessToken, newList, showCover);
            if (list.Count == 0)
            {
                return new ResponseBase()
                {
                    Message = "媒体文件上传失败",
                    StateCode = -1
                };
            }

            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "material/update_news";
            obj.AccessToken = accessToken;
        
            return obj.Post(new { media_id = mid, index = index, articles = list });
        }


        /// <summary>
        /// 获取素材类别的数量
        /// </summary>
        /// <returns></returns>
        public MaterialTypeCountResponse GetMaterialTypeCount(IAppConfig config)
        {
            var obj = new MaterialTypeCountRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "material/get_materialcount";
            obj.AccessToken = config.AccessToken;
            return obj.Get();
        }
        /// <summary>
        /// 获取其他类型（图片、语音、视频）的永久素材列表
        /// </summary>
        /// <param name="accessToken">文章列表，最多10条</param>
        /// <param name="index">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回 </param>
        /// <param name="count">返回素材的数量，取值在1到20之间 </param>
        /// <param name="type">素材的类型（图片、语音、视频）</param>
        /// <returns></returns>
        public MaterialListResponse GetMaterialList(string accessToken, int index, int count, MaterialType type)
        {
            count = Math.Min(20, count);
            var obj = new MaterialListRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "material/batchget_material";
            obj.AccessToken = accessToken;
            return obj.Post(new
            {
                type = type.GetDefaultValue(),
                offset = index,
                count = count
            });
            
        }
        /// <summary>
        /// 获取永久图文消息素材列表
        /// </summary>
        /// <param name="accessToken">文章列表，最多10条</param>
        /// <param name="index">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回 </param>
        /// <param name="count">返回素材的数量，取值在1到20之间 </param>
        /// <returns></returns>
        public MaterialListResponse GetNewMaterialList(string accessToken, int index, int count)
        {
            count = Math.Min(20, count);
            var obj = new MaterialListRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "material/batchget_material";
            obj.AccessToken = accessToken;
            return obj.Post(new { type = "news", offset = index, count = count });
        }

        
        #endregion
    }
}
