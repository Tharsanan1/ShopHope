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

namespace ShopHope
{
    public partial class Manager : Form
    {
        static System.Timers.Timer timer = new System.Timers.Timer();
        static int count;
        static Object o = new object();
        static Random random = new Random();
        public Manager()
        {
            
            InitializeComponent();
            timer.Interval = 2000;
            timer.Elapsed += Timer_Elapsed;
            backGroundPanal.BackgroundImage = Properties.Resources.LogoShopHope;
            timer.Start();
            count = 0;
            
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //MessageBox.Show("");
            lock (o)
            {
                int x = random.Next(250, 1000);
                int y = random.Next(20, 500);
                changeLoation(backGroundPanal, x, y);
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


    }
}
