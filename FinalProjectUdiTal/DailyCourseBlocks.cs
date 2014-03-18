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
    public partial class DailyCourseBlocks : UserControl
    {
        #region Members

        private List<AgendaItem> _items;
        private List<ToolTip> _tooltips = new List<ToolTip>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the list of items for this day.
        /// </summary>
        internal List<AgendaItem> AgendaItems
        {
            get { return _items ; }
            set
            {
                _items = value;
                this.RefreshView();
            }
        }

        #endregion

        #region Ctors

        public DailyCourseBlocks()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Refreshes the view of the control to display the current Course Blocks.
        /// </summary>
        private void RefreshView()
        {
            ClearView();

            if (this.AgendaItems == null || this.AgendaItems.Count == 0)
            {
                return;
            }

            AgendaItem prevItem = null;
            foreach (AgendaItem item in this.AgendaItems.OrderBy(item => item.StartTime.TimeOfDay))
            {
                Label itemLabel = CreateLabelForItem(item, prevItem);
                ToolTip itemToolTip = CreateToolTipForItem(item, itemLabel);
                _tooltips.Add(itemToolTip);
                this.pnlLayout.Controls.Add(itemLabel);

                prevItem = item;
            }

        }

        private void ClearView()
        {
            // Dispose labels
            Control[] childControls = pnlLayout.Controls.Cast<Control>().ToArray();
            foreach (Control ctrl in childControls)
            {
                ctrl.Dispose();
            }

            // Dispose tooltips
            ToolTip[] tooltips = _tooltips.ToArray();
            foreach (ToolTip tt in tooltips)
            {
                tt.Dispose();
            }
            _tooltips.Clear();
        }

        private Label CreateLabelForItem(AgendaItem item, AgendaItem prevItem)
        {
            // Create label
            Label lbl = new Label();
            lbl.Padding = new Padding(5);
            lbl.Width = this.Width;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lbl.AutoSize = false;
            lbl.Font = new System.Drawing.Font("Tahoma", 8, FontStyle.Bold);
            lbl.Text = item.CourseName;
            lbl.BackColor = item.BackColor;

            int top, height;
            GetLabelDisplayProperties(item.StartTime, item.EndTime, prevItem == null ? null : (DateTime?)prevItem.EndTime, out top, out height);

            lbl.Margin = new Padding(0, top, 0, 0);
            lbl.Height = height;
            lbl.Width = this.Width;

            return lbl;
        }

        private ToolTip CreateToolTipForItem(AgendaItem item, Label lbl)
        {
            // Create ToolTip
            ToolTip tt = new ToolTip();
            string courseID = item.CourseNumber + "." + (item.GroupNumber < 10 ? "0" + item.GroupNumber : item.GroupNumber.ToString());
            string tooltipText = item.EndTime.ToShortTimeString() + " - " + item.StartTime.ToShortTimeString() + "\n" +
                                 item.CourseName + "\n" +
                                 courseID + ", נ.ז: " + item.CourseCredit + "\n" +
                                 item.TeacherName;
            tt.InitialDelay = 0;
            tt.SetToolTip(lbl, tooltipText);
            tt.ToolTipTitle = item.CourseType == 1 ? "שיעור" : "תרגול";

            return tt;
        }

        private void GetLabelDisplayProperties(DateTime startTime, DateTime endTime, DateTime? endTimePrev, out int top, out int height)
        {
            // The control represents a day that starts at 8am and ends at 10pm. Classes are synced with a quarter of an hour.
            // Therefore, the control is used to display 14 hours, which are 56 quarters.
            int quarterHeight = Convert.ToInt32(this.Height/56);   // The height of a single quarter.
            int startTimeInQuarters = (startTime.Hour - 8) * 4 + (startTime.Minute / 15);
            int endTimeInQuarters = (endTime.Hour - 8) * 4 + (endTime.Minute / 15);
            int endTimePrevQuarters = !endTimePrev.HasValue ? 0 : (endTimePrev.Value.Hour - 8) * 4 + (endTimePrev.Value.Minute / 15);

            top = (startTimeInQuarters -endTimePrevQuarters) * quarterHeight;
            height = (endTimeInQuarters-startTimeInQuarters) * quarterHeight;
        }
        

        #endregion
    }
}
