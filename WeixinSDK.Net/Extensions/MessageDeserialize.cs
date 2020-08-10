using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinSDK.Net.Entity;

namespace WeixinSDK.Net.Extensions
{
    /// <summary>
    /// 消息转换扩展
    /// </summary>
    public static class MessageDeserialize
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static TextMessage ToText(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<TextMessage>();
        }

        /// <summary>
        /// 图片消息
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static ImageMessage ToImage(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<ImageMessage>();
        }


        /// <summary>
        /// 语音消息
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static VoiceMessage ToVoice(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<VoiceMessage>();
        }

        /// <summary>
        /// 视频消息
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static VideoMessage ToVideo(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<VideoMessage>();
        }
        /// <summary>
        /// 音乐消息
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static MusicMessage ToMusic(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<MusicMessage>();
        }
        
        /// <summary>
        /// 接收位置消息
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static LocationMessage ToLocationMessage(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<LocationMessage>();
        }

        /// <summary>
        /// 接收服务器的 Ticket 验证
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static ComponentTicket ToComponentTicket(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<ComponentTicket>();
        }

        /// <summary>
        /// 取消授权事件
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static UnAuthorizedMessage ToUnAuthorizedMessage(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<UnAuthorizedMessage>();
        }

        /// <summary>
        /// 授权成功事件
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static AuthorizedMessage ToAuthorizedMessage(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<AuthorizedMessage>();
        }

        /// <summary>
        /// 链接消息
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static LinkMessage ToLink(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<LinkMessage>();
        }

        /// <summary>
        /// 关注事件。注意，未关注用户在扫描二维码是也会触发这个事件。
        /// <pre>EventKey值，qrscene_为前缀，后面为二维码的参数值</pre>
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static SubscribeEvent ToSubscribe(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<SubscribeEvent>();
        }


        /// <summary>
        /// 取消关注事件
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static UnSubscribeEvent ToUnSubscribe(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<UnSubscribeEvent>();
        }


        /// <summary>
        /// 二维码扫描
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static ScanEvent ToScan(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<ScanEvent>();
        }

        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static MenuEvent ToMenuTouch(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<MenuEvent>();
        }


        /// <summary>
        /// 位置推送事件
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static LocationEvent ToLocationEvent(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<LocationEvent>();
        }

        /// <summary>
        /// 群发完成后的事件
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static MasssendJobEvent ToMasssendjobfinish(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<MasssendJobEvent>();
        }

        /// <summary>
        /// 连接客服事件
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static ServiceSwitchEvent ToKFContact(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<ServiceSwitchEvent>();
        }

        /// <summary>
        /// 连接客服事件
        /// </summary>
        /// <param name="msgobj"></param>
        /// <returns></returns>
        public static VerifySuccess VerifySuccess(this ReceiveMessage msgobj)
        {
            return msgobj.MessageDeserialize<VerifySuccess>();
        }


    }
}
