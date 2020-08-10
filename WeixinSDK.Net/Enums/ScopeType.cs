using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WeixinSDK.Net.Enums
{
    /// <summary>
    /// 网站授权作用域
    /// </summary>
    [Description("网站授权作用域")]
    public enum ScopeType
    {
        /// <summary>
        /// 不弹出授权页面，直接跳转，只能获取用户openid
        /// </summary>
        [Description("基本")]
        [Display(Name="基本", Description = "不弹出授权确认")]
        Snsapi_Base,
        /// <summary>
        /// 弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息
        /// </summary>
        [Description("高级")]
        [Display(Name = "高级", Description = "弹出授权确认")]
        Snsapi_UserInfo,
        /// <summary>
        /// 用户获取用户的详细信息
        /// </summary>
        [Description("超级")]
        [Display(Name = "超级", Description ="允许在PC端打开")]
        Snsapi_Login
    }
}
