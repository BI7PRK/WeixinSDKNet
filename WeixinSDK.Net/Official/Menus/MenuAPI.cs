using WeixinSDK.Net.Entity;

namespace WeixinSDK.Net.Official
{
    public sealed class MenuAPI
    {
        #region 自定义菜单

        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ResponseBase CreateMenus(ButtonEntity btn, string token)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "menu/create";
            obj.AccessToken = token;
            return obj.Post(btn);
        }

        public static ResponseBase CreateMenus(string json, string token)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "menu/create";
            obj.AccessToken = token;
            return obj.Post(json);
        }

        /// <summary>
        /// 删除自定义菜单
        /// </summary>
        public static ResponseBase RemoveMenus(string token)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.MethodName = "menu/delete";
            obj.AccessToken = token;
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            return obj.Get();
        }

        /// <summary>
        /// 查询自定义菜单以及个性化菜单
        /// </summary>
        public static ResponseBase GetMenus(string token)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.MethodName = "menu/get";
            obj.AccessToken = token;
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            return obj.Get();
        }

        #region 个性化菜单
        /// <summary>
        /// 个性化菜单是在原基础菜单上增加 matchrule 信息
        /// </summary>
        /// <param name="json"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ResponseBase AddConditional(string json, string token)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "menu/addconditional";
            obj.AccessToken = token;
            return obj.Post(json);
        }

        /// <summary>
        /// 删除个性化菜单
        /// </summary>
        /// <param name="menuid">菜单ID (可以通过自定义菜单查询接口获取。 )</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ResponseBase DelConditional(string menuid, string token)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.MethodName = "menu/delconditional";
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.AccessToken = token;
            return obj.Post(new { menuid = "menuid" });
        }

        /// <summary>
        /// 测试菜单匹配
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ResponseBase TryMatchConditional(string openId, string token)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.MethodName = "menu/trymatch";
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.AccessToken = token;
            return obj.Post(new { user_id = "openId" });

        }

        #endregion


        #endregion
    }
}
