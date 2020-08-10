using System.Collections.Generic;

namespace WeixinSDK.Net.Official.Card
{
    public interface ICard
    {

    }

    public class DateInfo
    {
        public string type { get; set; }
        public int begin_timestamp { get; set; }
        public int end_timestamp { get; set; }
    }

    public class Sku
    {
        public int quantity { get; set; }
    }

    public class BaseInfo
    {
        public string logo_url { get; set; }
        public string brand_name { get; set; }
        public string code_type { get; set; }
        public string title { get; set; }
        public string color { get; set; }
        public string notice { get; set; }
        public string service_phone { get; set; }
        public string description { get; set; }
        public DateInfo date_info { get; set; }
        public Sku sku { get; set; }
        public int use_limit { get; set; }
        public int get_limit { get; set; }
        public bool use_custom_code { get; set; }
        public bool bind_openid { get; set; }
        public bool can_share { get; set; }
        public bool can_give_friend { get; set; }
        public List<int> location_id_list { get; set; }
        public string center_title { get; set; }
        public string center_sub_title { get; set; }
        public string center_url { get; set; }
        public string custom_url_name { get; set; }
        public string custom_url { get; set; }
        public string custom_url_sub_title { get; set; }
        public string promotion_url_name { get; set; }
        public string promotion_url { get; set; }
        public string source { get; set; }
    }

    public class UseCondition
    {
        public string accept_category { get; set; }
        public string reject_category { get; set; }
        public bool can_use_with_other_discount { get; set; }
    }

    public class Abstract
    {
        public string @abstract { get; set; }
        public List<string> icon_url_list { get; set; }
    }

    public class TextImageList
    {
        public string image_url { get; set; }
        public string text { get; set; }
    }

    public class TimeLimit
    {
        public string type { get; set; }
        public int begin_hour { get; set; }
        public int end_hour { get; set; }
        public int begin_minute { get; set; }
        public int end_minute { get; set; }
    }

    public class AdvancedInfo
    {
        public UseCondition use_condition { get; set; }
        public Abstract @abstract { get; set; }
        public List<TextImageList> text_image_list { get; set; }
        public List<TimeLimit> time_limit { get; set; }
        public List<string> business_service { get; set; }
    }

    public class  CardInfo
    {
        public BaseInfo base_info { get; set; }
        public AdvancedInfo advanced_info { get; set; }
        
    }

    #region 团购券
    public class GrouponCardInfo : CardInfo
    {
        public string deal_detail { get; set; }
    }

    public class GrouponCard
    {
        public string card_type { get => "GROUPON"; }
        public CardInfo groupon { get; set; }
    }

    /// <summary>
    /// 团购券
    /// </summary>
    public class GrouponCardObject: ICard
    {
        public GrouponCard card { get; set; }
    }
    #endregion
    
    #region 代金券

    public class CashCardInfo : CardInfo
    {
        public int least_cost { get; set; }
        public int reduce_cost { get; set; }
    }


    public class CashCard
    {
        public string card_type { get => "CASH"; }
        public CashCardInfo cash { get; set; }
    }

    /// <summary>
    /// 代金券
    /// </summary>
    public class CashCardObject: ICard
    {
        public CashCard card { get; set; }
    }
    #endregion

    #region 折扣券

    public class DiscountCardInfo : CardInfo
    {
        public int discount { get; set; }
    }

    public class DiscountCard
    {
        public string card_type { get => "DISCOUNT"; }
        public DiscountCardInfo discount { get; set; }
    }

    /// <summary>
    /// 折扣券
    /// </summary>
    public class DiscountCardObject : ICard
    {
        public DiscountCard card { get; set; }
    }
    #endregion


    #region 兑换券

    public class GiftCardInfo : CardInfo
    {
        public string gift { get; set; }
    }

    public class GiftCard
    {
        public string card_type { get => "GIFT"; }
        public GiftCardInfo discount { get; set; }
    }

    /// <summary>
    /// 兑换券
    /// </summary>
    public class GiftCardObject: ICard
    {
        public GiftCard card { get; set; }
    }
    #endregion


    #region 优惠券

    public class GeneralCardInfo : CardInfo
    {
        public string default_detail { get; set; }
    }

    public class GeneralCard
    {
        public string card_type { get => "GENERAL_COUPON"; }
        public GeneralCardInfo discount { get; set; }
    }
    /// <summary>
    /// 优惠券
    /// </summary>
    public class GeneralCardObject: ICard
    {
        public GeneralCard card { get; set; }
    }
    #endregion




    public class GiftBaseInfo
    {
        public List<string> mchid_list { get; set; }
        public int begin_time { get; set; }
        public int end_time { get; set; }
    }

    public class MemberRule
    {
        public string card_id { get; set; }
        public int least_cost { get; set; }
        public int max_cost { get; set; }
        public string jump_url { get; set; }
    }

    public class RuleInfo
    {
        public string type { get; set; }
        public GiftBaseInfo base_info { get; set; }
        public MemberRule member_rule { get; set; }
    }

    public class PaygiftCard
    {
        public RuleInfo rule_info { get; set; }
    }


    public class FailMchidList
    {
        public string mchid { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public int occupy_rule_id { get; set; }
        public string occupy_appid { get; set; }
    }


    public class BatchgetObject
    {
        public string type { get; set; }
        public bool effective { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
    }



}
