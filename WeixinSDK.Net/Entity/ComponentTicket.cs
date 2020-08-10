using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Entity
{
    [Serializable]
    public class ComponentTicket
    {
        public string AppId { get; set; }

        public int CreateTime { get; set; }

        public string ComponentVerifyTicket { get; set; }
    }
}
