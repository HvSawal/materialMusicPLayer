using MaterialSkin.Controls;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EP
{
    public partial class Form5 : MaterialForm
    {
        public Form5(String musicUrl)
        {
            InitializeComponent();
            materialLabel1.ForeColor = Color.White;
            axWindowsMediaPlayer1.URL = musicUrl;
        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Close();
            f1.Show();
        }
    }
}
