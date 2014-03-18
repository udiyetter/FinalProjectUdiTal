using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectUdiTal
{
    class Course
    {
        int m_CourseId;
        int m_CourseSubGroup;
        int m_CourseType;
        int m_CourseStratTimeSlot;
        int m_CourseEndTimeSlot;
        int m_CourseTeacherCode;
        int m_CourseGrade;
        int m_CourseScore;

        public Course(int i_CourseId, int i_CourseSubGroup, int i_CourseType, int i_CourseStratTimeSlot, int i_CourseEndTimeSlot,
            int i_CourseTeacherCode, int i_CourseGrade, int i_CourseScore)
        {
            m_CourseId = i_CourseId;
            m_CourseSubGroup = i_CourseSubGroup;
            m_CourseType = i_CourseType;
            m_CourseStratTimeSlot = i_CourseStratTimeSlot;
            m_CourseEndTimeSlot = i_CourseEndTimeSlot;
            m_CourseTeacherCode = i_CourseTeacherCode;
            m_CourseGrade = i_CourseGrade;
            m_CourseScore = i_CourseScore;
        }

        public Course(int i_CourseId, int i_CourseGrade)
        {
            m_CourseId = i_CourseId;
            m_CourseGrade = i_CourseGrade;
        }

        public int Score
        {
            get { return m_CourseScore; }
            set { m_CourseScore = value; }
        }
        public int ID
        {
            get
            {
                return m_CourseId;
            }
            set
            {
                m_CourseId = value;
            }
        }
        public int SubGroup
        {
            get
            {
                return m_CourseSubGroup;
            }
            set
            {
                m_CourseSubGroup = value;
            }
        }
        public int Type
        {
            get
            {
                return m_CourseType;
            }
            set
            {
                m_CourseType = value;
            }
        }
        public int StartTimeSlot
        {
            get
            {
                return m_CourseStratTimeSlot;
            }
            set
            {
                m_CourseStratTimeSlot = value;
            }
        }
        public int EndTimeSlot
        {
            get
            {
                return m_CourseEndTimeSlot;
            }
            set
            {
                m_CourseEndTimeSlot = value;
            }
        }
        public int TeacherCode
        {
            get
            {
                return m_CourseTeacherCode;
            }
            set
            {
                m_CourseTeacherCode = value;
            }
        }
        public int Grade
        {
            get
            {
                return m_CourseGrade;
            }
            set
            {
                m_CourseGrade = value;
            }
        }

    }
}
