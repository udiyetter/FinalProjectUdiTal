using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProjectUdiTal
{
    class AgendaItem
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek Day { get; set; }
        public int CourseNumber { get; set; }
        public string CourseName { get; set; }
        public int GroupNumber { get; set; }
        public int CourseType { get; set; }
        public string TeacherName { get; set; }
        public int CourseCredit { get; set; }

        public System.Drawing.Color BackColor { get; set; }
    }
}
