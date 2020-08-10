using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinSDK.Net.Official
{

    #region 实体类
    public class PhotoList
    {
        public string photo_url { get; set; }
    }

    public class BaseInfo
    {
        /// <summary>
        /// 商户自己的id，用于后续审核通过收到poi_id 的通知时，做对应关系。请商户自己保证唯一识别性
        /// </summary>
        /// <value>
        /// The sid.
        /// </value>
        public string sid { get; set; }
        /// <summary>
        /// 门店名称 (15个汉字或30个英文字符内)
        /// </summary>
        /// <value>
        /// The name of the business.
        /// </value>
        public string business_name { get; set; }
        /// <summary>
        /// 分店名称（不应包含地区信息，不应与门店名有重复，错误示例：北京王府井店） 20 个字 以内
        /// </summary>
        /// <value>
        /// The name of the branch.
        /// </value>
        public string branch_name { get; set; }
        /// <summary>
        /// 门店所在的省份（直辖市填城市名,如：北京 市） 10个字 以内
        /// </summary>
        /// <value>
        /// The province.
        /// </value>
        public string province { get; set; }
        /// <summary>
        ///门店所在的城市 10个字 以内
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string city { get; set; }
        /// <summary>
        /// 门店所在地区 10个字 以内
        /// </summary>
        /// <value>
        /// The district.
        /// </value>
        public string district { get; set; }
        /// <summary>
        /// 门店所在的详细街道地址（不要填写省市信息）
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string address { get; set; }
        /// <summary>
        /// 门店的电话（纯数字，区号、分机号均由“-”隔开）
        /// </summary>
        /// <value>
        /// The telephone.
        /// </value>
        public string telephone { get; set; }
        /// <summary>
        /// 门店的类型（不同级分类用“,”隔开，如：美食，川菜，火锅。详细分类参见附件：微信门店类目表）
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public List<string> categories { get; set; }
        /// <summary>
        /// 坐标类型： 1 为火星坐标 2 为sogou经纬度 3 为百度经纬度 4 为mapbar经纬度 5 为GPS坐标 6 为sogou墨卡托坐标 注：高德经纬度无需转换可直接使用
        /// </summary>
        /// <value>
        /// The type of the offset.
        /// </value>
        public int offset_type { get; set; }
        /// <summary>
        /// 门店所在地理位置的经度
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double longitude { get; set; }
        /// <summary>
        /// 门店所在地理位置的纬度（经纬度均为火星坐标，最好选用腾讯地图标记的坐标）
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double latitude { get; set; }
        /// <summary>
        /// 图片列表，url 形式，可以有多张图片，尺寸为 640*340px。必须为上一接口生成的url。 图片内容不允许与门店不相关，不允许为二维码、员工合照（或模特肖像）、营业执照、无门店正门的街景、地图截图、公交地铁站牌、菜单截图等
        /// </summary>
        /// <value>
        /// The photo list.
        /// </value>
        public List<PhotoList> photo_list { get; set; }
        /// <summary>
        /// 推荐品，餐厅可为推荐菜；酒店为推荐套房；景点为推荐游玩景点等，针对自己行业的推荐内容 200字以内
        /// </summary>
        /// <value>
        /// The recommend.
        /// </value>
        public string recommend { get; set; }
        /// <summary>
        /// 特色服务，如免费wifi，免费停车，送货上门等商户能提供的特色功能或服务
        /// </summary>
        /// <value>
        /// The special.
        /// </value>
        public string special { get; set; }
        /// <summary>
        /// 商户简介，主要介绍商户信息等 300字以内
        /// </summary>
        /// <value>
        /// The introduction.
        /// </value>
        public string introduction { get; set; }
        /// <summary>
        /// 营业时间，24 小时制表示，用“-”连接，如 8:00-20:00
        /// </summary>
        /// <value>
        /// The open time.
        /// </value>
        public string open_time { get; set; }
        /// <summary>
        /// 人均价格，大于0 的整数
        /// </summary>
        /// <value>
        /// The average price.
        /// </value>
        public int avg_price { get; set; }
    }

    public class Business
    {

        public BaseInfo base_info { get; set; }
    }
    
    public class BusinessObject
    {
        public Business business { get; set; }
    }

    class map
    {
        public long poi_id { get; set; }
    }
    #endregion

    public class CreateStoreResponse : ResponseBase
    {
        public long StoreId { get; set; }

    }

    internal class CreateStoreRequest : RequestBase<CreateStoreResponse>
    {
      
    }

    public class StoreInfoResponse : ResponseBase
    {
        public Business business { get; set; }
    }


    public class Storeistesponse : ResponseBase
    {
        public List<Business> business_list { get; set; }
    }

    /// <summary>
    /// 门店API
    /// </summary>
    public sealed class StoreAPI
    {
        /// <summary>
        /// 创建小店
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="info">The information.</param>
        /// <returns></returns>
        public static CreateStoreResponse CreateStore(string accessToken, BusinessObject info)
        {
            var obj = new RequestBase<CreateStoreResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "poi/addpoi";
            obj.AccessToken = accessToken;

            var result = obj.Post(JsonConvert.SerializeObject(info));
            if (!result.IsError)
            {
                var map = JsonConvert.DeserializeObject<map>(result.ResonseBody);
                result.StoreId = map.poi_id;
            }
            return result;
        }

        /// <summary>
        /// Updates the store.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="info">The information.</param>
        /// <returns></returns>
        public static ResponseBase UpdateStore(string accessToken, BusinessObject info)
        {
            var obj = new RequestBase<ResponseBase>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "poi/updatepoi";
            obj.AccessToken = accessToken;
            return obj.Post(JsonConvert.SerializeObject(info));
        }

        /// <summary>
        /// Gets the store.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="poiId">The poi identifier.</param>
        /// <returns></returns>
        public static StoreInfoResponse GetStore(string accessToken, long poiId)
        {
            var obj = new RequestBase<StoreInfoResponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "poi/getpoi";
            obj.AccessToken = accessToken;

            var result = obj.Post(JsonConvert.SerializeObject(new { poi_id= poiId }));
            if (!result.IsError)
            {
                var map = JsonConvert.DeserializeObject<BusinessObject>(result.ResonseBody);
                return ObjectCloneValue<StoreInfoResponse>.SetValue(result, map);
            }
            return result;
        }

        /// <summary>
        /// Gets the store list.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="begin">The begin.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public static Storeistesponse GetStoreList(string accessToken, int begin = 0, int limit =  10)
        {
            var obj = new RequestBase<Storeistesponse>();
            obj.ServiceUrl = BaseConfig.WECHAT_CGI_API_URL;
            obj.MethodName = "poi/getpoilist";
            obj.AccessToken = accessToken;

            var result = obj.Post(JsonConvert.SerializeObject(new
            {
                begin,
                limit
            }));

            if (!result.IsError)
            {
                var map = JsonConvert.DeserializeObject<Storeistesponse>(result.ResonseBody);
                return ObjectCloneValue<Storeistesponse>.SetValue(result, map);
            }
            return result;
        }
    }
}
