using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using MySql.Data.MySqlClient;
using System.Web;
using System.Net.Mail;

namespace ShopHope
{
    public partial class Login : Form
    {
        static System.Timers.Timer timer = new System.Timers.Timer();
        static int count;
        static Object o = new object();
        static Random random = new Random();
        static object lockObject = new object();
        public Login()
        {
            
            InitializeComponent();
            signUpPanel.Visible = false;
            timer.Interval = 2000;
            timer.Elapsed += Timer_Elapsed;
            backGroundPanal.BackgroundImage = Properties.Resources.LogoShopHope;
            timer.Start();
            count = 0;
            userNameTxt.Text = "tharsanan";
            passWordTxt.Text = "123456";
            passWordTxt.PasswordChar = '\u25CF';
            //ManagerForm.getManagerForm().Show();
            
            StockManagementSystem.doDailyWork();
            EmployeeManagementSystem.doDailyWork();

        }
        

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //MessageBox.Show("");
            lock (o)
            {
                int x = random.Next(250, 1000);
                int y = random.Next(20, 500);
                try {
                    changeLoation(backGroundPanal, x, y);
                }
                catch(Exception ex) {
                    
                }
                
            }
            
        }
        private void changeLoation(Panel p , int x, int  y)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { changeLoation(p,x,y); }));
            }
            else
            {
                Point point = new Point(x, y);
                backGroundPanal.Location = point;
                //helllo i made a change
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "     Are you sure you want to close? \n            It will close all windows", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        private void backGroundPanal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void label3_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (IDTxt.Text.Length > 10)
            {
                IDTxt.Text = IDTxt.Text.Substring(0, IDTxt.Text.Length - 1);
                IDTxt.SelectionStart = IDTxt.Text.Length;
            }
        }

        private void signUpBtn_Click(object sender, EventArgs e)
        {
            if(signUpPanel.Visible == false) {
                signUpPanel.Visible = true;
            }   
            else {
                nameTxt.Text = "";
                IDTxt.Text = "";
                PhoneNoTxt.Text = "";
                mailTxt.Text = "";
                ageTxt.Text = "";
                passwordSecondTxt.Text = "";
                confirmCodeTxt.Text = "";
                stockManagerRadioBtn.Checked = false;
                salesManRadioBtn.Checked = false;
                signUpPanel.Visible = false;
            }
        }

        private void signUpSecondBtn_Click(object sender, EventArgs e)
        {
            if(!(nameTxt.Text.Length>0 && IDTxt.Text.Length>0 && PhoneNoTxt.Text.Length>0 && mailTxt.Text.Length>0 && ageTxt.Text.Length>0 && confirmCodeTxt.Text.Length>0)) {
                    MessageBox.Show("Please fill all boxes.");    
                return;
            }
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable WHERE userId = '1'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if(!dataReader.GetString("passWord").Equals(confirmCodeTxt.Text)) {
                        MessageBox.Show("Confirmation code wrong!");
                        return;
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager    " + ex.Message);
            }
            finally {
                conn.Close();
            }

            lock (lockObject)
            {
                try
                {
                    conn = Connection.getConnection();
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable WHERE userName = '" + nameTxt.Text + "'", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        MessageBox.Show("User name already exist");
                        return;
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at stock manager    " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                signUpPanel.Visible = false;
                if (salesManRadioBtn.Checked)
                {
                    SalesMan.getEmployee(nameTxt.Text, IDTxt.Text, mailTxt.Text, long.Parse(PhoneNoTxt.Text), int.Parse(ageTxt.Text), passwordSecondTxt.Text, "SalesMan");
                    userNameTxt.Text = nameTxt.Text;
                    nameTxt.Text = "";
                    IDTxt.Text = "";
                    mailTxt.Text = "";
                    PhoneNoTxt.Text = "";
                    ageTxt.Text = "";
                    passwordSecondTxt.Text = "";

                }
                else if (stockManagerRadioBtn.Checked)
                {
                    StockManager.getEmployee(nameTxt.Text, IDTxt.Text, mailTxt.Text, long.Parse(PhoneNoTxt.Text), int.Parse(ageTxt.Text), passwordSecondTxt.Text, "StockManager");
                    userNameTxt.Text = nameTxt.Text;
                    nameTxt.Text = "";
                    IDTxt.Text = "";
                    mailTxt.Text = "";
                    PhoneNoTxt.Text = "";
                    ageTxt.Text = "";
                    passwordSecondTxt.Text = "";

                }
                else
                {
                    Labour.getEmployee(nameTxt.Text, IDTxt.Text, mailTxt.Text, long.Parse(PhoneNoTxt.Text), int.Parse(ageTxt.Text), passwordSecondTxt.Text, "Labour");
                    userNameTxt.Text = nameTxt.Text;
                    nameTxt.Text = "";
                    IDTxt.Text = "";
                    mailTxt.Text = "";
                    PhoneNoTxt.Text = "";
                    ageTxt.Text = "";
                    passwordSecondTxt.Text = "";
                }
                signUpPanel.Visible = false;
            }
        }

        private void signInBtn_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable WHERE userName = '"+userNameTxt.Text+"' AND passWord = '"+passWordTxt.Text+"'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                Form form;
                while (dataReader.Read())
                {
                    if(dataReader.GetString("post").Equals("Manager")) {
                        form = ManagerForm.getManagerForm();
                        form.Show();
                    }
                    else if(dataReader.GetString("post").Equals("StockManager")) {
                        StockManagerForm.getStockManagerForm(dataReader.GetString("userName")).Show();
                    }
                    else if (dataReader.GetString("post").Equals("SalesMan"))
                    {
                        SalesManForm.getsalesManform(dataReader.GetString("userName")).Show();
                    }
                    else {
                        MessageBox.Show("Sorry you don't have any windows...");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager    " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void PhoneNoTxt_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in PhoneNoTxt.Text)
            {
                if (!char.IsDigit(c))
                {
                    PhoneNoTxt.Text = PhoneNoTxt.Text.Substring(0, PhoneNoTxt.Text.Length - 1);
                    PhoneNoTxt.SelectionStart = PhoneNoTxt.Text.Length;
                }

            }
            
        }

        private void ageTxt_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in ageTxt.Text)
            {
                if (!char.IsDigit(c))
                {
                    ageTxt.Text = ageTxt.Text.Substring(0, ageTxt.Text.Length - 1);
                    ageTxt.SelectionStart = ageTxt.Text.Length;
                }

            }
            if (ageTxt.Text.Length > 2)
            {
                ageTxt.Text = ageTxt.Text.Substring(0, ageTxt.Text.Length - 1);
                ageTxt.SelectionStart = ageTxt.Text.Length;
            }
        }

        private void forgotBtn_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable WHERE userName = '" + userNameTxt.Text + "' ", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    MailMessage mail = new MailMessage("shophopecompany@gmail.com", dataReader.GetString("emailAdress"),"Your Password Info",("User Name : "+dataReader.GetString("userName")+"\n password : "+dataReader.GetString("passWord")));
                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.Port = 587;
                    client.Credentials = new System.Net.NetworkCredential("shophopecompany@gmail.com","shophope@mora");
                    client.EnableSsl = true;
                    client.Send(mail);
                    MessageBox.Show("Check your mail : "+ dataReader.GetString("emailAdress"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager    " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}