using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ShopHope
{
    class EmployeeManagementSystem
    {
        public static void doDailyWork() {
            MySqlConnection conn = Connection.getConnection();
            string date = "";
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.system WHERE id = 1", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    date = dataReader.GetString("date");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager weight combo " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            DateTime dateTime = DateTime.Today;
            if(!date.Equals(dateTime.ToString("yyyyMMdd")))    // checked as new day
            {
                checkEmployeesAsAbsent();
                changeDateForLeave();
                if (!date.Substring(0,6).Equals(dateTime.ToString("yyyyMM"))) {
                    doMonthlyWork();
                }
                try
                {
                    conn = Connection.getConnection();
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE shophope.system SET date = '"+ dateTime.ToString("yyyyMMdd") + "' WHERE id = 1", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        date = dataReader.GetString("date");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at stock manager weight combo " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static void checkEmployeesAsAbsent() {
            MySqlConnection conn = Connection.getConnection();
            List<bool> attandanceList = new List<bool>();
            List<int> attandanceCountList = new List<int>();
            List<int> overTimeList = new List<int>();
            List<int> userIdList = new List<int>();
            List<string> leavesCountList = new List<string>(); 
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    attandanceCountList.Add(int.Parse(dataReader.GetString("monthlyAttandance")));
                    userIdList.Add(int.Parse(dataReader.GetString("userId")));
                    overTimeList.Add(int.Parse(dataReader.GetString("todayOverTime"))+int.Parse(dataReader.GetString("overTime")));
                    leavesCountList.Add(dataReader.GetString("leavesPerMonth"));
                    if (dataReader.GetString("presence").Equals("t")) {
                        attandanceList.Add(true);
                    }
                    else { attandanceList.Add(false); }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager weight combo " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            for (int i = 0; i < attandanceList.Count; i++)
            {
                if (attandanceList[i])
                {
                    try
                    {
                
                        conn = Connection.getConnection();
                        conn.Open();
                        MySqlCommand command = new MySqlCommand("UPDATE shophope.employeetable SET monthlyAttandance = '" + (attandanceCountList[i]+1).ToString() + "', overTime = '"+overTimeList[i].ToString()+"',todayOverTime = '0', presence = 'f' WHERE userId = '"+userIdList[i].ToString()+"'", conn);
                        MySqlDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("error occured at stock manager weight combo " + ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else {
                    try
                    {

                        conn = Connection.getConnection();
                        conn.Open();
                        MySqlCommand command = new MySqlCommand("UPDATE shophope.employeetable SET leavesPerMonth = '" + (int.Parse(leavesCountList[i]) + 1).ToString() + "',todayOverTime = '0' WHERE userId = '" + userIdList[i].ToString() + "'", conn);
                        MySqlDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("error occured at stock manager weight combo " + ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            
        }
        public static void doMonthlyWork() {
            MySqlConnection conn = Connection.getConnection();
            List<int> attandanceCountList = new List<int>();
            List<string> idList = new List<string>();
            List<string> salaryList = new List<string>();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    salaryList.Add(((int.Parse(dataReader.GetString("monthlyAttandance"))*500)+(int.Parse(dataReader.GetString("overTime"))*300)).ToString());
                    idList.Add(dataReader.GetString("userId"));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager weight combo " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            for (int i = 0; i<idList.Count; i++){
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE shophope.employeetable SET leavesPerMonth = '0' , salary = '"+salaryList[i]+"' , monthlyAttandance = '0' , overTime = '0' WHERE userId = '"+idList[i]+"'", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at stock manager weight combo " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            Mediator.notifyManager("Monthly Salary Information Updated");
        }
        public static void changeDateForLeave()
        {
            MySqlConnection conn = Connection.getConnection();
            List<string> nameList = new List<string>();
            List<string> priorityList = new List<string>();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.leave", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    nameList.Add(dataReader.GetString("name"));
                    priorityList.Add(dataReader.GetString("priority"));
                    Console.WriteLine("added name: "+ dataReader.GetString("name")+" priority: "+ dataReader.GetString("priority"));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager weight combo " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            nameList.RemoveAt(0);
            priorityList.RemoveAt(0);
            nameList.Add("");
            priorityList.Add("0");
            for(int i=0; i<14; i++) {
                conn = Connection.getConnection();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE shophope.leave SET name = '"+nameList[i]+"', priority = '"+priorityList[i]+"' WHERE day = '"+(i+1).ToString()+"'", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at stock manager weight combo " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                Console.WriteLine("updated name: "+nameList[i]+" priority: "+ priorityList[i]+" at : "+i);
            }
        }
    }
}
