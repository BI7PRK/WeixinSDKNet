using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WeixinSDK.Net
{
    public sealed class ObjectCloneValue<TDerive> where TDerive : class, new()
    {
        private static BindingFlags bFlags = BindingFlags.Instance
              | BindingFlags.Public
              | BindingFlags.GetProperty
              | BindingFlags.SetProperty
              | BindingFlags.IgnoreCase;

        /// <summary>
        /// 复制值
        /// </summary>
        /// <typeparam name="TBase">基类</typeparam>
        /// <param name="objBase">基类对象值</param>
        /// <returns></returns>
        public static TDerive Clone(object objBase)
        {
            var TDer = new TDerive();
            if (objBase == null) return TDer;
            var derType = TDer.GetType();
            var baseType = objBase.GetType();

            if (!baseType.IsClass)
                return TDer;

            var bFlags = BindingFlags.Instance 
                | BindingFlags.Public 
                | BindingFlags.GetProperty 
                | BindingFlags.SetProperty 
                | BindingFlags.IgnoreCase;

            foreach (var item in derType.GetProperties(bFlags))
            {
                try
                {
                    var Prope = baseType.GetProperty(item.Name.ToLower(), bFlags);
                    if (Prope != null && item.CanWrite)
                    {
                        var objValue = Prope.GetValue(objBase, null);
                        item.SetValue(TDer, objValue, null);
                    }
                }
                catch { }
            }
            return TDer;
        }

        public static TDerive SetValue(TDerive derive, object obj)
        {
            if (obj == null) return derive;
            var sourceType = derive.GetType();
            foreach (var item in sourceType.GetProperties(bFlags))
            {
                try
                {
                    var Prope = obj.GetType().GetProperty(item.Name.ToLower(), bFlags);
                    if (Prope != null && item.CanWrite)
                    {
                        var objValue = Prope.GetValue(obj, null);
                        item.SetValue(derive, objValue, null);
                    }
                }
                catch { }
            }
            return derive;
        }
    }
}