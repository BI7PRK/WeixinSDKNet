using System;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WeixinSDK.Net.Pay.Enums
{
    public enum WechatPayError
    {
        [Description("未知错误")]
        Unknown,
        [Description("成功")]
        OK,
        [Description("成功")]
        SUCCESS,
        [Description("商户无此接口权限")]
        NOAUTH,

        [Description("余额不足")]
        NOTENOUGH,

        [Description("商户订单已支付")]
        ORDERPAID,

        [Description("订单已关闭")]
        ORDERCLOSED,

        [Description("系统错误或系统超时")]
        SYSTEMERROR,

        [Description("参数中缺少APPID")]
        APPID_NOT_EXIST,

        [Description("参数中缺少MCHID")]
        MCHID_NOT_EXIST,

        [Description("appid和mch_id不匹配")]
        APPID_MCHID_NOT_MATCH,

        [Description("缺少必要的请求参数")]
        LACK_PARAMS,

        [Description("商户订单号重复")]
        OUT_TRADE_NO_USED,

        [Description("签名错误")]
        SIGNERROR,

        [Description("XML格式错误")]
        XML_FORMAT_ERROR,

        [Description("未使用post传递参数")]
        REQUIRE_POST_METHOD,

        [Description("post数据为空")]
        POST_DATA_EMPTY,

        [Description("编码格式错误。请使用UTF-8编码格式")]
        NOT_UTF8,

        [Description("退款业务流程错误，需要商户触发重试来解决")]
        BIZERR_NEED_RETRY,

        [Description("订单已经超过退款期限")]
        TRADE_OVERDUE,

        [Description("业务错误")]
        ERROR,

        [Description("退款请求失败")]
        USER_ACCOUNT_ABNORMAL,

        [Description("无效请求过多")]
        INVALID_REQ_TOO_MUCH,

        [Description("无效transaction_id")]
        INVALID_TRANSACTIONID,

        [Description("参数错误")]
        PARAM_ERROR,

        [Description("频率限制")]
        FREQUENCY_LIMITED,

        [Description("参数错误")]
        INVALID_REQUEST,

        [Description("订单不存在")]
        ORDERNOTEXIST
    }
    
}