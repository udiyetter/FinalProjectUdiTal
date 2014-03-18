using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace FinalProjectUdiTal
{
    class Student
    {

        const int k_MaximumNumberOfCourses = 7;
        private const string k_CS = @"server=sql2.freemysqlhosting.net;userid=sql217024;
            password=bL9*yK9*;database=sql217024";

        private static Random rnd = new Random();
        int m_StudentId;
        string m_StudentFirstName;
        string m_StudentLastName;
        float m_StudentAverageGrades;
        int m_NumberOfPointsTheStudentGained;
        int m_NumberOfSemesterThatTheStudentIsTaking;
        int m_numberOfRequiredCourseTheStudentWantToTake;
        int m_numberOfElectiveCourseTheStudentWantToTake;
        int[] m_StudentPreferences = new int[4];
        List<Course> m_ListOfCoursesTheStudentPass;
        List<Course> m_ListOfCoursesTheStudentFailed;
        List<Course> m_ListOfCoursesTheStudentRegisteredToThisSemester; //course he registered this semester
        List<Course> m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester;//A+B courses
        List<int> m_ListOfCourseTypeCodesWitchTheStudentPreferred;
        List<Course> m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference; //C courses
        List<ePreferences> m_StudentPreference;
        List<ePreferences> m_StudentStartAtHourPreference;//done
        List<ePreferences> m_StudentWindowsPreference; //done
        List<ePreferences> m_StudentStartingAfterPrefernce; //done
        List<ePreferences> m_StudentStudyDayPreference;//done


        public enum ePreferences
        {
            startingAt8 = 1, startingAt9, startingAt10, startingAt11, startingAt12, startingAt13, startingAt14, startingAt15, startingAt16, noWindow, oneHourWindow, twoHourWindow, threeHourWindow, fourHourWindow, fiveHourWindow, sixHourWindow, startingAfter9 = 21, startingAfter10, startingAfter11, startingAfter12, startingAfter13, startingAfter14, startingAfter15, startingAfter16, startingAfter17, notStudying = 99, noPreference
        }

        public Student(string i_StudentFirstName, string i_StudentLastName, int i_StudentId)
        {
            m_StudentFirstName = i_StudentFirstName;
            m_StudentLastName = i_StudentLastName;
            m_StudentId = i_StudentId;
            m_ListOfCoursesTheStudentFailed = new List<Course>();
            m_ListOfCoursesTheStudentPass = new List<Course>();
            m_ListOfCoursesTheStudentRegisteredToThisSemester = new List<Course>();
            m_ListOfCourseTypeCodesWitchTheStudentPreferred = new List<int>();
            m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester = new List<Course>();
            m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference = new List<Course>();
            m_StudentPreference = new List<ePreferences>();
            m_StudentStartAtHourPreference = new List<ePreferences>();
            m_StudentWindowsPreference = new List<ePreferences>();
            m_StudentStartingAfterPrefernce = new List<ePreferences>();
            m_StudentStudyDayPreference = new List<ePreferences>();
            updateStudentCoursLists(i_StudentId);
            addStudentPreference();
            updateCourseListThatTheStudentHaveToTake(this.Semester);
            getListOfElectiveCourses();
            clearUnPrereqCoursesFtomList(m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester);
            clearUnPrereqCoursesFtomList(m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference);
            clearDuplicateCoursesFromLists();
            //updateCourseScore();
            orderRequiredCourseListByScore();
            clearUnStudiedCoursesFromList(m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester);
            clearUnStudiedCoursesFromList(m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference);
        }

        public Student(int i_StudentId)
        {
            m_ListOfCoursesTheStudentFailed = new List<Course>();
            m_ListOfCoursesTheStudentPass = new List<Course>();
            m_ListOfCoursesTheStudentRegisteredToThisSemester = new List<Course>();
            m_ListOfCourseTypeCodesWitchTheStudentPreferred = new List<int>();
            m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester = new List<Course>();
            m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference = new List<Course>();
            m_StudentPreference = new List<ePreferences>();
            m_StudentStartAtHourPreference = new List<ePreferences>();//done
            m_StudentWindowsPreference = new List<ePreferences>(); //done
            m_StudentStartingAfterPrefernce = new List<ePreferences>(); //done
            m_StudentStudyDayPreference = new List<ePreferences>();//done
            updateStudenPropertiesFromDataBase(i_StudentId);
            updateStudentCoursLists(i_StudentId);
            addStudentPreference();
            updateCourseListThatTheStudentHaveToTake(this.Semester);
            getListOfElectiveCourses();
            clearUnPrereqCoursesFtomList(m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester);
            clearUnPrereqCoursesFtomList(m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference);
            clearDuplicateCoursesFromLists();
            //updateCourseScore();
            orderRequiredCourseListByScore();
            clearUnStudiedCoursesFromList(m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester);
            clearUnStudiedCoursesFromList(m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference);
        }

        public int ID
        {
            get { return m_StudentId; }
        }

        public string FisrtName
        {
            get { return m_StudentFirstName; }
        }

        public int Semester
        {
            get
            { return m_NumberOfSemesterThatTheStudentIsTaking; }
        }

        public string LastName
        {
            get { return m_StudentLastName; }
        }

        public float AveregeGrade
        {
            get { return m_StudentAverageGrades; }
        }

        public int NumberOfPointsGained
        {
            get { return m_NumberOfPointsTheStudentGained; }
        }

        public List<Course> ListOfFailedCourses
        {
            get { return m_ListOfCoursesTheStudentFailed; }
        }

        public List<Course> ListOfPassedCourses
        {
            get { return m_ListOfCoursesTheStudentPass; }
        }

        public List<ePreferences> Preference
        {
            get { return m_StudentPreference; }
        }

        public List<ePreferences> StartHourPreferenceList
        {
            get { return m_StudentStartAtHourPreference; }
        }
        public List<ePreferences> WindowsPreferenceList
        {
            get { return m_StudentWindowsPreference; }
        }
        public List<ePreferences> StartingAfterHourPreferenceList
        {
            get { return m_StudentStartingAfterPrefernce; }
        }
        public List<ePreferences> StudyDaysPreferenceList
        {
            get { return m_StudentStudyDayPreference; }
        }

        public List<Course> ListOfRegisteredCourses
        {
            get { return m_ListOfCoursesTheStudentRegisteredToThisSemester; }
        }

        public List<Course> ListOfCoursesThatTheStudentNeedsToTakeThisSemester
        {
            get { return m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester; }
        }

        public List<int> ListOfPreferredCoursesCode
        {
            get { return m_ListOfCourseTypeCodesWitchTheStudentPreferred; }
        }

        public List<Course> ElectiveCourses
        {
            get { return m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference; }
        }

        public float StudentAverageGrades
        { get {return m_StudentAverageGrades; } }

        public int NumberOfPointsTheStudentGained
        { get { return m_NumberOfPointsTheStudentGained; } }

        public int NumberOfSemesterThatTheStudentIsTaking
        { get { return m_NumberOfSemesterThatTheStudentIsTaking; } }

        public int numberOfRequiredCourseTheStudentWantToTake
        { get { return m_numberOfRequiredCourseTheStudentWantToTake; }
        set { m_numberOfRequiredCourseTheStudentWantToTake=value; }
        }

        public int numberOfElectiveCourseTheStudentWantToTake
        { get { return m_numberOfElectiveCourseTheStudentWantToTake; }
            set { m_numberOfElectiveCourseTheStudentWantToTake = value; }
        }

        public void updateStudenPropertiesFromDataBase(int i_StudentID)
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;
            int i;
            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();

                string quoteByString = "SELECT * FROM sql217024.students where idStudents = '" + i_StudentID + "'";

                MySqlCommand cmd = new MySqlCommand(quoteByString, conn);
                rdr = cmd.ExecuteReader();

                rdr.Read();
                m_StudentId = rdr.GetInt32(0);
                m_StudentFirstName = rdr.GetString(1);
                m_StudentLastName = rdr.GetString(2);
                m_NumberOfSemesterThatTheStudentIsTaking = rdr.GetInt32(3);
                for (i = 4; i < 8; i++)
                {
                    m_ListOfCourseTypeCodesWitchTheStudentPreferred.Add(rdr.GetInt32(i));
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
            //updateStudentCoursLists(m_StudentId);
            //updateCourseListThatTheStudentHaveToTake(m_NumberOfSemesterThatTheStudentIsTaking);
            //getListOfElectiveCourses();

        }

        public void updateStudentCoursLists(int i_StudentId)
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;
            Course CourseToAdd;
            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();

                string quoteByString = "SELECT * FROM sql217024.studentsgrades where StudentID = '" + i_StudentId + "'";

                MySqlCommand cmd = new MySqlCommand(quoteByString, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    CourseToAdd = new Course(rdr.GetInt32(2), rdr.GetInt32(3));
                    if (rdr.GetInt32(3) >= 60)
                    {
                        m_ListOfCoursesTheStudentPass.Add(CourseToAdd);
                    }
                    else
                    {
                        m_ListOfCoursesTheStudentFailed.Add(CourseToAdd);
                    }
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

        public void updateCourseListThatTheStudentHaveToTake(int i_StudentSemeste)
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;
            Course CourseToAdd;
            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();

                string quoteByString = "SELECT * FROM sql217024.coursesplan where Semester = '" + i_StudentSemeste + "'";

                MySqlCommand cmd = new MySqlCommand(quoteByString, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    CourseToAdd = new Course(rdr.GetInt32(0), -1);
                    m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester.Add(CourseToAdd);
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
            if (m_ListOfCoursesTheStudentFailed.Count != 0)
            {
                foreach (Course failedCourse in m_ListOfCoursesTheStudentFailed)
                {
                    m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester.Add(failedCourse);
                }
            }
            updateCourseScore();
        }

        public void getListOfElectiveCourses()
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;
            Course CourseToAdd;
            int numberOfPreference = 0;
            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();
                while (m_ListOfCourseTypeCodesWitchTheStudentPreferred.Count > numberOfPreference)
                {
                    string quoteByString = "SELECT * FROM sql217024.coursesclass where coursesClassType = '" + m_ListOfCourseTypeCodesWitchTheStudentPreferred[numberOfPreference] + "'";
                    MySqlCommand cmd = new MySqlCommand(quoteByString, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        CourseToAdd = new Course(rdr.GetInt32(0), -1);
                        m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference.Add(CourseToAdd);
                    }
                    numberOfPreference++;
                    if (rdr != null)
                    {
                        rdr.Close();
                    }
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

        public void clearUnPrereqCoursesFtomList(List<Course> i_listOfCoursesToCheck)
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;
            //Course CourseToAdd;
            int numberOfPrereqCourses = 0;
            bool courseIsPrereq;
            List<Course> ListOfCoursesToDelete = new List<Course>();
            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();
                //to add a list of courses that every time that we need to remove course we add the course into it and after the algo is finished we remove them all
                foreach (Course checkedCourse in i_listOfCoursesToCheck)
                {
                    string quoteByString = "SELECT courseid, pre1, pre2, pre3, pre4, pre5, pre6, pre7, pre8, pre9  FROM sql217024.coursesprereq where courseid = '" + checkedCourse.ID + "'";
                    MySqlCommand cmd = new MySqlCommand(quoteByString, conn);
                    rdr = cmd.ExecuteReader();
                    rdr.Read();
                    numberOfPrereqCourses = 0;
                    if (rdr.HasRows)
                    {
                        while (numberOfPrereqCourses < rdr.FieldCount)
                        {
                            courseIsPrereq = false;
                            if (!rdr.IsDBNull(numberOfPrereqCourses + 1))
                            {
                                foreach (Course cr in ListOfPassedCourses)
                                {
                                    if (cr.ID == rdr.GetInt32(numberOfPrereqCourses + 1))
                                    {
                                        courseIsPrereq = true;
                                    }
                                    if (courseIsPrereq == true)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                            if (courseIsPrereq == false)
                            {
                                ListOfCoursesToDelete.Add(checkedCourse);
                                //m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester.Remove(checkedCourse);
                                break;
                            }
                            numberOfPrereqCourses++;
                        }
                    }                    
                    if (rdr != null)
                    {
                        rdr.Close();
                    }
                }
                foreach (Course courseToDelete in ListOfCoursesToDelete)
                {
                    i_listOfCoursesToCheck.Remove(courseToDelete);
                }
                //ListOfCoursesToDelete.Clear();
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

        public void clearDuplicateCoursesFromLists()
        {
            List<Course> listOfCoursesToDelete = new List<Course>();
            bool duplicate = false;
            foreach (Course checkedCourseWantsToTake in m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference)
            {
                foreach (Course checkedCourseTook in m_ListOfCoursesTheStudentPass)
                {
                    if (checkedCourseTook.ID == checkedCourseWantsToTake.ID)
                    {
                        duplicate = true;
                        listOfCoursesToDelete.Add(checkedCourseWantsToTake);
                        break;
                    }
                }
                if (duplicate == true)
                {
                    break;
                }
            }
            if (listOfCoursesToDelete.Count > 0)
            {
                foreach (Course deletedCourse in listOfCoursesToDelete)
                {
                    m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference.Remove(deletedCourse);
                }
            }
        }

        public void updateCourseScore()
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;
            int currentPossition = 0;
            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();

                for (; ListOfCoursesThatTheStudentNeedsToTakeThisSemester.Count > currentPossition; currentPossition++ )
                {
                    string quoteByString = "SELECT * FROM sql217024.coursescore where courseid =  '" + ListOfCoursesThatTheStudentNeedsToTakeThisSemester[currentPossition].ID + "'";

                    MySqlCommand cmd = new MySqlCommand(quoteByString, conn);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ListOfCoursesThatTheStudentNeedsToTakeThisSemester[currentPossition].Score = rdr.GetInt32(1);

                    }
                    conn.Close();
                    conn.Open();
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
        
            //if (m_ListOfCoursesTheStudentFailed.Count != 0)
            //{
            //    foreach (Course failedCourse in m_ListOfCoursesTheStudentFailed)
            //    {
            //        m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester.Add(failedCourse);
            //    }
            //}
        }

        public Course[] modifyArrayOfCourses(int i_NumberOfRequiredCourses)
        {
            int min1, min2, count;
            Course[] returnArr;
            min1 = Math.Min(i_NumberOfRequiredCourses, m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester.Count);
            min2 = m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference.Count;
            count = 0;
            returnArr = new Course[min1 + min2];
            orderRequiredCourseListByScore();
            while (count < min1)
            {
                returnArr[count] = m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester[count];
                count ++;
            }

            while ((count - min1) < min2)
            {
                returnArr[count] = getRandomCourse(m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference, returnArr, min1);
                count++;
            }
            return returnArr;
        }

        private Course getRandomCourse(List<Course> i_ListOfCourses, Course[] i_courseArray, int i_startPosition)
        {
            int index = 0;
            bool isInTheArray = true;
            while (isInTheArray)
            {
                index = rnd.Next(i_ListOfCourses.Count);
                isInTheArray = isCourseInTheArray(i_ListOfCourses[index], i_courseArray, i_startPosition);
            }
            return i_ListOfCourses[index]; 
        }

        private bool isCourseInTheArray(Course i_course, Course[] i_courseArray, int i_startPosition)
        {
            bool returnAnswer = false;
            while (i_startPosition < i_courseArray.Length && returnAnswer == false)
            {
                if (i_courseArray[i_startPosition] != null)
                {
                    if (i_course.ID == i_courseArray[i_startPosition].ID)
                    {
                        returnAnswer = true;
                    }
                    i_startPosition++;
                }
                else
                {
                    break;
                }
            }
            return returnAnswer;
        }

        private void orderRequiredCourseListByScore()
        {
            int listSize = m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester.Count;
            for (int i = 0; i < listSize; i++)
            {
                m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester.Add(getMaxItem(listSize - i, m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester));
            }  
        }

        private Course getMaxItem(int i_SizeOfList, List<Course> i_ListOfCourses)
        {
            Course maxItem;
            int i, maxiItemIndex;
            if (i_SizeOfList != 1)
            {
                maxItem = i_ListOfCourses[0];
                maxiItemIndex = 0;
                for (i = 1; i < i_SizeOfList; i++)
                {
                    if (i_ListOfCourses[i].Score > maxItem.Score)
                    {
                        maxItem = i_ListOfCourses[i];
                        maxiItemIndex = i;
                    }
                }
                i_ListOfCourses.RemoveAt(maxiItemIndex);
                return maxItem;

            }
            maxItem = i_ListOfCourses[0];
            i_ListOfCourses.RemoveAt(0);
            return maxItem;
        }

       
        //randomly selection - corect it
        //private void insertEleventCourseToArray(Course[] courseArray, int i_NumberOfElevtiveCourses)
        //{
        //    int currentPossition = 0;
        //    if (i_NumberOfElevtiveCourses < m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference.Count)
        //    {
        //        for (; currentPossition < i_NumberOfElevtiveCourses; currentPossition++)
        //        {
        //            //ToDo
        //            //to add a function that checks if the course is in the array and, the courses will add rundomly
        //            courseArray[currentPossition] = m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference[currentPossition];
                    
        //        }
                
        //    }
        //    else if (i_NumberOfElevtiveCourses >= m_ListOfElectiveCoursesThatTheStudentCanTakeByPreference.Count)
        //    {
        //        for (; currentPossition < m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester.Count; currentPossition++)
        //        {
        //            courseArray[currentPossition] = m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester[currentPossition];
        //        }
        //    }
            
        //}

        private void insertRequiredCourseToArray(Course[] courseArray, int i_NumberOfRequiredCourses)
        {
            //int coursesTahtAddedToArray = 0;
            if (i_NumberOfRequiredCourses < m_ListOfCoursesTheStudentNeedsToBeRegisteredToThisSemester.Count)
            {
            }

        }

        internal int[] modifyNumbersOfCoursesToArray(Course[] m_ArrayOfCourses)
        {
            int[] returnArr = new int[m_ArrayOfCourses.Length];
            int count = 0;
            while (count < m_ArrayOfCourses.Length)
            {
                returnArr[count] = m_ArrayOfCourses[count].ID;
                count++;
            }
            return returnArr;
        }

        private void addStudentPreference()
        {
            getWindowsPreferenceFromDb();
            getAtStartHourPreferenceFromDb();
            getStartAfterHourPrefernceFromDb();
            initializeStudyDaysPreference();
        }

        //Must be the last from the 4th preferences
        private void initializeStudyDaysPreference()
        {
            int i = 0;
            for (; i < 6; i++)
            {
                if (m_StudentStartAtHourPreference[i] == ePreferences.notStudying || m_StudentStartingAfterPrefernce[i] == ePreferences.notStudying || m_StudentWindowsPreference[i] == ePreferences.notStudying)
                {
                    m_StudentStudyDayPreference.Add(ePreferences.notStudying);
                }
                else
                {
                    m_StudentStudyDayPreference.Add(ePreferences.noPreference);
                }
                
            }
        }

        private void getStartAfterHourPrefernceFromDb()
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();
                string quoteByString = "SELECT * FROM sql217024.studentsdontstartbefore where id = " + m_StudentId;
                MySqlCommand cmd = new MySqlCommand(quoteByString, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    m_StudentStartingAfterPrefernce.Add((ePreferences)rdr.GetInt32("Sunday"));
                    m_StudentStartingAfterPrefernce.Add((ePreferences)rdr.GetInt32("Monday"));
                    m_StudentStartingAfterPrefernce.Add((ePreferences)rdr.GetInt32("Tuesday"));
                    m_StudentStartingAfterPrefernce.Add((ePreferences)rdr.GetInt32("Wednesday"));
                    m_StudentStartingAfterPrefernce.Add((ePreferences)rdr.GetInt32("Thursday"));
                    m_StudentStartingAfterPrefernce.Add((ePreferences)rdr.GetInt32("Friday"));
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

        private void getAtStartHourPreferenceFromDb()
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();
                string quoteByString = "SELECT * FROM sql217024.studentsbeginpref where id = " + m_StudentId;
                MySqlCommand cmd = new MySqlCommand(quoteByString, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    m_StudentStartAtHourPreference.Add((ePreferences)rdr.GetInt32("Sunday"));
                    m_StudentStartAtHourPreference.Add((ePreferences)rdr.GetInt32("Monday"));
                    m_StudentStartAtHourPreference.Add((ePreferences)rdr.GetInt32("Tuesday"));
                    m_StudentStartAtHourPreference.Add((ePreferences)rdr.GetInt32("Wednesday"));
                    m_StudentStartAtHourPreference.Add((ePreferences)rdr.GetInt32("Thursday"));
                    m_StudentStartAtHourPreference.Add((ePreferences)rdr.GetInt32("Friday"));
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

        private void getWindowsPreferenceFromDb()
        {
            
             MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();
                string quoteByString = "SELECT * FROM sql217024.studentswindowspref where id  = " + m_StudentId;
                MySqlCommand cmd = new MySqlCommand(quoteByString, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    m_StudentWindowsPreference.Add((ePreferences)rdr.GetInt32("Sunday"));
                    m_StudentWindowsPreference.Add((ePreferences)rdr.GetInt32("Monday"));
                    m_StudentWindowsPreference.Add((ePreferences)rdr.GetInt32("Tuesday"));
                    m_StudentWindowsPreference.Add((ePreferences)rdr.GetInt32("Wednesday"));
                    m_StudentWindowsPreference.Add((ePreferences)rdr.GetInt32("Thursday"));
                    m_StudentWindowsPreference.Add((ePreferences)rdr.GetInt32("Friday"));
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

        public void clearUnStudiedCoursesFromList(List<Course> i_ListOfCoursesToCheck)
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;
            List<Course> ListOfCoursesToDelete = new List<Course>();
            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();
                //to add a list of courses that every time that we need to remove course we add the course into it and after the algo is finished we remove them all
                foreach (Course checkedCourse in i_ListOfCoursesToCheck)
                {
                    string quoteByString = "SELECT count(*) FROM sql217024.coursesblocks where idcourses = '" + checkedCourse.ID + "'";
                    MySqlCommand cmd = new MySqlCommand(quoteByString, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (rdr.GetInt32(0) == 0)
                        {
                            ListOfCoursesToDelete.Add(checkedCourse);
                        }
                    }                   
                    if (rdr != null)
                    {
                        rdr.Close();
                    }
                }
                foreach (Course courseToDelete in ListOfCoursesToDelete)
                {
                    i_ListOfCoursesToCheck.Remove(courseToDelete);
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
            
    }
}
