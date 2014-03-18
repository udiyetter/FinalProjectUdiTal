namespace FinalProjectUdiTal
{
    partial class DailyCourseBlocks
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.ttFirstClass = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // pnlLayout
            // 
            this.pnlLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlLayout.Location = new System.Drawing.Point(0, 0);
            this.pnlLayout.Name = "pnlLayout";
            this.pnlLayout.Size = new System.Drawing.Size(150, 324);
            this.pnlLayout.TabIndex = 0;
            // 
            // ttFirstClass
            // 
            this.ttFirstClass.AutomaticDelay = 0;
            this.ttFirstClass.AutoPopDelay = 32000;
            this.ttFirstClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ttFirstClass.InitialDelay = 0;
            this.ttFirstClass.IsBalloon = true;
            this.ttFirstClass.ReshowDelay = 0;
            this.ttFirstClass.ShowAlways = true;
            this.ttFirstClass.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttFirstClass.ToolTipTitle = "פרטי קורס";
            this.ttFirstClass.UseAnimation = false;
            // 
            // DailyCourseBlocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLayout);
            this.Name = "DailyCourseBlocks";
            this.Size = new System.Drawing.Size(150, 324);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlLayout;
        private System.Windows.Forms.ToolTip ttFirstClass;
    }
}
