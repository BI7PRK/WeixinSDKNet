
using WeixinSDK.Net.Pay.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeixinSDK.Net.Pay.Entity
{
    /// <summary>
    /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
    /// 统一下单数据类
    /// </summary>
    public class UnifiedOrder : IMetaEntity
    {
        /// <summary>
        /// 微信支付分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"
        /// </summary>

        [StringLength(32, ErrorMessage = "{0}超过{1}个字符")]
        public string device_info { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名。通过签名算法计算得出的签名值
        /// </summary>

        [StringLength(32, ErrorMessage = "{0}超过{1}个字符")]
        public string sign { get; private set; }

        /// <summary>
        /// 签名类型，默认为MD5，支持HMAC-SHA256和MD5
        /// </summary>

        [StringLength(32, ErrorMessage = "{0}超过{1}个字符")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SignType sign_type { get; set; }
        
        /// <summary>
        /// 商品简单描述，该字段请按照规范传递
        /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_2
        /// </summary>
        [StringLength(128, ErrorMessage = "{0}超过{1}个字符")]
        public string body { get; set; }

        /// <summary>
        /// 商品详细列表，使用Json格式，传输签名前请务必使用CDATA标签将JSON文本串保护起来。
        /// </summary>
        [StringLength(6000, ErrorMessage = "{0}超过{1}个字符")]
        [DataType(DataType.MultilineText)]
        public string detail { get; set; }

        /// <summary>
        /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。
        /// </summary>
        [StringLength(127, ErrorMessage = "{0}超过{1}个字符")]
        public string attach { get; set; }

        /// <summary>
        /// 商户系统内部订单号，要求32个字符内、且在同一个商户号下唯一
        /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_2
        /// </summary>
        [StringLength(32, ErrorMessage = "{0}超过{1}个字符")]
        public string out_trade_no { get; set; }

        /// <summary>
        /// 标价币种。符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_2
        /// </summary>
        [StringLength(16, ErrorMessage = "{0}超过{1}个字符")]
        public string fee_type { get; set; }

        /// <summary>
        /// 订单总金额，单位为分
        /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_2
        /// </summary>
        [MaxLength(int.MaxValue, ErrorMessage = "{0}超过{1}个字符")]
        public int total_fee { get; set; }

        /// <summary>
        /// APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP
        /// </summary>
        [StringLength(16, ErrorMessage = "{0}超过{1}个字符")]
        public string spbill_create_ip { get; set; }

        /// <summary>
        /// 订单生成时间，格式为yyyyMMddHHmmss
        /// </summary>
        [StringLength(14, ErrorMessage = "{0}超过{1}个字符")]
        public string time_start { get; set; }

        /// <summary>
        /// 订单失效时间，格式为yyyyMMddHHmmss
        /// </summary>
        [StringLength(14, ErrorMessage = "{0}超过{1}个字符")]
        public string time_expire { get; set; }

        /// <summary>
        /// 商品标记，使用代金券或立减优惠功能时需要的参数
        /// https://pay.weixin.qq.com/wiki/doc/api/tools/sp_coupon.php?chapter=12_1
        /// </summary>
        [StringLength(32, ErrorMessage = "{0}超过{1}个字符")]
        public string goods_tag { get; set; }

        /// <summary>
        /// 异步接收微信支付结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。
        /// </summary>
        [StringLength(256, ErrorMessage = "{0}超过{1}个字符")]
        public string notify_url { get; set; }

        /// <summary>
        /// 交易类型。取值如下：JSAPI，NATIVE，APP等
        /// </summary>
        [StringLength(16, ErrorMessage = "{0}超过{1}个字符")]
        public TradeType trade_type { get; set; }

        /// <summary>
        /// 商品ID。trade_type=NATIVE时（即扫码支付），此参数必传。此参数为二维码中包含的商品ID，商户自行定义。
        /// </summary>
        [StringLength(32, ErrorMessage = "{0}超过{1}个字符")]
        public string product_id { get; set; }

        /// <summary>
        /// 指定支付方式。上传此参数no_credit--可限制用户不能使用信用卡支付
        /// </summary>
        [StringLength(32, ErrorMessage = "{0}超过{1}个字符")]
        public string limit_pay { get; set; }

        /// <summary>
        /// 用户标识。trade_type=JSAPI，此参数必传，用户在商户appid下的唯一标识。openid如何获取，可参考【获取openid】。企业号请使用【企业号OAuth2.0接口】获取企业号内成员userid，再调用【企业号userid转openid接口】进行转换
        /// </summary>
        [StringLength(128, ErrorMessage = "{0}超过{1}个字符")]
        public string openid { get; set; }

        /// <summary>
        /// Y，传入Y时，支付成功消息和支付详情页将出现开票入口。需要在微信支付商户平台或微信公众平台开通电子发票功能，传此字段才可生效
        /// </summary>
        public string receipt { get; set; } = "N";

        /// <summary>
        /// trade_type=MWEB，此参数必传
        /// https://pay.weixin.qq.com/wiki/doc/api/H5.php?chapter=9_20&index=1
        /// 该字段用于上报支付的场景信息,针对H5支付有以下三种场景,请根据对应场景上报,H5支付不建议在APP端使用，针对场景1，2请接入APP支付，不然可能会出现兼容性问题
        /// </summary>
        public H5Info scene_info { get; set; }

        public void setSign(string s)
        {
            sign = s;
        }
    }

    public class H5Info
    {
        public ISceneInfo h5_info { get; set; }
    }


    public interface ISceneInfo
    {
        string type { get; }
    }

    public class IosScene : ISceneInfo
    {

        public string type { get => "IOS"; }
        public string app_name { get; set; }
        public string bundle_id { get; set; }
    }

    public class AndroidScene : ISceneInfo
    {
        public string type { get => "Android"; }
        public string app_name { get; set; }
        public string package_name { get; set; }
    }

    public class WapScene : ISceneInfo
    {
        public string type { get => "Wap"; }
        public string wap_url { get; set; }
        public string wap_name { get; set; }
    }
}