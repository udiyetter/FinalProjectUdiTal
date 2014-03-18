using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Linq;

namespace FinalProjectUdiTal
{
    class GeneticAlgorithm
    {
        private const string k_CS = @"server=sql2.freemysqlhosting.net;userid=sql217024;
                password=bL9*yK9*;database=sql217024";

        const int k_NumberOfStandartRequiredCourses = 4;
        const int k_NumberOfStandartElectivesCourses = 3;
        const int k_MutationListSize = 500;
        const int k_MutationCrossOverSize = 1000;
        internal const int k_SizeOfFatherList = 40;
        private static Random rnd = new Random();
        static private List<VectorOfCourses> m_OrderList;

        Population m_Population;
        List<VectorOfCourses> m_Fathers;
        Course[] m_ArrayOfCourses ;//= new Course[k_NumberOfStandartRequiredCourses + k_NumberOfStandartElectivesCourses];
        int[] m_arrayOfCoursesNumbers;
        Student m_Student;
        List<List<CourseBlock>> courseBlockDB = new List<List<CourseBlock>>();

        public GeneticAlgorithm(int i_studentId, int i_numberOfRequireds, int i_numberOfElectives)
        {
            int i;
            m_OrderList = new List<VectorOfCourses>();
            m_Fathers = new List<VectorOfCourses>();
            m_Student = new Student(i_studentId);
            m_Student.numberOfElectiveCourseTheStudentWantToTake = i_numberOfElectives;
            m_Student.numberOfRequiredCourseTheStudentWantToTake = i_numberOfRequireds;
            m_ArrayOfCourses = m_Student.modifyArrayOfCourses(i_numberOfRequireds);
            m_arrayOfCoursesNumbers = m_Student.modifyNumbersOfCoursesToArray(m_ArrayOfCourses);
            UpdateDataOfBlocksFromDB();
            m_Population = new Population(m_arrayOfCoursesNumbers, courseBlockDB, m_Student);
            updateFatherList(m_Population.Vectors);
            crossOverFatherList(m_Fathers);
            for (i = 0; i < 3; i++)
            {
                updateFatherList(m_Fathers);
                crossOverFatherList(m_Fathers);
            }
            m_OrderList = m_Fathers.OrderBy(VectorOfCourses => VectorOfCourses.Rate).ToList();
            removeDuplicateVectors(m_OrderList, ref m_OrderList);
        }

        private void removeDuplicateVectors(List<VectorOfCourses> i_CheckedList, ref List<VectorOfCourses> i_OutList)
        {
            int j, i = 0;
            List<VectorOfCourses> newList = new List<VectorOfCourses>();
            bool[] duplicate = new bool[i_CheckedList.Count];
            for (i = 0; i < i_CheckedList.Count - 1; i++)
            {
                if (duplicate[i] == false)
                {
                    for (j = i + 1; j < i_CheckedList.Count; j++)
                    {
                        if (isDuplicateVector(i_CheckedList[i], i_CheckedList[j]))
                        {
                            duplicate[j] = true;
                        }
                    }
                }
            }
            for (i=0; i < i_CheckedList.Count; i++)
            {
                if (duplicate[i] == false)
                {
                    newList.Add(i_CheckedList[i]);
                }
            }
            //i_OutList.Clear();
            i_OutList = newList;

        }

        private bool isDuplicateVector(VectorOfCourses i_FirstVector, VectorOfCourses i_SecondVector)
        {
            bool res = true; ;
            int i;
            VectorOfCourses tempVectorFirst = CreateOnlyStudyCoursesVector(i_FirstVector);
            VectorOfCourses tempVectorSecond = CreateOnlyStudyCoursesVector(i_SecondVector);
            if (i_FirstVector.Rate != i_SecondVector.Rate)
            {
                res = false;
            }
            else if (CountNumberOfCourses(i_FirstVector) != CountNumberOfCourses(i_SecondVector))
            {
                res = false;
            }
            else
            {
                for (i = 0; i < tempVectorFirst.Vector.Count; i++)
                {
                    if (!isCourseInVector(tempVectorFirst.Vector[0], tempVectorSecond.Vector))
                    {
                        res = false;
                    }
                }

                //if (isCourseInVector(i_FirstVector.Vector[0], i_SecondVector.Vector))
                //{
                //    if (isCourseInVector(i_FirstVector.Vector[1], i_SecondVector.Vector))
                //    {
                //        if (isCourseInVector(i_FirstVector.Vector[2], i_SecondVector.Vector))
                //        {
                //            if (isCourseInVector(i_FirstVector.Vector[3], i_SecondVector.Vector))
                //            {
                //                if (isCourseInVector(i_FirstVector.Vector[4], i_SecondVector.Vector))
                //                {
                //                    if (isCourseInVector(i_FirstVector.Vector[5], i_SecondVector.Vector))
                //                    {
                //                        if (isCourseInVector(i_FirstVector.Vector[6], i_SecondVector.Vector))
                //                        {
                //                            res = true;
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
            }
            return res;
        }

        private VectorOfCourses CreateOnlyStudyCoursesVector(VectorOfCourses i_Vector)
        {
            VectorOfCourses resVector = new VectorOfCourses();
            int i;
            for (i = 0; i < i_Vector.Vector.Count; i++)
            {
                if (i_Vector.Vector[i].StartTimeSlotOfFirstClass != -1)
                {
                    resVector.Vector.Add(i_Vector.Vector[i]);
                }
            }
            return resVector;

        }

        private int CountNumberOfCourses(VectorOfCourses i_Vector)
        {
            int count = 0;
            int i;
            for (i = 0; i < i_Vector.Vector.Count; i++)
            {
                if (i_Vector.Vector[i].StartTimeSlotOfFirstClass != -1)
                {
                    count++;
                }
            }
            return count;
        }

        private bool isCourseInVector(CourseBlock i_CourseBlock, List<CourseBlock> i_ListOfCourses)
        {
            int i;
            bool res = false;
            for (i = 0; i < i_ListOfCourses.Count; i++)
            {
                if ((i_CourseBlock.ID == i_ListOfCourses[i].ID) && (i_CourseBlock.StartTimeSlotOfFirstClass == i_ListOfCourses[i].StartTimeSlotOfFirstClass) && (i_CourseBlock.StartTimeSlotOfSecondClass == i_ListOfCourses[i].StartTimeSlotOfSecondClass) && (i_CourseBlock.StartTimeSlotOfThirdClass == i_ListOfCourses[i].StartTimeSlotOfThirdClass))
                {
                    res = true;
                }
            }
            return res;
        }


        void UpdateDataOfBlocksFromDB()
        {
            int i=0;
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            for (; i < m_arrayOfCoursesNumbers.Length; i++)
            {
                List<CourseBlock> listBlock = new List<CourseBlock>();
                try
                {            
                    conn = new MySqlConnection(k_CS);
                    conn.Open();
                    //string quaery = "select * FROM sql217024.coursesblocks where idcourses = " + m_arrayOfCoursesNumbers[i];
                    string query = string.Format(@"SELECT blocks.*,
                                                    courses.TitleCourses, credits.courseCredit,
                                                    teachers1.teacherName AS 'teacher1', starttime1.StartTime AS 'startTime1', endtime1.EndTime AS 'endTime1',
                                                    teachers2.teacherName AS 'teacher2', starttime2.StartTime AS 'startTime2', endtime2.EndTime AS 'endTime2',
                                                    teachers3.teacherName AS 'teacher3', starttime3.StartTime AS 'startTime3', endtime3.EndTime AS 'endTime3'
                                                    FROM sql217024.coursesblocks blocks
                                                    LEFT JOIN sql217024.coursescredit credits ON blocks.idCourses = credits.CourseID
                                                    LEFT JOIN sql217024.coursesnames courses ON blocks.idCourses = courses.idCourses
                                                    LEFT JOIN sql217024.teachersmapping teachers1 ON blocks.Teacher1 = teachers1.TeacherID
                                                    LEFT JOIN sql217024.teachersmapping teachers2 ON blocks.Teacher2 = teachers2.TeacherID
                                                    LEFT JOIN sql217024.teachersmapping teachers3 ON blocks.Teacher3 = teachers3.TeacherID
                                                    LEFT JOIN sql217024.timemapping starttime1 ON blocks.startTimeSlot1 = starttime1.TimeID
                                                    LEFT JOIN sql217024.timemapping starttime2 ON blocks.startTimeSlot2 = starttime2.TimeID
                                                    LEFT JOIN sql217024.timemapping starttime3 ON blocks.startTimeSlot3 = starttime3.TimeID
                                                    LEFT JOIN sql217024.timemapping endtime1 ON blocks.endTimeSlot1 = endtime1.TimeID
                                                    LEFT JOIN sql217024.timemapping endtime2 ON blocks.endTimeSlot2 = endtime2.TimeID
                                                    LEFT JOIN sql217024.timemapping endtime3 ON blocks.endTimeSlot3 = endtime3.TimeID
                                                    WHERE blocks.idCourses = {0}", m_arrayOfCoursesNumbers[i]);

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        CourseBlock newBlock = null;

                        // No third class
                        if (rdr.IsDBNull(12))
                        {
                            // No second class
                            if (rdr.IsDBNull(7))
                            {
                                newBlock = new CourseBlock(rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetInt32(5), rdr.GetInt32(6), -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);
                            }
                            else
                            {
                                newBlock = new CourseBlock(rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetInt32(5), rdr.GetInt32(6), rdr.GetInt32(7), rdr.GetInt32(8), rdr.GetInt32(9), rdr.GetInt32(10), rdr.GetInt32(11), -1, -1, -1, -1, -1);
                            }
                        }
                        else
                        {
                            newBlock = new CourseBlock(rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetInt32(5), rdr.GetInt32(6), rdr.GetInt32(7), rdr.GetInt32(8), rdr.GetInt32(9), rdr.GetInt32(10), rdr.GetInt32(11), rdr.GetInt32(12), rdr.GetInt32(13), rdr.GetInt32(14), rdr.GetInt32(15), rdr.GetInt32(16));
                        }

                        // First class data (always exists)
                        newBlock.StartTimeSlotOfFirstClassEx = DateTime.Parse(rdr.GetString("startTime1"));
                        newBlock.EndTimeSlotOfFirstClassEx = DateTime.Parse(rdr.GetString("endTime1"));
                        newBlock.TeacherNameOfFirstClass = rdr.GetString("teacher1");

                        // Second class data
                        if (newBlock.StartTimeSlotOfSecondClass != -1)
                        {
                            newBlock.StartTimeSlotOfSecondClassEx =  DateTime.Parse(rdr.GetString("startTime2"));
                            newBlock.EndTimeSlotOfSecondClassEx = DateTime.Parse(rdr.GetString("endTime2"));
                            newBlock.TeacherNameOfSecondClass = rdr.GetString("teacher2");
                        }

                        // Third class data
                        if (newBlock.StartTimeSlotOfThirdClass != -1)
                        {
                            newBlock.StartTimeSlotOfThirdClassEx =  DateTime.Parse(rdr.GetString("startTime3"));
                            newBlock.EndTimeSlotOfThirdClassEx = DateTime.Parse(rdr.GetString("endTime3"));
                            newBlock.TeacherNameOfThirdClass = rdr.GetString("teacher3");
                        }

                        newBlock.CourseCredit = rdr.GetInt32("courseCredit");
                        newBlock.CourseTitle = rdr.GetString("TitleCourses");

                        // Add to list
                        listBlock.Add(newBlock);

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
                addNullCourse(listBlock, m_arrayOfCoursesNumbers[i]);
                courseBlockDB.Add(listBlock);
                }
            }

        private void addNullCourse(List<CourseBlock> i_listBolck, int i_CourseNumber)
        {
            i_listBolck.Add(new CourseBlock(i_CourseNumber, -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1));
        }

        private void updateFatherList(List<VectorOfCourses> i_Vector)
        {
            int count = 0;
            int first, second;
            bool mutation;
            List<VectorOfCourses> myList = new List<VectorOfCourses>();
            for (; count < k_SizeOfFatherList; count++)
            {
                first = rnd.Next(k_SizeOfFatherList);
                second = rnd.Next(k_SizeOfFatherList);
                mutation = (rnd.Next(k_MutationListSize) == 1);
                if (i_Vector[first].Rate != i_Vector[second].Rate)
                {
                    if (i_Vector[first].Rate < i_Vector[second].Rate)
                    {
                        if (mutation)
                        {
                            myList.Add(new VectorOfCourses(i_Vector[second]));
                        }
                        else
                        {
                            myList.Add(new VectorOfCourses(i_Vector[first]));
                        }
                    }
                    else
                    {
                        if (mutation)
                        {
                            myList.Add(new VectorOfCourses(i_Vector[first]));
                        }
                        else
                        {
                            myList.Add(new VectorOfCourses(i_Vector[second]));
                        }
                    }
                }
                else
                {
                    if (rnd.Next(2) == 1)
                    {
                        myList.Add(new VectorOfCourses(i_Vector[first]));
                    }
                    else
                    {
                        myList.Add(new VectorOfCourses(i_Vector[second]));
                    }

                }
            }
            m_Fathers.Clear();
            m_Fathers = myList;
        }

        private void crossOverFatherList(List<VectorOfCourses> i_Vector )
        {
            List<VectorOfCourses> crossedVector = new List<VectorOfCourses>();
            VectorOfCourses vectorFirst;
            VectorOfCourses vectorSecond;
            int count = 0;
            int first, second, cut;
            for (; count < k_SizeOfFatherList; count++)
            {
                first= rnd.Next(k_SizeOfFatherList);
                second = rnd.Next(k_SizeOfFatherList);
                cut = rnd.Next(k_NumberOfStandartElectivesCourses + k_NumberOfStandartRequiredCourses);
                if ((cut != 0) && (first != second))
                {
                    crossOver(out vectorFirst, out vectorSecond, i_Vector[first], i_Vector[second], cut);
                    checkForMutation(vectorFirst, courseBlockDB);
                    checkForMutation(vectorSecond, courseBlockDB);
                    //vectorFirst.rateVector(m_Student);
                    //vectorSecond.rateVector(m_Student);
                    if(vectorFirst.Rate < vectorSecond.Rate)
                    {
                        crossedVector.Add(vectorFirst);
                    }
                    else if (vectorFirst.Rate > vectorSecond.Rate)
                    {
                        crossedVector.Add(vectorSecond);
                    }
                    else
                    {
                        if (rnd.Next(2) == 1)
                        {
                            crossedVector.Add(vectorFirst);
                        }
                        else
                        {
                            crossedVector.Add(vectorSecond);
                        }
                    }
                    
                }
                else
                {
                    if (first == second)
                    {
                        vectorFirst = new VectorOfCourses(i_Vector[first]);
                        checkForMutation(vectorFirst, courseBlockDB);
                        crossedVector.Add(vectorFirst);
                    }
                    else
                    {
                        vectorFirst = new VectorOfCourses(i_Vector[first]);
                        vectorSecond = new VectorOfCourses(i_Vector[second]);
                        checkForMutation(vectorFirst, courseBlockDB);
                        checkForMutation(vectorSecond, courseBlockDB);
                        if (vectorFirst.Rate < vectorSecond.Rate)
                        {
                            crossedVector.Add(vectorFirst);
                        }
                        else if (vectorFirst.Rate > vectorSecond.Rate)
                        {
                            crossedVector.Add(vectorSecond);
                        }
                        else
                        {
                            if (rnd.Next(2) == 1)
                            {
                                crossedVector.Add(vectorFirst);
                            }
                            else
                            {
                                crossedVector.Add(vectorSecond);
                            }
                        }
                    }
                }
            }
            m_Fathers.Clear();
            m_Fathers = crossedVector;
        }

        //insert rateVector function
        private void checkForMutation(VectorOfCourses i_Vector, List<List<CourseBlock>> i_CourseBlock)
        {
            int count = 0;
            bool rateAgain = false;
            int index;
            for (; count < i_Vector.Vector.Count; count++)
            {
                if (rnd.Next(k_MutationCrossOverSize) == 1)
                {
                    rateAgain = true;
                    index = getIndex(i_Vector.Vector[count].ID, i_CourseBlock);
                    i_Vector.Vector[count] = i_CourseBlock[index][rnd.Next(i_CourseBlock[index].Count)];
                }
            }
            if (rateAgain)
            {
                i_Vector.rateVector(m_Student);
            }
        }

        //there is no safty in it, if ID is not found it will return the size of the list 
        private int getIndex(int i_Id, List<List<CourseBlock>> i_CourseBlock)
        {
            int count = 0;
            bool found = false;
            while ((!found)&&(count<i_CourseBlock.Count))
            {
                if (i_CourseBlock[count][0].ID == i_Id)
                {
                    found = true;
                }
                else
                {
                    count++;
                }
            }
            return count;
        }
        //insert rateVector function
        private void crossOver(out VectorOfCourses o_VectorFirst, out VectorOfCourses o_VectorSecond, VectorOfCourses i_VectorOfCoursesFirst, VectorOfCourses i_VectorOfCoursesSecond, int i_CutRatio)
        {
            o_VectorFirst = new VectorOfCourses();
            o_VectorSecond = new VectorOfCourses();
            o_VectorFirst.Vector.AddRange(i_VectorOfCoursesFirst.Vector.GetRange(0, i_CutRatio));
            o_VectorFirst.Vector.AddRange(i_VectorOfCoursesSecond.Vector.GetRange(i_CutRatio, i_VectorOfCoursesSecond.Vector.Count - i_CutRatio));
            o_VectorSecond.Vector.AddRange(i_VectorOfCoursesSecond.Vector.GetRange(0, i_CutRatio));
            o_VectorSecond.Vector.AddRange(i_VectorOfCoursesFirst.Vector.GetRange(i_CutRatio, i_VectorOfCoursesFirst.Vector.Count - i_CutRatio));
            o_VectorFirst.rateVector(m_Student);
            o_VectorSecond.rateVector(m_Student);
        }

        //ToDo
        //create a function that run all the functions by order
        void runGenetic()
        {
        }

        public List<VectorOfCourses> FinalList
        {
            get { return m_OrderList; }
        }

        //TODO - finalize this function
        static public string GetVectorOfCousesFromFinalList(int i_IndexNumberInTheVector)
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();
                string courseName = "SELECT titlecourses FROM sql217024.coursesnames where idcourses = " + m_OrderList[i_IndexNumberInTheVector];
                string quoteByString = "SELECT titlecourses FROM sql217024.coursesnames where idcourses = " + m_OrderList[i_IndexNumberInTheVector];

                MySqlCommand cmd = new MySqlCommand(quoteByString, conn);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                   
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

            string resultString = "";
            return resultString;
        }
    }

    



        //buildPopulation()
    
}

