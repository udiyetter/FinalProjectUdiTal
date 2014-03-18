using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace FinalProjectUdiTal
{
    class Project
    {
        static public void Main()
        {

            LoginForm logIn = new LoginForm();
            OutputForm summaryForm = new OutputForm();
            GeneticAlgorithm myGeneticAlgo;
            logIn.ShowDialog();
            if (logIn.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                
                myGeneticAlgo = new GeneticAlgorithm(int.Parse(logIn.ComboBoxUserId.Text),4,3);
                summaryForm.m_FinalCourseList = new List<VectorOfCourses>(myGeneticAlgo.FinalList);
                summaryForm.ShowDialog();
            }
           


            
        }
    }
}