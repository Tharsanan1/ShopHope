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
        static List<SalesManForm> salesManFormList = new List<SalesManForm>();
        static List<string> formPointer = new List<string>();
        static object lockObject = new object();
        double totalPrice;
        string returnedId;
        List<string> stockIdList;
        List<string> quantityList;
        private SalesManForm(string name)
        {
            InitializeComponent();
            returnPanel.Visible = false;
            fillStockIDComboBox();
            fillCatagoryComboBox();
            fillReturnCatagoryComboBox();
            totalPrice = 0;
            returnedId = "";
            stockIdList = new List<string>();
            quantityList = new List<string>();
        }
        public static SalesManForm getsalesManform(string name)
        {
            lock (lockObject)
            {
                if (salesManFormList.Count == 0)
                {
                    salesManFormList.Add(new SalesManForm(name.ToUpper()));
                    formPointer.Add(name);
                    return salesManFormList[0];
                }
                else
                {
                    if (formPointer.Contains(name))
                    {
                        return salesManFormList[formPointer.IndexOf(name)];
                    }
                    else
                    {
                        salesManFormList.Add(new SalesManForm(name.ToUpper()));
                        formPointer.Add(name);
                        return salesManFormList[salesManFormList.Count - 1];
                    }
                }
            }
        }

        private void SalesManForm_Load(object sender, EventArgs e)
        {

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
                    Hide();
                    e.Cancel = true;
                    break;
            }
        }
        
        public void fillStockIDComboBox()
        {
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string s = dataReader.GetString("stockID");
                    stockIDComboBox.Items.Add(s);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
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
        
        public void fillReturnCatagoryComboBox()
        {
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                List<string> returnCatagoryList = new List<string>();
                while (dataReader.Read())
                {
                    string s = dataReader.GetString("catagory");
                    if (!returnCatagoryList.Contains(s))
                    {
                        returnCatagoryComboBox.Items.Add(s);
                        returnCatagoryList.Add(s);
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



            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command;
                command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory = '" + catagoryComboBox.Text + "' and brand = '" + brandComboBox.Text + "' and name = '" + nameComboBox.Text + "' and weight = '" + weightComboBox.Text + "'", conn);

                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (stockIdList.Contains(dataReader.GetString("stockId")))
                    {
                        MessageBox.Show("Already this kind of product you can edit there!");
                        return;
                    }
                    if (numberOfStocksForPurchase.Text.Length == 0)
                    {
                        numberOfStocksForPurchase.Text = "1";
                    }
                    string s = dataReader.GetString("price");
                    DataGridViewRow row = (DataGridViewRow)billDataGridView.Rows[0].Clone();
                    row.Cells[0].Value = nameComboBox.Text;
                    row.Cells[1].Value = numberOfStocksForPurchase.Text;
                    row.Cells[2].Value = s;
                    string temp = s;
                    if (numberOfStocksForPurchase.Text.Length > 0)
                    {
                        temp = (double.Parse(s) * double.Parse(numberOfStocksForPurchase.Text)).ToString();
                    }
                    totalPrice += double.Parse(temp);
                    row.Cells[3].Value = temp;
                    billDataGridView.Rows.Add(row);
                    catagoryComboBox.Text = "";
                    stockIdList.Add(dataReader.GetString("stockId"));
                    quantityList.Add(dataReader.GetString("quantity"));
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

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = Connection.getConnection();
            try
            {

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
                bool flag = false;
                while (dataReader.Read())
                {
                    MessageBox.Show("Available stock quantity is " + dataReader.GetString("quantity"));
                    flag = true;
                }
                if (!flag)
                {
                    MessageBox.Show("No stocks available.");
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
            conn = Connection.getConnection();
            string updatingString = "";
            try
            {

                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.system WHERE id = '1'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    updatingString = dataReader.GetString("isStocksUpdating");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager");
            }
            finally
            {
                conn.Close();
            }
            if (updatingString.Equals("true"))
            {
                MessageBox.Show("Stocks are in updating mode.");
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
                Console.WriteLine("error occured at stock manager: " + ex.Message);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager:  " + ex.Message);
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
                Console.WriteLine("error occured at stock manager:  " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void stockIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = Connection.getConnection();
            try
            {

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
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager:  " + ex.Message);
            }
            finally
            {
                conn.Close();
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

            if (returnPanel.Visible == false)
            {
                returnPanel.Visible = true;
            }
            else
            {
                returnPanel.Visible = false;
                Mediator.notifyManager(returnNameComboBox.Text + " has returned.");
            }
        }

        private void returnCatagoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            returnBrandComboBox.Text = "";
            returnNameComboBox.Text = "";
            returnWeightComboBox.Text = "";
            returnBrandComboBox.Items.Clear();
            returnNameComboBox.Items.Clear();
            returnWeightComboBox.Items.Clear();
            MySqlConnection conn = Connection.getConnection();
            try
            {

                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + returnCatagoryComboBox.Text + "'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string s = dataReader.GetString("brand");
                    returnBrandComboBox.Items.Add(s);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager:  " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void returnBrandComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            returnNameComboBox.Text = "";
            returnWeightComboBox.Text = "";
            returnNameComboBox.Items.Clear();
            returnWeightComboBox.Items.Clear();
            MySqlConnection conn = Connection.getConnection();
            try
            {

                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + returnCatagoryComboBox.Text + "' and brand ='" + returnBrandComboBox.Text + "'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string s = dataReader.GetString("name");
                    returnNameComboBox.Items.Add(s);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager");
            }
            finally
            {
                conn.Close();
            }
        }

        private void returnNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            returnWeightComboBox.Text = "";
            returnWeightComboBox.Items.Clear();
            MySqlConnection conn = Connection.getConnection();
            try
            {

                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + returnCatagoryComboBox.Text + "' and brand ='" + returnBrandComboBox.Text + "' and name = '" + returnNameComboBox.Text + "'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {

                    string s = dataReader.GetString("weight");
                    returnWeightComboBox.Items.Add(s);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager:  " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void returnQuantityTxt_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in returnQuantityTxt.Text)
            {
                if (!char.IsDigit(c))
                {
                    returnQuantityTxt.Text = returnQuantityTxt.Text.Substring(0, returnQuantityTxt.Text.Length - 1);
                    returnQuantityTxt.SelectionStart = returnQuantityTxt.Text.Length;

                }

            }
        }

        private void lastBillBtn_Click(object sender, EventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = billDataGridView.CurrentCell.RowIndex;
                MessageBox.Show(selectedRow.ToString());
                totalPrice -= double.Parse(billDataGridView.Rows[selectedRow].Cells[3].Value.ToString());
                stockIdList.RemoveAt(selectedRow);
                billDataGridView.Rows.RemoveAt(selectedRow);
                quantityList.RemoveAt(selectedRow);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void finishBtn_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)billDataGridView.Rows[0].Clone();
            row.Cells[0].Value = "Total";
            row.Cells[3].Value = totalPrice.ToString();
            billDataGridView.Rows.Add(row);
            addBtn.Enabled = false;
            deleteBtn.Enabled = false;
            for (int i = 0; i < stockIdList.Count; i++)
            {
                MySqlConnection conn = Connection.getConnection();
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE shophope.stocks SET quantity = '" + (int.Parse(quantityList[i]) - int.Parse(billDataGridView.Rows[i].Cells[1].Value.ToString())).ToString() + "' WHERE stockId = '" + stockIdList[i] + "'", conn);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error occured at stock manager weight combo" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            finishBtn.Enabled = false;
            stockIdList.Clear();
            quantityList.Clear();
        }

        private void newBillBtn_Click(object sender, EventArgs e)
        {
            billDataGridView.Rows.Clear();
            billDataGridView.Refresh();
            addBtn.Enabled = true;
            deleteBtn.Enabled = true;
            finishBtn.Enabled = true;
            stockIdList.Clear();
            quantityList.Clear();
        }

        private void returnWeightComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = Connection.getConnection();
            try
            {

                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + returnCatagoryComboBox.Text + "' and brand ='" + returnBrandComboBox.Text + "' and name = '" + returnNameComboBox.Text + "' and weight = '" + returnWeightComboBox.Text + "'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    returnedId = dataReader.GetString("stockID");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager weight combo");
            }
            finally
            {
                conn.Close();
            }
        }

        private void returnedItemsFinishBtn_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = Connection.getConnection();
            bool flag = false;
            try
            {

                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + returnCatagoryComboBox.Text + "' and brand ='" + returnBrandComboBox.Text + "' and name = '" + returnNameComboBox.Text + "' and weight = '" + returnWeightComboBox.Text + "'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    returnedId = dataReader.GetString("stockID");
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager weight combo");
            }
            finally
            {
                conn.Close();
            }
            if (returnQuantityTxt.Text.Length > 0 && flag)
            {
                Mediator.notifyManager("/stock " + returnedId + " returned. quantity= " + returnQuantityTxt.Text);
                MessageBox.Show("Notified");
            }
            else {
                MessageBox.Show("Wrong Product");
            }
        }
    }
}

