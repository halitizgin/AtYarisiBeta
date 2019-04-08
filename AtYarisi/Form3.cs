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
using System.Data.OleDb;

namespace AtYarisi
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=veriler.accdb;Jet OLEDB:Database Password=kodevreni");

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1("1. At", "2. At", "3. At", "4. At", null, false);
            MessageBox.Show(form1.hakkinda, "Hakkında", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.kodevreni.com"); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.kodevreni.com/index.php?app=members&module=messaging&section=send&do=form&fromMemberID=36"); 
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
        string cozunurluk = "";
        private void Form3_Load(object sender, EventArgs e)
        {
            cozunurluk = Screen.PrimaryScreen.Bounds.Width.ToString() + " x " + Screen.PrimaryScreen.Bounds.Height.ToString();
            bool hatali = false;
            try
            {
                baglanti.Open();
            }
            catch (Exception hata)
            {
                hatali = true;
                if (hata.Message == "'Microsoft.ACE.OLEDB.12.0' sağlayıcısı yerel makine kayıtlı değil." || hata.Message == "The 'Microsoft.ACE.OLEDB.12.0' provider is not registered on the local machine.")
                {
                    MessageBox.Show("Hatayı çözebilmek için açılan kurulumu gerçekleştirmeniz gerekmektedir.\nEğer kurulum açılmaz ise 'Gerekliler' klasörü altındaki 'AccessDatabaseEngine.exe' dosyasını çalıştırıp kurulumu yapınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start("Gerekliler\\AccessDatabaseEngine.exe");
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                if (hatali == true)
                {
                    Application.ExitThread();
                }
            }
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            this.Hide();
            form4.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
        }
    }
}
