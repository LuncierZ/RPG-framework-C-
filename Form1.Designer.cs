namespace island
{
    partial class Island
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Scene = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Scene)).BeginInit();
            this.SuspendLayout();
            // 
            // Scene
            // 
            this.Scene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Scene.Location = new System.Drawing.Point(0, 0);
            this.Scene.Name = "Scene";
            this.Scene.Size = new System.Drawing.Size(1000, 625);
            this.Scene.TabIndex = 0;
            this.Scene.TabStop = false;
            this.Scene.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Scene_MouseClick);
            this.Scene.MouseEnter += new System.EventHandler(this.Scene_MouseEnter);
            this.Scene.MouseLeave += new System.EventHandler(this.Scene_MouseLeave);
            this.Scene.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Scene_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Island
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 625);
            this.Controls.Add(this.Scene);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Island";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ISLAND";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Island_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Scene)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Scene;
        private System.Windows.Forms.Timer timer1;
    }
}

