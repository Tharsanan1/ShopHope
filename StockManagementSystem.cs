using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ShopHope
{
    class StockManagementSystem
    {
        static System.Timers.Timer timer;
        

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
        }
        public static void doDailyWork() {
            checkForLackStocks();
            checkOfferClose();
        }
        public static void checkOfferClose() {
            MySqlConnection conn = Connection.getConnection();
            DateTime date = DateTime.Today;
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string dateString = dataReader.GetString("offerDate");
                    if(dataReader.GetString("offerDate").Length==0) {
                        dateString = "0";
                    }
                    if(int.Parse(dateString) <int.Parse(date.ToString("yyyyMMdd"))) { // checked for new day
                        string id = dataReader.GetString("stockId"); 
                        MySqlConnection conn1 = Connection.getConnection();
                        string realPrice = "";
                        string realOfferPrice = "";
                        try
                        {
                            conn1.Open();
                            MySqlCommand command1 = new MySqlCommand("SELECT * FROM shophope.stocks WHERE stockId = '"+id+"'", conn1);
                            MySqlDataReader dataReader1 = command1.ExecuteReader();
                            while (dataReader1.Read())
                            {
                                realPrice = dataReader1.GetString("realPrice");
                                realOfferPrice = dataReader1.GetString("price");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error occured at stock manager " + ex.Message);
                        }
                        finally
                        {
                            conn1.Close();
                        }
                        conn1 = Connection.getConnection();
                        try
                        {
                            conn1.Open();
                            MySqlCommand command1 = new MySqlCommand("UPDATE shophope.stocks SET price = '"+realPrice+"', offerDate = '' WHERE stockId = '" + id + "'", conn1);
                            MySqlDataReader dataReader1 = command1.ExecuteReader();
                            while (dataReader1.Read())
                            {
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error occured at stock manager " + ex.Message);
                        }
                        finally
                        {
                            conn1.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private static void checkForLackStocks() {
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    
                    if(int.Parse(dataReader.GetString("warningLevel"))>=int.Parse(dataReader.GetString("quantity")) && dataReader.GetString("warnable").Equals("t")) {
                        Mediator.notifyManager("Low Stock Warning stock Id: "+dataReader.GetString("stockId"));

                    }

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
