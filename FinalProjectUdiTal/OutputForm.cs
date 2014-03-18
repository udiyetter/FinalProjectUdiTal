using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinalProjectUdiTal
{
    public partial class OutputForm : Form
    {
        private const string k_CS = @"server=sql2.freemysqlhosting.net;userid=sql217024;
            password=bL9*yK9*;database=sql217024";
        const int k_NumberOfSolutionToPresent = 4;
        const int MAX_NUMBER_OF_COURSES = 7;
        int m_NumberOfClass;
        //string m_OutPutMessage;
        internal List<VectorOfCourses> m_FinalCourseList;
        //string[] messgaeArray = new string[k_NumberOfSolutionToPresent];
        private Color[] _colors;

        public OutputForm()
        {
            InitializeComponent();
            m_NumberOfClass = 0;
            buttonNext.Enabled = false;
            buttonPrev.Enabled = false;

            _colors = new Color[] {
                Color.FromArgb(255,209,41),
                Color.FromArgb(149,174,57),
                Color.FromArgb(212,46,32),
                Color.FromArgb(111,204,212),
                Color.FromArgb(247,145,60),
                Color.FromArgb(224,125,96),
                Color.FromArgb(253,230,188)
            };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_NumberOfClass != Math.Min(k_NumberOfSolutionToPresent - 1, m_FinalCourseList.Count - 1))
            {
                m_NumberOfClass++;
                buttonPrev.Enabled = true;
                ShowSolution(m_FinalCourseList[m_NumberOfClass]);
                if (m_NumberOfClass == Math.Min(k_NumberOfSolutionToPresent-1,m_FinalCourseList.Count - 1))
                {
                    buttonNext.Enabled = false;
                }

                lblCurrPos.Text = m_NumberOfClass + 1 + "/" + Math.Min(k_NumberOfSolutionToPresent , m_FinalCourseList.Count );
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (m_NumberOfClass != 0)
            {
                m_NumberOfClass--;
                buttonNext.Enabled = true;
                ShowSolution(m_FinalCourseList[m_NumberOfClass]);
                if (m_NumberOfClass == 0)
                {
                    buttonPrev.Enabled = false;
                }

                lblCurrPos.Text = m_NumberOfClass + 1 + "/" + Math.Min(k_NumberOfSolutionToPresent, m_FinalCourseList.Count);
            }
        }

        /*
        private void buttonShowSolution_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;
            int i;
            int j;
            int loop;
            int s;
            buttonNext.Enabled = true;
            buttonPrev.Enabled = false;
            buttonShowSolution.Enabled = false;
            string[] quoteArr = new string[17];
            string[] solutionArr = new string[17];
            for (i=0; i < k_NumberOfSolutionToPresent; i++)
            {
                s = i + 1;
                messgaeArray[i] = "";
                messgaeArray[i] += "Solution " + s + " of 4 \n";               
                messgaeArray[i] += "Schedule grade is: " + (100 - m_FinalCourseList[i].Rate) + "\n";
                messgaeArray[i] += "\n";
                for(j=0; j <= 6; j++)
                {
                    quoteArr[0] = "SELECT titlecourses FROM sql217024.coursesnames where idcourses = '" + m_FinalCourseList[i].Vector[j].ID + "'"; //quoteCourseName
                    quoteArr[1] = "SELECT teachername FROM sql217024.teachersmapping where teacherid = '" + m_FinalCourseList[i].Vector[j].TeacherCodeOfFirstClass + "'"; //quoteFirstTeacherName
                    quoteArr[2] = "SELECT teachername FROM sql217024.teachersmapping where teacherid = '" + m_FinalCourseList[i].Vector[j].TeacherCodeOfSecondClass + "'"; //quoteSecondTeacherName
                    quoteArr[3] = "SELECT teachername FROM sql217024.teachersmapping where teacherid = '" + m_FinalCourseList[i].Vector[j].TeacherCodeOfThirdClass + "'"; //quoteThirdTeacherName
                    quoteArr[4] = "SELECT coursecredit FROM sql217024.coursescredit where courseid = '" + m_FinalCourseList[i].Vector[j].ID + "'"; //quoteFirstCourseCresit
                    quoteArr[5] = "SELECT starttime FROM sql217024.timemapping where timeid =  '" + m_FinalCourseList[i].Vector[j].StartTimeSlotOfFirstClass + "'"; //startHourFirst
                    quoteArr[6] = "SELECT starttime FROM sql217024.timemapping where timeid =  '" + m_FinalCourseList[i].Vector[j].StartTimeSlotOfSecondClass + "'"; //startHourSecond
                    quoteArr[7] = "SELECT starttime FROM sql217024.timemapping where timeid =  '" + m_FinalCourseList[i].Vector[j].StartTimeSlotOfThirdClass + "'"; //startHourThird
                    quoteArr[8] = "SELECT endtime FROM sql217024.timemapping where timeid =  '" + m_FinalCourseList[i].Vector[j].EndTimeSlotOfFirstClass + "'"; //endHourFirst
                    quoteArr[9] = "SELECT endtime FROM sql217024.timemapping where timeid =  '" + m_FinalCourseList[i].Vector[j].EndTimeSlotOfSecondClass + "'"; //endHourSecond
                    quoteArr[10] = "SELECT endtime FROM sql217024.timemapping where timeid =  '" + m_FinalCourseList[i].Vector[j].EndTimeSlotOfThirdClass + "'"; //endHourThird
                    quoteArr[11] = m_FinalCourseList[i].Vector[j].ID + "." + m_FinalCourseList[i].Vector[j].GroupNumberForFirstClass; //courseCodeFirst
                    quoteArr[12] = m_FinalCourseList[i].Vector[j].ID + "." + m_FinalCourseList[i].Vector[j].GroupNumberForSecondClass; //courseCodeSecond
                    quoteArr[13] = m_FinalCourseList[i].Vector[j].ID + "." + m_FinalCourseList[i].Vector[j].GroupNumberForThirdClass; //courseCodeThird
                    quoteArr[14] = m_FinalCourseList[i].Vector[j].TypeOfFirstClass.ToString(); //courseTypeFisrt
                    quoteArr[15] = m_FinalCourseList[i].Vector[j].TypeOfSecondClass.ToString(); //courseTypeSecond
                    quoteArr[16] = m_FinalCourseList[i].Vector[j].TypeOfThirdClass.ToString(); //courseTypeThird

                    for (loop = 0; loop < 11; loop++)
                    {
                        try
                        {
                            conn = new MySqlConnection(k_CS);
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand(quoteArr[loop], conn);
                            rdr = cmd.ExecuteReader();
                            rdr.Read();
                            if (rdr.HasRows)
                            {
                                solutionArr[loop] = rdr.GetString(0);
                            }
                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine("Error: {0}", ex.ToString());

                        }
                        finally
                        {
                            if (rdr != null)
                            {
                                rdr.Close();
                            }

                            if (conn != null)
                            {
                                conn.Close();
                            }

                        }
                    }
                    
                    if( m_FinalCourseList[i].Vector[j].StartTimeSlotOfFirstClass != -1)
                    {

                        messgaeArray[i] += quoteArr[11] + " " + solutionArr[0] + " " + (m_FinalCourseList[i].Vector[j].TypeOfFirstClass == 1 ? "שיעור" : "תרגיל") + " " + solutionArr[1] + " " + solutionArr[4] + " נ.ז." + " " + (getDayFromTimeFrame(m_FinalCourseList[i].Vector[j].StartTimeSlotOfFirstClass)) + " " + solutionArr[5] + " " + solutionArr[8] + "\n";
                    }
                    if (m_FinalCourseList[i].Vector[j].StartTimeSlotOfSecondClass != -1)
                    {

                        messgaeArray[i] += quoteArr[12] + " " + solutionArr[0] + " " + (m_FinalCourseList[i].Vector[j].TypeOfSecondClass == 1 ? "שיעור" : "תרגיל") + " " + solutionArr[2] + " " + solutionArr[4] + " נ.ז." + " " + (getDayFromTimeFrame(m_FinalCourseList[i].Vector[j].StartTimeSlotOfSecondClass)) + " " + solutionArr[6] + " " + solutionArr[9] + "\n";
                    }
                    if (m_FinalCourseList[i].Vector[j].StartTimeSlotOfThirdClass != -1)
                    {

                        messgaeArray[i] += quoteArr[13] + solutionArr[0] + " " + (m_FinalCourseList[i].Vector[j].TypeOfThirdClass == 1 ? "שיעור" : "תרגיל") + " " + solutionArr[3] + " " + solutionArr[4] +" נ.ז." + " " + (getDayFromTimeFrame(m_FinalCourseList[i].Vector[j].StartTimeSlotOfThirdClass)) + " " + solutionArr[7] + " " + solutionArr[10] + "\n";
                    }
                }

            }
            labelOutput.Text = messgaeArray[0];
            m_NumberOfClass = 0;
            
        }
         * */

        private void buttonShowSolution_Click(object sender, EventArgs e)
        {
            buttonNext.Enabled = true;
            buttonPrev.Enabled = false;
            buttonShowSolution.Enabled = false;

            ShowSolution(m_FinalCourseList[0]);

            lblCurrPos.Text = "1/" + k_NumberOfSolutionToPresent;
        }

        private void OutputForm_Load(object sender, EventArgs e)
        {
            ucHours.StartHour = 8;
            ucHours.EndHour = 22;
            ucHours.InitControl();
        }

        private void tblAgenda_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            // Color right column
            if (e.Column == 0)
            {
                ColorCell(e, Color.LightGray);
            }

            // Color head row
            else if (e.Row == 0 && e.Column > 0)
            {
                ColorCell(e, Color.FromKnownColor(KnownColor.LightSteelBlue));
            };
        }

        private void ColorCell(TableLayoutCellPaintEventArgs e, Color c)
        {
            Graphics g = e.Graphics;
            Rectangle r = e.CellBounds;
            g.FillRectangle(new SolidBrush(c), r);
        }

        private void ShowSolution(VectorOfCourses weekCourses)
        {
            lblRate.Text = (100 - weekCourses.Rate).ToString();
            List<AgendaItem> allAgendaItems = new List<AgendaItem>();

            for (int i = 0; i < MAX_NUMBER_OF_COURSES; i++)
            {
                allAgendaItems.AddRange(ExtractAgendaItemsFromBlock(weekCourses.Vector[i]));
            }
            AssignBackColors(allAgendaItems);

            var groupedItems = allAgendaItems.GroupBy(item => (int)item.Day).ToArray(); // Group by day of the week
            
            // For each day of the week
            for (int i = 0; i < 6; i++)
            {
                var foundItemsForDay = groupedItems.FirstOrDefault(g => g.Key == i);
                this.DailyUserControls[i].AgendaItems = foundItemsForDay == null ? new List<AgendaItem>() : foundItemsForDay.ToList();
                Console.WriteLine("Day " + i + " -> " + (foundItemsForDay != null ? foundItemsForDay.Count() : 0) + " items");
            }

            Console.WriteLine("----------------- SOLUTION SHOW ENDED -----------------");
        }

        private void AssignBackColors(IEnumerable<AgendaItem> all)
        {
            int colorIndex = 0;
            var groupedByCourse = all.GroupBy(item => item.CourseNumber);
            foreach (var group in groupedByCourse)
            {
                group.ToList().ForEach(item => item.BackColor = _colors[colorIndex]);
                colorIndex++;
            }
        }

        private Dictionary<int, DailyCourseBlocks> _dailyUserControls = null;
        private Dictionary<int, DailyCourseBlocks> DailyUserControls
        {
            get
            {
                // Lazy init
                if (_dailyUserControls == null)
                {
                    _dailyUserControls = new Dictionary<int, DailyCourseBlocks>()
                    {
                        {0, ucDaily1},
                         {1, ucDaily2},
                          {2, ucDaily3},
                          {3, ucDaily4},
                            {4, ucDaily5},
                           {5, ucDaily6}
                    };
                }

                return _dailyUserControls;
            }
        }

        private List<AgendaItem> ExtractAgendaItemsFromBlock(CourseBlock b)
        {
            List<AgendaItem> items = new List<AgendaItem>();

            // First class (if exists)
            if (b.HasFirstClass)
            {
                AgendaItem first = new AgendaItem()
                {
                    CourseNumber = b.ID,
                    CourseName = b.CourseTitle,
                    CourseType = b.TypeOfFirstClass,
                    TeacherName = b.TeacherNameOfFirstClass,
                    GroupNumber = b.GroupNumberForFirstClass,
                    StartTime = b.StartTimeSlotOfFirstClassEx.Value,
                    EndTime = b.EndTimeSlotOfFirstClassEx.Value,
                    Day = b.DayOfFirstClass,
                    CourseCredit = b.CourseCredit
                };
                items.Add(first);
            }

            // Second class (if exists)
            if (b.HasSecondClass)
            {
                AgendaItem second = new AgendaItem()
                {
                    CourseNumber = b.ID,
                    CourseName = b.CourseTitle,
                    CourseType = b.TypeOfSecondClass,
                    TeacherName = b.TeacherNameOfSecondClass,
                    GroupNumber = b.GroupNumberForSecondClass,
                    StartTime = b.StartTimeSlotOfSecondClassEx.Value,
                    EndTime = b.EndTimeSlotOfSecondClassEx.Value,
                    Day = b.DayOfSecondClass,
                    CourseCredit = b.CourseCredit
                };
                items.Add(second);
            }

            // Third class (if exists)
            if (b.HasThirdClass)
            {
                AgendaItem third = new AgendaItem()
                {
                    CourseNumber = b.ID,
                    CourseName = b.CourseTitle,
                    CourseType = b.TypeOfThirdClass,
                    TeacherName = b.TeacherNameOfThirdClass,
                    GroupNumber = b.GroupNumberForThirdClass,
                    StartTime = b.StartTimeSlotOfThirdClassEx.Value,
                    EndTime = b.EndTimeSlotOfThirdClassEx.Value,
                    Day = b.DayOfThirdClass,
                    CourseCredit = b.CourseCredit
                };
                items.Add(third);
            }

            return items;
        }
    }
}
