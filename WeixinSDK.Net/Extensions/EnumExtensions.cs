﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WeixinSDK.Net.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举的 Description
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var field = type.GetField(enumValue.ToString());
            if (field == null) return string.Empty;
            var descriptions = field.GetCustomAttributes(typeof(DescriptionAttribute), true) as DescriptionAttribute[];

            if (descriptions.Any())
                return descriptions.First().Description;


            var display = field.GetCustomAttributes(typeof(DisplayAttribute), true) as DisplayAttribute[];

            if (display.Any())
                return display.First().Description;

            return string.Empty;
        }
        /// <summary>
        /// 获取枚举的 DefaultValue
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static object GetDefaultValue(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var field = type.GetField(enumValue.ToString());
            if (field == null) return string.Empty;
            var value = field.GetCustomAttributes(typeof(DefaultValueAttribute), true) as DefaultValueAttribute[];

            if (value.Any())
                return value.First().Value;
            
            return string.Empty;
        }
        /// <summary>
        /// 获取DefaultValue值转成String类型
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDefstrValue(this Enum enumValue)
        {
            return GetDefaultValue(enumValue).ToString();
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var field = type.GetField(enumValue.ToString());
            if (field == null) return string.Empty;
            var value = field.GetCustomAttributes(typeof(DisplayAttribute), true) as DisplayAttribute[];

            if (value.Any())
                return value.First().Name;

            return string.Empty;
        }


    }

    public sealed class EnumForEach<T>
    {
        /// <summary>
        /// 将枚举遍历成字典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, T> GetDictionary()
        {
            Dictionary<string, T> dict = new Dictionary<string, T>();
            var sourceType = typeof(T);
            foreach (string key in Enum.GetNames(sourceType))
            {
                dict.Add(key, (T)Enum.Parse(sourceType, key));
            }
            return dict;
        }
    }


}
