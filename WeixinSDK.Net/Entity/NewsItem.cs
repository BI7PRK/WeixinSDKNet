using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable]
    public class NewsItem
    {
        public string Title;
        public string Description;
        public string Url;
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl;
    }
}
