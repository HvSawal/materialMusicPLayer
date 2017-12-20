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
    public partial class Form4 : MaterialForm
    {
        String description;
        String score;
        String musicName;
        DataAccess da = new DataAccess();
        public Form4(String musicName)
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            this.musicName = musicName;
            
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if(materialRadioButton1.Checked)
            {
                score = "1";
                description = "bad";
            }

            else if (materialRadioButton2.Checked)
            {
                score = "2";
                description = "fine";
            }

            else if (materialRadioButton3.Checked)
            {
                score = "3";
                description = "average";
            }

            else if (materialRadioButton4.Checked)
            {
                score = "4";
                description = "good";
            }

            else if (materialRadioButton5.Checked)
            {
                score = "5";
                description = "excellent";
            }

            da.addRating("2", score, description, musicName);
            this.Close();
        }
    }
}
