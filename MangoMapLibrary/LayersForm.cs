using SharpMap.Layers;
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
    public partial class LayersForm : Form
    {
        private MapView map;
        public LayersForm(MapView map)
        {
            InitializeComponent();
            this.map = map;

            this.Load += LayersForm_Load;
        }

        private void LayersForm_Load(object sender, EventArgs e)
        {
            this.RefreshList();            
        }

        private void RefreshList()
        {
            this.checkedListBox1.Items.Clear();
            var map = this.map.Map();
            foreach (ILayer layer in map.Layers)
            {

                int index = this.checkedListBox1.Items.Add(layer);
                this.checkedListBox1.SetItemChecked(index, layer.Enabled);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.map.CreateLayer(openFileDialog1.FileName);
                this.RefreshList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var tmp   = this.checkedListBox1.SelectedItems;
            List<ILayer> items = new List<ILayer>();
            foreach(ILayer item in tmp)
            {
                items.Add(item);
            }
            foreach(var item in items)
            {
                this.map.RemoveLayer((item as ILayer).LayerName);
                this.map.Refresh();
                this.RefreshList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var tmp = this.checkedListBox1.SelectedItem;
            if(tmp is MangoVectorLayer)
            {
                MangoVectorLayer layer = (MangoVectorLayer)tmp;
                LayerProperty p = new LayerProperty(layer);
                if(p.ShowDialog() == DialogResult.OK)
                {
                    map.InvokeOnLayerChanged(layer);
                }
                return;
            }
            
            if(tmp is MangoLabelLayer)
            {
                MangoLabelLayer layer = tmp as MangoLabelLayer;
                
                LabelLayerProperty p = new LabelLayerProperty(layer.Layer);
                if(p.ShowDialog() == DialogResult.OK)
                {
                    map.InvokeOnLayerChanged(layer);
                }
                return;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tmp = this.checkedListBox1.SelectedItem;
            button3.Enabled = button5.Enabled = button4.Enabled = (tmp is MangoVectorLayer || tmp is MangoGDILayer);
            button2.Enabled = tmp is AbstractMangoLayer;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var layer = this.checkedListBox1.SelectedItem;
        }
    }
}
