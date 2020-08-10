using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WeixinSDK.Net.Entity
{
     [Serializable()]
    public class LocationSelect : EventBase
    {
         public SendLocationInfo SendLocationInfo { get; set; }
    }
     [Serializable()]
     public class SendLocationInfo
     {
         public double Location_X { get; set; }
         public double Location_Y { get; set; }
         public double Scale { get; set; }
         public string Label { get; set; }
         public string Poiname { get; set; }
     }
}
