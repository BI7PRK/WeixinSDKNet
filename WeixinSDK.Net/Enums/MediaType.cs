using System.ComponentModel;

namespace WeixinSDK.Net.Enums
{
    /// <summary>
    /// 图片（image）: 128K，支持JPG格式
    /// 语音（voice）：256K，播放长度不超过60s，支持AMR\MP3格式
    /// 视频（video）：1MB，支持MP4格式
    /// 缩略图（thumb）：64KB，支持JPG格式 
    /// </summary>
    [Description("媒体类型")]
    public enum MediaType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        Image,
        /// <summary>
        /// 语音
        /// </summary>
        [Description("语音")]
        Voice,
        /// <summary>
        /// 视频
        /// </summary>
        [Description("视频")]
        Video,
        /// <summary>
        /// 略图
        /// </summary>
        [Description("略图")]
        Thumb
    }
}
