
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using WeixinSDK.Net.Interface;

namespace WeixinSDK.Net
{
    /// <summary>
    /// 消息加密/解密
    /// </summary>
    internal class MessageEncryptHelper
    {
        public enum CryptErrorCode : int
        {
            [Description("成功")]
            CryptErrorCode_OK = 0,
            [Description("签名验证错误")]
            ValidateSignature_Error = -40001,
            [Description("XML转换错误")]
            ParseXml_Error = -40002,
            [Description("计算签名错误")]
            ComputeSignature_Error = -40003,
            [Description("密钥不合法")]
            IllegalAesKey_Error = -40004,
            [Description("AppID验证错误")]
            ValidateAppid_Error = -40005,
            [Description("加密错误")]
            EncryptAES_Error = -40006,
            [Description("解密错误")]
            DecryptAES_Error = -40007,
            [Description("缓冲数据不合法")]
            IllegalBuffer_Error = -40008,
            [Description("Base64编码错误")]
            EncodeBase64_Error = -40009,
            [Description("Base64解码错误")]
            DecodeBase64_Error = -40010
        };



        /// <summary>
        /// 信息解密
        /// </summary>
        /// <param name="config">用户接口配置。需要使用 EncryptKey 和 Token</param>
        /// <param name="msgSignature">签名串，对应URL参数的signature</param>
        /// <param name="sTimeStamp">时间戳，对应URL参数</param>
        /// <param name="sNonce">随机串，对应URL参数</param>
        /// <param name="sPostData">密文，对应POST请求的数据</param>
        /// <param name="sMsg">解密后的原文，当return返回0时有效</param>
        /// <returns></returns>
        public static CryptErrorCode MessageDecrypt(IAppConfig config, string msgSignature, string sTimeStamp, string sNonce, string sPostData, ref string sMsg)
        {
            //var token = config.Token;
            if (config.EncryptKey.Length != 43)
            {
                return CryptErrorCode.IllegalAesKey_Error;
            }
            XmlDocument doc = new XmlDocument();
            XmlNode root;
            string sEncryptMsg;
            try
            {
                doc.LoadXml(sPostData);
                root = doc.FirstChild;
                //string sig = root["MsgSignature"].InnerText;
                //string timestamp = root["TimeStamp"].InnerText;
                //string nonce = root["Nonce"].InnerText;
                sEncryptMsg = root["Encrypt"].InnerText;

            }
            catch (Exception)
            {
                return CryptErrorCode.ParseXml_Error;
            }
            //verify signature
            var ret = VerifySignature(config.ApiToken, sTimeStamp, sNonce, sEncryptMsg, msgSignature);
            if (ret != CryptErrorCode.CryptErrorCode_OK)
                return ret;

            var errCode = CryptErrorCode.CryptErrorCode_OK;
            string cpid = "";
            try
            {
                sMsg = Cryptography.AESDecrypt(sEncryptMsg, config.EncryptKey, ref cpid);
            }
            catch (FormatException)
            {
                errCode = CryptErrorCode.DecodeBase64_Error;
            }
            catch (Exception)
            {
                errCode = CryptErrorCode.DecryptAES_Error;
            }
            if (errCode == CryptErrorCode.CryptErrorCode_OK && cpid != config.AppId)
                errCode = CryptErrorCode.ValidateAppid_Error;
        
            return errCode;
        }

        /// <summary>
        /// 信息加密
        /// </summary>
        /// <param name="config">用户接口配置。</param>
        /// <param name="sReplyMsg">待回复用户的消息，xml格式的字符串</param>
        /// <param name="sTimeStamp">时间戳，可以自己生成，也可以用URL参数的timestamp</param>
        /// <param name="sNonce">随机串</param>
        /// <param name="sEncryptMsg">已加密内容</param>
        /// <returns></returns>
        public static CryptErrorCode MessageEncrypt(IAppConfig config, string sReplyMsg, string sTimeStamp, string sNonce, ref string sEncryptMsg)
        {
            if (config.EncryptKey.Length != 43)
            {
                return CryptErrorCode.IllegalAesKey_Error;
            }

            string raw = "";
            try
            {
                raw = Cryptography.AESEncrypt(sReplyMsg, config.EncryptKey, config.AppId);
            }
            catch (Exception)
            {
                return CryptErrorCode.EncryptAES_Error;
            }
            string MsgSigature = "";
            var ret = GenarateSinature(config.ApiToken, sTimeStamp, sNonce, raw, ref MsgSigature);
            if (CryptErrorCode.CryptErrorCode_OK != ret)
                return ret;

            StringBuilder sb = new StringBuilder("<xml>");
            sb.AppendFormat("<Encrypt><![CDATA[{0}]]></Encrypt>", raw);
            sb.AppendFormat("<MsgSignature><![CDATA[{0}]]></MsgSignature>", MsgSigature);
            sb.AppendFormat("<TimeStamp><![CDATA[{0}]]></TimeStamp>", sTimeStamp);
            sb.AppendFormat("<Nonce><![CDATA[{0}]]></Nonce>", sNonce).AppendLine("</xml>");
            sEncryptMsg = sb.ToString();

            return CryptErrorCode.CryptErrorCode_OK;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">接口Token</param>
        /// <param name="sTimeStamp"></param>
        /// <param name="sNonce"></param>
        /// <param name="sMsgEncrypt"></param>
        /// <param name="sSigture"></param>
        /// <returns></returns>
        private static CryptErrorCode VerifySignature(string token, string sTimeStamp, string sNonce, string sMsgEncrypt, string sSigture)
        {
            string hash = "";
            var ret = GenarateSinature(token, sTimeStamp, sNonce, sMsgEncrypt, ref hash);
            if (ret != CryptErrorCode.CryptErrorCode_OK)
                return ret;

            if (hash != sSigture)
                return CryptErrorCode.ValidateSignature_Error;

            return CryptErrorCode.CryptErrorCode_OK;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">接口Token</param>
        /// <param name="sTimeStamp"></param>
        /// <param name="sNonce"></param>
        /// <param name="sMsgEncrypt"></param>
        /// <param name="sMsgSignature"></param>
        /// <returns></returns>
        private static CryptErrorCode GenarateSinature(string token, string sTimeStamp, string sNonce, string sMsgEncrypt, ref string sMsgSignature)
        {
            ArrayList AL = new ArrayList();
            AL.Add(token);
            AL.Add(sTimeStamp);
            AL.Add(sNonce);
            AL.Add(sMsgEncrypt);
            AL.Sort(new DictionarySort());
            string raw = "";
            for (int i = 0; i < AL.Count; ++i)
            {
                raw += AL[i];
            }

            SHA1 sha;
            ASCIIEncoding enc;
            string hash = "";
            try
            {
                sha = new SHA1CryptoServiceProvider();
                enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(raw);
                byte[] dataHashed = sha.ComputeHash(dataToHash);
                hash = BitConverter.ToString(dataHashed).Replace("-", "");
                hash = hash.ToLower();
            }
            catch (Exception)
            {
                return CryptErrorCode.ComputeSignature_Error;
            }
            sMsgSignature = hash;
            return CryptErrorCode.CryptErrorCode_OK;
        }
    }
}
