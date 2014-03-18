using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectUdiTal
{
    class StudentPreference
    {
        private static Dictionary<Student.ePreferences, string> _mapping = null;

         static StudentPreference()
        {
            // Initialization of static members
            _mapping = new Dictionary<Student.ePreferences, string>()
            {
                // Windows
                {Student.ePreferences.noWindow, "ללא חלונות"},
                {Student.ePreferences.oneHourWindow, "שעה"},
                {Student.ePreferences.twoHourWindow, "שעתיים"},
                {Student.ePreferences.threeHourWindow, "3 שעות"},
                {Student.ePreferences.fourHourWindow, "4 שעות"},
                {Student.ePreferences.fiveHourWindow, "5 שעות"},
                {Student.ePreferences.sixHourWindow, "6 שעות"},

                // Starting at
                {Student.ePreferences.startingAt8, "8:00"},
                {Student.ePreferences.startingAt9, "9:00"},
                {Student.ePreferences.startingAt10, "10:00"},
                {Student.ePreferences.startingAt11, "11:00"},
                {Student.ePreferences.startingAt12, "12:00"},
                {Student.ePreferences.startingAt13, "13:00"},
                {Student.ePreferences.startingAt14, "14:00"},
                {Student.ePreferences.startingAt15, "15:00"},
                {Student.ePreferences.startingAt16, "16:00"},

                // Starting after
                {Student.ePreferences.startingAfter9, "9:00"},
                {Student.ePreferences.startingAfter10, "10:00"},
                {Student.ePreferences.startingAfter11, "11:00"},
                {Student.ePreferences.startingAfter12, "12:00"},
                {Student.ePreferences.startingAfter13, "13:00"},
                {Student.ePreferences.startingAfter14, "14:00"},
                {Student.ePreferences.startingAfter15, "15:00"},
                {Student.ePreferences.startingAfter16, "16:00"},
                {Student.ePreferences.startingAfter17, "17:00"},

                {Student.ePreferences.notStudying, "לא ללמוד"},
                {Student.ePreferences.noPreference, "אין העדפה"}

            };
        }

        public StudentPreference(Student i_Student)
        {
            List<Student.ePreferences> m_ListOfPreferences = new List<Student.ePreferences>();
            //int count = 0;
                
           

        }

        /// <summary>
        /// Gets a mapping of Student Preferences and a representative string.
        /// </summary>
        internal static Dictionary<Student.ePreferences, string> PreferencesMapping
        {
            get
            {
                return _mapping;
            }
        }
    }
}
