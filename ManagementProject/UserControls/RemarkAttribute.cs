using System;
using System.Reflection;
namespace ManagementProject.UserControls
{
    /// <summary>
    /// 是给枚举用的，提供一个额外信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute(string remark) => this.Remark = remark;

        public string Remark { get; private set; }
    }

    public static class RemarkExtend
    {
        /// <summary>
        /// 获取标记信息(扩展方法)
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetRemark(this Enum enumValue)
        {
            Type type = enumValue.GetType();
            FieldInfo field = type.GetField(enumValue.ToString());
            if (field.IsDefined(typeof(RemarkAttribute), true))
            {
                RemarkAttribute remarkAttribute = (RemarkAttribute)field.GetCustomAttribute(typeof(RemarkAttribute));
                return remarkAttribute.Remark;
            }
            else
            {
                return enumValue.ToString();
            }
        }
    }
}
