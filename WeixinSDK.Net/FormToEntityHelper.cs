using System;
using System.Collections.Specialized;
using System.Reflection;

namespace WeixinSDK.Net
{
    /// <summary>
    /// 表单转实体对象
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public sealed class FormToEntityHelper<TModel> 
        where TModel : class, new()
    {
        private static BindingFlags flags = BindingFlags.Instance
           | BindingFlags.Public
           | BindingFlags.IgnoreCase;

        /// <summary>
        /// 参数集合转实体对象
        /// </summary>
        /// <param name="collection">表单键值集合</param>
        /// <returns></returns>
        public static TModel DeserializeNameValue(NameValueCollection collection)
        {
            Type modelType = typeof(TModel);
            TModel entity = new TModel();
            foreach (var item in collection.AllKeys)
            {
                var value = collection[item];
                var Proty = modelType.GetProperty(item, flags);
                if (Proty != null && !Proty.PropertyType.IsGenericType && !string.IsNullOrEmpty(value) && Proty.CanWrite)
                {
                    try
                    {
                        if(Proty.PropertyType.IsEnum)
                        {
                            Proty.SetValue(entity, Enum.Parse(Proty.PropertyType, value), null);
                        }
                        else
                        {
                            Proty.SetValue(entity, Convert.ChangeType(value, Proty.PropertyType), null);
                        }
                      
                    }
                    catch (Exception)
                    {
                        //throw new Exception(val + ":"+ ex.Message);
                    }
                  
                }
              
            }
            return entity;
        }

    }
}