using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SilkroadServerManager
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        { 
            comboBox1.SelectedIndex = 0;
            if (Language.Default.language == "Türkçe")
            {
                comboBox1.SelectedIndex = 1;
                this.Text = "Ayarlar";
                label1.Text = "Dil Seçimi :";
                button1.Text = "Kaydet";
                label1.Location = new Point(45,26);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Language.Default.language = "English";
                    Language.Default.Save();
                    break;
                case 1:
                    Language.Default.language = "Türkçe";
                    Language.Default.Save();
                    break;
            }
            Process.Start(Application.ExecutablePath);
            Environment.Exit(0);
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                en.Show();
                tr.Hide();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                tr.Show();
                en.Hide();
            }
        }
    }
}
