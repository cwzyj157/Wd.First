using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace OtherTestProject.Console
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    internal sealed class EnumDescribeAttribute : System.Attribute
    {
        /// <summary>
        /// 描述信息
        /// </summary>
        private string describe;

        /// <summary>
        /// 构造函数
        /// </summary>
        public EnumDescribeAttribute()
        {
        }

        public string Describe
        {
            get { return describe; }
            set { describe = value; }
        }
    }

    /// <summary>
    /// 单据业务流程归属模块类型
    /// </summary>
    internal enum OrderBizModuleType
    {
        /// <summary>
        /// 采购管理
        /// </summary>
        [EnumDescribe(Describe = "采购管理")]
        PO,
        /// <summary>
        /// 销货管理
        /// </summary>
        [EnumDescribe(Describe = "销货管理")]
        SA,
        /// <summary>
        /// 库存管理
        /// </summary>
        [EnumDescribe(Describe = "库存管理")]
        KC,
        /// <summary>
        /// 质检管理
        /// </summary>
        [EnumDescribe(Describe = "质检管理")]
        QC,
        /// <summary>
        /// 生产管理
        /// </summary>
        [EnumDescribe(Describe = "生产管理")]
        MO,
        /// <summary>
        /// 委外加工
        /// </summary>
        [EnumDescribe(Describe = "委外加工")]
        TW,
        /// <summary>
        /// 售后服务
        /// </summary>
        [EnumDescribe(Describe = "售后服务")]
        MT
    }

    internal static class OrderBizModuleTypeExtension
    {
        public static string GetDescribe(this OrderBizModuleType orderBizModuleType)
        {
            string result = string.Empty;

            BindingFlags bflags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.DeclaredOnly | BindingFlags.Static;
            MemberTypes mtypes = MemberTypes.Field | MemberTypes.Property;

            MemberInfo[] memberInfos = typeof(OrderBizModuleType).FindMembers(mtypes, bflags, Type.FilterName, orderBizModuleType.ToString());

            if (memberInfos != null && memberInfos.Length > 0)
            {
                bool isDefined = Attribute.IsDefined(memberInfos[0], typeof(EnumDescribeAttribute), false);
                if (isDefined)
                {
                    EnumDescribeAttribute attribute = Attribute.GetCustomAttribute(memberInfos[0], typeof(EnumDescribeAttribute), false)
                        as EnumDescribeAttribute;
                    if (attribute != null)
                    {
                        result = attribute.Describe;
                        System.Console.WriteLine("{0}:{1}", orderBizModuleType.ToString(), attribute.Describe);
                    }
                }
            }
            return result;
        }
    }
}
