using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable()]
    public class MaterList
    {
        /// <summary>
        /// 该类型的素材的总数 
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 本次调用获取的素材的数量 
        /// </summary>
        public int item_count { get; set; }

        public List<MaterItem> item { get; set; }
    }

    [Serializable()]
    public class MaterItem
    {
        public string media_id { get; set; }

        /// <summary>
        /// 图文内容才有
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MaterContents content { get; set; }

        public long update_time { get; set; }

        /// <summary>
        /// 非图文素材时才有
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string name { get; set; }
        /// <summary>
        /// 非图文内容才有
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long url { get; set; }
    }

    [Serializable()]
    public class MaterContents
    {
        public List<ContentItem> news_item { get; set; }
    }

    [Serializable()]
    public class ContentItem
    {
        public string title { get; set; }
        public int thumb_media_id { get; set; }
        public int show_cover_pic { get; set; }
        public string author { get; set; }
        public string digest { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string content_source_url { get; set; }
    }
}
