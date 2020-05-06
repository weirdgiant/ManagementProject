using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.UserControls.AlarmControls
{
    /// <summary>
    /// 水压类型
    /// </summary>
    [Remark("水压类型")]
    public enum WaterPressureType
    {
        /// <summary>
        /// 喷淋水压
        /// </summary>
        [Remark("喷淋水压")]
        Spray,

        /// <summary>
        /// 泵房水压
        /// </summary>
        [Remark("泵房水压")]
        PumpRoom,

        /// <summary>
        /// 消防水压
        /// </summary>
        [Remark("消防水压")]
        Fire,

        /// <summary>
        /// 液位
        /// </summary>
        [Remark("液位")]
        LiquidLevel,
    }
}
