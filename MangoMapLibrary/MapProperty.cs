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

    public partial class MapProperty : Form
    {
        private MangoMap map;

        public MapProperty(MangoMap map)
        {
            InitializeComponent();
            this.map = map;

            this.Load += MapProperty_Load;
        }

        private void MapProperty_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = map.name;
            this.textBox2.Text = map.type.ToString();
            this.textBox3.Text = map.defaultLongitude.ToString();
            this.textBox4.Text = map.defaultLatitude.ToString();
            this.textBox5.Text = map.defaultZoomLevel.ToString();
            this.textBox6.Text = map.idx.ToString();
            this.label7.Text = Color.FromArgb(map.backgroundColor).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.map.name = this.textBox1.Text;
            this.map.type = int.Parse(this.textBox2.Text);
            map.defaultLongitude = double.Parse(this.textBox3.Text);
            map.defaultLatitude = double.Parse(this.textBox4.Text);
            map.defaultZoomLevel = int.Parse(this.textBox5.Text);
            map.idx = int.Parse(this.textBox6.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult ret = this.colorDialog1.ShowDialog();
            if(ret == DialogResult.OK)
            {
                this.map.backgroundColor = this.colorDialog1.Color.ToArgb();
                MapProperty_Load(sender, e);
            }
        }
    }
}
