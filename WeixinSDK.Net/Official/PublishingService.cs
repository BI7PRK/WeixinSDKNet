
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using WeixinSDK.Net.Entity;
using WeixinSDK.Net.Interface;
using WeixinSDK.Net.Enums;
using WeixinSDK.Net.Extensions;

namespace WeixinSDK.Net.Official
{
    
    /// <summary>
    /// 全网发布
    /// https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1419318611&lang=zh_CN
    /// </summary>
    public class PublishingService
    {
        /// <summary>
        /// 全网发布自动化测试公众号
        /// </summary>
        public const string PublishingUserName = "gh_3c884a361561";

        /// <summary>
        /// 全网发布自动化测试Appid
        /// </summary>
        public const string PublishingAppId = "wx570bc396a51b8ff8";

        /// <summary>
        /// 第三方平台全网发布服务
        /// </summary>
        /// <param name="message"></param>
        /// <param name="config">第三方平台配置</param>
        /// <param name="saveAuthCode">获取已保存的 query_auth_code 回调</param>
        /// <param name="authAction">保存微信服务器推送过来的 query_auth_code</param>
        public static void  CheckPublishing(ReceiveMessage message, IAppConfig config, Func<string, string> saveAuthCode, Action<AuthorizedMessage> authAction)
        {
            if (!message.IsSuccess)
            {
                return;
            }

            if(message.ToUserName != PublishingUserName && message.AppId != PublishingAppId)
            {
                return;
            }


            try
            {
                if (message.MessageType == MessageType.Authorized)
                {
                    var auth = message.ToAuthorizedMessage();
                    authAction(auth);
                    WriteLogFile("获取 query_auth_code ==> " + auth.AuthorizationCode);
                    return;
                }

            }
            catch (Exception ex)
            {
                WriteLogFile("发生异常（Authorized） ==> " + ex.Message);
            }
           
            switch (message.MessageType)
            {
                case MessageType.Text:
                    {
  
                        // 模拟粉丝发送文本消息给专用测试公众号，第三方平台方需根据文本消息的内容进行相应的响应：
                        var obj = message.ToText();
                        
                        if (obj.Content == "TESTCOMPONENT_MSG_TYPE_TEXT")
                        {
                            var result = ReplyMegAPI.ReplyText(config, obj.Content + "_callback", obj);
                            WriteLogFile("回复普通文本消息 ==> " + result.IsError + ", " + result.Message);
                            return;
                        }
                        try
                        {
                            // 模拟粉丝发送文本消息给专用测试公众号，第三方平台方需在5秒内返回空串表明暂时不回复，
                            // 然后再立即使用客服消息接口发送消息回复粉丝
                            var authCode = saveAuthCode(message.AppId);
                            if (obj.Content == "QUERY_AUTH_CODE:" + authCode)
                            {
                                HttpContext.Current.Response.Write("");
                                var atuh = ComponentAppinter.ComponentAuthorizerToken(config.AppId, authCode, config.AccessToken);
                                if (!atuh.IsError)
                                {
                                    var result = SendMegAPI.SendTextMessage(atuh.AccessToken, obj.FromUserName, authCode + "_from_api");
                                    WriteLogFile("回复Api文本消息 ==> " + result.IsError + ", " + result.Message);
                                }
                                else
                                {
                                    WriteLogFile("使用授权码换取失败 ==> " + atuh.IsError + ", " + atuh.Message);
                                }

                                return;
                            }

                            WriteLogFile("消息内容不匹配 ==> " + message.Message);
                        }
                        catch (Exception ex)
                        {
                            WriteLogFile("发生异常（query_auth_code） ==> " + ex.Message);
                        }
                        
                    }
                    break;
                case MessageType.Event:
                    {
                        // 模拟粉丝触发专用测试公众号的事件，并推送事件消息到专用测试公众号，
                        // 第三方平台方开发者需要提取推送XML信息中的event值，
                        // 并在5秒内立即返回按照下述要求组装的文本消息给粉丝。
                        var result = ReplyMegAPI.ReplyText(config, message.EventType.ToString().ToUpper() + "from_callback", new MessageBase
                        {
                            FromUserName = message.FromUserName,
                            ToUserName = message.ToUserName
                        });

                        WriteLogFile("发送事件消息 ==> " + result.IsError + ", " + result.Message);
                    }
                    break;
                case MessageType.Component_verify_ticket:
                    {
                        // 模拟推送component_verify_ticket给开发者，开发者需按要求回复（接收到后必须直接返回字符串success）。
                        HttpContext.Current.Response.Output.Write("success");
                        WriteLogFile("模拟推送component_verify_ticket给开发者 ==> success");
                    }
                    break;
                case MessageType.Unauthorized:
                    {
                        WriteLogFile("取消授权 ==> success");
                    }
                    break;

            }
            

        }

        private static void WriteLogFile(string str)
        {
            try
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PublishingService_Log.log");
                using (var write = new StreamWriter(file, true, Encoding.UTF8))
                {
                    write.WriteLine("{0}: {1}", DateTime.Now, str);
                }
            }
            catch
            {

            }
            
        }
    }
}
