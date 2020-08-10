using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinSDK.Net.Errors
{
    public class ErrorInfo
    {

        public ErrorInfo()
        { }

        private string _Message;
        private int _ErrorCode;
        public ErrorInfo(string message)
        {
            this._Message = message;
        }

        public ErrorInfo(string message, int code)
        {
            this._Message = message;
            this._ErrorCode = code;
        }

        public string Message
        {
            get { return _Message; }
        }
        /// <summary>
        /// 错误编号
        /// </summary>
        public int ErrorCode
        {
            get { return _ErrorCode; }
        }
    }
}
