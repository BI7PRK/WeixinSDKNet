using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{

    [Serializable]
    public class MusicMessage : MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
        public MusicInfo Music { get; set; }
     
    }

    [Serializable]
    public class MusicInfo
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public string HQMusicUrl { get; set; }

        public string MusicUrl { get; set; }

        public string ThumbMediaId { get; set; }
    }

}
