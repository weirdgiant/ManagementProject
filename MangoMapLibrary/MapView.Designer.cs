namespace MangoMapLibrary
{
    partial class MapView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapView));
            this.mapBox1 = new SharpMap.Forms.MapBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.label_coor = new System.Windows.Forms.ToolStripLabel();
            this.label_zoomlevel = new System.Windows.Forms.ToolStripLabel();
            this.mapZoomToolStrip1 = new SharpMap.Forms.ToolBar.MapZoomToolStrip(this.components);
            this.mapZoomToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapBox1
            // 
            this.mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            this.mapBox1.AllowDrop = true;
            this.mapBox1.BackColor = System.Drawing.SystemColors.Control;
            this.mapBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mapBox1.CustomTool = null;
            this.mapBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapBox1.FineZoomFactor = 10D;
            this.mapBox1.Location = new System.Drawing.Point(0, 0);
            this.mapBox1.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            this.mapBox1.Name = "mapBox1";
            this.mapBox1.PanOnClick = false;
            this.mapBox1.QueryGrowFactor = 5F;
            this.mapBox1.QueryLayerIndex = 0;
            this.mapBox1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.ShowProgressUpdate = false;
            this.mapBox1.Size = new System.Drawing.Size(779, 536);
            this.mapBox1.TabIndex = 0;
            this.mapBox1.Text = "mapBox1";
            this.mapBox1.WheelZoomMagnitude = -2D;
            this.mapBox1.MouseMove += new SharpMap.Forms.MapBox.MouseEventHandler(this.mapBox1_MouseMove);
            this.mapBox1.Click += new System.EventHandler(this.mapBox1_Click);
            this.mapBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.MapView_DragDrop);
            this.mapBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.MapView_DragEnter);
            this.mapBox1.DragOver += new System.Windows.Forms.DragEventHandler(this.MapView_DragOver);
            this.mapBox1.Resize += new System.EventHandler(this.mapBox1_Resize);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "图层";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // label_coor
            // 
            this.label_coor.Name = "label_coor";
            this.label_coor.Size = new System.Drawing.Size(32, 22);
            this.label_coor.Text = "坐标";
            // 
            // label_zoomlevel
            // 
            this.label_zoomlevel.Name = "label_zoomlevel";
            this.label_zoomlevel.Size = new System.Drawing.Size(56, 22);
            this.label_zoomlevel.Text = "缩放级别";
            // 
            // mapZoomToolStrip1
            // 
            this.mapZoomToolStrip1.Enabled = false;
            this.mapZoomToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.label_coor,
            this.label_zoomlevel});
            this.mapZoomToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.mapZoomToolStrip1.MapControl = this.mapBox1;
            this.mapZoomToolStrip1.Name = "mapZoomToolStrip1";
            this.mapZoomToolStrip1.Size = new System.Drawing.Size(779, 25);
            this.mapZoomToolStrip1.TabIndex = 1;
            this.mapZoomToolStrip1.Text = "mapZoomToolStrip1";
            this.mapZoomToolStrip1.Visible = false;
            // 
            // MapView
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mapZoomToolStrip1);
            this.Controls.Add(this.mapBox1);
            this.Name = "MapView";
            this.Size = new System.Drawing.Size(779, 536);
            this.mapZoomToolStrip1.ResumeLayout(false);
            this.mapZoomToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpMap.Forms.MapBox mapBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel label_coor;
        private System.Windows.Forms.ToolStripLabel label_zoomlevel;
        private SharpMap.Forms.ToolBar.MapZoomToolStrip mapZoomToolStrip1;
    }
}
