using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace FinalProjectUdiTal
{
    class VectorOfCourses
    {
        private static Random rnd = new Random();
        private const string k_CS = @"server=sql2.freemysqlhosting.net;userid=sql217024;
            password=bL9*yK9*;database=sql217024";

        const int k_MaximumNumberOfCourses = 7;
        List<CourseBlock> m_Vector; //vector of courses when each vector is choosen rendomly
        double m_VectorRating;

        public VectorOfCourses()
        {
            m_Vector = new List<CourseBlock>();
            m_VectorRating = -1;
        }

        public VectorOfCourses(int[] i_arrayOfNumberOfCourses, List<List<CourseBlock>> i_BlockList, Student i_Student)
        {
            //MySqlConnection conn = null;
            //MySqlDataReader rdr = null;
            //List<int> testlist = new List<int>();
            int[] rand;
            m_Vector = new List<CourseBlock>();
            int count = 0;
            for (; count < i_Student.numberOfRequiredCourseTheStudentWantToTake; count++)
            {
                //testlist.Add(rnd.Next(i_BlockList[count].Count)); ;
                CourseBlock newBlock = new CourseBlock(i_BlockList[count][rnd.Next(i_BlockList[count].Count)]);
                m_Vector.Add(newBlock);
            }
            selectRandomCoursesPositions(i_Student.numberOfElectiveCourseTheStudentWantToTake, out rand);
            for (count = 0; count < i_Student.numberOfElectiveCourseTheStudentWantToTake; count++)
            {
                CourseBlock newBlock = new CourseBlock(i_BlockList[i_Student.numberOfRequiredCourseTheStudentWantToTake + rand[count]][rnd.Next(i_BlockList[i_Student.numberOfRequiredCourseTheStudentWantToTake + rand[count]].Count)]);
                m_Vector.Add(newBlock);
            }
            rateVector(i_Student);
        }

        public VectorOfCourses(VectorOfCourses i_Vector)
        {
            int count = 0;
            m_Vector = new List<CourseBlock>();
            for (; count < k_MaximumNumberOfCourses; count++)
            {
                m_Vector.Add(new CourseBlock(i_Vector.m_Vector[count]));
            }
            m_VectorRating = i_Vector.m_VectorRating;
        }

        public bool addCourse(CourseBlock i_course)
        {
            if (m_Vector.Count >= k_MaximumNumberOfCourses)
            { return false; }
            else
            { 
            m_Vector.Add(i_course);
            return true;
            }
        }

        public void removeCourse(CourseBlock i_Course)
        {
            m_Vector.Remove(i_Course);
        }
        
        public void corectVector()
        {
        }

        public void rateVector(Student i_Student)
        {
            m_VectorRating = 0;
            int count = 0;
            for (; count < i_Student.StartingAfterHourPreferenceList.Count; count++)
            {
                if (i_Student.StartingAfterHourPreferenceList[count] != Student.ePreferences.noPreference)
                {
                    checkStartingAfterTime(count, i_Student.StartingAfterHourPreferenceList[count]);
                }
                if (i_Student.StartHourPreferenceList[count] != Student.ePreferences.noPreference)
                {
                    checkStartingAtSpecificHour(count, i_Student.StartHourPreferenceList[count]);
                }
                if (i_Student.WindowsPreferenceList[count] != Student.ePreferences.noPreference)
                {
                }
                if (i_Student.StudyDaysPreferenceList[count] != Student.ePreferences.noPreference)
                {
                    checkIfStudyInDay(count);
                }
            }
            checkCoursesOverlap();
            checkForEmptyCourses();
        }

        private void checkStartingAtSpecificHour(int i_Day, Student.ePreferences i_Preferences)
        {
            int i=0;
            bool study = true;
            if (isStudyInADay(i_Day))
            {
                study = false;
                for (; i < k_MaximumNumberOfCourses; i++)
                {
                    if (isStarting(i_Day, i_Preferences))
                    {
                        study = true;
                    }
                }
            }
            if (!study)
            {
                m_VectorRating += 15;
            }
        }

        private bool isStarting(int i_Day, Student.ePreferences i_Preferences)
        {
            int startingAt = staringTimeToInt(i_Preferences);
            int i = 0;
            bool result = false;
            for (; i < k_MaximumNumberOfCourses; i++)
            {
                if (m_Vector[i].StartTimeSlotOfFirstClass != -1)
                {
                    switch (i_Day)
                    {
                        case 0:
                            if (m_Vector[i].StartTimeSlotOfFirstClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfFirstClass > startingAt * 4) && (m_Vector[i].StartTimeSlotOfFirstClass <= (startingAt + 1) * 4))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfSecondClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfSecondClass > startingAt * 4) && (m_Vector[i].StartTimeSlotOfSecondClass <= (startingAt + 1) * 4))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfThirdClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfThirdClass > startingAt * 4) && (m_Vector[i].StartTimeSlotOfThirdClass <= (startingAt + 1) * 4))
                                {
                                    result = true;
                                }
                            }
                            break;
                        case 1:
                             if (m_Vector[i].StartTimeSlotOfFirstClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfFirstClass > ((startingAt * 4)+52)) && (m_Vector[i].StartTimeSlotOfFirstClass <= (((startingAt+1) * 4)+52)))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfSecondClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfSecondClass > ((startingAt * 4) + 52)) && (m_Vector[i].StartTimeSlotOfSecondClass <= (((startingAt + 1) * 4) + 52)))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfThirdClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfThirdClass > ((startingAt * 4) + 52)) && (m_Vector[i].StartTimeSlotOfThirdClass <= (((startingAt + 1) * 4) + 52)))
                                {
                                    result = true;
                                }
                            }
                            break;
                        case 2:
                            if (m_Vector[i].StartTimeSlotOfFirstClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfFirstClass > ((startingAt * 4) + 104)) && (m_Vector[i].StartTimeSlotOfFirstClass <= (((startingAt + 1) * 4) + 104)))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfSecondClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfSecondClass > ((startingAt * 4) + 104)) && (m_Vector[i].StartTimeSlotOfSecondClass <= (((startingAt + 1) * 4) + 104)))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfThirdClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfThirdClass > ((startingAt * 4) + 104)) && (m_Vector[i].StartTimeSlotOfThirdClass <= (((startingAt + 1) * 4) + 104)))
                                {
                                    result = true;
                                }
                            }
                            break;
                        case 3:
                            if (m_Vector[i].StartTimeSlotOfFirstClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfFirstClass > ((startingAt * 4) + 156)) && (m_Vector[i].StartTimeSlotOfFirstClass <= (((startingAt + 1) * 4) + 156)))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfSecondClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfSecondClass > ((startingAt * 4) + 156)) && (m_Vector[i].StartTimeSlotOfSecondClass <= (((startingAt + 1) * 4) + 156)))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfThirdClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfThirdClass > ((startingAt * 4) + 156)) && (m_Vector[i].StartTimeSlotOfThirdClass <= (((startingAt + 1) * 4) + 156)))
                                {
                                    result = true;
                                }
                            }
                            break;
                        case 4:
                            if (m_Vector[i].StartTimeSlotOfFirstClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfFirstClass > ((startingAt * 4) + 208)) && (m_Vector[i].StartTimeSlotOfFirstClass <= (((startingAt + 1) * 4) + 208)))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfSecondClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfSecondClass > ((startingAt * 4) + 208)) && (m_Vector[i].StartTimeSlotOfSecondClass <= (((startingAt + 1) * 4) + 208)))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfThirdClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfThirdClass > ((startingAt * 4) + 208)) && (m_Vector[i].StartTimeSlotOfThirdClass <= (((startingAt + 1) * 4) + 208)))
                                {
                                    result = true;
                                }
                            }
                            break;
                        case 5:
                            if (m_Vector[i].StartTimeSlotOfFirstClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfFirstClass > ((startingAt * 4) + 261)) && (m_Vector[i].StartTimeSlotOfFirstClass <= (((startingAt + 1) * 4) + 261)))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfSecondClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfSecondClass > ((startingAt * 4) + 261)) && (m_Vector[i].StartTimeSlotOfSecondClass <= (((startingAt + 1) * 4) + 261)))
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[i].StartTimeSlotOfThirdClass != -1)
                            {
                                if ((m_Vector[i].StartTimeSlotOfThirdClass > ((startingAt * 4) + 261)) && (m_Vector[i].StartTimeSlotOfThirdClass <= (((startingAt + 1) * 4) + 261)))
                                {
                                    result = true;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
           return result;
        }

        private int staringTimeToInt(Student.ePreferences i_Preferences)
        {
            int result = 0;
            switch (i_Preferences)
            {
                case (Student.ePreferences.startingAt8):
                    result = 0;
                    break;
                case(Student.ePreferences.startingAt9):
                    result = 1;
                    break;
                case(Student.ePreferences.startingAt10):
                    result = 2;
                    break;
                case(Student.ePreferences.startingAt11):
                    result = 3;
                    break;
                case(Student.ePreferences.startingAt12):
                    result = 4;
                    break;
                case(Student.ePreferences.startingAt13):
                    result = 5;
                    break;
                case(Student.ePreferences.startingAt14):
                    result = 6;
                    break;
                case(Student.ePreferences.startingAt15):
                    result = 7;
                    break;
                case(Student.ePreferences.startingAt16):
                    result = 8;
                    break;
            }
            return result;

            
        }

        private bool isStudyInADay(int i_Day)
        {
            int count = 0;
            bool result = false;
            for (; count < k_MaximumNumberOfCourses; count++)
            {
                if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                {
                    switch (i_Day)
                    {
                        case 0:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 1 && m_Vector[count].StartTimeSlotOfFirstClass <= 52)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 1 && m_Vector[count].StartTimeSlotOfSecondClass <= 52)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 1 && m_Vector[count].StartTimeSlotOfThirdClass <= 52)
                                {
                                    result = true;
                                }
                            }
                            break;
                        case 1:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 53 && m_Vector[count].StartTimeSlotOfFirstClass <= 104)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 53 && m_Vector[count].StartTimeSlotOfSecondClass <= 104)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 53 && m_Vector[count].StartTimeSlotOfThirdClass <= 104)
                                {
                                    result = true;
                                }
                            }
                            break;
                        case 2:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 105 && m_Vector[count].StartTimeSlotOfFirstClass <= 156)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 105 && m_Vector[count].StartTimeSlotOfSecondClass <= 156)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 105 && m_Vector[count].StartTimeSlotOfThirdClass <= 156)
                                {
                                    result = true;
                                }
                            }
                            break;
                        case 3:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 157 && m_Vector[count].StartTimeSlotOfFirstClass <= 208)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 157 && m_Vector[count].StartTimeSlotOfSecondClass <= 208)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 157 && m_Vector[count].StartTimeSlotOfThirdClass <= 208)
                                {
                                    result = true;
                                }
                            }
                            break;
                        case 4:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 209 && m_Vector[count].StartTimeSlotOfFirstClass <= 261)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 209 && m_Vector[count].StartTimeSlotOfSecondClass <= 261)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 209 && m_Vector[count].StartTimeSlotOfThirdClass <= 261)
                                {
                                    result = true;
                                }
                            }
                            break;
                        case 5:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 262 && m_Vector[count].StartTimeSlotOfFirstClass <= 285)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 262 && m_Vector[count].StartTimeSlotOfSecondClass <= 285)
                                {
                                    result = true;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 262 && m_Vector[count].StartTimeSlotOfThirdClass <= 285)
                                {
                                    result = true;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    
                }
                if (result)
                {
                    break;
                }
            }
            return result;
        }

        private void checkForEmptyCourses()
        {
            int i = 0;
            int count = 0;
            double res;
            for (; i < k_MaximumNumberOfCourses; i++)
            {
                if (m_Vector[i].StartTimeSlotOfFirstClass == -1)
                {
                    count++;
                }
            }
            if (count != 0)
            {
                res = Math.Pow(3.0, (double)count);
                m_VectorRating += res;
            }

        }

        private void checkCoursesOverlap()
        {
            int insideLoop, outsideLoop, overlaps = 0;
            for (insideLoop = 0; insideLoop < k_MaximumNumberOfCourses - 1; insideLoop++)
            {
                for (outsideLoop = insideLoop + 1; outsideLoop < k_MaximumNumberOfCourses; outsideLoop++)
                {
                   overlaps += CountOverlaps(m_Vector[insideLoop], m_Vector[outsideLoop]);
                    
                    //Console.WriteLine("OVERLAPS: " + overlaps);

                    /*if (isOverLap(m_Vector[insideLoop].StartTimeSlotOfFirstClass, m_Vector[insideLoop].EndTimeSlotOfFirstClass, m_Vector[insideLoop].StartTimeSlotOfSecondClass, m_Vector[insideLoop].EndTimeSlotOfSecondClass, m_Vector[insideLoop].StartTimeSlotOfThirdClass, m_Vector[insideLoop].EndTimeSlotOfThirdClass,
                                  m_Vector[outsideLoop].StartTimeSlotOfFirstClass, m_Vector[outsideLoop].EndTimeSlotOfFirstClass, m_Vector[outsideLoop].StartTimeSlotOfSecondClass, m_Vector[outsideLoop].EndTimeSlotOfSecondClass, m_Vector[outsideLoop].StartTimeSlotOfThirdClass, m_Vector[outsideLoop].EndTimeSlotOfThirdClass))
                    {
                        m_VectorRating += 60;
                    }*/
                }
            }

            m_VectorRating += (overlaps * 60);
        }
        
        
        private bool isOverLap(int i_StartFisrt1, int i_EndFirst1, int i_startSecond1, int i_endSecond1, int i_StartThird1, int i_EndThird1,
                               int i_StartFisrt2, int i_EndFirst2, int i_startSecond2, int i_endSecond2, int i_StartThird2, int i_EndThird2)
        {
            bool result = false;
            if (i_StartFisrt1 == -1 || i_StartFisrt2 == -1)
            {
                return false;
            }
            
            if (i_StartFisrt1 != -1 && i_StartFisrt2 != -1)
            {
                if (i_StartFisrt2 >= i_StartFisrt1 && i_StartFisrt2 <= i_EndFirst1)
                {
                    return true;
                }
                if (i_StartFisrt1 >= i_StartFisrt2 && i_StartFisrt1 <= i_StartFisrt2)
                {
                    return true;
                }
                if (i_startSecond1 != -1 && i_startSecond2 != -1)
                {
                    if (i_startSecond1 >= i_StartFisrt2 && i_startSecond1 <= i_EndFirst2)
                    {
                        return true;
                    }
                    if (i_StartFisrt2 >= i_startSecond1 && i_StartFisrt2 <= i_endSecond1)
                    {
                        return true;
                    }
                    if (i_startSecond1 >= i_startSecond2 && i_startSecond1 <= i_endSecond2)
                    {
                        return true;
                    }
                    if (i_startSecond2 >= i_startSecond1 && i_startSecond2 <= i_endSecond1)
                    {
                        return true;
                    }
                    if (i_StartFisrt1 >= i_startSecond2 && i_StartFisrt1 <= i_endSecond2)
                    {
                        return true;
                    }
                    if (i_startSecond2 >= i_StartFisrt1 && i_startSecond2 <= i_EndFirst1)
                    {
                        return true;
                    }
                    if (i_StartThird1 != -1 && i_StartThird2 != -1)
                    {
                        if (i_StartThird1 >= i_StartFisrt2 && i_StartThird1 <= i_EndFirst2)
                        {
                            return true;
                        }
                        if (i_StartFisrt2 >= i_StartThird1 && i_StartFisrt2 <= i_EndThird1)
                        {
                            return true;
                        }
                        if (i_StartThird1 >= i_startSecond2 && i_StartThird1 <= i_endSecond2)
                        {
                            return true;
                        }
                        if (i_startSecond2 >= i_StartThird1 && i_startSecond2 <= i_EndThird1)
                        {
                            return true;
                        }
                        if (i_StartThird1 >= i_StartThird2 && i_StartThird1 <= i_EndThird2)
                        {
                            return true;
                        }
                        if (i_StartThird2 >= i_StartThird1 && i_StartThird2 <= i_EndThird1)
                        {
                            return true;
                        }
                        if (i_startSecond1 >= i_StartThird2 && i_startSecond1 <= i_EndThird2)
                        {
                            return true;
                        }
                        if (i_StartThird2 >= i_startSecond1 && i_StartThird2 <= i_endSecond1)
                        {
                            return true;
                        }
                        if (i_StartFisrt1 >= i_StartThird2 && i_StartFisrt1 <= i_EndThird2)
                        {
                            return true;
                        }
                        if (i_StartThird2 >= i_StartFisrt1 && i_StartThird2 <= i_EndFirst1)
                        {
                            return true;
                        }
                    }
                }
            }
            return result;
        }
        

        private int CountOverlaps(CourseBlock block1, CourseBlock block2)
        {
            int count = 0;

            // First/First
            if (IsOveralap(block1.StartTimeSlotOfFirstClass, block1.EndTimeSlotOfFirstClass, block2.StartTimeSlotOfFirstClass, block2.EndTimeSlotOfFirstClass))
                count++;

            // First/Second
            if (IsOveralap(block1.StartTimeSlotOfFirstClass, block1.EndTimeSlotOfFirstClass, block2.StartTimeSlotOfSecondClass, block2.EndTimeSlotOfSecondClass))
                count++;

            // First/Third
            if (IsOveralap(block1.StartTimeSlotOfFirstClass, block1.EndTimeSlotOfFirstClass, block2.StartTimeSlotOfThirdClass, block2.EndTimeSlotOfThirdClass))
                count++;

            ////

            // Second/First
            //if (IsOveralap(block1.StartTimeSlotOfSecondClass, block1.EndTimeSlotOfSecondClass, block2.StartTimeSlotOfFirstClass, block2.EndTimeSlotOfFirstClass))
            //    count++;

            // Second/Second
            if (IsOveralap(block1.StartTimeSlotOfSecondClass, block1.EndTimeSlotOfSecondClass, block2.StartTimeSlotOfSecondClass, block2.EndTimeSlotOfSecondClass))
                count++;

            // Second/Third
            if (IsOveralap(block1.StartTimeSlotOfSecondClass, block1.EndTimeSlotOfSecondClass, block2.StartTimeSlotOfThirdClass, block2.EndTimeSlotOfThirdClass))
                count++;

            ////

            // Third/First
            //if (IsOveralap(block1.StartTimeSlotOfThirdClass, block1.EndTimeSlotOfThirdClass, block2.StartTimeSlotOfFirstClass, block2.EndTimeSlotOfFirstClass))
            //    count++;

            // Third/Second
            //if (IsOveralap(block1.StartTimeSlotOfThirdClass, block1.EndTimeSlotOfThirdClass, block2.StartTimeSlotOfSecondClass, block2.EndTimeSlotOfSecondClass))
            //    count++;

            // Third/Third
            if (IsOveralap(block1.StartTimeSlotOfThirdClass, block1.EndTimeSlotOfThirdClass, block2.StartTimeSlotOfThirdClass, block2.EndTimeSlotOfThirdClass))
                count++;

            return count;
        }

        private bool IsOveralap(int startTime1, int endTime1, int startTime2, int endTime2)
        {
            // Valid situation:
            //bool isValid = (startTime2 >= endTime1 && endTime2 > endTime1) || // Course 2 after course 1
            //               (startTime1 >= endTime2 && endTime1 > endTime2);   // Course 1 after course 2
            //return !isValid;

            if (startTime1 == -1 || startTime2 == -1)
                return false;

            return
                (startTime1 == startTime2) ||
                (startTime1 > startTime2 && startTime1 < endTime2) ||
                (endTime1 > startTime2 && endTime1 < endTime2) ||
                (startTime2 > startTime1 && startTime2 < endTime1) ||
                (endTime2 > startTime1 && endTime2 < endTime1);
        }

        private void checkStartingAfterTime(int i_Day, Student.ePreferences i_startingTime)
        {
            int count = 0;
            int startCount = 0;
            for (; count < k_MaximumNumberOfCourses; count++)
            {
                if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                {
                    startCount = (i_startingTime == Student.ePreferences.startingAfter9 ? 0 : (i_startingTime == Student.ePreferences.startingAfter10 ? 1 : (i_startingTime == Student.ePreferences.startingAfter11 ? 2 : (i_startingTime == Student.ePreferences.startingAfter12 ? 3 : (i_startingTime == Student.ePreferences.startingAfter13 ? 4 : (i_startingTime == Student.ePreferences.startingAfter14 ? 5 : (i_startingTime == Student.ePreferences.startingAfter15 ? 6 : (i_startingTime == Student.ePreferences.startingAfter16 ? 7 : (i_startingTime == Student.ePreferences.startingAfter17 ? 8 : 0)))))))));
                    switch (i_Day)
                    {
                        case 0:
                            if (m_Vector[count].StartTimeSlotOfFirstClass >= (5 + startCount * 4) && m_Vector[count].StartTimeSlotOfFirstClass <= (9 + startCount * 4))
                            {
                                m_VectorRating += 5;
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= (5 + startCount * 4) && m_Vector[count].StartTimeSlotOfSecondClass <= (9 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= (5 + startCount * 4) && m_Vector[count].StartTimeSlotOfThirdClass <= (9 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            break;
                        case 1:
                            if (m_Vector[count].StartTimeSlotOfFirstClass >= (57 + startCount * 4) && m_Vector[count].StartTimeSlotOfFirstClass <= (60 + startCount * 4))
                            {
                                m_VectorRating += 5;
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= (57 + startCount * 4) && m_Vector[count].StartTimeSlotOfSecondClass <= (60 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= (57 + startCount * 4) && m_Vector[count].StartTimeSlotOfThirdClass <= (60 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            break;
                        case 2:
                            if (m_Vector[count].StartTimeSlotOfFirstClass >= (109 + startCount * 4) && m_Vector[count].StartTimeSlotOfFirstClass <= (112 + startCount * 4))
                            {
                                m_VectorRating += 5;
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= (109 + startCount * 4) && m_Vector[count].StartTimeSlotOfSecondClass <= (112 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= (109 + startCount * 4) && m_Vector[count].StartTimeSlotOfThirdClass <= (112 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            break;
                        case 3:
                            if (m_Vector[count].StartTimeSlotOfFirstClass >= (161 + startCount * 4) && m_Vector[count].StartTimeSlotOfFirstClass <= (164 + startCount * 4))
                            {
                                m_VectorRating += 5;
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= (161 + startCount * 4) && m_Vector[count].StartTimeSlotOfSecondClass <= (164 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= (161 + startCount * 4) && m_Vector[count].StartTimeSlotOfThirdClass <= (164 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            break;
                        case 4:
                            if (m_Vector[count].StartTimeSlotOfFirstClass >= (213 + startCount * 4) && m_Vector[count].StartTimeSlotOfFirstClass <= (216 + startCount * 4))
                            {
                                m_VectorRating += 5;
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= (213 + startCount * 4) && m_Vector[count].StartTimeSlotOfSecondClass <= (216 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= (213 + startCount * 4) && m_Vector[count].StartTimeSlotOfThirdClass <= (216 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            break;
                        case 5:
                            if (m_Vector[count].StartTimeSlotOfFirstClass >= (266 + startCount * 4) && m_Vector[count].StartTimeSlotOfFirstClass <= (269 + startCount * 4))
                            {
                                m_VectorRating += 5;
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= (266 + startCount * 4) && m_Vector[count].StartTimeSlotOfSecondClass <= (269 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= (266 + startCount * 4) && m_Vector[count].StartTimeSlotOfThirdClass <= (269 + startCount * 4))
                                {
                                    m_VectorRating += 5;
                                }
                            }
                            break;
                        default:
                            break;
                    }  
                }
            }
        }

        private void checkIfStudyInDay(int i_Day)
        {
            int count = 0;
            for (; count < k_MaximumNumberOfCourses; count++)
            {
                if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                {
                    switch (i_Day)
                    {
                        case 0:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 1 && m_Vector[count].StartTimeSlotOfFirstClass <= 52)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 1 && m_Vector[count].StartTimeSlotOfSecondClass <= 52)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 1 && m_Vector[count].StartTimeSlotOfThirdClass <= 52)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            break;
                        case 1:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 53 && m_Vector[count].StartTimeSlotOfFirstClass <= 104)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 53 && m_Vector[count].StartTimeSlotOfSecondClass <= 104)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 53 && m_Vector[count].StartTimeSlotOfThirdClass <= 104)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            break;
                        case 2:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 105 && m_Vector[count].StartTimeSlotOfFirstClass <= 156)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 105 && m_Vector[count].StartTimeSlotOfSecondClass <= 156)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 105 && m_Vector[count].StartTimeSlotOfThirdClass <= 156)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            break;
                        case 3:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 157 && m_Vector[count].StartTimeSlotOfFirstClass <= 208)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 157 && m_Vector[count].StartTimeSlotOfSecondClass <= 208)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 157 && m_Vector[count].StartTimeSlotOfThirdClass <= 208)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            break;
                        case 4:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 209 && m_Vector[count].StartTimeSlotOfFirstClass <= 261)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 209 && m_Vector[count].StartTimeSlotOfSecondClass <= 261)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 209 && m_Vector[count].StartTimeSlotOfThirdClass <= 261)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            break;
                        case 5:
                            if (m_Vector[count].StartTimeSlotOfFirstClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfFirstClass >= 262 && m_Vector[count].StartTimeSlotOfFirstClass <= 285)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfSecondClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfSecondClass >= 262 && m_Vector[count].StartTimeSlotOfSecondClass <= 285)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            if (m_Vector[count].StartTimeSlotOfThirdClass != -1)
                            {
                                if (m_Vector[count].StartTimeSlotOfThirdClass >= 262 && m_Vector[count].StartTimeSlotOfThirdClass <= 285)
                                {
                                    m_VectorRating += 15;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void selectRandomCoursesPositions(int i_AmountOfNumbers, out int[] o_randArr)
        {
            int next, count;
            o_randArr = new int[i_AmountOfNumbers];
            count = 0;
            next = rnd.Next(i_AmountOfNumbers);
            o_randArr[count] = next;
            count++;
            while (count != i_AmountOfNumbers)
            {
                next = rnd.Next(i_AmountOfNumbers);
                while(isIn(next, count, o_randArr))
                {
                    next = rnd.Next(i_AmountOfNumbers);
                }
                o_randArr[count] = next;
                count++;
            }
        }

        private bool isIn(int i_NumberToCheck, int i_Count, int[] i_Arr)
        {
            int count = 0;
            bool isIn = false;
            while (count < i_Count)
            {
                if (i_NumberToCheck == i_Arr[count])
                {
                    isIn = true;
                }
                count++;
            }
            return isIn;
        }

        public int maxCourses
        {
            get { return k_MaximumNumberOfCourses; }
            set { }
        }

        public double Rate
        { get { return m_VectorRating; }
            set { }
        }

        public List<CourseBlock> Vector
        {
            get { return m_Vector; }
        }

        public static int CompareVectors(VectorOfCourses i_VectorOne, VectorOfCourses i_VectorTwo)
        {
            if (i_VectorOne.Rate > i_VectorTwo.Rate)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

    }
}
