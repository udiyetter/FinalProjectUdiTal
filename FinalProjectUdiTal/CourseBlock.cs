using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectUdiTal
{
    class CourseBlock
    {
        public CourseBlock(int i_CourseId, int i_GroupNumberForFirstClass, int i_TypeOfFirstClass, int i_StartTimeSlotOfFirstClass,
        int i_EndTimeSlotOfFirstClass, int i_TeacherCodeOfFirstClass, int i_GroupNumberForSecondClass, int i_TypeOfSecondClass,
        int i_StartTimeSlotOfSecondClass, int i_EndTimeSlotOfSecondClass, int i_TeacherCodeOfSecondClass, int i_GroupNumberForThirdClass,
        int i_TypeOfThirdClass, int i_StartTimeSlotOfThirdClass, int i_EndTimeSlotOfThirdClass, int i_TeacherCodeOfThirdClass)
        {
            m_CourseId = i_CourseId;
            m_GroupNumberForFirstClass = i_GroupNumberForFirstClass;
            m_TypeOfFirstClass = i_TypeOfFirstClass;
            m_StartTimeSlotOfFirstClass = i_StartTimeSlotOfFirstClass;
            m_EndTimeSlotOfFirstClass = i_EndTimeSlotOfFirstClass;
            m_TeacherCodeOfFirstClass = i_TeacherCodeOfFirstClass;
            m_GroupNumberForSecondClass = i_GroupNumberForSecondClass;
            m_TypeOfSecondClass = i_TypeOfSecondClass;
            m_StartTimeSlotOfSecondClass = i_StartTimeSlotOfSecondClass;
            m_EndTimeSlotOfSecondClass = i_EndTimeSlotOfSecondClass;
            m_TeacherCodeOfSecondClass = i_TeacherCodeOfSecondClass;
            m_GroupNumberForThirdClass = i_GroupNumberForThirdClass;
            m_TypeOfThirdClass = i_TypeOfThirdClass;
            m_StartTimeSlotOfThirdClass = i_StartTimeSlotOfThirdClass;
            m_EndTimeSlotOfThirdClass = i_EndTimeSlotOfThirdClass;
            m_TeacherCodeOfThirdClass = i_TeacherCodeOfThirdClass;
        }

        public CourseBlock(CourseBlock i_Block)
        {
            m_CourseId = i_Block.ID;
            m_GroupNumberForFirstClass = i_Block.GroupNumberForFirstClass;
            m_TypeOfFirstClass = i_Block.TypeOfFirstClass;
            m_StartTimeSlotOfFirstClass = i_Block.StartTimeSlotOfFirstClass;
            m_EndTimeSlotOfFirstClass = i_Block.EndTimeSlotOfFirstClass;
            m_TeacherCodeOfFirstClass = i_Block.TeacherCodeOfFirstClass;
            m_GroupNumberForSecondClass = i_Block.GroupNumberForSecondClass;
            m_TypeOfSecondClass = i_Block.TypeOfSecondClass;
            m_StartTimeSlotOfSecondClass = i_Block.StartTimeSlotOfSecondClass;
            m_EndTimeSlotOfSecondClass = i_Block.EndTimeSlotOfSecondClass;
            m_TeacherCodeOfSecondClass = i_Block.TeacherCodeOfSecondClass;
            m_GroupNumberForThirdClass = i_Block.GroupNumberForThirdClass;
            m_TypeOfThirdClass = i_Block.TypeOfThirdClass;
            m_StartTimeSlotOfThirdClass = i_Block.StartTimeSlotOfThirdClass;
            m_EndTimeSlotOfThirdClass = i_Block.EndTimeSlotOfThirdClass;
            m_TeacherCodeOfThirdClass = i_Block.TeacherCodeOfThirdClass;

            this.CourseCredit = i_Block.CourseCredit;
            this.CourseTitle = i_Block.CourseTitle;
            this.StartTimeSlotOfFirstClassEx = i_Block.StartTimeSlotOfFirstClassEx;
            this.EndTimeSlotOfFirstClassEx = i_Block.EndTimeSlotOfFirstClassEx;
            this.TeacherNameOfFirstClass = i_Block.TeacherNameOfFirstClass;
            this.StartTimeSlotOfSecondClassEx = i_Block.StartTimeSlotOfSecondClassEx;
            this.EndTimeSlotOfSecondClassEx = i_Block.EndTimeSlotOfSecondClassEx;
            this.TeacherNameOfSecondClass = i_Block.TeacherNameOfSecondClass;
            this.StartTimeSlotOfThirdClassEx = i_Block.StartTimeSlotOfThirdClassEx;
            this.EndTimeSlotOfThirdClassEx = i_Block.EndTimeSlotOfThirdClassEx;
            this.TeacherNameOfThirdClass = i_Block.TeacherNameOfThirdClass;

        }

        int m_CourseId;

        int m_GroupNumberForFirstClass;
        int m_TypeOfFirstClass;
        int m_StartTimeSlotOfFirstClass;
        int m_EndTimeSlotOfFirstClass;
        int m_TeacherCodeOfFirstClass;

        int m_GroupNumberForSecondClass;
        int m_TypeOfSecondClass;
        int m_StartTimeSlotOfSecondClass;
        int m_EndTimeSlotOfSecondClass;
        int m_TeacherCodeOfSecondClass;

        int m_GroupNumberForThirdClass;
        int m_TypeOfThirdClass;
        int m_StartTimeSlotOfThirdClass;
        int m_EndTimeSlotOfThirdClass;
        int m_TeacherCodeOfThirdClass;

        public int ID
        { get { return m_CourseId; } }

        public int GroupNumberForFirstClass
        { get { return m_GroupNumberForFirstClass; } }

        public int TypeOfFirstClass
        { get { return m_TypeOfFirstClass; } }

        public int StartTimeSlotOfFirstClass
        { get { return m_StartTimeSlotOfFirstClass; } }

        public int EndTimeSlotOfFirstClass
        { get { return m_EndTimeSlotOfFirstClass; } }

        public int TeacherCodeOfFirstClass
        { get { return m_TeacherCodeOfFirstClass; } }

        public int GroupNumberForSecondClass
        { get { return m_GroupNumberForSecondClass; } }

        public int TypeOfSecondClass
        { get { return m_TypeOfSecondClass; } }

        public int StartTimeSlotOfSecondClass
        { get { return m_StartTimeSlotOfSecondClass; } }

        public int EndTimeSlotOfSecondClass
        { get { return m_EndTimeSlotOfSecondClass; } }

        public int TeacherCodeOfSecondClass
        { get { return m_TeacherCodeOfSecondClass; } }

        public int GroupNumberForThirdClass
        { get { return m_GroupNumberForThirdClass; } }

        public int TypeOfThirdClass
        { get { return m_TypeOfThirdClass; } }

        public int StartTimeSlotOfThirdClass
        { get { return m_StartTimeSlotOfThirdClass; } }

        public int EndTimeSlotOfThirdClass
        { get { return m_EndTimeSlotOfThirdClass; } }

        public int TeacherCodeOfThirdClass
        { get { return m_TeacherCodeOfThirdClass; } }


        public string CourseTitle { get; set; }
        public int CourseCredit { get; set; }

        public DateTime? StartTimeSlotOfFirstClassEx { get; set; }
        public DateTime? StartTimeSlotOfSecondClassEx { get; set; }
        public DateTime? StartTimeSlotOfThirdClassEx { get; set; }

        public DateTime? EndTimeSlotOfFirstClassEx { get; set; }
        public DateTime? EndTimeSlotOfSecondClassEx { get; set; }
        public DateTime? EndTimeSlotOfThirdClassEx { get; set; }

        public string TeacherNameOfFirstClass { get; set; }
        public string TeacherNameOfSecondClass { get; set; }
        public string TeacherNameOfThirdClass { get; set; }

        public bool HasFirstClass { get { return this.StartTimeSlotOfFirstClassEx.HasValue; } }
        public bool HasSecondClass { get { return this.StartTimeSlotOfSecondClassEx.HasValue; } }
        public bool HasThirdClass { get { return this.StartTimeSlotOfThirdClassEx.HasValue; } }

        public DayOfWeek DayOfFirstClass { get { return this.GetDayFromTimeFrame(this.StartTimeSlotOfFirstClass); } }
        public DayOfWeek DayOfSecondClass { get { return this.GetDayFromTimeFrame(this.StartTimeSlotOfSecondClass); } }
        public DayOfWeek DayOfThirdClass { get { return this.GetDayFromTimeFrame(this.StartTimeSlotOfThirdClass); } }

        private DayOfWeek GetDayFromTimeFrame(int i_TimeFrame)
        {
            if (i_TimeFrame >= 1 && i_TimeFrame <= 52)
            {
                return DayOfWeek.Sunday;
            }
            else if (i_TimeFrame >= 53 && i_TimeFrame <= 104)
            {
                return DayOfWeek.Monday;
            }
            else if (i_TimeFrame >= 105 && i_TimeFrame <= 156)
            {
                return DayOfWeek.Tuesday;
            }
            else if (i_TimeFrame >= 157 && i_TimeFrame <= 208)
            {
                return DayOfWeek.Wednesday;
            }
            else if (i_TimeFrame >= 209 && i_TimeFrame <= 261)
            {
                return DayOfWeek.Thursday;
            }
            else if (i_TimeFrame >= 262 && i_TimeFrame <= 285)
            {
                return DayOfWeek.Friday;
            }
            else
            {
                throw new Exception("Could not determine day of week for this course!");
            }
        }
    }
}
