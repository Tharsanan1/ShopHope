using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ShopHope
{
    public partial class ManagerForm : Form
    {
        static System.Timers.Timer timer = new System.Timers.Timer();
        static int notificationCount;
        static List<string> notificationStringList = new List<string>();
        static ManagerForm managerForm;
        static string notificationString = "";
        public static object lockObject = new object();
        private ManagerForm()
        {
            InitializeComponent();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
            timer.Start();
        }
        public static ManagerForm getManagerForm() {
            if(managerForm == null) {
                managerForm = new ManagerForm();
            }
            return managerForm;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (lockObject)
            {
                MySqlConnection conn = Connection.getConnection();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable WHERE userId = 1", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (dataReader.GetString("notificationSpace").Equals(""))
                        {
                            notificationCount = 0;
                            break;
                        }
                        if (notificationString.Equals(dataReader.GetString("notificationSpace")))
                        {
                            return;
                        }
                        notificationString = dataReader.GetString("notificationSpace");
                        string[] sentenceArr = dataReader.GetString("notificationSpace").Split('*');
                        //MessageBox.Show("notification string : "+notificationString+" notificationCount : "+notificationCount+"sentenceArr legth: "+sentenceArr.Length);
                        int newCount = sentenceArr.Length;
                        if(notificationCount>0) { notificationCount--; }
                        for (int i = notificationCount; i < newCount; i++)
                        {
                            MessageBox.Show(sentenceArr[i]);
                            notificationStringList.Add(sentenceArr[i]);
                        }
                        notificationCount = newCount;
                    }
                    int count = notificationCount;
                    count -= 1;
                    //MessageBox.Show("count: "+count.ToString());
                    changeLbl((count));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at stock manager timer elapsed " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void changeLbl(int s)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { changeLbl(s); }));
            }
            else
            {
                if(s>0) {
                    countNotificationLbl.Text = s.ToString();
                }
                else {
                    countNotificationLbl.Text = "";
                }
            }
        }

        private void Manager_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("x " + e.X + "y " + e.Y);
        }   

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("x " + e.X + "y " + e.Y);
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("x " + e.X + "y " + e.Y);
        }

        private void profilePanel_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("x " + e.X + "y " + e.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Product(1,2,"a","b","c",2.3,3.4);
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {
            lock (lockObject)
            {
                notificationCountLbl.Text = "";
                MySqlConnection conn = Connection.getConnection();
                for (int i = 0; i < notificationStringList.Count - 1; i++)
                {
                    MessageBox.Show("notificatiion split: " + notificationStringList[i]);
                    if (notificationStringList[i].Length == 0)
                    {
                        break;
                    }
                    if (notificationStringList[i].Substring(0, 1).Equals("/"))
                    {
                        DialogResult dialog = MessageBox.Show(notificationStringList[i].Substring(1), "", MessageBoxButtons.YesNo);
                        string temp = notificationStringList[i].Substring(7);

                        string quantity = "";
                        string returnQuantity = "";

                        try
                        {

                            conn.Open();
                            MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE stockId = '" + getId(temp) + "'", conn);
                            MySqlDataReader dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                quantity = dataReader.GetString("quantity");
                                returnQuantity = dataReader.GetString("returnQuantity");
                            }
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error occured at stock manager timer elapsed " + ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                        if (dialog == DialogResult.Yes)
                        {
                            try
                            {
                                conn = Connection.getConnection();
                                conn.Open();
                                MessageBox.Show(getQuantity(temp) + " " + returnQuantity);
                                MySqlCommand command = new MySqlCommand("UPDATE shophope.stocks SET returnQuantity = '" + (int.Parse(getQuantity(temp)) + int.Parse(returnQuantity)).ToString() + "' WHERE stockId = '" + getId(temp) + "'", conn);
                                MySqlDataReader dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("error occured at stock manager timer elapsed " + ex.Message);
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                        else
                        {
                            try
                            {
                                conn = Connection.getConnection();
                                conn.Open();
                                MySqlCommand command = new MySqlCommand("UPDATE shophope.stocks SET quantity = '" + (int.Parse(getQuantity(temp)) + int.Parse(quantity)).ToString() + "' WHERE stockId = '" + getId(temp) + "'", conn);
                                MySqlDataReader dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("error occured at stock manager timer elapsed " + ex.Message);
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show(notificationStringList[i]);
                    }
                }
                notificationStringList.Clear();
                conn = Connection.getConnection();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE shophope.employeetable SET notificationSpace= '' WHERE userId = 1", conn);
                    notificationString = "";
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {

                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at stock manager click " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }





        private void notificationPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }





        private void notificationPanel_MouseClick(object sender, MouseEventArgs e)
        {
            lock (lockObject)
            {
                notificationCountLbl.Text = "";
                MySqlConnection conn = Connection.getConnection();
                for (int i = 0; i < notificationStringList.Count - 1; i++)
                {
                    //MessageBox.Show("notificatiion split: " + notificationStringList[i]);
                    if (notificationStringList[i].Length == 0)
                    {
                        continue;
                    }
                    if (notificationStringList[i].Substring(0, 1).Equals("/"))
                    {
                        DialogResult dialog = MessageBox.Show(notificationStringList[i].Substring(1), "", MessageBoxButtons.YesNo);
                        string temp = notificationStringList[i].Substring(7);

                        string quantity = "";
                        string returnQuantity = "";

                        try
                        {

                            conn.Open();
                            MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE stockId = '" + getId(temp) + "'", conn);
                            MySqlDataReader dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                quantity = dataReader.GetString("quantity");
                                returnQuantity = dataReader.GetString("returnQuantity");
                            }
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error occured at stock manager timer elapsed " + ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                        if (dialog == DialogResult.Yes)
                        {
                            try
                            {
                                conn = Connection.getConnection();
                                conn.Open();
                                //MessageBox.Show(getQuantity(temp) + " " + returnQuantity);
                                MySqlCommand command = new MySqlCommand("UPDATE shophope.stocks SET returnQuantity = '" + (int.Parse(getQuantity(temp)) + int.Parse(returnQuantity)).ToString() + "' WHERE stockId = '" + getId(temp) + "'", conn);
                                MySqlDataReader dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("error occured at stock manager timer elapsed " + ex.Message);
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                        else
                        {
                            try
                            {
                                conn = Connection.getConnection();
                                conn.Open();
                                MySqlCommand command = new MySqlCommand("UPDATE shophope.stocks SET quantity = '" + (int.Parse(getQuantity(temp)) + int.Parse(quantity)).ToString() + "' WHERE stockId = '" + getId(temp) + "'", conn);
                                MySqlDataReader dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("error occured at stock manager timer elapsed " + ex.Message);
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show(notificationStringList[i]);
                    }
                }
                notificationStringList.Clear();
                conn = Connection.getConnection();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE shophope.employeetable SET notificationSpace= '' WHERE userId = 1", conn);
                    notificationString = "";
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {

                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at stock manager click " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }








        public string getId(string s)
        {
            string temp = "";
            foreach(char c in s) {
                if(c == ' ') {
                    break;
                }
                temp += c;
            }
            return temp;
        }
        public string getQuantity(string s) {
            string temp = "";
            for (int i = s.Length-1; i>0; i--) {
                if (s.Substring(i,1).Equals(" "))
                {
                    break;
                }
                temp += s.Substring(i, 1);
            }
            return temp;
        }
    }
}
