using MaterialSkin;
using MaterialSkin.Controls;
using Oracle.DataAccess.Client;
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
    public partial class Form1 : MaterialForm
    {
        string[] fileNames, pathNames, f, p;
        int count = 0, midCount = 5;
        DataAccess da = new DataAccess();
        OracleDataReader musicRecords;
        OracleDataReader playlistRecords;
        OracleDataReader moodRecords;
        OracleDataReader artistRecords;
        public Form1()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue800, Primary.Blue900, Primary.BlueGrey500, Accent.Red400, TextShade.WHITE);
            tabPage1.Text = "All Songs";
            tabPage2.Text = "Moods";
            tabPage3.Text = "Playlists";
            tabPage4.Text = "Artists";
            tabPage5.Text = "Themes";
            materialLabel3.ForeColor = Color.White;

            musicRecords = da.getMusic();

            while(musicRecords.Read())
            {
                listBox1.Items.Add(musicRecords["m_name"].ToString());
            }

            playlistRecords = da.getPlaylists();
            
            while (playlistRecords.Read())
            {
                listBox2.Items.Add(playlistRecords["p_name"].ToString());
            }

            moodRecords = da.getMoods();

            while (moodRecords.Read())
            {
                listBox6.Items.Add(moodRecords["mo_name"].ToString());
            }

            artistRecords = da.getArtists();

            while(artistRecords.Read())
            {
                listBox4.Items.Add(artistRecords["a_name"].ToString());
            }
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileNames = openFileDialog1.SafeFileNames;
                pathNames = openFileDialog1.FileNames;

                for(int i = 0; i < fileNames.Length; i++)
                {
                    da.addMusic(midCount, fileNames[i], pathNames[i], 1, 1, "Nirvana", "Rock");
                    midCount++;
                }
                
                musicRecords = da.getMusic();
                listBox1.Items.Clear();
                while (musicRecords.Read())
                {
                    listBox1.Items.Add(musicRecords["m_name"].ToString());
                }
                fileNames = null;
                pathNames = null;
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            musicRecords = da.getMusic();
            while (musicRecords.Read())
            {
                if(musicRecords["m_name"].ToString().Equals(listBox1.SelectedItem.ToString()))
                {
                    mediaPlayer.URL = musicRecords["m_path"].ToString();
                }
            }
        }

        /*private void materialLabel1_Click(object sender, EventArgs e)
        {
            TabPage t = materialTabControl1.TabPages[4];
            materialTabControl1.SelectedTab = t;
        }*/

        private void materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Blue200, TextShade.WHITE);
        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue800, Primary.Blue900, Primary.Blue200, Accent.Red400, TextShade.WHITE);
        }

        private void materialRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Red400, TextShade.WHITE);
            
        }

        private void materialRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Teal700, Primary.Teal800, Primary.BlueGrey500, Accent.Orange400, TextShade.WHITE);
            
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //listBox3.Items.Add("");
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            if(listBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select the playlist first!");
            }
            else
            {
                Form3 f3 = new Form3(listBox2.SelectedItem.ToString());
                f3.Show();
            }
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Select a song to rate!");
            }
            else
            {
                Form4 f4 = new Form4(listBox1.SelectedItem.ToString());
                f4.Show();
            }
        }

        private void materialLabel3_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5(mediaPlayer.URL);
            mediaPlayer.URL = null;
            f5.Show();
            this.Hide();
        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleDataReader moodMusic = da.getMusicByMood(listBox6.SelectedItem.ToString());
            listBox7.Items.Clear();
            while(moodMusic.Read())
            {
                listBox7.Items.Add(moodMusic["m_name"].ToString());
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleDataReader playlistMusic = da.getMusicByPlaylist(listBox2.SelectedItem.ToString());
            listBox3.Items.Clear();
            while (playlistMusic.Read())
            {
                listBox3.Items.Add(playlistMusic["m_name"].ToString());
            }
        }

        private void listBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            musicRecords = da.getMusic();
            while(musicRecords.Read())
            {
                if(musicRecords["m_name"].ToString().Equals(listBox3.SelectedItem.ToString()))
                {
                    mediaPlayer.URL = musicRecords["m_path"].ToString();
                    break;
                }
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox5.Items.Clear();
            artistRecords = da.getArtists();
            while (artistRecords.Read())
            {
                if (artistRecords["a_name"].ToString().Equals(listBox4.SelectedItem.ToString()))
                {
                    listBox5.Items.Add(artistRecords["a_age"].ToString());
                    listBox5.Items.Add(artistRecords["a_phno"].ToString());
                }
            }
        }

        private void listBox7_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            musicRecords = da.getMusic();
            while (musicRecords.Read())
            {
                if (musicRecords["m_name"].ToString().Equals(listBox7.SelectedItem.ToString()))
                {
                    mediaPlayer.URL = musicRecords["m_path"].ToString();
                    break;
                }
            }
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.Show();
        }
    }
}
