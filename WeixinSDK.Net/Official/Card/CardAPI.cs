using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinSDK.Net.Official.Card;

namespace WeixinSDK.Net.Official
{

    public class CreateCardResponse : ResponseBase
    {
        public string CardId { get; set; } 
    }

    public class UpdateCardResponse : ResponseBase
    {
        public bool SendCheck { get; set; }
    }

  
    public  sealed partial class CardAPI
    {
        /// <summary>
        /// 创建卡券
        /// </summary>
        /// <param name="card">The card.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public static CreateCardResponse Create(ICard card, string accessToken)
        {
            var obj = new RequestBase<CreateCardResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "card/create";
            obj.AccessToken = accessToken;
            var result = obj.Post(JsonConvert.SerializeObject(card));
            if (!result.IsError)
            {
                var map = JsonConvert.DeserializeObject<JObject>(result.ResonseBody);
                if(map.Property("card_id") != null)
                {
                    result.CardId = map.GetValue("card_id").ToString();
                }
            }
            return result;
        }


        public static UpdateCardResponse Update(ICard card, string accessToken)
        {
            var obj = new RequestBase<UpdateCardResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "card/update";
            obj.AccessToken = accessToken;
            var result = obj.Post(JsonConvert.SerializeObject(card));
            if (!result.IsError)
            {
                var map = JsonConvert.DeserializeObject<JObject>(result.ResonseBody);
                if (map.Property("send_check") != null)
                {
                    result.SendCheck = (bool)map.GetValue("send_check");
                }
            }
            return result;
        }

        /// <summary>
        /// 设置买单启用卡券。设置买单的card_id必须已经配置了门店，否则会报错。
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="cardId">The card identifier.</param>
        /// <param name="isOpen">if set to <c>true</c> [is open].</param>
        /// <returns></returns>
        public static CreateCardResponse SetPaycell(string accessToken, string cardId, bool isOpen = true)
        {
            var obj = new RequestBase<CreateCardResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "card/paycell/set";
            obj.AccessToken = accessToken;
            return obj.Post(JsonConvert.SerializeObject(new
            {
                card_id = cardId,
                is_open = isOpen
            }));
        }

        /// <summary>
        /// 自助核销
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="cardId">The card identifier.</param>
        /// <param name="isOpen">if set to <c>true</c> [is open].</param>
        /// <returns></returns>
        public static CreateCardResponse SetConsumeCell(string accessToken, string cardId, bool isOpen = true)
        {
            var obj = new RequestBase<CreateCardResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "card/selfconsumecell/set";
            obj.AccessToken = accessToken;
            return obj.Post(JsonConvert.SerializeObject(new
            {
                card_id = cardId,
                is_open = isOpen
            }));
        }


        /// <summary>
        /// 拉取会员信息（积分查询）接口
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="cardId">The card identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static MemberCardInfoResponse GetMemberCard(string accessToken, string cardId, string code)
        {
            var obj = new RequestBase<MemberCardInfoResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_API_URL;
            obj.MethodName = "card/membercard/userinfo/get";
            obj.AccessToken = accessToken;
            var result =  obj.Post(JsonConvert.SerializeObject(new
            {
                card_id = cardId,
                is_open = code
            }));
            if (!result.IsError)
            {
                result.CardInfo = JsonConvert.DeserializeObject<MemberCardInfo>(result.ResonseBody);
            }
            return result;
        }

        
    }
}
