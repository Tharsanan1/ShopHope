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
        static string ConnString = "server = 127.0.0.1; port = 3308; DATABASE = shophope; UID = root; PASSWORD = Thars@123;";
        static MySqlConnection conn = new MySqlConnection(ConnString);
        static MySqlConnection conn1;
        static MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
        static MySqlCommandBuilder commandBuilder;
        static MySqlCommand command;
        static List<MySqlConnection> connList = new List<MySqlConnection>();
        public static void performConnection(string s) {
            try
            {
                
                command = new MySqlCommand(s,conn);
                //dataAdapter.SelectCommand = new MySqlCommand(s, conn);
                //commandBuilder = new MySqlCommandBuilder(dataAdapter);
                conn.Open();
                MySqlDataReader dataReader;
                dataReader = command.ExecuteReader();
                while(dataReader.Read()) {

                }
                conn.Close();
                
            }
            catch(Exception e) {
                Console.WriteLine("exception occured in connection: "+e.Message);
            }
        }
        public static MySqlConnection getConnection()
        {
            if(connList.Count == 0) {
                connList.Add(new MySqlConnection(ConnString));
                return connList[0];
            }
            else {
                //bool flag = false;
                for(int i =0; i<connList.Count; i++) {
                    if(connList[i].State.ToString().Equals("Closed")) {
                        return connList[i];
                    }
                }
                connList.Add(new MySqlConnection(ConnString));
                return connList[connList.Count-1];
            }
        }
    }
}
