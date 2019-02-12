using ManagementProject.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ManagementProject
{
    public static class MultiPanel
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
        public static void InitGrid(Grid grid ,int playerNums)
        {
            OriginalGrid = grid;
            MaxPlayerNums = playerNums;
            DictPanel = new Dictionary<int, Player>();
            for (int index=1;index<MaxPlayerNums;index ++)
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

        public static void SetModel6()
        {

        }



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
