using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{

    [Serializable]
    public class LocationMessage : MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal Location_X { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Location_Y { get; set; }
        /// <summary>
        /// 地图放大级别
        /// </summary>
        public int Scale { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Label { get; set; }
    }
}
