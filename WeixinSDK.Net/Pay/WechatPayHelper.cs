
using WeixinSDK.Net.Extensions;
using WeixinSDK.Net.Http;
using WeixinSDK.Net.Pay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using WeixinSDK.Net.Pay.Enums;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Pay
{

    public class WechatPayHelper
    {

        private static string BaseUrl = "https://api.mch.weixin.qq.com/pay";
        /// <summary>
        /// 
        /// </summary>
        public WechatPayHelper(){}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendbox"></param>
        public WechatPayHelper(bool sendbox)
        {
            if (sendbox)
            {
                BaseUrl = "https://api.mch.weixin.qq.com/sandboxnew/pay";
            }
            
        }
        
        
        public static PaynotifyResult ProcessNotify(string xml)
        {
            return MetaDataHeler.ToEntity<PaynotifyResult>(xml);
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">API密钥</param>
        /// <returns></returns>
        public async Task<OrderQueryResult> GetOrderQuery(OrderQuery data, string key)
        {
            try
            {
                var xml = await HttpProxy.PostAsync(BaseUrl + "/orderquery", data.ToXml(key));
                return MetaDataHeler.ToEntity<OrderQueryResult>(xml);
            }
            catch
            {

               
            }
            return null; 
        }

        /// <summary>
        /// 公众号统一下单
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">API密钥</param>
        /// <returns></returns>
        public async Task<DataResult> WechatPayOrderResult(UnifiedOrder data, string key)
        {
            try
            {
                var xml = await HttpProxy.PostAsync(BaseUrl + "/unifiedorder", data.ToXml(key));
                var result = MetaDataHeler.ToEntity<PayOrderResult>(xml);
                if (result.result_code == PayResult.SUCCESS && result.return_code == PayResult.SUCCESS)
                {
                    var nonce_str = RandomCode(16);
                    var obj = new WechatBridge
                    {
                        appId = result.appid,
                        nonceStr = nonce_str,
                        package = $"prepay_id={result.prepay_id}",
                        trade_type = result.trade_type,
                        signType = result.sign_type,

                    };

                    obj.paySign = ToSign(obj, key);

                    return new DataResult()
                    {
                        Result = obj,
                        Message = xml
                    };
                }
                return new DataResult
                {
                    IsError = true,
                    Message = result.err_code_des ?? result.return_msg
                };

            }
            catch (Exception ex)
            {
                return new DataResult
                {
                    IsError = true,
                    Message = ex.Message
                };
            }
          
        }
        /// <summary>
        /// H5支付场景
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<DataResult> H5PayOrderResult(UnifiedOrder data, string key)
        {
            try
            {
                var xml = await HttpProxy.PostAsync(BaseUrl + "/unifiedorder", data.ToXml(key));
                var result = MetaDataHeler.ToEntity<PayOrderResult>(xml);
                if (result.result_code == PayResult.SUCCESS && result.return_code == PayResult.SUCCESS)
                {
                    return new DataResult()
                    {
                        Result = new H5Bridge
                        {
                            mweb_url = result.mweb_url,
                            prepayid = result.prepay_id
                        }
                    };
                }
                return new DataResult
                {
                    IsError = true,
                    Message = result.err_code_des ?? result.return_msg
                };
            }
            catch (Exception ex)
            {

                return new DataResult
                {
                    IsError = true,
                    Message = ex.Message
                };
            }
           

        }

        /// <summary>
        /// APP支付场景
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataResult AppPayOrderResult(UnifiedOrder data, string key)
        {
            try
            {
                var xml = HttpProxy.PostAsync(BaseUrl + "/unifiedorder", data.ToXml(key)).Result;
                var result = MetaDataHeler.ToEntity<PayOrderResult>(xml);
                if (result.result_code == PayResult.SUCCESS && result.return_code == PayResult.SUCCESS)
                {
                    var nonce_str = RandomCode(16);
                    var obj = new AppBridge
                    {
                        appId = result.appid,
                        partnerid = result.mch_id,
                        prepayid = result.prepay_id,
                        nonceStr = nonce_str,
                        signType = result.sign_type,
                        trade_type = result.trade_type
                    };

                    obj.paySign = ToSign(obj, key);

                    return new DataResult()
                    {
                        Result = obj,
                        Message = xml
                    };

                }
                return new DataResult
                {
                    IsError = true,
                    Message =$"return_msg: {result.return_msg} \r\ncode_des:{result.err_code_des}"
                };
            }
            catch (Exception ex)
            {

                return new DataResult
                {
                    IsError = true,
                    Message = ex.Message
                };
            }
           
        }

        /// <summary>
        /// 生成随机代码
        /// </summary>
        /// <param name="len">长度</param>
        /// <param name="numOnly">是否仅为数字</param>
        /// <returns></returns>
        public static string RandomCode(int len, bool numOnly = false)
        {
            string strCode = string.Empty;
            if (!numOnly)
            {
                Random random = new Random();
                int code;
                for (int i = 0; i < len; i++)
                {
                    int rand = random.Next();

                    switch (rand % 3)
                    {
                        case 1:
                            code = 65 + rand % 26;
                            break;
                        case 2:
                            code = 97 + rand % 26;
                            break;
                        default:
                            code = 48 + rand % 10;
                            break;
                    }
                    strCode += ((char)code).ToString();
                }
            }
            else
            {
                int min = int.Parse("1".PadRight(len, '0'));
                int max = int.Parse("9".PadRight(len, '9'));
                strCode = new Random().Next(min, max).ToString();
            }

            return strCode;

        }

        /// <summary>
        /// 生成扫描支付模式一 URL
        /// </summary>
        /// <param name="appId">公众号</param>
        /// <param name="key">API密钥</param>
        /// <param name="mchId">商户ID</param>
        /// <param name="submchId">商户ID</param>
        /// <returns></returns>
        public static string GetPrePayUrl(string appId, string key, string mchId, string submchId = null)
        {

            var list = new List<string>();
            list.Add(string.Format("appid={0}", appId));
            list.Add(string.Format("mch_id={0}", mchId));
            if (!string.IsNullOrEmpty(submchId))
            {
                list.Add(string.Format("sub_mch_id={0}", submchId));
            }
            list.Add(string.Format("time_stamp={0}", DateTime.Now.ToUnixSeconds()));
            list.Add(string.Format("nonce_str={0}", RandomCode(16)));
            list.Add(string.Format("product_id={0}", DateTime.Now.Ticks));
            var str = string.Join("&", list.OrderBy(s => s)) + "&key=" + key;
            //return str;
            //MD5加密
            var sb = new StringBuilder();
            foreach (byte b in MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str)))
            {
                sb.Append(b.ToString("x2"));
            }
            list.Add(string.Format("&sign={0}", sb.ToString().ToUpper()));

            return "weixin://wxpay/bizpayurl?" + string.Join("&", list);
        }


        /// <summary>
        ///关闭订单
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public PayOrderResult CloseOrder(OrderClose data, string key)
        {
            var xml = HttpProxy.PostAsync(BaseUrl + "/closeorder", data.ToXml(key)).Result;
            return MetaDataHeler.ToEntity<PayOrderResult>(xml);

        }

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static async Task<RefundResult> Refund(RefundObject data, string key)
        {
            try
            {
                var xml = await HttpProxy.PostAsync("https://api.mch.weixin.qq.com/secapi/pay/refund", data.ToXml(key));
                return MetaDataHeler.ToEntity<RefundResult>(xml);
            }
            catch (Exception ex)
            {

            }
            return null;

        }

        /// <summary>
        /// 下载订单
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<DownBillResult> DownloadBill(DownBillObject data, string key)
        {
            try
            {
                var xml = await HttpProxy.PostAsync(BaseUrl + "/downloadbill", data.ToXml(key));
                return MetaDataHeler.ToEntity<DownBillResult>(xml);
            }
            catch 
            {

            }

            return null;
        }

        /// <summary>
        /// 下载资金账单
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<ResultInfoBase> DownloadFundFlow(DownBillObject data, string key)
        {
            try
            {
                var xml = await HttpProxy.PostAsync(BaseUrl + "/downloadfundflow", data.ToXml(key));
                return MetaDataHeler.ToEntity<ResultInfoBase>(xml);
            }
            catch
            {

             
            }

            return null;
        }

        /// <summary>
        /// 申请沙箱密钥
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static async Task<SignKeyResult> GetSignKey(BaseObject data, string key)
        {
            try
            {
                var xml = await HttpProxy.PostAsync("https://api.mch.weixin.qq.com/sandboxnew/pay/getsignkey", data.ToXml(key));
                return MetaDataHeler.ToEntity<SignKeyResult>(xml);

            }
            catch
            {

            }

            return null;
        }


        /// <summary>
        /// 二次签名方法
        /// </summary>
        /// <param name="bridge"></param>
        /// <param name="key">商户密钥</param>
        /// <returns></returns>
        public static string ToSign(IBridge bridge, string key)
        {
            var Exclude = new string[] { "trade_type" };

            var objType = bridge.GetType();

            var Dict = new Dictionary<string, object>();
            foreach (var item in objType.GetProperties())
            {
                var value = item.GetValue(bridge);
                if (value != null  && !Exclude.Any(x=>x == item.Name))
                {
                    Dict.Add(item.Name, value);
                }
            }
            var str = string.Join("&", Dict.Select(s => $"{s.Key}={s.Value}").OrderBy(s => s)) + "&key=" + key;
            //return str;
           
            //MD5加密
            var sb = new StringBuilder();
            foreach (byte b in MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str)))
            {
                sb.Append(b.ToString("x2"));
            }
            //using (var writer = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sign.txt"), true))
            //{
            //    writer.WriteLine(str);
            //    writer.WriteLine(sb.ToString().ToUpper());
            //}

            //所有字符转为大写
            return sb.ToString().ToUpper();
        }

    }
    
}