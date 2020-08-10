using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable]
    public class VoiceMessage : MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 当开启语音识别功能时。返回的XML才会带上这个字段
        /// </summary>
        public string Recognition { get; set; }
    }
}
