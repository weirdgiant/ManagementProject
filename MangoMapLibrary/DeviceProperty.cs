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
    public partial class DeviceProperty : Form
    {
        AbstractLayerElement device;

        public DeviceProperty(AbstractLayerElement device)
        {
            InitializeComponent();
            this.device = device;
            this.textBox3.Text = device.Angle.ToString();
        }

        private void DeviceProperty_Load(object sender, EventArgs e)
        {
            //label5.Text = this.device.device.ip;
            textBox2.Text = this.device.Ele.name;
            textBox1.Text = this.device.Ele.code;
            textBox3.Text = this.device.Angle.ToString();
            label3.Text = this.device.Type;
            label9.Text = this.device.p.ToString();

            if (device.Ele!=null)
                textBox4.Text = device.Ele.mapId.ToString();

            if(device is LayerElementBuilding)
            {
                this.textBox4.Text = (device as LayerElementBuilding).Ext.linkMap.ToString();
            }
            //
            textBox3.Enabled = !(device is LayerElementFence);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            device.Angle = int.Parse(textBox3.Text);
            device.Ele.name = textBox2.Text;
            device.Ele.code = textBox1.Text;
            if (device is LayerElementBuilding)
            {
                (device as LayerElementBuilding).Ext.linkMap = int.Parse(this.textBox4.Text);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '-' || e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
                e.Handled = false;
            }
            else { e.Handled = true; }
        }
    }
}
