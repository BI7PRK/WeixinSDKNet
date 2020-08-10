using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WeixinSDK.Net.Enums
{
    /// <summary>
    /// 消息加密模式
    /// </summary>
    public enum EncryptType
    {
        /// <summary>
        /// 明文模式下，不使用消息体加解密功能，安全系数较低
        /// </summary>

        [Description("明文模式")]
        [Display(Name="明文模式")]
        Plaintext,
        /// <summary>
        /// 兼容模式下，明文、密文将共存，方便开发者调试和维护
        /// </summary>
        [Description("兼容模式")]
        [Display(Name = "兼容模式")]
        Compatible,
        /// <summary>
        /// 安全模式下，消息包为纯密文，需要开发者加密和解密，安全系数高
        /// </summary>
        [Description("安全模式")]
        [Display(Name = "安全模式")]
        Security
    }
}
