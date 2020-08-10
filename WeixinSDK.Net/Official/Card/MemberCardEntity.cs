using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Official.Card
{
    
    public class CustomField
    {
        public string name_type { get; set; }
        public string url { get; set; }
    }

    public class CustomCell
    {
        public string name { get; set; }
        public string tips { get; set; }
        public string url { get; set; }
    }

    public class BonusRule
    {
        public int cost_money_unit { get; set; }
        public int increase_bonus { get; set; }
        public int max_increase_bonus { get; set; }
        public int init_increase_bonus { get; set; }
        public int cost_bonus_unit { get; set; }
        public int reduce_money { get; set; }
        public int least_money_to_use_bonus { get; set; }
        public int max_reduce_bonus { get; set; }
    }

    public class MemberCard
    {
        public string background_pic_url { get; set; }
        public BaseInfo base_info { get; set; }
        public AdvancedInfo advanced_info { get; set; }
        public bool supply_bonus { get; set; }
        public bool supply_balance { get; set; }
        public string prerogative { get; set; }
        public bool auto_activate { get; set; }
        public CustomField custom_field1 { get; set; }
        public string activate_url { get; set; }
        public CustomCell custom_cell1 { get; set; }
        public BonusRule bonus_rule { get; set; }
        public int discount { get; set; }
    }

    public class Card
    {
        public string card_type { get; set; }
        public MemberCard member_card { get; set; }
    }

    public class MemberCardObject : ICard
    {
        public Card card { get; set; }
    }

    /// <summary>
    /// 会员卡修改对象
    /// </summary>
    /// <seealso cref="WeixinSDK.Net.Official.Card.ICard" />
    public class EditMemberCardObject : ICard
    {
        public string card_id { get; set; }
        public MemberCard member_card { get; set; }
    }




    #region 会员卡



    public class MemberCardInfoResponse : ResponseBase
    {
        public MemberCardInfo CardInfo { get; set; }
    }
    public class CommonFieldList
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class UserInfo
    {
        public List<CommonFieldList> common_field_list { get; set; }
        public List<object> custom_field_list { get; set; }
    }

    public class MemberCardInfo
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public string membership_number { get; set; }
        public int bonus { get; set; }
        public string sex { get; set; }
        public UserInfo user_info { get; set; }
        public string user_card_status { get; set; }
    }
    #endregion
}
