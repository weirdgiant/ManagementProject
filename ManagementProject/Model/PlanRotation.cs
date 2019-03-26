using System;

namespace ManagementProject.Model
{
    public class PlanRotation
    {
        /// <summary>
        /// 轮询名称
        /// </summary>
        public string PRName { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public double PRDuration { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string PRTime { get; set; }

        /// <summary>
        /// 场景名称            
        /// </summary>
        public string PRScenesName { get; set; }

        /// <summary>
        /// 是否显示下一条按钮
        /// </summary>
        public bool IsShowNext { get; set; }

        /// <summary>
        /// 可以删除吗
        /// </summary>
        public bool CanDelete { get; set; } = true;
    }
}
