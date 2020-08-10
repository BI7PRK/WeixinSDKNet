using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Pay.Entity
{
    public class DownBillObject : BaseObject
    {


        public int bill_date { get; set; }

        /// <summary>
        /// BillTypes
        /// </summary>
        /// <value>
        /// The type of the bill.
        /// </value>
        /// <seealso cref="WeixinSDK.Net.Pay.Enums.BillTypes" />
        public string bill_type { get; set; }

        public string tar_type { get => "GZIP"; }
    }
}
