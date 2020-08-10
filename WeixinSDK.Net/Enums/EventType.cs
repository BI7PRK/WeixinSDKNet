using System.ComponentModel;

namespace WeixinSDK.Net.Enums
{
    public enum EventType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("无")]
        None,
        /// <summary>
        /// 关注事件
        /// </summary>
        [Description("关注事件")]
        Subscribe,
        /// <summary>
        /// 取消关注事件
        /// </summary>
        [Description("取消关注事件")]
        Unsubscribe,
        /// <summary>
        /// 扫描带参数二维码事件
        /// </summary>
        [Description("扫描带参数二维码事件")]
        Scan,
        /// <summary>
        /// 点击菜单拉取消息时事件
        /// </summary>
        [Description("点击菜单拉取消息时事件")]
        Click,
        /// <summary>
        /// 点击菜单跳转链接时事件
        /// </summary>
        [Description("点击菜单跳转链接时事件")]
        View,
        /// <summary>
        /// 上报地理位置
        /// </summary>
        [Description("上报地理位置")]
        Location,
        /// <summary>
        /// 群发信息结果事件
        /// </summary>
        [Description("群发信息结果")]
        Masssendjobfinish,
        /// <summary>
        /// 创建客服会话
        /// </summary>
        [Description("创建客服会话")]
        Kf_create_session,

        /// <summary>
        /// 关闭会话
        /// </summary>
        [Description("关闭会话")]
        Kf_close_session,
        /// <summary>
        /// 转接会话
        /// </summary>
        [Description("转接会话")]
        Kf_switch_session,
        /// <summary>
        /// 扫码推事件的事件推送 
        /// </summary>
        [Description("扫码推事件的事件推送 ")]
        Scancode_push,
        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框的事件推送  
        /// </summary>
        [Description("扫码推事件且弹出“消息接收中”提示框的事件推送")]
        Scancode_waitmsg,
        /// <summary>
        /// 弹出系统拍照发图的事件推送 
        /// </summary>
        [Description("弹出系统拍照发图的事件推送")]
        Pic_sysphoto,
        /// <summary>
        /// 弹出拍照或者相册发图的事件推送  
        /// </summary>
        [Description("弹出拍照或者相册发图的事件推送")]
        Pic_photo_or_album,
        /// <summary>
        /// 弹出微信相册发图器的事件推送
        /// </summary>
        [Description("弹出微信相册发图器的事件推送")]
        Pic_weixin,
        /// <summary>
        /// 弹出地理位置选择器的事件推送 
        /// </summary>
        [Description("弹出地理位置选择器的事件推送")]
        Location_select,
        /// <summary>
        /// 门店审核事件
        /// </summary>
        [Description("审核结果事件推送")]
        Poi_check_notify,
   
        /// <summary>
        /// 发送模板信息的结果推送
        /// </summary>
        [Description("发送模板信息的结果推送")]
        Templatesendjobfinish,
        /// <summary>
        /// 订单付款通知
        /// </summary>
        [Description("订单付款通知")]
        Merchant_order,

        /// <summary>
        /// 资质认证成功
        /// </summary>
        [Description("资质认证成功")]
        Qualification_verify_success,

        /// <summary>
        /// 资质认证失败
        /// </summary>
        [Description("资质认证失败")]
        Qualification_verify_fail,

        /// <summary>
        /// 名称认证成功
        /// </summary>
        [Description("名称认证成功")]
        Naming_verify_success,

        /// <summary>
        /// 名称认证失败
        /// </summary>
        [Description("名称认证失败")]
        Naming_verify_fail,

        /// <summary>
        /// 年审通知
        /// </summary>
        [Description("年审通知")]
        Annual_renew,

        /// <summary>
        /// 认证过期失效
        /// </summary>
        [Description("认证过期失效")]
        Verify_expired
    }
}
