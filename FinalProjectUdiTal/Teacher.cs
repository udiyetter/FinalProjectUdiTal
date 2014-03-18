using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectUdiTal
{
    class Teacher
    {
        string m_TeacerFirstName;
        string m_TeacerLastName;
        int m_TeacherCode;
        List<Course> m_ListOfCoursesThatTheTeacherIsTeach;

        public Teacher(string i_TeacherFirstName, string i_TeacherLasttName, int i_TeacherCode)
        {
            m_TeacerFirstName = i_TeacherFirstName;
            m_TeacerLastName = i_TeacherLasttName;
            m_TeacherCode = i_TeacherCode;
            m_ListOfCoursesThatTheTeacherIsTeach = new List<Course>();
        }

        public string FirstName
        {
            get
            {
                return m_TeacerFirstName;
            }

        }

        public string LastName
        {
            get
            {
                return m_TeacerLastName;
            }

        }

        public int Code
        {
            get
            {
                return m_TeacherCode;
            }

        }

        public List<Course> CourseList
        {
            get
            {
                return m_ListOfCoursesThatTheTeacherIsTeach;
            }
        }

        void InsertCourseToListOfTeacherCourses(Course i_courseToAdd)
        {
            m_ListOfCoursesThatTheTeacherIsTeach.Add(i_courseToAdd);
        }

        void DeleteCourseToListOfTeacherCourses(Course i_courseToDelete)
        {
            m_ListOfCoursesThatTheTeacherIsTeach.Remove(i_courseToDelete);
        }
    }
}
