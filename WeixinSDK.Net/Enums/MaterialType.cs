using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Enums
{
    /// <summary>
    /// 素材类型
    /// </summary>
    public enum MaterialType
    {
        /// <summary>
        /// 图文
        /// </summary>
        [Description("图文"),
        DefaultValue("news")]
        News,
        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片"),
        DefaultValue("image")]
        Image,
        /// <summary>
        /// 语音
        /// </summary>
        [Description("语音"),
        DefaultValue("voice")]
        Voice,
        /// <summary>
        /// 视频
        /// </summary>
        [Description("视频"),
        DefaultValue("video")]
        Video
    }
}
