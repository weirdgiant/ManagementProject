using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.UserControls.FightControl.CamResource
{
    /// <summary>
    /// 目录项的信息，例如驱动器，文件或文件夹
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// 这个Item的类型
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// 全路径
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// 此目录的名称
        /// </summary>
        public string Name => Type == DirectoryItemType.Drive ? FullPath : DirectoryStructure.GetFileFolderName(FullPath);
    }
}
