using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinalProjectUdiTal
{
    public partial class AgendHours : UserControl
    {
        public AgendHours()
        {
            InitializeComponent();
        }

        public int StartHour { get; set; }
        public int EndHour { get; set; }

        public void InitControl()
        {
            int labelHeight = this.Height / (EndHour - StartHour);
            for (int i = StartHour; i < EndHour; i++)
			{
                Label lbl = new Label();
                lbl.BackColor = Color.Transparent;
                lbl.AutoSize = false;
                lbl.Font = lblExample.Font;
                lbl.ForeColor = lblExample.ForeColor;
                lbl.Text = (i < 10 ? "0" + i: i.ToString()) + ":00";
                lbl.Height = labelHeight;
                lbl.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);

                pnlLayout.Controls.Add(lbl);
			}
        }
    }
}
