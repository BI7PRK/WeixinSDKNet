using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable]
    public class UploadNewEnity
    {
        /// <summary>
        /// 图文消息的作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 图文消息的标题 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 在图文消息页面点击“阅读原文”后的页面 
        /// </summary>
        public string SourceUrl { get; set; }
        /// <summary>
        /// 图文消息页面的内容，支持HTML标签 
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图文消息的描述 
        /// </summary>
        public string Digest { get; set; }
        /// <summary>
        /// 缩略图片文件
        /// </summary>
        public string Thumbmid { get; set; }
    }
}
