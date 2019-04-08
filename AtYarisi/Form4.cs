using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.VisualBasic;

namespace AtYarisi
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=veriler.accdb");
        TextBox textbox = new TextBox();


        private void Form4_Load(object sender, EventArgs e)
        {
            string sifre = Microsoft.VisualBasic.Interaction.InputBox("Şifreyi giriniz:", "Şifre", "", 50, 50);
            if (sifre != "")
            {
                baglanti.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=veriler.accdb;Jet OLEDB:Database Password=" + sifre;
            }
            bool hatali = false;
            try
            {
                baglanti.Open();
            }
            catch (Exception hata)
            {
                hatali = true;
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                try
                {
                    if (hatali == true)
                    {
                        if (baglanti.State == ConnectionState.Connecting)
                        {
                            baglanti.Close();
                        }
                        Application.ExitThread();
                    }
                    else
                    {
                        OleDbCommand komut = new OleDbCommand("SELECT * FROM kisiler", baglanti);
                        listView1.Columns.Add("ID");
                        listView1.Columns.Add("Kullanıcı Adı");
                        listView1.Columns.Add("Şifre");
                        listView1.Columns.Add("Para");
                        listView1.Columns.Add("Yenilenme");
                        OleDbDataReader okuyucu = komut.ExecuteReader();
                        while (okuyucu.Read())
                        {
                            int count = listView1.Items.Count;
                            listView1.Items.Add(okuyucu["id"].ToString());
                            listView1.Items[count].SubItems.Add(okuyucu["kullanici"].ToString());
                            listView1.Items[count].SubItems.Add(okuyucu["sifre"].ToString());
                            listView1.Items[count].SubItems.Add(okuyucu["para"].ToString());
                            listView1.Items[count].SubItems.Add(okuyucu["yenilenme"].ToString());
                        }
                    }
                }
                catch (Exception hata)
                {
                    if (baglanti.State == ConnectionState.Connecting)
                    {
                        baglanti.Close();
                    }
                    MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.ExitThread();
                }
            }
        }

        public void listele(ListView listView)
        {
            baglan(baglanti);
            OleDbCommand komut = new OleDbCommand("SELECT * FROM kisiler", baglanti);
            OleDbDataReader okuyucu = komut.ExecuteReader();
            listView.Items.Clear();
            while(okuyucu.Read())
            {
                int count = listView.Items.Count;
                listView.Items.Add(okuyucu["id"].ToString());
                listView.Items[count].SubItems.Add(okuyucu["kullanici"].ToString());
                listView.Items[count].SubItems.Add(okuyucu["sifre"].ToString());
                listView.Items[count].SubItems.Add(okuyucu["para"].ToString());
                listView.Items[count].SubItems.Add(okuyucu["yenilenme"].ToString());
            }
        }

        public void baglan(OleDbConnection baglan)
        {
            if (baglan.State == ConnectionState.Closed)
            {
                baglan.Open();
            }
            else if (baglan.State == ConnectionState.Connecting)
            {
                baglan.Close();
                baglan.Open();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (kullaniciTextBox.Text != "" && sifreTextBox.Text != "")
                {
                    baglan(baglanti);
                    OleDbCommand komut = new OleDbCommand("SELECT * FROM kisiler WHERE kullanici='" + kullaniciTextBox.Text + "'", baglanti);
                    OleDbDataReader okuyucu = komut.ExecuteReader();
                    int count = 0;
                    while (okuyucu.Read())
                    {
                        count++;
                    }
                    if (count > 0)
                    {
                        MessageBox.Show("Bu kullanıcı zaten mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        baglan(baglanti);
                        OleDbCommand ekleme = new OleDbCommand("INSERT INTO kisiler (kullanici, sifre, para, yenilenme) VALUES ('" + kullaniciTextBox.Text + "', '" + sifreTextBox.Text + "', '" + Convert.ToInt32(paraTextBox.Value) + "', '" + Convert.ToInt32(yenilenmeNumeric.Value) + "')", baglanti);
                        ekleme.ExecuteNonQuery();
                        ekleme.Dispose();
                        baglanti.Close();
                        listele(listView1);
                        MessageBox.Show("Kullanıcı başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception hata)
            {
                if (baglanti.State == ConnectionState.Connecting)
                {
                    baglanti.Close();
                }
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                idTextBox.Text = listView1.SelectedItems[0].Text;
                kullaniciTextBox.Text = listView1.SelectedItems[0].SubItems[1].Text;
                sifreTextBox.Text = listView1.SelectedItems[0].SubItems[2].Text;
                paraTextBox.Text = listView1.SelectedItems[0].SubItems[3].Text;
                yenilenmeNumeric.Text = listView1.SelectedItems[0].SubItems[4].Text;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                sifreTextBox.PasswordChar = textbox.PasswordChar;
            }
            else
            {
                sifreTextBox.PasswordChar = '•';
            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Yönetici panelinden çıkmak istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (baglanti.State == ConnectionState.Connecting)
                {
                    baglanti.Close();
                }
                Form3 form3 = new Form3();
                this.Hide();
                form3.Show();
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "" && kullaniciTextBox.Text.Trim() != "" && sifreTextBox.Text.Trim() != "")
            {
                baglan(baglanti);
                OleDbCommand komut = new OleDbCommand("SELECT * FROM kisiler WHERE kullanici='" + kullaniciTextBox.Text + "'", baglanti);
                OleDbDataReader okuyucu = komut.ExecuteReader();
                int count = 0;
                string veri = "";
                while (okuyucu.Read())
                {
                    count++;
                    veri = okuyucu["kullanici"].ToString();
                }
                if (listView1.SelectedItems.Count == 1)
                {
                    if (count > 0 && listView1.SelectedItems[0].SubItems[1].Text != veri)
                    {
                        MessageBox.Show("Bu kullanıcı zaten mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        baglan(baglanti);
                        OleDbCommand duzenleme = new OleDbCommand("UPDATE kisiler SET kullanici='" + kullaniciTextBox.Text.Trim() + "', sifre='" + sifreTextBox.Text.Trim() + "', para='" + Convert.ToInt32(paraTextBox.Value) + "', yenilenme='" + Convert.ToInt32(yenilenmeNumeric.Value) + "' WHERE id=@id", baglanti);
                        duzenleme.Parameters.AddWithValue("@id", Convert.ToInt32(idTextBox.Text));
                        duzenleme.ExecuteNonQuery();
                        duzenleme.Dispose();
                        baglanti.Close();
                        listele(listView1);
                        MessageBox.Show("Düzenleme işlemi başarıyla tamamlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    OleDbCommand komut = new OleDbCommand("DELETE FROM kisiler WHERE id=@id", baglanti);
                    komut.Parameters.AddWithValue("@id", Convert.ToInt32(item.Text));
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                }
                baglanti.Close();
                listele(listView1);
                MessageBox.Show("Silme işlemi başarıyla tamamlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
