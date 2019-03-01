using ManagementProject.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ManagementProject
{
    /// <summary>
    /// 初始化Grid布局方式
    /// </summary>
    public  class MultiPanel
    {
        public static int CurrentModel = 0;
        public static Dictionary<int, Player> DictPanel = null;
        private static Grid OriginalGrid = null;
        private static Grid TempGrid1 = new Grid();
        private static Grid TempGrid2 = new Grid();
        private static int MaxPlayerNums = 0;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="playerNums">player数量</param>
        public static void InitGrid(Grid grid, int playerNums)
        {
            OriginalGrid = grid;
            MaxPlayerNums = playerNums;
            DictPanel = new Dictionary<int, Player>();
            for (int index = 1; index < MaxPlayerNums; index++)
            {
                Player playerPanel = new Player(PlayerWindowType.Playerback);
                playerPanel.Index = index;
                DictPanel.Add(index, playerPanel);
            }
        }
        /// <summary>
        /// 设置当前画面数量
        /// </summary>
        public static void SetCurrentModel(int model)
        {
            switch (model)
            {
                case 4:
                    SetModel4();
                    break;
                case 6:
                    SetModel6();
                    break;
                case 8:
                    SetModel8();
                    break;
                case 9:
                    SetModel9();
                    break;
                case 12:
                    SetModel12();
                    break;
                case 16:
                    SetModel8();
                    break;
            }
        }

        public static void SetCurrentLayout(int model)
        {
            switch (model)
            {
                case 1:
                    SetLayout1();
                    break;
                case 2:
                    SetLayout2();
                    break;
                case 3:
                    SetLayout3();
                    break;
                case 4:
                    SetLayout4();
                    break;
                case 5:
                    SetLayout5();
                    break;
            }
        }

        /// <summary>
        /// 获取当前画面
        /// </summary>
        /// <returns></returns>
        public static Player CurrentPlayerPanel()
        {
            for (int index = 1; index <= MaxPlayerNums; index++)
            {
                if (DictPanel[index].Selected)
                {
                    return DictPanel[index];
                }
            }
            return null;
        }

        /// <summary>
        /// 通过行列数设置画面分割
        /// </summary>
        /// <param name="column">列</param>
        /// <param name="row">行</param>
        public static void SetModelByColumnAndRow(int column, int row)
        {
            Clear();
            CreateColumnAndRow(OriginalGrid, row, column);

            int num = 1;
            for (int rowIndex = 0; rowIndex < row; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < column; columnIndex++)
                {
                    Player playerPanel = DictPanel[num];
                    Grid.SetColumn(playerPanel, columnIndex);
                    Grid.SetRow(playerPanel, rowIndex);
                    OriginalGrid.Children.Add(playerPanel);
                    num++;
                }
            }
        }
        #region PlayerPanel布局
        /// <summary>
        /// 将Grid分为4格
        /// </summary>
        public static void SetModel4()
        {

        }
        /// <summary>
        /// 将Grid分为6格
        /// </summary>
        public static void SetModel6()
        {

        }
        /// <summary>
        /// 将Grid分为8格
        /// </summary>
        public static void SetModel8()
        {

        }
        /// <summary>
        /// 将Grid分为9格
        /// </summary>
        public static void SetModel9()
        {

        }
        /// <summary>
        /// 将Grid分为12格
        /// </summary>
        public static void SetModel12()
        {

        }
        /// <summary>
        /// 将Grid分为16格
        /// </summary>
        public static void SetModel16()
        {

        }
        #endregion

        #region AlarmPage 布局
        /// <summary>
        /// AlarmMain布局
        /// //////
        /// --------- 
        /// |   1   |
        /// ---------
        /// </summary>
        public static void SetLayout1()
        {

        }
        /// <summary>
        /// AlarmMain布局
        /// --------- --------
        /// |   1   |   2    |
        /// --------- --------    
        /// |   3   |   4    |
        /// ------------------
        /// </summary>
        public static void SetLayout2()
        {

        }
        /// <summary>
        /// AlarmMain布局
        /// --------- --------
        /// |        1       |
        /// --------- --------    
        /// |   2   |   3    |
        /// ------------------
        /// </summary>
        public static void SetLayout3()
        {

        }
        /// <summary>
        /// AlarmMain布局
        /// --------- --------
        /// |        |   2   |
        /// |         --------    
        /// |   1    |   3   |
        /// |         --------
        /// |        |   4   |
        /// ------------------
        /// </summary>
        public static void SetLayout4()
        {

        }
        /// <summary>
        /// AlarmMain布局
        ///  -------- --------
        /// |        |   2   |
        /// |    1    --------    
        /// |        |   3   |
        ///  -----------------
        /// </summary>
        public static void SetLayout5()
        {

        }
        #endregion
        private static void Clear()
        {
            OriginalGrid.RowDefinitions.Clear();
            OriginalGrid.ColumnDefinitions.Clear();
            OriginalGrid.Children.Clear();
            TempGrid1.Children.Clear();
            TempGrid1.RowDefinitions.Clear();
            TempGrid1.ColumnDefinitions.Clear();
            TempGrid2.Children.Clear();
            TempGrid2.RowDefinitions.Clear();
            TempGrid2.ColumnDefinitions.Clear();

        }

        private static void CreateColumnAndRow(Grid grid, int row, int column)
        {
            for (int index = 0; index < row; ++index)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int index = 0; index < column; ++index)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

    }
}
