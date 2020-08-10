using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Official
{
    public class SpreadAPI
    {
        /// <summary>
        /// 生成ID二维码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="scene">是否为临时。</param>
        /// <param name="scene_id">场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）</param>
        /// <returns></returns>
        public static CreateQRCodeResponse CreateQRCode(string token, bool scene, long scene_id = 8888L)
        {
            var data = new object();
            if (scene && (scene_id <= 0 || scene_id > 100000L))
            {
                data = new
                {
                    action_name = "QR_LIMIT_SCENE",
                    action_info = new { scene = new { scene_id = scene_id, scene_str = "cosybras.com" } }
                };
            }
            else
            {
                long sceneId = (long)Math.Floor((new Random()).NextDouble() * 1000000000D);
                data = new
                {
                    expire_seconds = 604800,
                    action_name = "QR_SCENE",
                    action_info = new { scene = new { scene_id = sceneId } }
                };
            }
            var obj = new CreateQRCodeRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "qrcode/create";
            obj.AccessToken = token;
            return obj.Post(obj);
        }
        /// <summary>
        /// 获取的二维码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ticket">凭创建时的ticket可以在有效时间内换取二维码。 </param>
        /// <returns></returns>
        public static ShowQRCodeResponse ShowQRCode(string token, string ticket)
        {
            var obj = new ShowQRCodeRequest();
            obj.ServiceUrl = BaseConfig.WECHAT_TICKET_URL;
            obj.MethodName = "showqrcode?ticket=" + ticket;
            obj.AccessToken = token;
            return obj.Get(null);
        }
    }
}
