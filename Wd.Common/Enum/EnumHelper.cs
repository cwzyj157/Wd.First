using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Wd.Common
{
    // 枚举
    public class EnumHelper<T> where T : struct
    {
        /// <summary>
        /// 取得枚举所有项
        /// </summary>
        /// <returns></returns>
        public static List<string> GetNames()
        {
            return Enum.GetNames(typeof(T)).ToList();
        }
        /// <summary>
        /// 取得枚举项及指定自定义属性的结果集
        /// </summary>
        /// <typeparam name="V">自定义结果类型</typeparam>
        /// <param name="key">指定自定义属性类别</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, V>> GetNamesAndAttachData<V>(object key)
        {
            FieldInfo[] fiAry = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo fi in fiAry)
            {
                if (!fi.IsSpecialName)
                {
                    yield return new KeyValuePair<string, V>(fi.Name, fi.GetAttachedData<V>(key));
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class AttachDataAttribute : Attribute
    {
        public AttachDataAttribute(object key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        public object Key { get; private set; }

        public object Value { get; private set; }
    }

    public static class AttachDataExtensions
    {
        public static object GetAttachedData(
            this ICustomAttributeProvider provider, object key)
        {
            var attributes = (AttachDataAttribute[])provider.GetCustomAttributes(
                typeof(AttachDataAttribute), false);
            return attributes.First(a => a.Key.Equals(key)).Value;
        }

        public static T GetAttachedData<T>(
            this ICustomAttributeProvider provider, object key)
        {
            return (T)provider.GetAttachedData(key);
        }

        public static object GetAttachedData(this Enum value, object key)
        {
            return value.GetType().GetField(value.ToString()).GetAttachedData(key);
        }
        public static T GetAttachedData<T>(this Enum value, object key)
        {
            return (T)value.GetAttachedData(key);
        }
    }
}
