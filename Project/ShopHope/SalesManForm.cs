using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ShopHope
{
    public partial class SalesManForm : Form
    {
        public SalesManForm()
        {
            InitializeComponent();
            returnPanel.Visible = false;
            fillStockIDComboBox();
            fillCatagoryComboBox();
        }

        private void SalesManForm_Load(object sender, EventArgs e)
        {

        }
        public void fillStockIDComboBox()
        {
            try
            {
                MySqlConnection conn = Connection.getConnection();
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string s = dataReader.GetString("stockID");
                    stockIDComboBox.Items.Add(s);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager");
            }
        }
        public void fillCatagoryComboBox()
        {
            try
            {

                MySqlConnection conn = Connection.getConnection();
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
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager    " + ex.Message);
            }
        }
        public void fillBrandComboBox()
        {

        }
        public void fillNameComboBox()
        {

        }
        public void fillWeightComboBox()
        {

        }

        private void catagoryComboBox_Click(object sender, EventArgs e)
        {

        }

        

        

        

        //private void weightComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        MySqlConnection conn = Connection.getConnection();
        //        conn.Open();
        //        MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + catagoryComboBox.Text + "' and brand ='" + brandComboBox.Text + "' and name = '" + nameComboBox.Text + "' and weight = '" + weightComboBox.Text + "'", conn);
        //        MySqlDataReader dataReader = command.ExecuteReader();
        //        while (dataReader.Read())
        //        {
        //            priceTxt.Text = dataReader.GetString("price");
        //            quantityTxt.Text = dataReader.GetString("quantity");
        //            expieryTxt.Text = dataReader.GetString("expirydate");
        //            warningLevelTxt.Text = dataReader.GetString("warningLevel");
        //            stockIDLbl.Text = dataReader.GetString("stockID");
        //        }
        //        conn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("error occured at stock manager weight combo");
        //    }
        //}

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            catagoryComboBox.Items.Clear();
            brandComboBox.Items.Clear();
            nameComboBox.Items.Clear();
            weightComboBox.Items.Clear();
            stockIDComboBox.Items.Clear();
            fillCatagoryComboBox();
            fillStockIDComboBox();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                MySqlConnection conn = Connection.getConnection();
                conn.Open();
                MySqlCommand command;
                if (stockIDComboBox.Text.Length > 0)
                {
                    command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE stockID = '" + stockIDComboBox.Text + "'", conn);
                }
                else
                {
                    command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory = '" + catagoryComboBox.Text + "' and brand = '" + brandComboBox.Text + "' and name = '" + nameComboBox.Text + "' and weight = '" + weightComboBox.Text + "'", conn);
                }
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string s = dataReader.GetString("price");
                    DataGridViewRow row = (DataGridViewRow)billDataGridView.Rows[0].Clone();
                    row.Cells[0].Value = nameComboBox.Text;
                    row.Cells[1].Value = numberOfStocksForPurchase.Text;
                    row.Cells[2].Value = s;
                    row.Cells[3].Value = (int.Parse(s) * int.Parse(numberOfStocksForPurchase.Text)).ToString();
                    billDataGridView.Rows.Add(row);
                    catagoryComboBox.Text = "";
                }
               
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = Connection.getConnection();
                conn.Open();
                MySqlCommand command;
                if (stockIDComboBox.Text.Length > 0)
                {
                    command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE stockID = '" + stockIDComboBox.Text + "'", conn);
                }
                else {
                    command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory = '"+catagoryComboBox.Text+"' and brand = '"+brandComboBox.Text+"' and name = '"+nameComboBox.Text+"' and weight = '"+weightComboBox.Text+"'", conn);
                }
                MySqlDataReader dataReader = command.ExecuteReader();
                bool flag = false;
                while (dataReader.Read())
                {
                    MessageBox.Show("Available stock quantity is " + dataReader.GetString("quantity"));
                    flag = true;
                }
                if(!flag) {
                    MessageBox.Show("No stocks available.");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager");
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
            try
            {
                MySqlConnection conn = Connection.getConnection();
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
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager");
            }
        
    }

        private void brandComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            nameComboBox.Text = "";
            weightComboBox.Text = "";
            nameComboBox.Items.Clear();
            weightComboBox.Items.Clear();
            try
            {
                MySqlConnection conn = Connection.getConnection();
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
                Console.WriteLine("error occured at stock manager");
            }

        
    }

        private void nameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            weightComboBox.Text = "";
            weightComboBox.Items.Clear();
            try
            {
                MySqlConnection conn = Connection.getConnection();
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
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager");
            }
        
    }

        private void stockIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            try
            {
                MySqlConnection conn = Connection.getConnection();
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE stockID = '" + stockIDComboBox.Text + "'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())

                {


                    catagoryComboBox.Items.Clear();
                    catagoryComboBox.Items.Add(dataReader.GetString("catagory"));
                    catagoryComboBox.Text = dataReader.GetString("catagory");
                    brandComboBox.Items.Clear();
                    brandComboBox.Items.Add(dataReader.GetString("brand"));
                    brandComboBox.Text = dataReader.GetString("brand");
                    nameComboBox.Items.Clear();
                    nameComboBox.Items.Add(dataReader.GetString("name"));
                    nameComboBox.Text = dataReader.GetString("name");
                    weightComboBox.Items.Clear();
                    weightComboBox.Items.Add(dataReader.GetString("weight"));
                    weightComboBox.Text = dataReader.GetString("weight");

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager weight combo");
            }
        
    }

        private void billDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void numberOfStocksForPurchase_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in numberOfStocksForPurchase.Text)
            {
                if (!char.IsDigit(c))
                {
                    numberOfStocksForPurchase.Text = numberOfStocksForPurchase.Text.Substring(0, numberOfStocksForPurchase.Text.Length - 1);
                    numberOfStocksForPurchase.SelectionStart = numberOfStocksForPurchase.Text.Length; // add some logic if length is 0
                    //numberOfStocksForPurchase.SelectionLength = 0;
                }

            }
        }

        private void returnBtn_Click(object sender, EventArgs e)
        {
            Point point1 = new Point(32, 352);
            Point point2 = new Point(100,916);
            if (returnPanel.Visible==false) {
                //returnBtn.Location = point1;
                returnPanel.Visible = true;
                returnBtn.Visible = true;
            }
            else {
                //do the return part.
                //returnBtn.Location = point2;
                returnPanel.Visible = false;
                returnBtn.Visible = true;
            }
        }

        private void returnCatagoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            brandComboBox.Text = "";
            nameComboBox.Text = "";
            weightComboBox.Text = "";
            brandComboBox.Items.Clear();
            nameComboBox.Items.Clear();
            weightComboBox.Items.Clear();
            try
            {
                MySqlConnection conn = Connection.getConnection();
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
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager");
            }
        }

        private void returnBrandComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            nameComboBox.Text = "";
            weightComboBox.Text = "";
            nameComboBox.Items.Clear();
            weightComboBox.Items.Clear();
            try
            {
                MySqlConnection conn = Connection.getConnection();
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
                Console.WriteLine("error occured at stock manager");
            }
        }

        private void returnNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            weightComboBox.Text = "";
            weightComboBox.Items.Clear();
            try
            {
                MySqlConnection conn = Connection.getConnection();
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
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager");
            }
        }

        private void returnQuantityTxt_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in numberOfStocksForPurchase.Text)
            {
                if (!char.IsDigit(c))
                {
                    numberOfStocksForPurchase.Text = numberOfStocksForPurchase.Text.Substring(0, numberOfStocksForPurchase.Text.Length - 1);
                    numberOfStocksForPurchase.SelectionStart = numberOfStocksForPurchase.Text.Length;
                    
                }

            }
        }
    }

}

