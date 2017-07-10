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
    public partial class StockManagerForm : Form
    {
        static List<StockManagerForm> stockManagerFormList = new List<StockManagerForm>();
        static List<string> formPointer = new List<string>();
        static object lockObject = new object();
        private StockManagerForm(string name)
        {
            InitializeComponent();
            newStockPanel.Visible = false;
            fillCatagoryComboBox();
            fillStockIDComboBox();
            nameLbl.Text = name;
        }
        public static StockManagerForm getStockManagerForm(string name) {
            lock (lockObject) {
                if (stockManagerFormList.Count == 0) 
                {
                    stockManagerFormList.Add(new StockManagerForm(name.ToUpper()));
                    formPointer.Add(name);
                    return stockManagerFormList[0];
                }
                else {
                    if (formPointer.Contains(name)) 
                    {
                        return stockManagerFormList[formPointer.IndexOf(name.ToUpper())];
                    }
                    else 
                    {
                        stockManagerFormList.Add(new StockManagerForm(name.ToUpper()));
                        formPointer.Add(name);
                        return stockManagerFormList[stockManagerFormList.Count-1];
                    }
                }
            }   
        }
        private void StockManager_Load(object sender, EventArgs e)
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
                Console.WriteLine("error occured at stock manager");
            }
            finally {
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
            catch(Exception ex){
                Console.WriteLine("error occured at stock manager    "+ex.Message);
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
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='"+catagoryComboBox.Text+"'", conn);
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
                Console.WriteLine("error occured at stock manager");
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
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + catagoryComboBox.Text + "' and brand ='"+brandComboBox.Text+"'", conn);
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
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + catagoryComboBox.Text + "' and brand ='" + brandComboBox.Text + "' and name = '"+nameComboBox.Text+"'", conn);
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
                Console.WriteLine("error occured at stock manager");
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
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + catagoryComboBox.Text + "' and brand ='" + brandComboBox.Text + "' and name = '" + nameComboBox.Text + "' and weight = '"+weightComboBox.Text+"'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    priceTxt.Text = dataReader.GetString("price");
                    quantityTxt.Text = dataReader.GetString("quantity");
                    expieryTxt.Text = dataReader.GetString("expirydate");
                    warningLevelTxt.Text = dataReader.GetString("warningLevel");
                    stockIDLbl.Text = dataReader.GetString("stockID");
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

        private void stockIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE stockID = '"+stockIDComboBox.Text+"'", conn);
                stockIDLbl.Text = stockIDComboBox.Text;
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                
                {
                    priceTxt.Text = dataReader.GetString("price");
                    quantityTxt.Text = dataReader.GetString("quantity");
                    expieryTxt.Text = dataReader.GetString("expirydate");
                    warningLevelTxt.Text = dataReader.GetString("warningLevel");
                    
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
                Console.WriteLine("error occured at stock manager weight combo");
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double oldPrice = 0;
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE stockID = '"+stockIDLbl.Text+"'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    oldPrice = double.Parse(dataReader.GetString("price"));
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager change btn in stocks " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            if (!oldPrice.ToString().Equals(priceTxt.Text)) {
                MessageBox.Show("Notified");
                Mediator.notifyManager("Stock " + nameComboBox.Text + " changed from " + oldPrice.ToString() + " to " + priceTxt.Text);
            }
            
            try
            {
                conn = Connection.getConnection();
                conn.Open();
                string temp = int.Parse(quantityTxt.Text).ToString();
                if (addQuantityTxt.Text.Length!=0) {
                    temp = (int.Parse(addQuantityTxt.Text) + int.Parse(quantityTxt.Text)).ToString();
                }
                MySqlCommand command = new MySqlCommand("UPDATE shophope.stocks SET price ='"+priceTxt.Text+"',quantity = '"+ temp + "',expirydate = '"+expieryTxt.Text+"',warningLevel='"+warningLevelTxt.Text+"' WHERE stockID = '"+stockIDLbl.Text+"'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured at stock manager change btn "+ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void addQuantityBtn_Click(object sender, EventArgs e)
        {
            if (addQuantityTxt.Text.Length == 0)
            {
                addQuantityTxt.Text = "1";
            }
            else
            {
                addQuantityTxt.Text = (Int32.Parse(addQuantityTxt.Text) + 1).ToString();
            }
        }

        private void addNewInPanelBtn_Click(object sender, EventArgs e)
        {
            if(!(newCatagoryTxt.Text.Length>0 && newBrandTxt.Text.Length>0 && newNameTxt.Text.Length>0 && newWeightTxt.Text.Length>0)) {
                MessageBox.Show("Fill Boxes...");
                return;
            }
            MySqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM shophope.stocks WHERE catagory ='" + newCatagoryTxt.Text + "' and brand ='" + newBrandTxt.Text + "' and name = '" + newNameTxt.Text + "' and weight = '" + newWeightTxt.Text + "'", conn);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    MessageBox.Show("Already item exist!");
                    conn.Close();
                    return;
                }
                command = new MySqlCommand("INSERT INTO shophope.stocks (catagory,brand,name,weight,price,quantity,warningLevel,expirydate) VALUES ('"+newCatagoryTxt.Text+"','"+newBrandTxt.Text+"','"+newNameTxt.Text+"','"+newWeightTxt.Text+"','"+newPriceTxt.Text+"','"+newQuantityTxt.Text+"','"+newWarningLevelTxt.Text+"','"+newExpiryTxt.Text+"')",conn);
                conn.Close();
                conn = Connection.getConnection();
                conn.Open();
                dataReader = command.ExecuteReader();
                while(dataReader.Read()) {
                    
                }
            }
            catch (Exception ex)
            {
            
                Console.WriteLine("error occured at stock manager new add : "+ex.Message);
            }
            finally
            {
                conn.Close();
            }
            MessageBox.Show("Added");
            newStockPanel.Visible = false;
        }

        private void newWeightTxt_TextChanged(object sender, EventArgs e)
        {
            foreach(char c in newWeightTxt.Text) {
                if(!char.IsDigit(c) && c!='.') {
                    newWeightTxt.Text = newWeightTxt.Text.Substring(0,newWeightTxt.Text.Length-1);
                    newWeightTxt.SelectionStart = newWeightTxt.Text.Length;
                }

            }
        }

        private void newPriceTxt_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in newPriceTxt.Text)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    newPriceTxt.Text = newPriceTxt.Text.Substring(0, newPriceTxt.Text.Length - 1);
                    newPriceTxt.SelectionStart = newPriceTxt.Text.Length;
                }

            }
        }

        private void newQuantityTxt_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in newQuantityTxt.Text)
            {
                if (!char.IsDigit(c))
                {
                    newQuantityTxt.Text = newQuantityTxt.Text.Substring(0, newQuantityTxt.Text.Length - 1);
                    newPriceTxt.SelectionStart = newPriceTxt.Text.Length;
                }

            }
        }

        private void newWarningLevelTxt_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in newWarningLevelTxt.Text)
            {
                if (!char.IsDigit(c) )
                {
                    newWarningLevelTxt.Text = newWarningLevelTxt.Text.Substring(0, newWarningLevelTxt.Text.Length - 1);
                }

            }
        }

        private void addNewBtn_Click(object sender, EventArgs e)
        {
            if(newStockPanel.Visible == false) { 
                newStockPanel.Visible = true; 
            }
            else { 
                newStockPanel.Visible = false; 
            }
            
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void priceTxt_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
