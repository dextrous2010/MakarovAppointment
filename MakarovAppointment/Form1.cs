using MakarovAppointment.Site;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakarovAppointment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLaunch_Click(object sender, EventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.DoLogin("0680420918", "darianna");
            mainPage.MakeAnAppointment();
        }
    }
}
