namespace ShopHope
{
    partial class SalesManForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.catagoryComboBox = new System.Windows.Forms.ComboBox();
            this.stockIDComboBox = new System.Windows.Forms.ComboBox();
            this.weightComboBox = new System.Windows.Forms.ComboBox();
            this.nameComboBox = new System.Windows.Forms.ComboBox();
            this.brandComboBox = new System.Windows.Forms.ComboBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.billDataGridView = new System.Windows.Forms.DataGridView();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.onePriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberOfStocksForPurchase = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.returnBtn = new System.Windows.Forms.Button();
            this.returnPanel = new System.Windows.Forms.Panel();
            this.returnQuantityTxt = new System.Windows.Forms.TextBox();
            this.returnBrandComboBox = new System.Windows.Forms.ComboBox();
            this.returnNameComboBox = new System.Windows.Forms.ComboBox();
            this.returnWeightComboBox = new System.Windows.Forms.ComboBox();
            this.returnCatagoryComboBox = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lastBillBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.finishBtn = new System.Windows.Forms.Button();
            this.newBillBtn = new System.Windows.Forms.Button();
            this.returnedItemsFinishBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.billDataGridView)).BeginInit();
            this.returnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2200, 152);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Catagory";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1062, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Quantity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(856, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "StockID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(950, 411);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 17);
            this.label4.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(649, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Scale";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(444, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(235, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "Brand";
            // 
            // catagoryComboBox
            // 
            this.catagoryComboBox.FormattingEnabled = true;
            this.catagoryComboBox.Location = new System.Drawing.Point(32, 217);
            this.catagoryComboBox.Name = "catagoryComboBox";
            this.catagoryComboBox.Size = new System.Drawing.Size(149, 24);
            this.catagoryComboBox.TabIndex = 8;
            this.catagoryComboBox.SelectedIndexChanged += new System.EventHandler(this.catagoryComboBox_SelectedIndexChanged);
            // 
            // stockIDComboBox
            // 
            this.stockIDComboBox.FormattingEnabled = true;
            this.stockIDComboBox.Location = new System.Drawing.Point(859, 217);
            this.stockIDComboBox.Name = "stockIDComboBox";
            this.stockIDComboBox.Size = new System.Drawing.Size(149, 24);
            this.stockIDComboBox.TabIndex = 9;
            this.stockIDComboBox.SelectedIndexChanged += new System.EventHandler(this.stockIDComboBox_SelectedIndexChanged);
            // 
            // weightComboBox
            // 
            this.weightComboBox.FormattingEnabled = true;
            this.weightComboBox.Location = new System.Drawing.Point(652, 217);
            this.weightComboBox.Name = "weightComboBox";
            this.weightComboBox.Size = new System.Drawing.Size(149, 24);
            this.weightComboBox.TabIndex = 10;
            // 
            // nameComboBox
            // 
            this.nameComboBox.FormattingEnabled = true;
            this.nameComboBox.Location = new System.Drawing.Point(447, 217);
            this.nameComboBox.Name = "nameComboBox";
            this.nameComboBox.Size = new System.Drawing.Size(149, 24);
            this.nameComboBox.TabIndex = 11;
            this.nameComboBox.SelectedIndexChanged += new System.EventHandler(this.nameComboBox_SelectedIndexChanged);
            // 
            // brandComboBox
            // 
            this.brandComboBox.FormattingEnabled = true;
            this.brandComboBox.Location = new System.Drawing.Point(238, 217);
            this.brandComboBox.Name = "brandComboBox";
            this.brandComboBox.Size = new System.Drawing.Size(149, 24);
            this.brandComboBox.TabIndex = 12;
            this.brandComboBox.SelectedIndexChanged += new System.EventHandler(this.brandComboBox_SelectedIndexChanged);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(32, 281);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(564, 50);
            this.addBtn.TabIndex = 14;
            this.addBtn.Text = "ADD";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // billDataGridView
            // 
            this.billDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.billDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.billDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameColumn,
            this.quantityColumn,
            this.onePriceColumn,
            this.priceColumn});
            this.billDataGridView.Location = new System.Drawing.Point(1246, 185);
            this.billDataGridView.Name = "billDataGridView";
            this.billDataGridView.RowTemplate.Height = 24;
            this.billDataGridView.Size = new System.Drawing.Size(777, 709);
            this.billDataGridView.TabIndex = 15;
            this.billDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.billDataGridView_CellContentClick);
            // 
            // nameColumn
            // 
            this.nameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.Width = 240;
            // 
            // quantityColumn
            // 
            this.quantityColumn.HeaderText = "Qts";
            this.quantityColumn.Name = "quantityColumn";
            // 
            // onePriceColumn
            // 
            this.onePriceColumn.HeaderText = "Unit Price";
            this.onePriceColumn.Name = "onePriceColumn";
            // 
            // priceColumn
            // 
            this.priceColumn.HeaderText = "Price";
            this.priceColumn.Name = "priceColumn";
            // 
            // numberOfStocksForPurchase
            // 
            this.numberOfStocksForPurchase.Location = new System.Drawing.Point(1065, 217);
            this.numberOfStocksForPurchase.Name = "numberOfStocksForPurchase";
            this.numberOfStocksForPurchase.Size = new System.Drawing.Size(149, 22);
            this.numberOfStocksForPurchase.TabIndex = 16;
            this.numberOfStocksForPurchase.TextChanged += new System.EventHandler(this.numberOfStocksForPurchase_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(652, 281);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(562, 50);
            this.button2.TabIndex = 17;
            this.button2.Text = "CHECK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // returnBtn
            // 
            this.returnBtn.Location = new System.Drawing.Point(32, 352);
            this.returnBtn.Name = "returnBtn";
            this.returnBtn.Size = new System.Drawing.Size(75, 23);
            this.returnBtn.TabIndex = 18;
            this.returnBtn.Text = "Return";
            this.returnBtn.UseVisualStyleBackColor = true;
            this.returnBtn.Click += new System.EventHandler(this.returnBtn_Click);
            // 
            // returnPanel
            // 
            this.returnPanel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.returnPanel.Controls.Add(this.returnedItemsFinishBtn);
            this.returnPanel.Controls.Add(this.returnQuantityTxt);
            this.returnPanel.Controls.Add(this.returnBrandComboBox);
            this.returnPanel.Controls.Add(this.returnNameComboBox);
            this.returnPanel.Controls.Add(this.returnWeightComboBox);
            this.returnPanel.Controls.Add(this.returnCatagoryComboBox);
            this.returnPanel.Controls.Add(this.label17);
            this.returnPanel.Controls.Add(this.label16);
            this.returnPanel.Controls.Add(this.label15);
            this.returnPanel.Controls.Add(this.label14);
            this.returnPanel.Controls.Add(this.label13);
            this.returnPanel.Controls.Add(this.label12);
            this.returnPanel.Controls.Add(this.label11);
            this.returnPanel.Controls.Add(this.label10);
            this.returnPanel.Controls.Add(this.label9);
            this.returnPanel.Controls.Add(this.label8);
            this.returnPanel.Location = new System.Drawing.Point(32, 381);
            this.returnPanel.Name = "returnPanel";
            this.returnPanel.Size = new System.Drawing.Size(564, 513);
            this.returnPanel.TabIndex = 19;
            // 
            // returnQuantityTxt
            // 
            this.returnQuantityTxt.Location = new System.Drawing.Point(206, 206);
            this.returnQuantityTxt.Name = "returnQuantityTxt";
            this.returnQuantityTxt.Size = new System.Drawing.Size(149, 22);
            this.returnQuantityTxt.TabIndex = 21;
            this.returnQuantityTxt.TextChanged += new System.EventHandler(this.returnQuantityTxt_TextChanged);
            // 
            // returnBrandComboBox
            // 
            this.returnBrandComboBox.FormattingEnabled = true;
            this.returnBrandComboBox.Location = new System.Drawing.Point(206, 73);
            this.returnBrandComboBox.Name = "returnBrandComboBox";
            this.returnBrandComboBox.Size = new System.Drawing.Size(149, 24);
            this.returnBrandComboBox.TabIndex = 20;
            this.returnBrandComboBox.SelectedIndexChanged += new System.EventHandler(this.returnBrandComboBox_SelectedIndexChanged);
            // 
            // returnNameComboBox
            // 
            this.returnNameComboBox.FormattingEnabled = true;
            this.returnNameComboBox.Location = new System.Drawing.Point(206, 118);
            this.returnNameComboBox.Name = "returnNameComboBox";
            this.returnNameComboBox.Size = new System.Drawing.Size(149, 24);
            this.returnNameComboBox.TabIndex = 19;
            this.returnNameComboBox.SelectedIndexChanged += new System.EventHandler(this.returnNameComboBox_SelectedIndexChanged);
            // 
            // returnWeightComboBox
            // 
            this.returnWeightComboBox.FormattingEnabled = true;
            this.returnWeightComboBox.Location = new System.Drawing.Point(206, 162);
            this.returnWeightComboBox.Name = "returnWeightComboBox";
            this.returnWeightComboBox.Size = new System.Drawing.Size(149, 24);
            this.returnWeightComboBox.TabIndex = 18;
            this.returnWeightComboBox.SelectedIndexChanged += new System.EventHandler(this.returnWeightComboBox_SelectedIndexChanged);
            // 
            // returnCatagoryComboBox
            // 
            this.returnCatagoryComboBox.FormattingEnabled = true;
            this.returnCatagoryComboBox.Location = new System.Drawing.Point(206, 30);
            this.returnCatagoryComboBox.Name = "returnCatagoryComboBox";
            this.returnCatagoryComboBox.Size = new System.Drawing.Size(149, 24);
            this.returnCatagoryComboBox.TabIndex = 17;
            this.returnCatagoryComboBox.SelectedIndexChanged += new System.EventHandler(this.returnCatagoryComboBox_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(134, 70);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(15, 20);
            this.label17.TabIndex = 16;
            this.label17.Text = ":";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(134, 118);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 20);
            this.label16.TabIndex = 15;
            this.label16.Text = ":";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(134, 159);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 20);
            this.label15.TabIndex = 14;
            this.label15.Text = ":";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(134, 203);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 20);
            this.label14.TabIndex = 13;
            this.label14.Text = ":";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(134, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 20);
            this.label13.TabIndex = 12;
            this.label13.Text = ":";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 206);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 17);
            this.label12.TabIndex = 11;
            this.label12.Text = "Quantity";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 162);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 17);
            this.label11.TabIndex = 10;
            this.label11.Text = "Scale";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "Brand";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "Catagory";
            // 
            // lastBillBtn
            // 
            this.lastBillBtn.Location = new System.Drawing.Point(1246, 917);
            this.lastBillBtn.Name = "lastBillBtn";
            this.lastBillBtn.Size = new System.Drawing.Size(75, 23);
            this.lastBillBtn.TabIndex = 20;
            this.lastBillBtn.Text = "Last Bill";
            this.lastBillBtn.UseVisualStyleBackColor = true;
            this.lastBillBtn.Click += new System.EventHandler(this.lastBillBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(1605, 917);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteBtn.TabIndex = 21;
            this.deleteBtn.Text = "Delete";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // finishBtn
            // 
            this.finishBtn.Location = new System.Drawing.Point(1950, 917);
            this.finishBtn.Name = "finishBtn";
            this.finishBtn.Size = new System.Drawing.Size(75, 23);
            this.finishBtn.TabIndex = 22;
            this.finishBtn.Text = "Finish";
            this.finishBtn.UseVisualStyleBackColor = true;
            this.finishBtn.Click += new System.EventHandler(this.finishBtn_Click);
            // 
            // newBillBtn
            // 
            this.newBillBtn.Location = new System.Drawing.Point(1139, 352);
            this.newBillBtn.Name = "newBillBtn";
            this.newBillBtn.Size = new System.Drawing.Size(75, 23);
            this.newBillBtn.TabIndex = 23;
            this.newBillBtn.Text = "New Bill";
            this.newBillBtn.UseVisualStyleBackColor = true;
            this.newBillBtn.Click += new System.EventHandler(this.newBillBtn_Click);
            // 
            // returnedItemsFinishBtn
            // 
            this.returnedItemsFinishBtn.Location = new System.Drawing.Point(206, 265);
            this.returnedItemsFinishBtn.Name = "returnedItemsFinishBtn";
            this.returnedItemsFinishBtn.Size = new System.Drawing.Size(149, 23);
            this.returnedItemsFinishBtn.TabIndex = 22;
            this.returnedItemsFinishBtn.Text = "Finish";
            this.returnedItemsFinishBtn.UseVisualStyleBackColor = true;
            this.returnedItemsFinishBtn.Click += new System.EventHandler(this.returnedItemsFinishBtn_Click);
            // 
            // SalesManForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1906, 1037);
            this.Controls.Add(this.newBillBtn);
            this.Controls.Add(this.finishBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.lastBillBtn);
            this.Controls.Add(this.returnPanel);
            this.Controls.Add(this.returnBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.numberOfStocksForPurchase);
            this.Controls.Add(this.billDataGridView);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.brandComboBox);
            this.Controls.Add(this.nameComboBox);
            this.Controls.Add(this.weightComboBox);
            this.Controls.Add(this.stockIDComboBox);
            this.Controls.Add(this.catagoryComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "SalesManForm";
            this.Text = "SalesManForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SalesManForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.billDataGridView)).EndInit();
            this.returnPanel.ResumeLayout(false);
            this.returnPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox catagoryComboBox;
        private System.Windows.Forms.ComboBox stockIDComboBox;
        private System.Windows.Forms.ComboBox weightComboBox;
        private System.Windows.Forms.ComboBox nameComboBox;
        private System.Windows.Forms.ComboBox brandComboBox;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.DataGridView billDataGridView;
        private System.Windows.Forms.TextBox numberOfStocksForPurchase;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn onePriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceColumn;
        private System.Windows.Forms.Button returnBtn;
        private System.Windows.Forms.Panel returnPanel;
        private System.Windows.Forms.TextBox returnQuantityTxt;
        private System.Windows.Forms.ComboBox returnBrandComboBox;
        private System.Windows.Forms.ComboBox returnNameComboBox;
        private System.Windows.Forms.ComboBox returnWeightComboBox;
        private System.Windows.Forms.ComboBox returnCatagoryComboBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button lastBillBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button finishBtn;
        private System.Windows.Forms.Button newBillBtn;
        private System.Windows.Forms.Button returnedItemsFinishBtn;
    }
}