using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtYarisi
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                Settings1.Default.optimize = true;
                Settings1.Default.Save();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                Settings1.Default.optimize = false;
                Settings1.Default.Save();
            }
            MessageBox.Show("Değişiklikler kaydedildi. Değişiklikler yeni oyunda etkin olacaktır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            if (Settings1.Default.optimize == true)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }
    }
}
