namespace FinalProjectUdiTal
{
    partial class OutputForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonShowSolution = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCurrPos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRate = new System.Windows.Forms.Label();
            this.tblAgenda = new System.Windows.Forms.TableLayoutPanel();
            this.ucHours = new FinalProjectUdiTal.AgendHours();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ucDaily1 = new FinalProjectUdiTal.DailyCourseBlocks();
            this.ucDaily2 = new FinalProjectUdiTal.DailyCourseBlocks();
            this.ucDaily3 = new FinalProjectUdiTal.DailyCourseBlocks();
            this.ucDaily4 = new FinalProjectUdiTal.DailyCourseBlocks();
            this.ucDaily5 = new FinalProjectUdiTal.DailyCourseBlocks();
            this.ucDaily6 = new FinalProjectUdiTal.DailyCourseBlocks();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.tblAgenda.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            this.buttonNext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonNext.Location = new System.Drawing.Point(881, 32);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(72, 35);
            this.buttonNext.TabIndex = 0;
            this.buttonNext.Text = "הבא >";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonPrev
            // 
            this.buttonPrev.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonPrev.Location = new System.Drawing.Point(959, 32);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(73, 35);
            this.buttonPrev.TabIndex = 1;
            this.buttonPrev.Text = "< הקודם";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonExit.Location = new System.Drawing.Point(11, 650);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(79, 40);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "יציאה";
            this.buttonExit.UseVisualStyleBackColor = true;
            // 
            // buttonShowSolution
            // 
            this.buttonShowSolution.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonShowSolution.Location = new System.Drawing.Point(1043, 32);
            this.buttonShowSolution.Name = "buttonShowSolution";
            this.buttonShowSolution.Size = new System.Drawing.Size(111, 35);
            this.buttonShowSolution.TabIndex = 4;
            this.buttonShowSolution.Text = "חשב פיתרון";
            this.buttonShowSolution.UseVisualStyleBackColor = true;
            this.buttonShowSolution.Click += new System.EventHandler(this.buttonShowSolution_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCurrPos);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblRate);
            this.groupBox1.Controls.Add(this.tblAgenda);
            this.groupBox1.Controls.Add(this.buttonShowSolution);
            this.groupBox1.Controls.Add(this.buttonNext);
            this.groupBox1.Controls.Add(this.buttonPrev);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox1.Location = new System.Drawing.Point(11, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1170, 630);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " פתרונות ";
            // 
            // lblCurrPos
            // 
            this.lblCurrPos.AutoSize = true;
            this.lblCurrPos.Location = new System.Drawing.Point(839, 41);
            this.lblCurrPos.Name = "lblCurrPos";
            this.lblCurrPos.Size = new System.Drawing.Size(11, 14);
            this.lblCurrPos.TabIndex = 8;
            this.lblCurrPos.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "אחוזי הצלחה לפיתרון:";
            // 
            // lblRate
            // 
            this.lblRate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblRate.Location = new System.Drawing.Point(10, 52);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(41, 15);
            this.lblRate.TabIndex = 6;
            this.lblRate.Text = "0";
            // 
            // tblAgenda
            // 
            this.tblAgenda.BackColor = System.Drawing.Color.White;
            this.tblAgenda.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblAgenda.ColumnCount = 7;
            this.tblAgenda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tblAgenda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblAgenda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblAgenda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblAgenda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblAgenda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblAgenda.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblAgenda.Controls.Add(this.ucHours, 0, 1);
            this.tblAgenda.Controls.Add(this.label1, 1, 0);
            this.tblAgenda.Controls.Add(this.label3, 2, 0);
            this.tblAgenda.Controls.Add(this.label4, 3, 0);
            this.tblAgenda.Controls.Add(this.label5, 4, 0);
            this.tblAgenda.Controls.Add(this.label6, 5, 0);
            this.tblAgenda.Controls.Add(this.label7, 6, 0);
            this.tblAgenda.Controls.Add(this.ucDaily1, 1, 1);
            this.tblAgenda.Controls.Add(this.ucDaily2, 2, 1);
            this.tblAgenda.Controls.Add(this.ucDaily3, 3, 1);
            this.tblAgenda.Controls.Add(this.ucDaily4, 4, 1);
            this.tblAgenda.Controls.Add(this.ucDaily5, 5, 1);
            this.tblAgenda.Controls.Add(this.ucDaily6, 6, 1);
            this.tblAgenda.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tblAgenda.Location = new System.Drawing.Point(6, 85);
            this.tblAgenda.Name = "tblAgenda";
            this.tblAgenda.RowCount = 2;
            this.tblAgenda.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblAgenda.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAgenda.Size = new System.Drawing.Size(1158, 539);
            this.tblAgenda.TabIndex = 5;
            this.tblAgenda.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tblAgenda_CellPaint);
            // 
            // ucHours
            // 
            this.ucHours.BackColor = System.Drawing.Color.Transparent;
            this.ucHours.EndHour = 0;
            this.ucHours.Location = new System.Drawing.Point(1095, 28);
            this.ucHours.Name = "ucHours";
            this.ucHours.Size = new System.Drawing.Size(59, 507);
            this.ucHours.StartHour = 0;
            this.ucHours.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(982, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "ראשון";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(809, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "שני";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(620, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "שלישי";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(441, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "רביעי";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(258, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "חמישי";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label7.Location = new System.Drawing.Point(78, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "שישי";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucDaily1
            // 
            this.ucDaily1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDaily1.Location = new System.Drawing.Point(914, 28);
            this.ucDaily1.Name = "ucDaily1";
            this.ucDaily1.Size = new System.Drawing.Size(174, 507);
            this.ucDaily1.TabIndex = 12;
            // 
            // ucDaily2
            // 
            this.ucDaily2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDaily2.Location = new System.Drawing.Point(733, 28);
            this.ucDaily2.Name = "ucDaily2";
            this.ucDaily2.Size = new System.Drawing.Size(174, 507);
            this.ucDaily2.TabIndex = 13;
            // 
            // ucDaily3
            // 
            this.ucDaily3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDaily3.Location = new System.Drawing.Point(552, 28);
            this.ucDaily3.Name = "ucDaily3";
            this.ucDaily3.Size = new System.Drawing.Size(174, 507);
            this.ucDaily3.TabIndex = 14;
            // 
            // ucDaily4
            // 
            this.ucDaily4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDaily4.Location = new System.Drawing.Point(371, 28);
            this.ucDaily4.Name = "ucDaily4";
            this.ucDaily4.Size = new System.Drawing.Size(174, 507);
            this.ucDaily4.TabIndex = 15;
            // 
            // ucDaily5
            // 
            this.ucDaily5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDaily5.Location = new System.Drawing.Point(190, 28);
            this.ucDaily5.Name = "ucDaily5";
            this.ucDaily5.Size = new System.Drawing.Size(174, 507);
            this.ucDaily5.TabIndex = 16;
            // 
            // ucDaily6
            // 
            this.ucDaily6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDaily6.Location = new System.Drawing.Point(4, 28);
            this.ucDaily6.Name = "ucDaily6";
            this.ucDaily6.Size = new System.Drawing.Size(179, 507);
            this.ucDaily6.TabIndex = 17;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.buttonExit);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1191, 701);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // OutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonExit;
            this.ClientSize = new System.Drawing.Size(1191, 701);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "OutputForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "חלון ראשי";
            this.Load += new System.EventHandler(this.OutputForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tblAgenda.ResumeLayout(false);
            this.tblAgenda.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNext;
        
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonShowSolution;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tblAgenda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private DailyCourseBlocks ucDaily1;
        private DailyCourseBlocks ucDaily2;
        private DailyCourseBlocks ucDaily3;
        private DailyCourseBlocks ucDaily4;
        private DailyCourseBlocks ucDaily5;
        private DailyCourseBlocks ucDaily6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private AgendHours ucHours;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.Label lblCurrPos;
        private System.Windows.Forms.Label label2;
        
    }
}