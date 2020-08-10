using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinSDK.Net.Entity;
using WeixinSDK.Net.Enums;

namespace WeixinSDK.Net.Official
{
    public sealed class SendMegAPI
    {

        #region 向用户发送信息
        private static string _SendMessageMethodName = "message/custom/send";

        /// <summary>
        /// 向用户发送文本信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="toUser"></param>
        /// <param name="content"></param>
        /// <param name="toKf">发给指定的客服</param>
        /// <returns></returns>
        public static ResponseBase SendTextMessage(string accessToken, string toUser, string content, string toKf = null)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = _SendMessageMethodName;
            obj.AccessToken = accessToken;
            object msg;
            if (toKf == null)
            {
                msg = new
                {
                    touser = toUser,
                    msgtype = "text",
                    text = new { content }
                };
            }
            else
            {
                msg = new
                {
                    touser = toUser,
                    msgtype = "text",
                    text = new { content = content },
                    customservice = new { kf_account = toKf }
                };
            }
            return obj.Post(msg);
        }

        /// <summary>
        /// 回复图片信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="toUser">用户openid</param>
        /// <param name="file">本地图片</param>
        /// <param name="toKf">发给指定的客服</param>
        /// <returns></returns>
        public static ResponseBase SendImageMessage(string accessToken, string toUser, string file, string toKf = null)
        {
            var obj = new ResponseBase();
            //上传
            var objUp = MediaAPI.UploadMessageMedia(accessToken, file, MediaType.Image);
            if (!string.IsNullOrEmpty(objUp.MediaId))
            {
              
                object msg;
                if (toKf == null)
                {
                    msg = new
                    {
                        touser = toUser,
                        msgtype = "image",
                        image = new
                        {
                            media_id = objUp.MediaId
                        }
                    };
                }
                else
                {
                    msg = new
                    {
                        touser = toUser,
                        msgtype = "image",
                        image = new
                        {
                            media_id = objUp.MediaId
                        },
                        customservice = new { kf_account = toKf }
                    };
                }
                var send = new RequestBase<ResponseBase>();
                send.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                send.MethodName = _SendMessageMethodName;
                send.AccessToken = accessToken;
                return send.Post(msg);
            }
            else
            {
                obj.Message = objUp.Message;
                obj.ResonseBody = objUp.ResonseBody;
            }
            return obj;
        }
        /// <summary>
        /// 回复语音信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="touser"></param>
        /// <param name="file">本地语音文件</param>
        /// <param name="toKf">发给指定的客服</param>
        /// <returns></returns>
        public static ResponseBase SendVoiceMessage(string accessToken, string touser, string file, string toKf = null)
        {
            var obj = new ResponseBase();
            //上传
            var objUp = MediaAPI.UploadMessageMedia(accessToken, file, MediaType.Voice);
            if (!string.IsNullOrEmpty(objUp.MediaId))
            {
               
                object msg;
                if (toKf == null)
                {
                    msg = new
                    {
                        touser = touser,
                        msgtype = "voice",
                        voice = new
                        {
                            media_id = objUp.MediaId
                        }
                    };
                }
                else
                {
                    msg = new
                    {
                        touser = touser,
                        msgtype = "voice",
                        voice = new
                        {
                            media_id = objUp.MediaId
                        },
                        customservice = new { kf_account = toKf }
                    };
                }
                var send = new RequestBase<ResponseBase>();
                send.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                send.MethodName = _SendMessageMethodName;
                send.AccessToken = accessToken;
                return send.Post(msg);
            }
            else
            {
                obj.Message = objUp.Message;
                obj.ResonseBody = objUp.ResonseBody;
            }
            return obj;
        }

        /// <summary>
        /// 回复视频信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="toUser">用户openId</param>
        /// <param name="title">视频标题</param>
        /// <param name="desc">视频简介</param>
        /// <param name="file">本地文件</param>
        /// <param name="toKf">发给指定的客服</param>
        /// <returns></returns>
        public static ResponseBase SendVideoMessage(string accessToken, string toUser, string title, string desc, string file, string toKf = null)
        {
            var obj = new ResponseBase();
            //上传
            var objUp = MediaAPI.UploadMessageMedia(accessToken, file, MediaType.Video);
            if (!string.IsNullOrEmpty(objUp.MediaId))
            {
               
                object msg;
                if (toKf == null)
                {
                    msg = new
                    {
                        touser = toUser,
                        msgtype = "video",
                        video = new
                        {
                            media_id = objUp.MediaId,
                            title = title,
                            description = desc
                        }
                    };
                }
                else
                {
                    msg = new
                    {
                        touser = toUser,
                        msgtype = "video",
                        video = new
                        {
                            media_id = objUp.MediaId,
                            title = title,
                            description = desc
                        },
                        customservice = new { kf_account = toKf }
                    };
                }
                var send = new RequestBase<ResponseBase>();
                send.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                send.MethodName = _SendMessageMethodName;
                send.AccessToken = accessToken;
                return send.Post(msg);
            }
            else
            {
                obj.Message = objUp.Message;
                obj.ResonseBody = objUp.ResonseBody;
            }
            return obj;
        }
        /// <summary>
        /// 回复音乐
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="toUser">用户openid</param>
        /// <param name="title">音乐标题</param>
        /// <param name="desc">音乐简介</param>
        /// <param name="thumb">上传缩略图</param>
        /// <param name="musicurl">音乐地址</param>
        /// <param name="hqurl">高质量音乐地址</param>
        /// <returns></returns>
        public static ResponseBase SendMusicMessage(string accessToken, string toUser, string title, string desc, string thumb, string musicurl, string hqurl = "")
        {

            var obj = new ResponseBase();
            //上传
            var objUp = MediaAPI.UploadMessageMedia(accessToken, thumb, MediaType.Image);
            if (!string.IsNullOrEmpty(objUp.MediaId))
            {
                var msg = new
                {
                    touser = toUser,
                    msgtype = "music",
                    music = new
                    {
                        title = title,
                        description = desc,
                        musicurl = musicurl,
                        hqmusicurl = hqurl,
                        thumb_media_id = objUp.MediaId

                    }
                };
                var send = new RequestBase<ResponseBase>();
                send.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                send.MethodName = _SendMessageMethodName;
                send.AccessToken = accessToken;
                return send.Post(msg);
            }
            else
            {
                obj.Message = objUp.Message;
                obj.ResonseBody = objUp.ResonseBody;
            }
            return obj;

        }
        /// <summary>
        /// 回复图文信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="toUser">用户openid</param>
        /// <param name="list">图文内容条目（最多10条）</param>
        /// <returns></returns>
        public static ResponseBase SendNewsMessage(string accessToken, string toUser, List<NewsItem> list)
        {
            if (list.Count > 10)
            {
                list.RemoveRange(10, list.Count - 10);
            }
            
            var msg  = new
            {
                touser = toUser,
                msgtype = "news",
                news = new
                {
                    articles = list.Select(s =>
                    {
                        return new
                        {
                            title = s.Title,
                            description = s.Description,
                            url = s.Url,
                            picurl = s.PicUrl
                        };
                    }).ToArray()
                }
            };
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = _SendMessageMethodName;
            obj.AccessToken = accessToken;
            return obj.Post(msg);
        }

        #endregion
    }
}
