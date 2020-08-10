using Newtonsoft.Json;
using System;

namespace WeixinSDK.Net.Official
{
    [Serializable]
    public class ResponseBase
    {
       
        [Serializable]
        internal class result
        {
            public long errcode { get; set; }
            public string errmsg { get; set; }
        }
        private string _body;
        private void CheckError(string body)
        {
            _body = body;
            this.StateCode = -999;
            if (string.IsNullOrWhiteSpace(body))
            {

                this.Message += "没有响应内容，检查 ResonseBody 是否已经赋值？";
                _IsErr = true;
                return;
            }

            body = body.TrimEnd().TrimStart();
            if (!body.StartsWith("{") && !body.EndsWith("}"))
            {
                
                this.Message += "响应的数据不是 Json";
                _IsErr = true;
                return;
            }
            var obj = JsonConvert.DeserializeObject<result>(body);
            if (obj != null && obj.errcode != 0)
            {
                var msg = ResultCode.GeMessage(obj.errcode);
                this.StateCode = obj.errcode;
                this.Message = msg ?? obj.errmsg;
                _IsErr = true;
            }
            else
            {
                this.Message = "OK";
                this.StateCode = 0;
            }
        }

        internal void setError(bool err)
        {
            _IsErr = err;
        }
        private bool _IsErr = false;
        /// <summary>
        /// 是否错误
        /// </summary>
        public bool IsError
        {
            get
            {
                return _IsErr;
            }
        }
        /// <summary>
        /// 返回代码
        /// </summary>
        public long StateCode { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 响应的原始内容
        /// </summary>
        internal string ResonseBody
        {
            get { return _body; }
            set
            {
                this.CheckError(value);
            }
        }
        private string _queryUrl;
        /// <summary>
        /// 完整的请求地址
        /// </summary>
        public string RequestQueryUrl
        {
            get { return _queryUrl; }
            internal set
            {
                _queryUrl = value;
            }
        }

        
    }
}
