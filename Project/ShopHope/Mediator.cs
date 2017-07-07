using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ShopHope
{
    class Mediator
    {
        public static void notifyManager(string s)
        {
            
                string notificationString = "";
                MySqlConnection conn = Connection.getConnection();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable WHERE userId =1", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        notificationString = dataReader.GetString("notificationSpace");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at mediator " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                try
                {
                    conn = Connection.getConnection();
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE shophope.employeetable SET notificationSpace = '" + (notificationString + s + "*") + "' WHERE userId = 1", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at mediator " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        
    }
}
