using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeixinSDK.Net.Official;
using WeixinSDK.Net;
using WeixinSDK.Net.Entity;
using WeixinSDK.Net.Enums;

namespace WeixinSDK.Net.Official
{

    #region 标签管理
    public class UserTagResponse : ResponseBase
    {
       public int Id { get; set; }
        public string Name { get; set; }
    }
   
    internal class GroupManageRequest : RequestBase<UserTagResponse>
    {
        class map
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public override UserTagResponse Post(object entity)
        {
            var obj = base.Post(entity);
            if (!obj.IsError)
            {
                var des = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (des != null)
                {
                    obj.Id = des.id;
                    obj.Name = des.name;
                }
            }
            return obj;
        }
    }

    #endregion

    #region 用户列表
    public class UserListResponse : ResponseBase
    {
        public UserListInfo UserDataList { get; set; }
    }
    public class UserListInfo
    {
        public int total { get; set; }
        public int count { get; set; }
        public UserOpenid data { get; set; }
        public string next_openid { get; set; }
    }
    
    public class UserOpenid
    {
        public string[] openid { get; set; }
    }


    internal class UserListRequest : RequestBase<UserListResponse>
    {
        public override UserListResponse Get(Dictionary<string, object> param)
        {
            var obj = base.Get(param);
            if (!obj.IsError)
            {
                obj.UserDataList = JsonConvert.DeserializeObject<UserListInfo>(obj.ResonseBody);
            }
            return obj;
        }
    }

    #endregion

    public sealed class UserTag
    {

        /// <summary>
        /// 创建用户标签
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static UserTagResponse CreateUserTag(string _name, string token)
        {
            var obj = new GroupManageRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "tags/create";
            obj.AccessToken = token;
            return obj.Post(new { tag= new { name= _name } });
        }
        /// <summary>
        /// 设置用户备注名
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="_remark"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ResponseBase SetUserRemark(string openId, string _remark, string token)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "user/info/updateremark";
            obj.AccessToken = token;
            return obj.Post(new { openid = openId, remark = _remark });
        }

        /// <summary>
        /// 获取某个关注者的信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static UserInfoResponse GetUserInfo(string token, string openid, LangType lang = LangType.zh_CN)
        {
            var obj = new RequestBase<UserInfoResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "user/info";
            obj.AccessToken = token;
            var dict = new Dictionary<string, object>();
            dict.Add("openid", openid);
            dict.Add("lang", lang);
            var result = obj.Get(dict);
            if (!result.IsError)
            {
                result.UserInfo = JsonConvert.DeserializeObject<UserInfo>(result.ResonseBody);
            }

            return result;
        }


        /// <summary>
        /// 获取关注者列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="next_openid"></param>
        /// <returns></returns>
        public static UserListResponse GetUserList(string token, string next_openid = null)
        {
            var obj = new UserListRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "user/get";
            var dict = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(next_openid))
            {
                dict.Add("next_openid", next_openid);
            };
            return obj.Get(dict);
        }

    }

}
