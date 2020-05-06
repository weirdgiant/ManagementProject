using System;

namespace ManagementProject.Model
{
    public class PlanRotation:INotifyPropertyChangedClass
    {
        /// <summary>
        /// 轮序编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 轮询名称
        /// </summary>
        public string PRName { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public double PRDuration { get; set; }

        private string timeText;

        /// <summary>
        /// 时间
        /// </summary>
        public string TimeText
        {
            get { return timeText; }
            set
            {
                timeText = value;
                NotifyPropertyChanged("TimeText");
            }
        }
        //private string pRTime;

        ///// <summary>
        ///// 时间
        ///// </summary>
        //public string PRTime
        //{
        //    get { return pRTime; }
        //    set
        //    {
        //        pRTime = value;
        //        NotifyPropertyChanged("PRTime");
        //    }
        //}

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
