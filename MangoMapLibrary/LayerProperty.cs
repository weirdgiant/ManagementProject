using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpMap.Layers;

namespace MangoMapLibrary
{
    public partial class LayerProperty : Form
    {
        private MangoVectorLayer layer;
        private Color color;
                
        public LayerProperty(MangoVectorLayer layer)
        {
            InitializeComponent();
            this.layer = layer as MangoVectorLayer;
            this.color = ParseUtil.ParseColor((layer.Layer.color));
            this.LoadTextureList();
            this.Load += LayerProperty_Load;
            
        }

        private void LoadTextureList()
        {
            string rootPath = Directory.GetCurrentDirectory() + "\\Texture";
            DirectoryInfo root = new DirectoryInfo(rootPath);
            FileInfo[] files = root.GetFiles();
            foreach (FileInfo info in files)
            {
                comboBox1.Items.Add("Texture/"+info.Name);
            }
        }

        private void LayerProperty_Load(object sender, EventArgs e)
        {
            
            if(layer.Layer.renderMode == 0)
            {
                this.radioButton1.Checked = true;
                this.button2.Enabled = true;
                this.comboBox1.Enabled = false;
            }
            if (layer.Layer.renderMode == 1)
            {
                this.radioButton2.Checked = true;
                this.button2.Enabled = false;
                this.comboBox1.Enabled = true;
            }
            this.comboBox1.Text = layer.Layer.textureFilename;
            this.label6.Text = layer.LayerTitle;
            label7.Text = this.color.ToString();
            this.checkBox1.Checked = this.layer.Layer.labelEnabled == 1;
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult ret = this.colorDialog1.ShowDialog();
            if(ret == DialogResult.OK)
            {
                this.color = this.colorDialog1.Color;
                LayerProperty_Load(sender, e);
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if(layer.Layer.renderMode == 1)
            {
                layer.Layer.textureFilename = comboBox1.Text;
            }
            else
            {
                layer.Layer.color = this.color.ToArgb().ToString();
            }

            layer.Layer.labelEnabled = this.checkBox1.Checked ? 1 : 0;
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.layer.Layer.renderMode = radioButton2.Checked ? 1 : 0;
            this.LayerProperty_Load(sender, e);
        }
    }
}
