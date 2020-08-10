using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable]
    public class VideoMessage : MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
}
