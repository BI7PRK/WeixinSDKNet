using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WeixinSDK.Net;
using WeixinSDK.Net.Entity;
using WeixinSDK.Net.Extensions;
using WeixinSDK.Net.Official;

namespace WeixinSDK.Net.Official
{
    #region ServiceListResponse
    public class ServiceListResponse : ResponseBase
    {
        public List<ServicerItem> ServiceList { get; set; }
    }

    public class ServicerItem
    {
        public string NickName { get; set; }
        public string Account { get; set; }
        public string HeadimgUrl { get; set; }
        public int ID { get; set; }
    }

    public class ServicerState : ServicerItem
    {
        /// <summary>
        /// 客服当前正在接待的会话数 
        /// </summary>
        public int AcceptedCase { get; set; }
        /// <summary>
        /// 客服设置的最大自动接入数 
        /// </summary>
        public int AutoAccept { get; set; }
        /// <summary>
        /// 客服在线状态 1：pc在线，2：手机在线。若pc和手机同时在线则为 1+2=3 
        /// </summary>
        public int Status { get; set; }
    }
    #endregion

    #region ServiceListRequest
    internal class ServiceListRequest : RequestBase<ServiceListResponse>
    {
        class map
        {
            public item[] kf_list { get; set; }
        }
        class item
        {
            public string kf_account { get; set; }
            public string kf_headimgurl { get; set; }
            public int kf_id { get; set; }
            public string kf_nick { get; set; }
        }

        public override ServiceListResponse Get(Dictionary<string, object> param = null)
        {
            var obj = base.Get();
            if (!obj.IsError)
            {
                var p = new List<ServicerItem>();
                var mapObj = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                foreach (var item in mapObj.kf_list)
                {
                    p.Add(new ServicerItem
                    {
                        Account = item.kf_account,
                        HeadimgUrl = item.kf_headimgurl,
                        ID = item.kf_id,
                        NickName = item.kf_nick
                    });
                }

                obj.ServiceList = p;
            }
            return obj;
        }
        
    }
    #endregion

    #region ServicerOnlineResponse

    public class ServicerOnlineResponse : ResponseBase
    {
        public List<ServicerState> Result { get; set; }
    }

    internal class ServicerOnlineRequest : RequestBase<ServicerOnlineResponse>
    {

        class map
        {
            public item[] kf_online_list { get; set; }
        }
        class item
        {
            public string kf_account { get; set; }
            public int kf_id { get; set; }
            public string kf_nick { get; set; }
            public int status { get; set; }
            public int auto_accept { get; set; }
            public int accepted_case { get; set; }
        }

        public override ServicerOnlineResponse Get(Dictionary<string, object> param = null)
        {
            var obj = base.Get();
            if (!obj.IsError)
            {
                var p = new List<ServicerState>();
                var mapObj = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                foreach (var item in mapObj.kf_online_list)
                {
                    p.Add(new ServicerState
                    {
                        Account = item.kf_account,
                        ID = item.kf_id,
                        NickName = item.kf_nick,
                        AcceptedCase = item.accepted_case,
                        AutoAccept = item.auto_accept,
                        Status = item.status
                    });
                }
                obj.Result = p;
            }
            return obj;
        }
        
    }
    #endregion

    #region GuestSessionResponse
    public class GuestSessionResponse : ResponseBase
    {
        public DateTime Createtime { get; set; }
        /// <summary>
        /// 客服帐号
        /// </summary>
        public string Account { get; set; }
    }

    internal class GuestSessionRequest : RequestBase<GuestSessionResponse>
    {
        class map
        {
            public int createtime { get; set; }
            public string kf_account { get; set; }
        }

        public override GuestSessionResponse Post(object entity)
        {
            var obj = base.Post(entity);
            if (!obj.IsError)
            {
                var mapObj = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (mapObj != null)
                {
                    obj.Createtime = mapObj.createtime.FromUnixSeconds();
                    obj.Account = mapObj.kf_account;
                }
            }
            return obj;
        }
        
        
    }

    #endregion
    
    #region WaitingCaseResponse
    public class WaitingCaseResponse : ResponseBase
    {
        /// <summary>
        /// 未接入会话数量 
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 未接入会话列表，最多返回100条数据 
        /// </summary>
        public List<WaitCaseItem> WaitCaseList { get; set; }
    }

    public class WaitCaseItem
    {
        /// <summary>
        /// 客人来访时间
        /// </summary>
        public DateTime Createtime { get; set; }
        /// <summary>
        /// 指定接待的客服，为空表示未指定客服 
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 客户openid 
        /// </summary>
        public string OpenId { get; set; }
    }

    internal class WaitingCaseRequest : RequestBase<WaitingCaseResponse>
    {
        class map
        {
            public int count { get; set; }
            public itemmap[] waitcaselist { get; set; }
        }
        class itemmap
        {
            public int createtime { get; set; }
            public string kf_account { get; set; }
            public string openid { get; set; }
        }

        public override WaitingCaseResponse Get(Dictionary<string, object> param = null)
        {
            var obj = base.Get();
            if (!obj.IsError)
            {
                var mapObj = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (mapObj != null)
                {
                    obj.Count = mapObj.count;
                    obj.WaitCaseList = mapObj.waitcaselist.Select(s => {
                        return new WaitCaseItem
                        {
                            Account = s.kf_account,
                            Createtime = s.createtime.FromUnixSeconds(),
                            OpenId = s.openid
                        };
                    }).ToList();
                }
            }
            return obj;
        }
    }
    #endregion

    #region SessionHistoryResponse
    public class SessionHistoryResponse : ResponseBase
    {
        public int Retcode { get; set; }
        public List<RecordItem> RecordList { get; set; }
    }

    public class RecordItem
    {
        /// <summary>
        /// 用户的标识，对当前公众号唯一 
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 操作ID（会话状态）
        /// </summary>
        public Opercode Opercode { get; set; }
        /// <summary>
        /// 聊天记录 
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 客服账号 
        /// </summary>
        public string Worker { get; set; }
    }

    public enum Opercode : int
    {
        /// <summary>
        /// 创建未接入会话 
        /// </summary>
        [Description("创建未接入会话")]
        NoSession = 1000,
        /// <summary>
        /// 接入会话 
        /// </summary>
        [Description("接入会话")]
        CreateSession = 1001,
        /// <summary>
        /// 主动发起会话 
        /// </summary>
        [Description("主动发起会话")]
        MsgToGuest = 1002,
        /// <summary>
        /// 关闭会话 
        /// </summary>
        [Description("关闭会话")]
        CloseSession = 1004,
        /// <summary>
        /// 抢接会话 
        /// </summary>
        [Description("抢接会话")]
        BeatSession = 1005,
        /// <summary>
        /// 公众号收到消息 
        /// </summary>
        [Description("公众号收到消息")]
        ReceivedMsg = 2001,
        /// <summary>
        /// 客服发送消息 
        /// </summary>
        [Description("客服发送消息")]
        MsgFromGuest = 2002,
        /// <summary>
        /// 客服收到消息 
        /// </summary>
        [Description("客服收到消息")]
        GuestReceivedMsg = 2003
    }

    public class ChatHistoryPost
    {
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }
        public string openid { get; set; }
        public int pageindex { get; set; }
        public int pagesize { get; set; }
    }
    #endregion

    #region SessionHistoryRequest
    internal class SessionHistoryRequest : RequestBase<SessionHistoryResponse>
    {
        class map
        {
            public int retcode { get; set; }
            public itemmap[] recordlist { get; set; }
        }
        class itemmap
        {
            public string openid { get; set; }
            public int opercode { get; set; }
            public string text { get; set; }
            public DateTime time { get; set; }
            public string worker { get; set; }
        }

        public override SessionHistoryResponse Post(object entity)
        {
            var obj = base.Post(entity);
            if (!obj.IsError)
            {
                var des = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (des != null)
                {
                    obj.Retcode = des.retcode;
                    obj.RecordList = des.recordlist.Select(s =>
                    {
                        return new RecordItem
                        {
                            OpenId = s.openid,
                            Opercode = (Opercode)s.opercode,
                            Text = s.text,
                            Time = s.time,
                            Worker = s.worker
                        };
                    }).ToList();
                }
            }
            return obj;
        }
        
      
    }
    #endregion

    public sealed class ServicerAPI
    {
        /// <summary>
        /// 接入到客服系统
        /// </summary>
        /// <param name="openId"> </param>
        /// <param name="toUser"></param>
        /// <returns></returns>
        public static bool SwitchToServicer(string openId, string toUser)
        {
            var msg = new SwitchService()
            {
                CreateTime = DateTime.Now.ToUnixSeconds(),
                FromUserName = openId,
                ToUserName = toUser
            };
            HttpContext.Current.Response.Write(msg.GetXmlString());
            return true;
        }

        /// <summary>
        /// 消息转发到指定客服
        /// </summary>
        /// <param name="openId"> </param>
        /// <param name="toUser">指定的客服</param>
        /// <returns></returns>
        public static bool TransferServicer(string openId, string toUser)
        {
            var msg = new SelectServicer()
            {
                CreateTime = DateTime.Now.ToUnixSeconds(),
                FromUserName = openId,
                ToUserName = toUser
            };
            HttpContext.Current.Response.Write(msg.GetXmlString());
            return true;
        }

        /// <summary>
        /// 获取客服列表
        /// </summary>
        /// <param name="accessToken"> </param>
        /// <returns></returns>
        public static ServiceListResponse GetServicerList(string accessToken)
        {
            var obj = new ServiceListRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "customservice/getkflist";
            obj.AccessToken = accessToken;
            return obj.Get();
        }
        /// <summary>
        /// 获取在线客服接待信息
        /// </summary>
        /// <param name="accessToken"> </param>
        /// <returns></returns>
        public static ServicerOnlineResponse GetServicerOnline(string accessToken)
        {
            var obj = new ServicerOnlineRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "customservice/getonlinekflist";
            obj.AccessToken = accessToken;
            return obj.Get();
        }

        /// <summary>
        /// 添加客服帐号
        /// </summary>
        /// <param name="accessToken"> </param>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。 </param>
        /// <param name="nickname">昵称客服昵称，最长6个汉字或12个英文字符 </param>
        /// <param name="pwd">32位MD5密码串</param>
        /// <returns></returns>
        public static ResponseBase AddServiceAccount(string accessToken, string account, string nickname, string pwd)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "customservice/kfaccount/add";
            obj.AccessToken = accessToken;
            return obj.Post ( new { kf_account = account, nickname = nickname, password = pwd });
            
        }
        /// <summary>
        /// 更新客服信息
        /// </summary>
        /// <param name="accessToken"> </param>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号 </param>
        /// <param name="name">昵称客服昵称，最长6个汉字或12个英文字符 </param>
        /// <param name="pwd">32位MD5密码串</param>
        /// <returns></returns>
        public static ResponseBase UpdateServiceAccount(string accessToken, string account, string name, string pwd)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "customservice/kfaccount/update";
            obj.AccessToken = accessToken;
            return obj.Post(new
            {
                kf_account = account,
                nickname = name,
                password = pwd
            });
            
        }
        /// <summary>
        /// 上传客服头像
        /// </summary>
        /// <param name="accessToken"> </param>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号 </param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static ResponseBase UpdateloadServicerIcon(string accessToken, string account, string file)
        {
            var files = new Dictionary<string, string>();
            files.Add("filename", file);

            var post = new Dictionary<string, string>();
            post.Add("kf_account", account);

            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "customservice/kfaccount/uploadheadimg";
            obj.AccessToken = accessToken;
            return obj.FormPost(post, files);
        }
        /// <summary>
        /// 删除客服账号
        /// </summary>
        /// <param name="accessToken"> </param>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号 </param>
        /// <returns></returns>
        public static ResponseBase DeleteServicer(string accessToken, string account)
        {
            var dict = new Dictionary<string, object>();
            dict.Add("kf_account", account);
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "customservice/kfaccount/del";
            obj.AccessToken = accessToken;
            return obj.Get(dict);
        }

        /// <summary>
        /// 创建客服会话
        /// </summary>
        /// <param name="accessToken"> </param>
        /// <param name="account">客服完整帐号</param>
        /// <param name="openId">接待ID</param>
        /// <param name="Text">附加信息，文本会展示在客服人员的多客服客户端 </param>
        /// <returns></returns>
        public ResponseBase CreateServiceSeesion(string accessToken, string account, string openId, string Text = "")
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "customservice/kfsession/create";
            obj.AccessToken = accessToken;
            return obj.Post( new { kf_account = account, openid = openId, text = Text });
        }
        /// <summary>
        /// 关闭客服会话
        /// </summary>
        /// <param name="accessToken"> </param>
        /// <param name="account">客服完整帐号</param>
        /// <param name="openId">接待ID</param>
        /// <param name="Text">附加信息，文本会展示在客服人员的多客服客户端 </param>
        /// <returns></returns>
        public ResponseBase CloseServiceSeesion(string accessToken, string account, string openId, string Text = "")
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "customservice/kfsession/close";
            obj.AccessToken = accessToken;
            return obj.Post( new { kf_account = account, openid = openId, text = Text });
        }
        /// <summary>
        /// 获取客户的会话状态
        /// </summary>
        /// <param name="accessToken"> </param>
        /// <param name="openid">客户openid </param>
        /// <returns></returns>
        public GuestSessionResponse GetServiceSeesion(string accessToken, string openid)
        {

            var obj = new GuestSessionRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "customservice/kfsession/getsession?openid=" + openid;
            obj.AccessToken = accessToken;
            return obj.Get();
        }
        /// <summary>
        /// 获取等待接入的客户列表
        /// </summary>
        /// <param name="accessToken"> </param>
        /// <returns></returns>
        public WaitingCaseResponse GetWaitingCaseList(string accessToken)
        {
            var obj = new WaitingCaseRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "customservice/kfsession/getwaitcase";
            obj.AccessToken = accessToken;
            return obj.Get();
        }


        /// <summary>
        /// 获取聊天记录
        /// </summary>
        /// <param name="accessToken"> </param>
        /// <param name="post"> </param>
        /// <returns></returns>
        public SessionHistoryResponse GetSessionHistory(string accessToken, ChatHistoryPost post)
        {
            var obj = new SessionHistoryRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "customservice/msgrecord/getrecord";
            obj.AccessToken = accessToken;
            return obj.Post(post);
        }
    }
}
