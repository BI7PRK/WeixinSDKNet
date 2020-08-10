using WeixinSDK.Net.Enums;
using WeixinSDK.Net.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    /// <summary>
    /// 事件消息基础类
    /// </summary>
    [Serializable]
    public abstract class EventBase : IMessageBase
    {
        /// <summary>
        /// 消息目标对象
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 消息来源对象
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 事件值，有些事件有这个值，有些没有。
        /// </summary>
        public string EventKey { get; set; }
    }

    /// <summary>
    /// <para>Event名称：</para>
    /// <para>scancode_push：扫码推事件的事件推送  </para>
    /// <para>scancode_waitmsg：扫码推事件且弹出“消息接收中”提示框的事件推送 </para>
    /// </summary>
    [Serializable]
    public class ScancodeEvent : EventBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ScanCodeInfo ScanCodeInfo { get; set; }
    }

    [Serializable]
    public class ScanCodeInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string ScanType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ScanResult { get; set; }
    }

    /// <summary>
    /// 扫描二维码事件
    /// </summary>
    [Serializable()]
    public class ScanEvent : EventBase
    {
        /// <summary>
        /// <para>1、如果用户还未关注公众号，则用户可以关注公众号，关注后微信会将带场景值关注事件推送给开发者。</para>
        /// <para>　a) 事件KEY值，qrscene_为前缀，后面为二维码的参数值 </para>
        /// <para>2、如果用户已经关注公众号，则微信会将带场景值扫描事件推送给开发者。 </para>
        /// <para>　a) 事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id</para>
        /// </summary>
        //public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片 
        /// </summary>
        public string Ticket { get; set; }
    }

    /// <summary>
    /// 选定客服会话事件
    /// </summary>
    [Serializable()]
    public class ServiceSessionEvent : EventBase
    {
        /// <summary>
        /// 客服帐号
        /// </summary>
        public string KfAccount { get; set; }
    }

   /// <summary>
   /// 进入客服事件
   /// </summary>
    [Serializable()]
    public class ServiceSwitchEvent : EventBase
    {
        /// <summary>
        /// 源客服帐号
        /// </summary>
        public string FromKfAccount { get; set; }
        /// <summary>
        /// 转到客服帐号
        /// </summary>
        public string ToKfAccount { get; set; }
    }


    /// <summary>
    /// 模板消息事件
    /// </summary>
    public class TempMsgStatusEvent : EventBase
    {
        public long MsgID { get; set; }
        /// <summary>
        /// 发送状态。若发送失败--failed: system failed（非用户拒绝） 
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// 取消关注事件
    /// </summary>
    [Serializable()]
    public class UnSubscribeEvent : EventBase
    {
    }


    /// <summary>
    /// 关注事件
    /// </summary>
    [Serializable()]
    public class SubscribeEvent : EventBase
    {
        /// <summary>
        /// 未关注的用户在扫描二维码时，会有这个值。
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }
    }


    /// <summary>
    /// <para>Event名称：</para>
    /// <para>pic_sysphoto：弹出系统拍照发图的事件推送 </para>
    /// <para>pic_photo_or_album：弹出拍照或者相册发图的事件推送 </para>
    /// <para>pic_weixin：弹出微信相册发图器的事件推送 </para>
    /// </summary>
    [Serializable()]
    public class PhotoEvent : EventBase
    {
        public SendPicsInfo SendPicsInfo { get; set; }
    }
    [Serializable()]
    public class SendPicsInfo
    {
        public int Count { get; set; }
        public PicList PicList { get; set; }
    }
    [Serializable()]
    public class PicList
    {
        public PicItem item { get; set; }
    }

    [Serializable()]
    public class PicItem
    {
        public string PicMd5Sum { get; set; }
    }

    /// <summary>
    /// 点击菜单事件
    /// </summary>
    [Serializable()]
    public class MenuEvent : EventBase
    {
        /// <summary>
        /// <para>当点击CLICK菜单类型时： </para>
        /// <para>事件KEY值，与自定义菜单接口中KEY值对应。</para>
        /// <para>当点击VIEW菜单类型时：</para>
        /// <para>事件KEY值，设置的跳转URL </para>
        /// </summary>
        public string EventKey { get; set; }
    }

    /// <summary>
    /// 消息群发事件
    /// </summary>
    public class MasssendJobEvent : EventBase
    {
        public long MsgID { get; set; }
        public string Status { get; set; }
        public int TotalCount { get; set; }
        /// <summary>
        /// 过滤（过滤是指特定地区、性别的过滤、用户设置拒收的过滤，用户接收已超4条的过滤）后，准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount 
        /// </summary>
        public int FilterCount { get; set; }
        public int SentCount { get; set; }
        public int ErrorCount { get; set; }
    }

    
}
