using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ShopHope
{
    class Connection
    {
        static string ConnString = "server = 192.168.0.101; port = 3308; DATABASE = shophope; UID = root; PASSWORD = 1234;";
        static MySqlConnection conn = new MySqlConnection(ConnString);
        static MySqlConnection conn1 = new MySqlConnection(ConnString);
        static MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
        static MySqlCommand command;
        static List<MySqlConnection> connList = new List<MySqlConnection>();
        static List<bool> freeList = new List<bool>();
        static object lockObject = new object();
        
        public static void performConnection(string s) {
            try
            {
                
                command = new MySqlCommand(s,conn);
                conn.Open();
                MySqlDataReader dataReader;
                dataReader = command.ExecuteReader();
                while(dataReader.Read()) {

                }
                
            }
            catch(Exception e) {
                Console.WriteLine("exception occured in connection: "+e.Message);
            }
            finally {
                conn.Close();
            }
        }
        public static MySqlConnection getExclusiveConnection()
        {
            return conn1;
        }
        public static MySqlConnection getConnection()
        {
            lock (lockObject)
            {

                foreach (MySqlConnection conn in connList)
                {
                    if (conn.State.ToString().Equals("Closed"))
                    {
                        freeList[connList.IndexOf(conn)] = true;
                    }
                }
                Console.WriteLine("connection count : " + connList.Count);
                if (connList.Count == 0)
                {
                    connList.Add(new MySqlConnection(ConnString));
                    freeList.Add(false);
                    return connList[0];
                }
                else
                {
                    //bool flag = false;
                    for (int i = 0; i < connList.Count; i++)
                    {
                        if (connList[i].State.ToString().Equals("Closed") && freeList[i] == true)
                        {
                            freeList[i] = false;
                            return connList[i];
                        }
                    }
                    connList.Add(new MySqlConnection(ConnString));
                    freeList.Add(false);
                    return connList[connList.Count - 1];
                }
            }
        }
    }
}
