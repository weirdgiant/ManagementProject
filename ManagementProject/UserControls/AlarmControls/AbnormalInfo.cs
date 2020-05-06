using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.UserControls.AlarmControls
{
    /// <summary>
    /// 异常信息
    /// </summary>
    [Remark("异常信息")]
    public enum AbnormalInfo
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Remark("正常")]
        Normal,

        /// <summary>
        /// 异常
        /// </summary>
        [Remark("异常")]
        Abnormal,
    }
}
