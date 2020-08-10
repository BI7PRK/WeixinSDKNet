using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinSDK.Net.Entity;
using WeixinSDK.Net.Enums;

namespace WeixinSDK.Net.Official
{
    public class FilterSendResponse : ResponseBase
    {
        public long MessageId { get; set; }
    }

    internal class FilterSendRequest : RequestBase<FilterSendResponse>
    {
        private class map
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public long msg_id { get; set; }
        }


    }

    public sealed class FilterSendAIP
    {
        #region 高级群发接口


        /// <summary>
        /// 根据群组ID群发图文信息
        /// </summary>
        /// <param name="accessToken">公众号配置</param>
        /// <param name="groupId"></param>
        /// <param name="newList"></param>
        /// <param name="showCover">是否在内容中显示主题图片</param>
        /// <returns></returns>
        public static FilterSendResponse SendAllNewsFiltergroup(string accessToken, int groupId, List<UploadNewEnity> newList, bool showCover = false)
        {
            var upObj = MaterialAPI.AddArticles(accessToken, newList, showCover);
            if (!string.IsNullOrEmpty(upObj.MediaId))
            {
                var obj = new FilterSendRequest();
                obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                obj.MethodName = "message/mass/sendall";
                obj.AccessToken = accessToken;
                return obj.Post(new
                {
                    filter = new
                    {
                        group_id = groupId
                    },
                    mpnews = new { media_id = upObj.MediaId },
                    msgtype = "mpnews"
                });
            }

            return new FilterSendResponse() { ResonseBody = upObj.ResonseBody };
        }
        /// <summary>
        /// 根据群组ID群发文本信息
        /// </summary>
        /// <param name="accessToken">公众号配置</param>
        /// <param name="groupId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static FilterSendResponse SendAllTextFiltergroup(string accessToken, int groupId, string content)
        {
            var obj = new FilterSendRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "message/mass/sendall";
            obj.AccessToken = accessToken;
            return obj.Post(new
            {
                filter = new
                {
                    group_id = groupId
                },
                text = new { content = content },
                msgtype = "text"
            });


        }

        /// <summary>
        /// 根据群组ID群发图片信息
        /// </summary>
        /// <param name="accessToken">公众号配置</param>
        /// <param name="groupId"></param>
        /// <param name="image">图片文件</param>
        /// <returns></returns>
        public static FilterSendResponse SendAllImageFiltergroup(string accessToken, int groupId, string image)
        {
            var upObj = MediaAPI.UploadMessageMedia(accessToken, image, MediaType.Image);
            if (!string.IsNullOrEmpty(upObj.MediaId))
            {
                var obj = new FilterSendRequest();
                obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                obj.MethodName = "message/mass/sendall";
                obj.AccessToken = accessToken;
                return obj.Post( new
                {
                    filter = new
                    {
                        group_id = groupId
                    },
                    image = new { media_id = upObj.MediaId },
                    msgtype = "image"
                });
            }

            return new FilterSendResponse() { ResonseBody = upObj.ResonseBody };
        }
        /// <summary>
        /// 根据群组ID群发语音信息
        /// </summary>
        /// <param name="accessToken">公众号配置</param>
        /// <param name="groupId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static FilterSendResponse SendAllVoiceFiltergroup(string accessToken, int groupId, string file)
        {
            var upObj = MediaAPI.UploadMessageMedia(accessToken, file, MediaType.Voice);
            if (!string.IsNullOrEmpty(upObj.MediaId))
            {
                var obj = new FilterSendRequest();
                obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                obj.MethodName = "message/mass/sendall";
                obj.AccessToken = accessToken;
                return obj.Post(new
                {
                    filter = new
                    {
                        group_id = groupId
                    },
                    voice = new { media_id = upObj.MediaId },
                    msgtype = "voice"
                });
            }
            return new FilterSendResponse() { ResonseBody = upObj.ResonseBody };
        }



        /// <summary>
        /// 根据群组ID群发视频信息
        /// </summary>
        /// <param name="accessToken">公众号配置</param>
        /// <param name="groupId"></param>
        /// <param name="file"></param>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static FilterSendResponse SendAllVideoFiltergroup(string accessToken, int groupId, string file, string title, string desc)
        {
            var upObj = MediaAPI.UploadMessageMedia(accessToken, file, MediaType.Video);
            if (!string.IsNullOrEmpty(upObj.MediaId))
            {
                //还需要复杂的一个步骤！
                // 查阅 http://mp.weixin.qq.com/wiki/15/5380a4e6f02f2ffdc7981a8ed7a40753.html#.E4.B8.8A.E4.BC.A0.E5.9B.BE.E6.96.87.E6.B6.88.E6.81.AF.E7.B4.A0.E6.9D.90.E3.80.90.E8.AE.A2.E9.98.85.E5.8F.B7.E4.B8.8E.E6.9C.8D.E5.8A.A1.E5.8F.B7.E8.AE.A4.E8.AF.81.E5.90.8E.E5.9D.87.E5.8F.AF.E7.94.A8.E3.80.91
                // 的视频部分
                var reUp = new UploadMediaRequest();
                reUp.ServiceUrl = BaseConfig.WECHAT_FILE_URL;
                reUp.MethodName = "media/uploadvideo";
                reUp.AccessToken = accessToken;
                var post =  new
                {
                    title = title,
                    media_id = upObj.MediaId,
                    description = desc
                };
                var result = reUp.Post(reUp);

                if (!string.IsNullOrEmpty(result.MediaId))
                {

                    var obj = new FilterSendRequest();
                    obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                    obj.MethodName = "message/mass/sendall";
                    obj.AccessToken = accessToken;
                    return obj.Post(new
                    {
                        filter = new
                        {
                            group_id = groupId
                        },
                        mpvideo = new
                        {
                            media_id = result.MediaId,
                            title = title,
                            description = desc
                        },
                        msgtype = "mpvideo"
                    });
                }
                else
                {
                    return new FilterSendResponse()
                    {
                         ResonseBody  = result.ResonseBody
                    };
                }
            }
            else
            {
                return new FilterSendResponse()
                {
                    ResonseBody = upObj.ResonseBody
                };
            }
        }




        private static string _SendAllMethod = "message/mass/send";
        /// <summary>
        /// 根据openId群发图文信息
        /// </summary>
        /// <param name="accessToken">公众号配置</param>
        /// <param name="openId"></param>
        /// <param name="newList"></param>
        /// <param name="showCover">是否在内容中显示主题图片</param>
        /// <returns></returns>
        public static FilterSendResponse SendAllNewsFilterOpenId(string accessToken, string[] openId, List<UploadNewEnity> newList, bool showCover = false)
        {
            var upObj = MaterialAPI.AddArticles(accessToken, newList, showCover);
            if (!string.IsNullOrEmpty(upObj.MediaId))
            {
                var obj = new FilterSendRequest();
                obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                obj.MethodName = _SendAllMethod;
                obj.AccessToken = accessToken;
                return obj.Post( new
                {
                    touser = openId,
                    mpnews = new
                    {
                        media_id = upObj.MediaId
                    },
                    msgtype = "mpnews"
                });
            }
            else
            {
                return new FilterSendResponse { ResonseBody = upObj.ResonseBody };
            }
        }
        /// <summary>
        /// 根据openId群发文本信息
        /// </summary>
        /// <param name="accessToken">公众号配置</param>
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public FilterSendResponse SendAllTextFilterOpenId(string accessToken, string[] openId, string content)
        {
            var obj = new FilterSendRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = _SendAllMethod;
            obj.AccessToken = accessToken;
            return obj.Post(new
            {
                touser = openId,
                text = new
                {
                    content = content

                },
                msgtype = "text"
            });
        }

        /// <summary>
        /// 根据群组ID群发图片信息
        /// </summary>
        /// <param name="accessToken">公众号配置</param>
        /// <param name="openId"></param>
        /// <param name="image">图片文件</param>
        /// <returns></returns>
        public static FilterSendResponse SendAllImageFilterOpenId(string accessToken, string[] openId, string image)
        {
            var upObj = MediaAPI.UploadMessageMedia(accessToken, image, MediaType.Image);
            if (!string.IsNullOrEmpty(upObj.MediaId))
            {
                var obj = new FilterSendRequest();
                obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                obj.MethodName = _SendAllMethod;
                obj.AccessToken = accessToken;
                return obj.Post( new
                {
                    touser = openId,
                    image = new
                    {
                        media_id = upObj.MediaId

                    },
                    msgtype = "image"
                });
            }
            else
            {
                return new FilterSendResponse()
                {
                    ResonseBody = upObj.ResonseBody
                };
            }
        }
        /// <summary>
        /// 根据群组ID群发语音信息
        /// </summary>
        /// <param name="accessToken">公众号配置</param>
        /// <param name="openId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static FilterSendResponse SendAllVoiceFilterOpenId(string accessToken, string[] openId, string file)
        {
            var upObj = MediaAPI.UploadMessageMedia(accessToken, file, MediaType.Voice);
            if (!string.IsNullOrEmpty(upObj.MediaId))
            {
                var obj = new FilterSendRequest();
                obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                obj.MethodName = _SendAllMethod;
                obj.AccessToken = accessToken;
                return obj.Post( new
                {
                    touser = openId,
                    voice = new
                    {
                        media_id = upObj.MediaId

                    },
                    msgtype = "voice"
                });
            }
            else
            {
                return new FilterSendResponse()
                {
                    ResonseBody = upObj.ResonseBody
                };
            }
        }


        /// <summary>
        /// 根据openId群发视频信息
        /// </summary>
        /// <param name="accessToken">公众号配置</param>
        /// <param name="openId"></param>
        /// <param name="file"></param>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static FilterSendResponse SendAllVideoFilterOpenId(string accessToken, string[] openId, string file, string title, string desc)
        {
            var upObj = MediaAPI.UploadMessageMedia(accessToken, file, MediaType.Video);
            if (!string.IsNullOrEmpty(upObj.MediaId))
            {
                var reUp = new UploadMediaRequest();
                reUp.ServiceUrl = BaseConfig.WECHAT_FILE_URL;
                reUp.MethodName = "media/uploadvideo";
                reUp.AccessToken = accessToken;
                var result = reUp.Post(new
                {
                    title = title,
                    media_id = upObj.MediaId,
                    description = desc
                });

                if (!string.IsNullOrEmpty(result.MediaId))
                {

                    var obj = new FilterSendRequest();
                    obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
                    obj.MethodName = _SendAllMethod;
                    obj.AccessToken = accessToken;
                    return obj.Post( new
                    {
                        touser = openId,
                        mpvideo = new
                        {
                            media_id = result.MediaId,
                            title = title,
                            description = desc
                        },
                        msgtype = "mpvideo"
                    });
                }
                else
                {
                    return new FilterSendResponse()
                    {
                       ResonseBody = result.ResonseBody
                    };
                }
            }
            else
            {
                return new FilterSendResponse()
                {
                    ResonseBody = upObj.ResonseBody
                };
            }
        }

        /// <summary>
        /// 删除群发信息
        /// (只有已经发送成功的消息才能删除删除消息只是将消息的图文详情页失效，已经收到的用户，还是能在其本地看到消息卡片。 另外，删除群发消息只能删除图文消息和视频消息，其他类型的消息一经发送，无法删除。)
        /// </summary>
        /// <param name="accessToken">公众号配置</param>
        /// <param name="msgId"></param>
        /// <returns></returns>
        public static ResponseBase DeleteSandAllMessage(string accessToken, int msgId)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "message/mass/delete";
            obj.AccessToken = accessToken;
            return obj.Post(new { msgid = msgId });
        }



        #endregion
    }
}
