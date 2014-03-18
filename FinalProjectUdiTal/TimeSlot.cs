using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace FinalProjectUdiTal
{
    class TimeSlot
    {
        int m_TimeSlotID;
        int m_TimeSlotDay;
        TimeSpan m_TimeSlotStartTime;
        TimeSpan m_TimeSlotEndTime;
        private const string k_CS = @"server=sql2.freemysqlhosting.net;userid=sql217024;
            password=bL9*yK9*;database=sql217024";

        public int ID
        {
            get
            {
                return m_TimeSlotID;
            }
            set{}
        }

        public int Day
        {
            get
            {
                return m_TimeSlotDay;
            }
            set { }
        }

        public string StrartTime
        {
            get
            {
                return m_TimeSlotStartTime.ToString();
            }
            set { }
        }

        public string EndTime
        {
            get
            {
                return m_TimeSlotEndTime.ToString();
            }
            set { }
        }

        public void readTimeFrame(int i_TimeID)
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;
            try
            {
                conn = new MySqlConnection(k_CS);
                conn.Open();

                string quoteByString = "SELECT * FROM sql217024.timemapping where TimeID = '" + i_TimeID + "'";

                MySqlCommand cmd = new MySqlCommand(quoteByString, conn);
                rdr = cmd.ExecuteReader();

                rdr.Read();
                m_TimeSlotID = rdr.GetInt32(0);
                m_TimeSlotDay = rdr.GetInt32(1);
                m_TimeSlotStartTime = rdr.GetTimeSpan(2);
                m_TimeSlotEndTime = rdr.GetTimeSpan(3);


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
