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
        object lockObjectInstant;
        static string price;
        System.Timers.Timer timerCountDown;
        int countDown;
        int limit;
        private ManagerForm()
        {
            InitializeComponent();
            limit = 5;
            lockObjectInstant = new object();
            timerCountDown = new System.Timers.Timer();
            timerCountDown.Interval = 1000;
            timerCountDown.Elapsed += TimerCountDown_Elapsed;
            timerCountDown.Start();
            timer.Elapsed += Timer_Elapsed;
            countDown = 0;
            timer.Interval = 1000;
            timer.Start();
            createOfferBtn.Enabled = false;
            dateTimePicker1.Enabled = false;
            fillCatagoryComboBox();
            offerPanel.Visible = false;
            viewStockDataGridView.Visible = false;
            employeeDataGridView.Visible = false;
            attandanceDataGridView.AutoGenerateColumns = false;
            DataGridViewButtonColumn buttonColoumn = new DataGridViewButtonColumn();
            buttonColoumn.HeaderText = "Present";
            buttonColoumn.Text = "check";
            buttonColoumn.UseColumnTextForButtonValue = true;
            attandanceDataGridView.Columns.Add(buttonColoumn);
            attandanceDataGridView.CellClick += AttandanceDataGridView_CellClick;
            attandanceDataGridView.Visible = false;
            salaryDatagridView.Visible = false;
            fillUserNameComboBox();
            dateComboBox.Enabled = false;
            reasonComboBox.Enabled = false;
            checkLeaveBtn.Enabled = false;
            leaveAvailabilityPanel.Visible = false;
            this.MouseClick += ManagerForm_MouseClick;
        }

        private void ManagerForm_MouseClick(object sender, MouseEventArgs e)
        {
            countDown = 0;
        }

        private void TimerCountDown_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (lockObjectInstant)
            {
                countDown++;
                if (countDown > limit)
                {
                    countDown = 0;
                    timerCountDown.Stop();
                    hide();
                }
            }
        }
        private void hide()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { hide(); }));
            }
            else
            {
                Hide();
            }
        }

        private void fillUserNameComboBox() {
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    userNameComboBox.Items.Add(dataReader.GetString("userName"));
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
            for(int i = 1; i<15; i++) {
                DateTime oneAfter = DateTime.Today.AddDays(i);
                dateComboBox.Items.Add(oneAfter.ToString("yyyy-MM-dd"));
            }
            reasonComboBox.Items.Add("Other less Important");
            reasonComboBox.Items.Add("Family Trip");
            reasonComboBox.Items.Add("Sick");
            reasonComboBox.Items.Add("Functions");
            reasonComboBox.Items.Add("Funeral");
            reasonComboBox.Items.Add("Other Most Important");

        }
        private void AttandanceDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                if(attandanceDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString() == "t") {
                    attandanceDataGridView.Rows[e.RowIndex].Cells[3].Value = "f";
                }
                else {
                    attandanceDataGridView.Rows[e.RowIndex].Cells[3].Value = "t";
                }
                
            }
        }

        public static ManagerForm getManagerForm() {
            if(managerForm == null) {
                managerForm = new ManagerForm();
            }
            return managerForm;
        }
        public void fillAttandancedataGridView()
        {
            if (attandanceDataGridView.Visible == true)
            {
                attandanceDataGridView.Rows.Clear();
                attandanceDataGridView.Visible = false;
            }
            else
            {
                attandanceDataGridView.Rows.Clear();
                MySqlConnection conn = Connection.getConnection();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)attandanceDataGridView.Rows[0].Clone();
                        row.Cells[0].Value = dataReader.GetString("userId");
                        row.Cells[1].Value = dataReader.GetString("userName");
                        row.Cells[2].Value = dataReader.GetString("todayOverTime");
                        row.Cells[3].Value = dataReader.GetString("presence");
                        attandanceDataGridView.Rows.Add(row);

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
                attandanceDataGridView.Refresh();
                attandanceDataGridView.Visible = true;
                employeeDataGridView.Visible = false;
                viewStockDataGridView.Visible = false;
                offerPanel.Visible = false;
                salaryDatagridView.Visible = false;
                leaveAvailabilityPanel.Visible = false;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (lockObject)
            {
                MySqlConnection conn = Connection.getExclusiveConnection();
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
                            //MessageBox.Show(sentenceArr[i]);
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
            
        }   

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void profilePanel_MouseClick(object sender, MouseEventArgs e)
        {

            performResetCountDown();
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
            performResetCountDown();
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
            performResetCountDown();
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void fillCatagoryComboBox()
        {
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                List<string> catagoryList = new List<string>();
                while (dataReader.Read())
                {
                    string s = dataReader.GetString("catagory");
                    if (!catagoryList.Contains(s))
                    {
                        catagoryComboBox.Items.Add(s);
                        catagoryList.Add(s);
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

        private void catagoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            brandComboBox.Text = "";
            nameComboBox.Text = "";
            weightComboBox.Text = "";
            brandComboBox.Items.Clear();
            nameComboBox.Items.Clear();
            weightComboBox.Items.Clear();
            MySqlConnection conn = Connection.getConnection();
            try
            {

                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + catagoryComboBox.Text + "'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                List<string> brandList = new List<string>();
                while (dataReader.Read())
                {
                    string s = dataReader.GetString("brand");
                    if (!brandList.Contains(s))
                    {
                        brandComboBox.Items.Add(s);
                        brandList.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager "+ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void brandComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            nameComboBox.Text = "";
            weightComboBox.Text = "";
            nameComboBox.Items.Clear();
            weightComboBox.Items.Clear();
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + catagoryComboBox.Text + "' and brand ='" + brandComboBox.Text + "'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                List<string> nameList = new List<string>();
                while (dataReader.Read())
                {
                    string s = dataReader.GetString("name");
                    if (!nameList.Contains(s))
                    {
                        nameComboBox.Items.Add(s);
                        nameList.Add(s);
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager "+ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void nameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            weightComboBox.Text = "";
            weightComboBox.Items.Clear();
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + catagoryComboBox.Text + "' and brand ='" + brandComboBox.Text + "' and name = '" + nameComboBox.Text + "'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                List<string> weightList = new List<string>();
                while (dataReader.Read())
                {

                    string s = dataReader.GetString("weight");
                    if (!weightList.Contains(s))
                    {
                        weightComboBox.Items.Add(s);
                        weightList.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager "+ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void weightComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + catagoryComboBox.Text + "' and brand ='" + brandComboBox.Text + "' and name = '" + nameComboBox.Text + "' and weight = '" + weightComboBox.Text + "'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    priceTxt.Text = dataReader.GetString("price");
                    price = dataReader.GetString("price");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager weight combo "+ex.Message);
            }
            finally
            {
                conn.Close();
            }
            dateTimePicker1.Enabled = true;
        }

        private void createOfferBtn_Click(object sender, EventArgs e)
        {
            if(!price.Equals(priceTxt.Text)) {
                string oldPrice = ""; 
                string dateTime = dateTimePicker1.Value.ToString("yyyyMMdd");
                MySqlConnection conn = Connection.getConnection();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + catagoryComboBox.Text + "' and brand ='" + brandComboBox.Text + "' and name = '" + nameComboBox.Text + "' and weight = '" + weightComboBox.Text + "'", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        oldPrice = dataReader.GetString("price");
                    }
                    MessageBox.Show("Offer Created...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at stock manager weight combo " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                conn = Connection.getConnection();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE shophope.stocks SET price = '"+priceTxt.Text+"' , offerPrice = '"+oldPrice+"', offerDate = '"+dateTime+"' WHERE catagory ='" + catagoryComboBox.Text + "' and brand ='" + brandComboBox.Text + "' and name = '" + nameComboBox.Text + "' and weight = '" + weightComboBox.Text + "'", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {

                    }
                    MessageBox.Show("Offer Created...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at stock manager weight combo "+ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                dateTimePicker1.Enabled = false;
                brandComboBox.Text = "";
                nameComboBox.Text = "";
                weightComboBox.Text = "";
                priceTxt.Text = "";
                createOfferBtn.Enabled = false;
                brandComboBox.Items.Clear();
                nameComboBox.Items.Clear();
                weightComboBox.Items.Clear();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Today;
            if (int.Parse(date.ToString("yyyyMMdd")) < int.Parse(dateTimePicker1.Value.ToString("yyyyMMdd")) && !price.Equals(priceTxt.Text))
            {
                createOfferBtn.Enabled = true;
            }
            else {
                createOfferBtn.Enabled = false;
            }
        }

        private void priceTxt_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in priceTxt.Text)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    priceTxt.Text = priceTxt.Text.Substring(0, priceTxt.Text.Length - 1);
                    priceTxt.SelectionStart = priceTxt.Text.Length;
                }

            }
        }
        public void createOffer() {
            if (offerPanel.Visible == true)
            {
                offerPanel.Visible = false;
            }
            else
            {
                offerPanel.Visible = true;
                viewStockDataGridView.Rows.Clear();
                viewStockDataGridView.Visible = false;
                employeeDataGridView.Visible = false;
                attandanceDataGridView.Visible = false;
                salaryDatagridView.Visible = false;
                leaveAvailabilityPanel.Visible = false;
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {
            createOffer();
            performResetCountDown();
        }

        private void createOfferPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void createOfferPanel_MouseClick(object sender, MouseEventArgs e)
        {
            createOffer(); 
            performResetCountDown();
        }
        public void fillViewStock() {
            if (viewStockDataGridView.Visible == true)
            {
                viewStockDataGridView.Rows.Clear();
                viewStockDataGridView.Visible = false;
            }
            else
            {
                viewStockDataGridView.Rows.Clear();
                MySqlConnection conn = Connection.getConnection();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)viewStockDataGridView.Rows[0].Clone();
                        row.Cells[0].Value = dataReader.GetString("stockId");
                        row.Cells[1].Value = dataReader.GetString("catagory");
                        row.Cells[2].Value = dataReader.GetString("brand");
                        row.Cells[3].Value = dataReader.GetString("name");
                        row.Cells[4].Value = dataReader.GetString("weight");
                        row.Cells[5].Value = dataReader.GetString("price");
                        row.Cells[6].Value = dataReader.GetString("quantity");
                        viewStockDataGridView.Rows.Add(row);

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
                viewStockDataGridView.Refresh();
                viewStockDataGridView.Visible = true;
                offerPanel.Visible = false;
                attandanceDataGridView.Visible = false;
                employeeDataGridView.Visible = false;
                leaveAvailabilityPanel.Visible = false;
                salaryDatagridView.Visible = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            fillViewStock();
            performResetCountDown();
        }

        private void viewStockPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void viewStockPanel_MouseClick(object sender, MouseEventArgs e)
        {
            fillViewStock();
            performResetCountDown();
        }

        public void createEmployeeDetail() {
            if (employeeDataGridView.Visible == true)
            {
                employeeDataGridView.Rows.Clear();
                employeeDataGridView.Visible = false;

            }
            else
            {
                MySqlConnection conn = Connection.getConnection();
                employeeDataGridView.Rows.Clear();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)viewStockDataGridView.Rows[0].Clone();
                        row.Cells[0].Value = dataReader.GetString("userId");
                        row.Cells[1].Value = dataReader.GetString("userName");
                        row.Cells[2].Value = dataReader.GetString("nIC");
                        row.Cells[3].Value = dataReader.GetString("emailAdress");
                        row.Cells[4].Value = dataReader.GetString("adress");
                        row.Cells[5].Value = dataReader.GetString("phoneNum");
                        row.Cells[6].Value = dataReader.GetString("post");
                        employeeDataGridView.Rows.Add(row);

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
                employeeDataGridView.Refresh();
                employeeDataGridView.Visible = true;
                viewStockDataGridView.Visible = false;
                offerPanel.Visible = false;
                attandanceDataGridView.Visible = false;
                leaveAvailabilityPanel.Visible = false;
                salaryDatagridView.Visible = false;
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {
            createEmployeeDetail();
            performResetCountDown();
        }

        private void employeePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void employeePanel_MouseClick(object sender, MouseEventArgs e)
        {
            createEmployeeDetail();
            performResetCountDown();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            fillAttandancedataGridView();
            performResetCountDown();
        }

        private void attandancePanel_MouseClick(object sender, MouseEventArgs e)
        {
            fillAttandancedataGridView();
            performResetCountDown();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            createSalaryDataGrid();
            performResetCountDown();
        }
        private void createSalaryDataGrid() {
            if (salaryDatagridView.Visible == true)
            {
                salaryDatagridView.Rows.Clear();
                salaryDatagridView.Visible = false;
            }
            else
            {
                salaryDatagridView.Rows.Clear();
                MySqlConnection conn = Connection.getConnection();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.employeetable", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DataGridViewRow row = (DataGridViewRow)salaryDatagridView.Rows[0].Clone();
                        row.Cells[0].Value = dataReader.GetString("userId");
                        row.Cells[1].Value = dataReader.GetString("userName");
                        row.Cells[2].Value = dataReader.GetString("salary");
                        salaryDatagridView.Rows.Add(row);

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
                salaryDatagridView.Refresh();
                attandanceDataGridView.Visible = false;
                viewStockDataGridView.Visible = false;
                offerPanel.Visible = false;
                employeeDataGridView.Visible = false;
                leaveAvailabilityPanel.Visible = false;
                salaryDatagridView.Visible = true;
            }
        }

        private void userNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            reasonComboBox.Enabled = true;
        }

        private void reasonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateComboBox.Enabled = true;
        }

        private void dateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkLeaveBtn.Enabled = true;

        }

        private void checkLeaveBtn_Click(object sender, EventArgs e)
        {
            int priority = reasonComboBox.SelectedIndex+1;
            int dayIndex = dateComboBox.SelectedIndex+1;
            MessageBox.Show(dayIndex.ToString());
            string person = "";
            MySqlConnection conn = Connection.getConnection();
            bool flag = false;
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.leave WHERE day = '"+dayIndex+"'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    person = dataReader.GetString("name");
                    if(int.Parse(dataReader.GetString("priority"))<priority) {
                        flag = true;
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
            if(flag) {
                if(person.Length>0) {
                    DialogResult dialog = MessageBox.Show(person+" claimed that day leave. Do you want to interchange leave?","",MessageBoxButtons.YesNo);
                    if(dialog == DialogResult.Yes) {
                        changePerson(priority,dayIndex);
                    }
                    
                }
                else {
                    changePerson(priority, dayIndex);
                    MessageBox.Show("Request Accepted");
                }
            }
            else {
                MessageBox.Show("Denied");
            }
        }
        public void changePerson(int priority, int dayIndex) {
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("UPDATE shophope.leave SET name = '"+userNameComboBox.Text+"',priority = '"+priority.ToString()+"'WHERE day = '"+dayIndex.ToString()+"'", conn);
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

        private void label6_Click(object sender, EventArgs e)
        {
            showLeaveAvailabilityPanel();
            performResetCountDown();
        }
        private void showLeaveAvailabilityPanel()
        {
            if (leaveAvailabilityPanel.Visible == true)
            {
                leaveAvailabilityPanel.Visible = false;
            }
            else
            {
                leaveAvailabilityPanel.Visible = true;
                attandanceDataGridView.Visible = false;
                viewStockDataGridView.Visible = false;
                offerPanel.Visible = false;
                employeeDataGridView.Visible = false;
                salaryDatagridView.Visible = false;
            }
        }

        private void leavePanel_MouseClick(object sender, MouseEventArgs e)
        {
            showLeaveAvailabilityPanel();
            performResetCountDown();
        }




        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    getManagerForm().Hide();
                    e.Cancel = true;
                    break;
            }
        }

        private void ManagerForm_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void ManagerForm_Activated(object sender, EventArgs e)
        {
            ////countDown = 0;
            ////if (!timerCountDown.Enabled)
            ////{
            ////    timerCountDown.Start();
            ////}
            ////MessageBox.Show("activated");
        }

        private void label11_Click(object sender, EventArgs e)
        {
            performLogout();
        }
        private void performResetCountDown() {
            countDown = 0;
        }
        private void logoutPanel_MouseClick(object sender, MouseEventArgs e)
        {
            performLogout();
        }
        private void performLogout() {
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    break;
                default:
                    Hide();
                    break;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            performResetCountDown();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            performResetCountDown();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            performResetCountDown();
        }

        private void reminderPanel_Click(object sender, EventArgs e)
        {
            performResetCountDown();
        }

        private void salarypanel_MouseClick(object sender, MouseEventArgs e)
        {
            createSalaryDataGrid();
            performResetCountDown();
        }

        private void privatePanel_MouseClick(object sender, MouseEventArgs e)
        {

            performResetCountDown();
        }

        private void messagePanel_MouseClick(object sender, MouseEventArgs e)
        {

            performResetCountDown();
        }

        private void panel15_MouseClick(object sender, MouseEventArgs e)
        {

            performResetCountDown();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            countDown = 0;
            if(button1.Text == "Stop Auto Logout") {
                limit = 10000;
                button1.Text = "Start Auto Logout";
            }
            else {
                button1.Text = "Stop Auto Logout";
                limit = 5;
            }
        }






















































        //private void yearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DateTime today = DateTime.Today;
        //    if (yearComboBox.Text.Equals(today.ToString("yyyy"))) {
        //        int month = int.Parse(today.ToString("MM"));
        //        for(int i = month; i<13; i++) {
        //            MonthComboBox.Items.Add(i);
        //        }
        //    }
        //    else {
        //        for (int i = 1; i < 13; i++)
        //        {
        //            MonthComboBox.Items.Add(i);
        //        }
        //    }
        //}

        //private void MonthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DateTime today = DateTime.Today;
        //    int month = int.Parse(today.ToString("MM"));
        //    if (MonthComboBox.Text.Equals(today.ToString("MM")))
        //    {
        //        int day = int.Parse(today.ToString("dd"));
        //        if (new int[] { 1,3,5,7,8,10,12}.Contains(month)) {
        //            for (int i = day; i < 32; i++)
        //            {
        //                MonthComboBox.Items.Add(i);
        //            }
        //        }
        //        else {
        //            for (int i = day; i < 31; i++)
        //            {
        //                MonthComboBox.Items.Add(i);
        //            }
        //        }

        //    }
        //    else
        //    {
        //        if (new int[] { 1, 3, 5, 7, 8, 10, 12 }.Contains(month))
        //        {
        //            for (int i = 1; i < 32; i++)
        //            {
        //                MonthComboBox.Items.Add(i);
        //            }
        //        }
        //        else
        //        {
        //            for (int i = 1; i < 31; i++)
        //            {
        //                MonthComboBox.Items.Add(i);
        //            }
        //        }
        //    }
        //}
    }
}
