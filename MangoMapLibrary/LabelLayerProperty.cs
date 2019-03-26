using MangoMapLibrary.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangoMapLibrary
{
    public partial class LabelLayerProperty : Form
    {
        MangoLayer layer;

        Color color;
        Font font;

        public LabelLayerProperty(MangoLayer layer)
        {
            InitializeComponent();
            this.layer = layer;
            this.color = ParseUtil.ParseColor(layer.labelColor);
            this.font = ParseUtil.ParseFont(layer.labelFont, layer.labelFontSize);
            this.Load += LabelLayerProperty_Load;
        }

        private void LabelLayerProperty_Load(object sender, EventArgs e)
        {
            label8.Text = color.ToString();
            label9.Text = font.FontFamily.ToString();
            label10.Text = font.Size.ToString();
        }
                

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult ret = this.colorDialog2.ShowDialog();
            if (ret == DialogResult.OK)
            {
                this.color = this.colorDialog2.Color;

                LabelLayerProperty_Load(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult ret = this.fontDialog1.ShowDialog();
            if (ret == DialogResult.OK)
            {
                this.font = this.fontDialog1.Font;
                this.LabelLayerProperty_Load(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            layer.labelColor = this.color.ToArgb().ToString();
            layer.labelFont = this.font.FontFamily.Name;
            layer.labelFontSize = (int)this.font.Size;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
