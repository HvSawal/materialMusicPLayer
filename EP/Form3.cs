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
    public partial class Form3 : MaterialForm
    {
        String playlistName;
        DataAccess da = new DataAccess();
        List<String> musicOptions;
        public Form3(String playlistName)
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            
            this.playlistName = playlistName;
            musicOptions = da.getPlaylistMusicOptions(playlistName);
            foreach(String s in musicOptions)
            {
                checkedListBox1.Items.Add(s);
            }
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    da.addToPlaylist(checkedListBox1.Items[i].ToString(), playlistName);
                }
            }
            
        }
    }
}
