using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    public class UserInfo
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public int subscribe { get; set; }
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 用户头像，最后一个数值(***/0)代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），
        /// 用户没有头像时该项为空 
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom） 
        /// </summary>
        public string[] privilege { get; set; }
        /// <summary>
        /// 通过网页（snsapi_userinfo）拉取用户时，没有该字段
        /// </summary>
        public int subscribe_time { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 用户所在的分组ID（兼容旧的用户分组接口）
        /// </summary>
        public int groupid { get; set; }
        /// <summary>
        /// 用户被打上的标签ID列表
        /// </summary>
        public string tagid_list { get; set; }

        public watermark watermark { get; set; }

    }

    public class watermark
    {
        public string appid { get; set; }

        public int timestamp { get; set; }
    }
}
