using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManagementProject
{
    /// <summary>
    /// PdfReader.xaml 的交互逻辑
    /// </summary>
    public partial class PdfReader : Window
    {
        public string pdfPath { get; set; }
        public PdfReader(string path)
        {
            InitializeComponent();
            pdfPath = path;
            Loaded += PdfReader_Loaded;
            Closed += PdfReader_Closed; ;
        }

        private void PdfReader_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(pdfPath))
            {
                moonPdfPanel.OpenFile(pdfPath);
            }else
            {
                MessageBox.Show("加载文件失败");
            }
            
        }

        private void PdfReader_Closed(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(pdfPath))
                {
                    File.Delete(pdfPath);
                }
            }catch (Exception ex)
            {
                Logger.Error(typeof(PdfReader), ex.Message);
            }
        }

    }
}
