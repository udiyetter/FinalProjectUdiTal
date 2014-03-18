using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectUdiTal
{
    class Population
    {
        const int k_PopulationMaxSize = GeneticAlgorithm.k_SizeOfFatherList;
        List<VectorOfCourses> m_Vectors;
        int m_SizeOfVector = 0;
        private int[] m_arrayOfCoursesNumbers;
        private List<List<CourseBlock>> courseBlockDB;
        private Student m_Student;

        public Population(int[] i_arrayOfCoursesNumber, List<List<CourseBlock>> i_ListBlock, Student i_Student)
        {
            m_Vectors = new List<VectorOfCourses>();
            for (; m_SizeOfVector < k_PopulationMaxSize; m_SizeOfVector++) 
            { 
                m_Vectors.Add(new VectorOfCourses(i_arrayOfCoursesNumber, i_ListBlock, i_Student)); 
            }
        }


        public List<VectorOfCourses> Vectors
        {
            get { return m_Vectors; }
        }
        //ToDo
        //XL - B2
        //public VectorOfCourses selectRandomlyCoupleAndaReturnStronger()
        //{
            
        //}

        //ToDo
        //XL - B3
        //public VectorOfCourses crossOver(VectorOfCourses i_vectorNumberOne, VectorOfCourses i_vectorNumbertwo)
        //{
        //}
    }
}
