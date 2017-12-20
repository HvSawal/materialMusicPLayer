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
    public partial class Form2 : MaterialForm
    {
        DataAccess da = new DataAccess();
        
        public Form2()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if(materialTextField1.Text.Equals(""))
            {
                MessageBox.Show("Cannot have NoName Playlist!");
                this.Close();
            }
            else
            {
                String playlistName = materialTextField1.Text;
                da.createPlaylist("2", playlistName);
                this.Close();
            }
        }
    }
}
