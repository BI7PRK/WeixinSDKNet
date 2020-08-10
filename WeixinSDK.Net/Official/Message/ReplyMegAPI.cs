
using System;
using System.Collections.Generic;
using System.Web;
using WeixinSDK.Net.Entity;
using WeixinSDK.Net.Enums;
using WeixinSDK.Net.Extensions;
using WeixinSDK.Net.Interface;

namespace WeixinSDK.Net.Official
{
    public sealed class ReplyMegAPI
    {
        #region 发送被动信息
        /// <summary>
        /// 发送回复消息
        /// </summary>
        /// <param name="obj">发送消息的XML内容</param>
        /// <param name="config">公众号接口配置</param>
        private static ExceResult ReplyMessage(IXmlFormat obj, IAppConfig config)
        {
            var sTimeStamp = DateTime.Now.ToUnixSeconds().ToString();
            var sNonce = Math.Floor((new Random()).NextDouble() * 1000000000D).ToString();

            var xml = obj.GetXmlString();

            var msg = "OK";
            var encryMsg = "";
            try
            {
                if (config.EncryptType != EncryptType.Plaintext)
                {
                    var status = MessageEncryptHelper.MessageEncrypt(config, xml, sTimeStamp, sNonce, ref encryMsg);
                    if (status != MessageEncryptHelper.CryptErrorCode.CryptErrorCode_OK)
                    {
                        
                        HttpContext.Current.Response.Write("success");

                        return new ExceResult
                        {
                            IsError = true,
                            Message = status.GetDescription()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "text/xml";
            //HttpContext.Current.Response.StatusCode = 200;
            //HttpContext.Current.Response.SubStatusCode = 200;

            if(config.EncryptType != EncryptType.Security)
            {
                //HttpContext.Current.Response.Output.Write(xml);
            }
            else if(msg == "OK")
            {
                HttpContext.Current.Response.Output.Write(encryMsg);
            }
           
            //HttpContext.Current.Response.Flush();
            return new ExceResult { IsError = msg != "OK", Message = msg };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">公众号配置</param>
        /// <param name="content">内容</param>
        /// <param name="minfo">回复对象:ToUserName & FromUserName</param>
        /// <returns></returns>
        public static ExceResult ReplyText(IAppConfig config, string content, IMessageBase minfo)
        {
            var msg = new ReplyText()
            {
                CreateTime = DateTime.Now.ToUnixSeconds(),
                FromUserName = minfo.ToUserName,
                ToUserName = minfo.FromUserName,
                Content = content
            };
            return ReplyMessage(msg, config);
        }


        /// <summary>
        /// 发送被动响应图片信息，图片上传失败，则返回失败
        /// </summary>
        /// <param name="config">接收方</param>
        /// <param name="info">接收方</param>
        /// <param name="imgPath">图片绝对路径(最大128K，目前只支持jpg格式)</param>
        /// <returns>是否成功</returns>
        public static ExceResult ReplyImageMessage(IAppConfig config, Entity.MessageBase info, string imgPath)
        {

            var obj = MediaAPI.UploadMessageMedia(config.AccessToken, imgPath, MediaType.Image);
            if (!string.IsNullOrEmpty(obj.MediaId))
            {
                var msg = new ReplyImage
                {
                    CreateTime = DateTime.Now.ToUnixSeconds(),
                    FromUserName = info.ToUserName,
                    ToUserName = info.FromUserName,
                    Image = new ReplyMediaId { MediaId = obj.MediaId }
                };
                return ReplyMessage(msg, config);
            }
            return new ExceResult { IsError = true, Message = obj.Message };
        }

        /// <summary>
        /// 发送被动响应语音消息
        /// </summary>
        /// <param name="config"></param>
        /// <param name="info">接收方</param>
        /// <param name="voicePath">语音文件路径(支持AMR\MP3,最大256K，播放长度不超过60s)</param>
        /// <returns>是否成功</returns>
        public static ExceResult SendVoiceReplyMessage(IAppConfig config, Entity.MessageBase info, string voicePath)
        {
            var obj = MediaAPI.UploadMessageMedia(config.AccessToken, voicePath, MediaType.Voice);
            if (!string.IsNullOrEmpty(obj.MediaId))
            {
                var msg = new ReplyVoice()
                {
                    CreateTime = DateTime.Now.ToUnixSeconds(),
                    FromUserName = info.ToUserName,
                    ToUserName = info.FromUserName,
                    Voice = new ReplyMediaId { MediaId = obj.MediaId }
                };

                return ReplyMessage(msg, config);
            }
            return new ExceResult { IsError = true, Message = obj.Message };
        }


        /// <summary>
        /// 发送被动响应视频消息
        /// </summary>
        /// <param name="config"></param>
        /// <param name="info">接收方</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="videoPath">视频文件路径(1MB，支持MP4格式)</param>
        /// <returns>是否成功</returns>
        public static ExceResult SendVideoReplyMessage(IAppConfig config, Entity.MessageBase info, string title, string description, string videoPath)
        {
            var obj = MediaAPI.UploadMessageMedia(config.AccessToken, videoPath, MediaType.Voice);
            if (string.IsNullOrEmpty(obj.MediaId))
            {
                return new ExceResult { IsError = true, Message = obj.Message };
            }
            else
            {
                var msg = new ReplyVideo()
                {
                    CreateTime = DateTime.Now.ToUnixSeconds(),
                    FromUserName = info.ToUserName,
                    ToUserName = info.FromUserName,
                    Video = new ReplyVideoInfo
                    {
                        MediaId = obj.MediaId,
                        Description = description,
                        Title = title
                    }
                };

                return ReplyMessage(msg, config);
            }
        }


        /// <summary>
        /// 发送被动响应音乐消息
        /// </summary>
        /// <param name="config"></param>
        /// <param name="info">接收方</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高质量音乐链接</param>
        /// <param name="thumbMediaFilePath">缩略图文件路径(64KB，支持JPG格式 )</param>
        /// <returns>是否成功</returns>
        public static ExceResult SendMusicReplyMessage(IAppConfig config, Entity.MessageBase info, string title, string description, string musicUrl, string hqMusicUrl, string thumbMediaFilePath)
        {

            var obj = MediaAPI.UploadMessageMedia(config.AccessToken, thumbMediaFilePath, MediaType.Thumb);
            if (string.IsNullOrEmpty(obj.MediaId))
            {
                //LogHelper.WriteError("SendMusicReplyMessage => UploadFile:\r\n" + obj.Message);
                return new ExceResult { IsError = true, Message = obj.Message };
            }
            else
            {
                var msg = new ReplyMusic()
                {
                    CreateTime = DateTime.Now.ToUnixSeconds(),
                    FromUserName = info.ToUserName,
                    ToUserName = info.FromUserName,
                    Music = new ReplyMusicInfo
                    {
                        Description = description,
                        Title = title,
                        HQMusicUrl = hqMusicUrl,
                        MusicUrl = musicUrl,
                        ThumbMediaId = obj.MediaId
                    }
                };

                return ReplyMessage(msg, config);
            }

        }

        /// <summary>
        /// 回复图文被动信息
        /// </summary>
        /// <param name="config"></param>
        /// <param name="info"></param>
        /// <param name="news"></param>
        /// <returns></returns>
        public static ExceResult SendNewsReplyMessage(IAppConfig config, Entity.MessageBase info, List<item> news)
        {

            var msg = new ReplyNews
            {
                ArticleCount = news.Count,
                Articles = news,
                CreateTime = DateTime.Now.ToUnixSeconds(),
                FromUserName = info.ToUserName,
                ToUserName = info.FromUserName,
            };
            return ReplyMessage(msg, config);
        }
        #endregion
    }
}
