using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static WeixinSDK.Net.Http.HttpProxy;

namespace WeixinSDK.Net.Official
{
    internal class RequestBase<TModel> where  TModel : ResponseBase, new()
    {
        public string ServiceUrl
        {
            get;
            set;
        }

        public string MethodName
        {
            get;
            set;
        }
        /// <summary>
        /// 请求所需的Token
        /// </summary>
        public string AccessToken
        {
            get;
            set;
        }

        

        private string _TokenQueryName = "access_token";
        public string TokenQueryName
        {
            get
            {
                return _TokenQueryName;
            }

            set
            {
                _TokenQueryName = value;
            }
        }

        /// <summary>
        /// 附带token的完整请求地址,
        /// </summary>
        internal string QueryUri
        {

            get
            {
                var url = ServiceUrl + MethodName;
                if (!string.IsNullOrEmpty(AccessToken))
                {
                    url += url.IndexOf("?") > 0 ? "&" : "?";
                    url += TokenQueryName + "=" + AccessToken;
                }

                return url;
            }
        }
        
        public virtual TModel Get(Dictionary<string, object> param = null)
        {
            var obj = new TModel();
            obj.RequestQueryUrl = QueryUri;
            obj.ResonseBody = Task.Run(()=> GetStringAsync(QueryUri, param)).Result;
            return obj;
        }

        public virtual TModel Post(object entity)
        {
            var obj = new TModel();
            string json;
            if (entity.GetType().Name != typeof(string).Name)
            {
                json = JsonConvert.SerializeObject(entity);
            }
            else {
                json = entity.ToString();
            }
            obj.RequestQueryUrl = QueryUri;
            var res = Task.Run(()=> PostAsync(QueryUri, json)).Result;
            obj.ResonseBody = res.IsSuccess ? res.Body : res.Message;


            return obj;
        }
      
        public virtual TModel FormPost(Dictionary<string, string> formdata = null, Dictionary<string, string> files = null)
        {

            var obj = new TModel();
            obj.RequestQueryUrl = QueryUri;
            obj.ResonseBody = FormPostFile(new FormPostData
            {
                FileField = files,
                FormField = formdata,
                Url = QueryUri
            });
            return obj;
        }

        public virtual TModel Upload(string file)
        {

            var obj = new TModel();
            obj.RequestQueryUrl = QueryUri;
            using (var client = new WebClient())
            {
                var res = client.UploadFile(QueryUri, "POST", file);
                obj.ResonseBody = Encoding.Default.GetString(res);
            }
            return obj;
        }

        public virtual TModel FormUpload(Dictionary<string, string> formdta, Dictionary<string, string> files)
        {

            var obj = new TModel();
            obj.RequestQueryUrl = QueryUri;
            obj.ResonseBody = FormPostFile(new FormPostData
            {
                FileField = files,
                FormField = formdta,
                Url = QueryUri
            });

            return obj;
        }

    }
}
