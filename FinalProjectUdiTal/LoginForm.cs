using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinalProjectUdiTal
{
    public partial class LoginForm : Form
    {
        #region Members

        Student _student;
        internal GeneticAlgorithm myGeneticAlgo;
        string messageOut = "";

        private Label[][] _labelsMapping = null;

        #endregion

        #region Properties

        public ComboBox ComboBoxUserId
        {
            get { return comboBoxUdersID; }
        }

        private Label[][] LabelsMapping
        {
            get
            {
                // Lazy init
                if (_labelsMapping == null)
                {
                    // Window, StartsAt, StartsBefore, StudyAt
                    _labelsMapping = new Label[][] {
                        new Label[4] {lblWindowsDay1, lblStartAtDay1, lblStartBeforeDay1, lblStudyAtDay1},
                        new Label[4] {lblWindowsDay2, lblStartAtDay2, lblStartBeforeDay2, lblStudyAtDay2},
                        new Label[4] {lblWindowsDay3, lblStartAtDay3, lblStartBeforeDay3, lblStudyAtDay3},
                        new Label[4] {lblWindowsDay4, lblStartAtDay4, lblStartBeforeDay4, lblStudyAtDay4},
                        new Label[4] {lblWindowsDay5, lblStartAtDay5, lblStartBeforeDay5, lblStudyAtDay5},
                        new Label[4] {lblWindowsDay6, lblStartAtDay6, lblStartBeforeDay6, lblStudyAtDay6}
                    };
                }

                return _labelsMapping;
            }
        }

        #endregion

        #region Ctors

        public LoginForm()
        {
            InitializeComponent();
            buttonOK.Enabled = false;
            PreferenceLabel.Text = "";
            comboBoxUdersID.Items.Add(1234567);
            comboBoxUdersID.Items.Add(2345678);
            comboBoxUdersID.Items.Add(3456789);
            comboBoxUdersID.Items.Add(4567890);

        }

        #endregion

        #region Event Handlers

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxUdersID_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            string screenView = "";
            buttonOK.Enabled = true;
            Student newStudent = new Student(int.Parse(comboBoxUdersID.Text));
            screenView = string.Format(@"Student preferences are (Start hour at, Windows, Starting after hour, Is study at day):
 
Sunday:   {0}, {6}, {12}, {18}
Monday:   {1}, {7}, {13}, {19}
Tuesday:   {2}, {8}, {14}, {20}
Wednesday:   {3}, {9}, {15}, {21}
Thursday:   {4}, {10}, {16}, {22}
Friday:   {5}, {11}, {17}, {23}", newStudent.StartHourPreferenceList[0], newStudent.StartHourPreferenceList[1], newStudent.StartHourPreferenceList[2], newStudent.StartHourPreferenceList[3], newStudent.StartHourPreferenceList[4], newStudent.StartHourPreferenceList[5], newStudent.WindowsPreferenceList[0], newStudent.WindowsPreferenceList[1], newStudent.WindowsPreferenceList[2], newStudent.WindowsPreferenceList[3], newStudent.WindowsPreferenceList[4], newStudent.WindowsPreferenceList[5],
                            newStudent.StartingAfterHourPreferenceList[0], newStudent.StartingAfterHourPreferenceList[1], newStudent.StartingAfterHourPreferenceList[2], newStudent.StartingAfterHourPreferenceList[3], newStudent.StartingAfterHourPreferenceList[4], newStudent.StartingAfterHourPreferenceList[5], newStudent.StudyDaysPreferenceList[0], newStudent.StudyDaysPreferenceList[1], newStudent.StudyDaysPreferenceList[2], newStudent.StudyDaysPreferenceList[3], newStudent.StudyDaysPreferenceList[4], newStudent.StudyDaysPreferenceList[5]);
            PreferenceLabel.Text = screenView;
            */

            // NEW
            buttonOK.Enabled = true;
            Student newStudent = new Student(int.Parse(comboBoxUdersID.Text));
            for (int i = 0; i < 6; i++)
            {
                PopualtePrefLabels(newStudent, i,
                    this.LabelsMapping[i][0],
                    this.LabelsMapping[i][1],
                    this.LabelsMapping[i][2],
                    this.LabelsMapping[i][3]);
            }
        }

        //public List<VectorOfCourses> GeneticAlgoFinalList
        //{
        //    get { return myGeneticAlgo.FinalList; }
        //}

        #endregion

        #region Methods

        /// <summary>
        /// Populates the given labels with representative strings of the users preferences.
        /// </summary>
        /// <param name="student">The student to dispaly preferences of.</param>
        /// <param name="dayOfTheWeek">An integer indicating the day of the weeks (1-6).</param>
        /// <param name="windowsLabel">A label to display "Windows" preference.</param>
        /// <param name="startAtLabel">A label to display "Start At" preference.</param>
        /// <param name="startBeforeLabel">A label to display "Start Before" preference.</param>
        /// <param name="studyAtLabel">A label to display "Study At Day" preference.</param>
        private void PopualtePrefLabels(Student student, int dayOfTheWeek, Label windowsLabel, Label startAtLabel, Label startBeforeLabel, Label studyAtLabel)
        {
            if (windowsLabel != null)
            {
                Student.ePreferences windowsPref = student.WindowsPreferenceList[dayOfTheWeek];
                windowsLabel.Text = windowsPref.RepresentativeStringOf();
                StylizeLabelByPref(windowsLabel, windowsPref);
            }

            if (startAtLabel != null)
            {
                Student.ePreferences startAtPref = student.StartHourPreferenceList[dayOfTheWeek];
                startAtLabel.Text = startAtPref.RepresentativeStringOf();
                StylizeLabelByPref(startAtLabel, startAtPref);
            }

            if (startBeforeLabel != null)
            {
                Student.ePreferences startBeforePref =  student.StartingAfterHourPreferenceList[dayOfTheWeek];
                startBeforeLabel.Text = startBeforePref.RepresentativeStringOf();
                StylizeLabelByPref(startBeforeLabel, startBeforePref);
            }

            if (studyAtLabel != null)
            {
                Student.ePreferences studyAtPref = student.StudyDaysPreferenceList[dayOfTheWeek];
                studyAtLabel.Text = studyAtPref.RepresentativeStringOf();
                StylizeLabelByPref(studyAtLabel, studyAtPref);
            }
        }

        private void StylizeLabelByPref(Label label, Student.ePreferences pref)
        {
            label.Font = new Font(label.Font, pref != Student.ePreferences.noPreference ? FontStyle.Bold : FontStyle.Regular);
            label.ForeColor = pref == Student.ePreferences.noPreference ? Color.LightGray : Color.Black;
        }

        #endregion

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Column == 0 || e.Row == 0)
            {
                ColorCell(e, Color.FromKnownColor(KnownColor.LightSteelBlue));
            }
        }

        private void ColorCell(TableLayoutCellPaintEventArgs e, Color c)
        {
            Graphics g = e.Graphics;
            Rectangle r = e.CellBounds;
            g.FillRectangle(new SolidBrush(c), r);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // Activate double buffering at the form level.  All child controls will be double buffered as well.
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                return cp;
            }
        } 
    }
}
