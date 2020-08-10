using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WeixinSDK.Net.Interface;

namespace WeixinSDK.Net.Official
{
    /// <summary>
    /// AIP的基本处理，继承此类来实现逻辑
    /// </summary>
    public abstract class APIHandlerBase
    {
        #region CallbackHandler
        private class CallbackHandler
        {

            public string State { get; set; }
            /// <summary>
            /// 若用户禁止授权，则重定向后不会带上code参数
            /// </summary>
            public string Code { get; set; }
        }


        /// <summary>
        /// 用户授权回调，接收微信服务器的 Code 换取用户授权的Token。
        /// 业务现实在 CallbackAuthToken 中重写
        /// </summary>
        /// <param name="context"></param>
        /// <param name="config">微信公号接口配置</param>
        /// <param name="sender">可定义传递一些参数</param>
        public virtual void CallbackCodeHandler(HttpContext context, IAppConfig config, params object[] sender)
        {
            context.Response.Clear();
            context.Response.StatusCode = 200;
            context.Response.SubStatusCode = 200;

            var result = FormToEntityHelper<CallbackHandler>.DeserializeNameValue(context.Request.Params);

            AccessTokenResponse obj = new AccessTokenResponse();

            if (!string.IsNullOrEmpty(result.Code))
            {
                obj = TokenAPI.GetAuthToken(config, result.Code);
            }
            else
            {
                obj.ResonseBody = "{ errcode : -99985, errmsg : \"无法获取code，可能用户未接受授权\" }";
            }
            this.CallbackAuthToken(obj, result.State, sender);
        }


        /// <summary>
        ///  代码换取Token后要实现的功能：例如，获取用户信息。保存用户信息到数据库。
        /// </summary>
        /// <param name="result">返回的对象</param>
        /// <param name="state">返回的state参数</param>
        /// <param name="sender">传递自定义参数</param>
        protected abstract void CallbackAuthToken(AccessTokenResponse result, string state, params object[] sender);
        #endregion

        #region 接口的主要功能

        /// <summary>
        /// 微信接口响应
        /// </summary>
        /// <param name="context"></param>
        /// <param name="config">接口配置</param>
        public virtual void ReceiveMessageHandler(HttpContext context, IAppConfig config)
        {
            context.Response.Clear();
            context.Response.StatusCode = 200;
            context.Response.SubStatusCode = 200;
            ReceiveMessage(new ReceiveMessage(context, config), config);
        }
        /// <summary>
        /// 许多主要功能就在这里面实现
        /// 为防止微信服务器多次响应，请在没有业务实现时。 响应 context.Response.Output.Write("success"); 给微信服务器
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="config">微信公众号接口配置</param>
        protected abstract void ReceiveMessage(ReceiveMessage stream, IAppConfig config);

        #endregion
    }
}
