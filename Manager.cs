using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ShopHope
{
    class Manager : Employee
    {
        private Manager(string name, string id, string mail, long phoneNo, int age, string password) : base(name, id, mail, phoneNo, age, password,"Manager")
        {
        }
        public static bool getManager(string name, string id, string mail, long phoneNo, int age, string password) {
            bool flag = false;
            try
            {
                MySqlConnection conn = Connection.getConnection();
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable WHERE post = '"+"Manager"+"'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    flag = true;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager change btn " + ex.Message);
            }
            if(!flag) {
                new Manager(name,id,mail,phoneNo,age,password);
                
            }
            return !flag;

        }

        public static void getEmployee(string name, string id, string mail, long phoneNo, int age, string password, string post)
        {
            if(post.Equals("Manager")) {
                getManager(name,id,mail,phoneNo,age,password);
            }    
        }
    }
}
