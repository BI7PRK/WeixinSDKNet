using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinSDK.Net.Official.Card;

namespace WeixinSDK.Net.Official
{


    public class GetPaygiftCardResponse : ResponseBase
    {
        public RuleInfo rule_info { get; set; }
    }

    


    public class PaygiftCardResponse : ResponseBase
    {
        public int rule_id { get; set; }
        public List<FailMchidList> fail_mchid_list { get; set; }
        public List<string> succ_mchid_list { get; set; }
    }
    /// <summary>
    /// 礼品卡接口
    /// </summary>
    public sealed partial class CardAPI
    {

        /// <summary>
        /// 设置支付即会员时POST数据
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public static PaygiftCardResponse PaygiftCardAdd(PaygiftCard data, string accessToken)
        {
            var obj = new RequestBase<PaygiftCardResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "card/paygiftcard/add";
            obj.AccessToken = accessToken;
            var result =  obj.Post(data);

            if (!result.IsError)
            {
                return JsonConvert.DeserializeObject<PaygiftCardResponse>(result.ResonseBody);
            }
            return result;
        }

        /// <summary>
        ///  删除支付后投放卡券规则接口
        /// </summary>
        /// <param name="ruleId">The rule identifier.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public static ResponseBase PaygiftCardRemove(string ruleId, string accessToken)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "card/paygiftcard/delete";
            obj.AccessToken = accessToken;
            return obj.Post(new {
                rule_id = ruleId
            });
        }

        /// <summary>
        /// 查询支付后投放卡券规则详情接口
        /// </summary>
        /// <param name="ruleId">The rule identifier.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public static GetPaygiftCardResponse GetPaygiftCard(string ruleId, string accessToken)
        {
            var obj = new RequestBase<GetPaygiftCardResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "card/paygiftcard/getbyid";
            obj.AccessToken = accessToken;
            var result = obj.Post(new
            {
                rule_id = ruleId
            });


            if (!result.IsError)
            {
                return JsonConvert.DeserializeObject<GetPaygiftCardResponse>(result.ResonseBody);
            }
            return result;
        }

        /// <summary>
        /// 批量查询支付后投放卡券规则接口
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public static GetPaygiftCardResponse Batchget(BatchgetObject data, string accessToken)
        {
            var obj = new RequestBase<GetPaygiftCardResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "card/paygiftcard/batchget";
            obj.AccessToken = accessToken;
            var result = obj.Post(data);

            if (!result.IsError)
            {
                return JsonConvert.DeserializeObject<GetPaygiftCardResponse>(result.ResonseBody);

            }
            return result;
        }
    }
}
