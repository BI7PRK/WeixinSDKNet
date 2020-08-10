
using System;

namespace WeixinSDK.Net.Entity
{
     [Serializable()]
    public class LocationEvent : EventBase
    {
        /// <summary>
        /// 地理位置纬度 
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// 地理位置经度 
        /// </summary>
        public decimal Longitude { get; set; }
        /// <summary>
        /// 地理位置精度 
        /// </summary>
        public decimal Precision { get; set; }
    }
}
