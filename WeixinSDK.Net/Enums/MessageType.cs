using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Enums
{
    /// <summary>
    /// 微信响应的消息
    /// </summary>
    [Serializable()]
    [Description("信息类型")]
    public enum MessageType : byte
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None,
        /// <summary>
        /// 文本消息
        /// </summary>
        [Description("文本消息")]
        Text,
        /// <summary>
        /// 图片消息
        /// </summary>
        [Description("图片消息")]
        Image,
        /// <summary>
        /// 语音消息
        /// </summary>
        [Description("语音消息")]
        Voice,
        /// <summary>
        /// 视频消息
        /// </summary>
        [Description("视频消息")]
        Video,
        /// <summary>
        /// 音乐消息
        /// </summary>
        [Description("音乐消息")]
        Music,
        /// <summary>
        /// 短视频消息
        /// </summary>
        [Description("短视频消息")]
        Shortvideo,
        /// <summary>
        /// 图文消息
        /// </summary>
        [Description("图文消息")]
        News,
        /// <summary>
        /// 位置消息
        /// </summary>
        [Description("位置消息")]
        Location,
        /// <summary>
        /// 链接消息
        /// </summary>
        [Description("链接消息")]
        Link,
        /// <summary>
        /// 事件推送.例如：关注、点击自定义菜单、扫码。
        /// </summary>
        [Description("事件推送")]
        Event,
        /// <summary>
        /// 切换到多客服系统
        /// </summary>
        [Description("切换到多客服系统")]
        Transfer_customer_service,

        /// <summary>
        /// 推送component_verify_ticket协议
        /// </summary>
        [Description("推送component_verify_ticket协议")]
        Component_verify_ticket,

        /// <summary>
        /// 取消授权通知
        /// </summary>
        [Description("取消授权通知")]
        Unauthorized,

        /// <summary>
        /// 授权成功通知
        /// </summary>
        [Description("授权成功通知")]
        Authorized,

        /// <summary>
        /// 授权更新通知
        /// </summary>
        [Description("授权更新通知")]
        Updateauthorized,

        /// <summary>
        /// 门店审核事件推送
        /// </summary>
        Poi_check_notify,

        /// <summary>
        /// 门店审核事件推送
        /// </summary>
        [Description("买单事件")]
        User_pay_from_pay_cell

    }
}
