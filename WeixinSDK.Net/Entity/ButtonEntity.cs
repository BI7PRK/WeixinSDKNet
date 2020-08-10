using WeixinSDK.Net.Enums;
using System.ComponentModel;

namespace WeixinSDK.Net.Entity
{

    public interface IMenuButton
    {
        string name { get; set; }
    }
    /// <summary>
    /// <para>1、自定义菜单最多包括3个一级菜单，每个一级菜单最多包含5个二级菜单。</para>
    /// <para>2、一级菜单最多4个汉字，二级菜单最多7个汉字，多出来的部分将会以“...”代替。</para>
    /// <para>3、创建自定义菜单后，由于微信客户端缓存，需要24小时微信客户端才会展现出来。测试时可以尝试取消关注公众账号后再次关注，则可以看到创建后的效果。</para>
    /// </summary>
    public class ButtonEntity 
    {
        /// <summary>
        /// 最多包含3个
        /// </summary>
        public IMenuButton[] button { get; set; }
    }
     [Description("用于微信聊天界面的底部一级菜单"), DisplayName("主菜单"), DefaultValue("")]
    public class MenuButton : IMenuButton
    {
        public string name { get; set; }


        /// <summary>
        /// 子菜单最多包含5个
        /// </summary>
        public IMenuButton[] sub_button { get; set; }
    }

    /// <summary>
    /// 子级菜单按钮
    /// </summary>
    public class ClickMenuItem : IMenuButton
    {
        
        public string type { get; set; }
        public string key { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// 子级菜单按钮
    /// </summary>
    public class ViewMenuItem : IMenuButton
    {

        public string type { get; set; }
        public string url { get; set; }
        public string name { get; set; }
    }


}
