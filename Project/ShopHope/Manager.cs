using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopHope
{
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
        }

        private void Manager_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("x "+e.X+"y "+e.Y);
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
    }
}
