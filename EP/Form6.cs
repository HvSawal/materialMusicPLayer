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
using System.IO;

namespace EP
{
    public partial class Form6 : MaterialForm
    {
        public Form6()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            string path = @"d:\hello.txt";

            if(!File.Exists(path))
            {
                using(FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("There is some more text in this file.");
                    fs.Write(info, 0, info.Length);
                }
            }

            using(StreamReader sr = File.OpenText(path))
            {
                string s = "";
                String j = null;
                while((s = sr.ReadLine()) != null)
                {
                    j += s+"\n";
                }
                richTextBox1.Text = j;
            }
        }
    }
}
