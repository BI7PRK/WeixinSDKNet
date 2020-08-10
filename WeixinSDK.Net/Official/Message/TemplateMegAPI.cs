using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Official
{

    #region TemplateResponse
    public class TemplateResponse : ResponseBase
    {
        public string TemplateId { get; set; }
    }
    /// <summary>
    /// 发送模板信息实体
    /// </summary>
    public class TemplMsgPost
    {
        public string touser { get; set; }
        /// <summary>
        /// 模板ID，通过 GetTemplateId 方法获得
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 信息详细页地址
        /// </summary>
        public string url { get; set; }

        private string _topcolor = "#000000";
        /// <summary>
        /// 通知类型颜色
        /// </summary>
        public string topcolor
        {
            get { return _topcolor; }
            set { _topcolor = value; }
        }
        /// <summary>
        /// <para>数结构的属性可以自由定义。</para>
        /// <para>data = new { name = new TemplMsgValue{ value = "张三疯" }, age = .... }</para>
        /// <para>并在模板定义中引用。例如：（姓名：{{name.DATA}}）</para>
        /// </summary>
        public object data { get; set; }
    }
    /// <summary>
    /// 模板信息数据的值
    /// </summary>
    public class TemplMsgValue
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 定义的颜色
        /// </summary>
        private string _color = "#2f2f2f";
        public string color
        {
            get { return _color; }
            set { _color = value; }
        }
    }

    #endregion

    #region TemplateRequest
    internal class TemplateRequest : RequestBase<TemplateResponse>
    {
        class map
        {
            public string template_id { get; set; }
        }

        public override TemplateResponse Post(object entity)
        {
            var obj = base.Post(entity);
            if (!obj.IsError)
            {
                var des = JsonConvert.DeserializeObject<map>(obj.ResonseBody);
                if (des != null)
                {
                    obj.TemplateId = des.template_id;
                }
            }
            return obj;
        }
        
        
    }
    #endregion

    #region SendTempMsgResponse
    public class SendTempMsgResponse : ResponseBase
    {
        public long Msgid { get; set; }
    }
    internal class SendTempMsgRequest : RequestBase<SendTempMsgResponse>
    {

        private class map
        {
            public long msgid { get; set; }
        }

        public override SendTempMsgResponse Post(object entity)
        {
            var obj = base.Post(entity);
            if (!obj.IsError)
            {
                obj.Msgid = JsonConvert.DeserializeObject<map>(obj.ResonseBody).msgid;
            }
            return obj;
        }
        
    }
    #endregion

    #region Industry
    public class IndustryResponse : ResponseBase
    {
        [JsonProperty("primary_industry")]
        public IndustryInfo Primary { get; set; }

        [JsonProperty("secondary_industry")]
        public IndustryInfo Secondary { get; set; }
    }
    public class IndustryInfo
    {
        [JsonProperty("first_class")]
        public string First { get; set; }

        [JsonProperty("second_class")]
        public string second { get; set; }
    }
    internal class IndustryRequest : RequestBase<IndustryResponse>
    {

        public override IndustryResponse Get(Dictionary<string, object> param = null)
        {
            var obj = base.Get();
            if (!obj.IsError)
            {
                obj = JsonConvert.DeserializeObject<IndustryResponse>(obj.ResonseBody);
            }
            return obj;
        }

    }
    #endregion

    #region TemplateList

    public class TemplateListResponse : ResponseBase
    {
        public TemplateList Templates { get; set; }
    }

    internal class TemplateListRequest : RequestBase<TemplateListResponse>
    {
       
        public override TemplateListResponse Get(Dictionary<string, object> param = null)
        {
            var obj = base.Get();
            obj.Templates = JsonConvert.DeserializeObject<TemplateList>(obj.ResonseBody);
            return obj;
        }
    }

    public class TemplateList
    {
        [JsonProperty("template_list")]
        public TemplateInfo[] Items { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TemplateInfo
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        /// <summary>
        /// 模板标题
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 主行业
        /// </summary>
        [JsonProperty("primary_industry")]
        public string Primary { get; set; }

        /// <summary>
        /// 子行业
        /// </summary>
        [JsonProperty("deputy_industry")]
        public string Deputy { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
        
        /// <summary>
        /// 示例
        /// </summary>
        [JsonProperty("example")]
        public string Example { get; set; }

    }
    #endregion


    public sealed class TemplateMegAPI
    {
        /// <summary>
        /// 设置所属行业
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="code1">编号请参见开发文档</param>
        /// <param name="code2">编号请参见开发文档</param>
        /// <returns></returns>
        public static ResponseBase CreateIndustry(string accessToken, int code1, int code2)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "template/api_set_industry";
            obj.AccessToken = accessToken;
            return obj.Post( new { industry_id1 = code1, industry_id2 = code2 });
        }
        /// <summary>
        /// 获取设置的行业信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ResponseBase GetIndustry(string accessToken)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "template/get_industry";
            obj.AccessToken = accessToken;
            return obj.Get();
        }

        /// <summary>
        /// 添加模板并返回ID
        /// </summary>
        /// <param name="accessToken">公众号接口凭证 </param>
        /// <param name="shortId">模板库中模板的编号，有“TM**”和“OPENTMTM**”等形式 </param>
        /// <returns></returns>
        public static TemplateResponse AddTemplateId(string accessToken, string shortId)
        {
            var obj = new TemplateRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "template/api_add_template";
            obj.AccessToken = accessToken;
            return obj.Post(new { template_id_short = shortId });
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="accessToken">公众号接口凭证</param>
        /// <returns></returns>
        public static TemplateListResponse GetTemplateList(string accessToken)
        {
            var obj = new TemplateListRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "template/get_all_private_template";
            obj.AccessToken = accessToken;
            return obj.Get();
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="accessToken">公众号接口凭证</param>
        /// <param name="tmpId">模板ID</param>
        /// <returns></returns>
        public static ResponseBase RemoveTemplate(string accessToken, string tmpId)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "template/del_private_template";
            obj.AccessToken = accessToken;
            return obj.Post(new
            {
                template_id = tmpId
            });
        }


        /// <summary>
        /// 发送模板信息
        /// </summary>
        /// <param name="accessToken">公众号配置 </param>
        /// <param name="msg">要发送信息的模板实体</param>
        /// <returns></returns>
        public static SendTempMsgResponse SendTemplateMsg(string accessToken, TemplMsgPost msg)
        {
            var obj = new SendTempMsgRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "message/template/send";
            obj.AccessToken = accessToken;
            return obj.Post(msg);
        }
    }
}
