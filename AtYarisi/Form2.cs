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
using System.Collections;

namespace AtYarisi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=veriler.accdb;Jet OLEDB:Database Password=kodevreni");

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }
        int[] oranlar = { 1, 5, 10, 20, 25, 50, 100 };
        string[,] bahis;
        private void button1_Click(object sender, EventArgs e)
        {
            Array.Reverse(oranlar);
            if (textBox1.Text.Trim().Replace("|", "") != "" && textBox2.Text.Trim().Replace("|", "") != "" && textBox3.Text.Trim().Replace("|", "") != "" && textBox4.Text.Trim().Replace("|", "") != "" && listView1.Items.Count > 0)
            {
                bahis = new string[listView1.Items.Count, 5];
                int i = 0;
                foreach (ListViewItem item in listView1.Items)
                {
                    bahis[i, 0] = item.Text; // Kullanıcı Adı
                    bahis[i, 1] = item.SubItems[1].Text; // Bakiye
                    bahis[i, 2] = item.SubItems[2].Text; // Bahis
                    bahis[i, 3] = item.SubItems[4].Text; // Tahmin
                    bahis[i, 4] = item.SubItems[3].Text; // Kazanç
                    i++;
                }

                string cozunurluk = Screen.PrimaryScreen.Bounds.Width.ToString() + " x " + Screen.PrimaryScreen.Bounds.Height.ToString();
                if (cozunurluk == "1600 x 900")
                {
                    Form1 form1 = new Form1(textBox1.Text.Trim().Replace("|", ""), textBox2.Text.Trim().Replace("|", ""), textBox3.Text.Trim().Replace("|", ""), textBox4.Text.Trim().Replace("|", ""), bahis, true);
                    form1.Show();
                    this.Hide();
                }
                else if (cozunurluk == "1366 x 768")
                {
                    Form12 form12 = new Form12(textBox1.Text.Trim().Replace("|", ""), textBox2.Text.Trim().Replace("|", ""), textBox3.Text.Trim().Replace("|", ""), textBox4.Text.Trim().Replace("|", ""), bahis, true);
                    form12.Show();
                    this.Hide();
                }
                else if (cozunurluk == "1280 x 720")
                {
                    Form11 form11 = new Form11(textBox1.Text.Trim().Replace("|", ""), textBox2.Text.Trim().Replace("|", ""), textBox3.Text.Trim().Replace("|", ""), textBox4.Text.Trim().Replace("|", ""), bahis, true);
                    form11.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Boşlukları doldurunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tahmin.Items[0] = textBox1.Text.Trim();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            tahmin.Items[1] = textBox2.Text.Trim();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            tahmin.Items[2] = textBox3.Text.Trim();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            tahmin.Items[3] = textBox4.Text.Trim();
        }

        ArrayList kisiler = new ArrayList();

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                baglanti.Close();
                listView1.Columns.Add("Kullanıcı Adı");
                listView1.Columns.Add("Bakiye");
                listView1.Columns.Add("Bahis");
                listView1.Columns.Add("Kazanacak");
                listView1.Columns.Add("Tahmin");

                listView2.Columns.Add("Kullanıcı Adı");
                listView2.Columns.Add("Kalan Yarış");
                listView2.Items.Clear();
                baglan(baglanti);
                OleDbCommand komut = new OleDbCommand("SELECT * FROM kisiler WHERE para=0", baglanti);
                OleDbDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    int count = listView2.Items.Count;
                    listView2.Items.Add(okuyucu["kullanici"].ToString());
                    listView2.Items[count].SubItems.Add(okuyucu["yenilenme"].ToString());
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (baglanti.State == ConnectionState.Connecting)
                {
                    baglanti.Close();
                }
                Application.ExitThread();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tahmin.SelectedIndex != -1)
            {
                int kat = 0;
                if (tahmin.Text == textBox1.Text)
                {
                    kat = 100 / Convert.ToInt32(textBox1.Tag);
                }
                else if (tahmin.Text == textBox2.Text)
                {
                    kat = 100 / Convert.ToInt32(textBox2.Tag);
                }
                else if (tahmin.Text == textBox3.Text)
                {
                    kat = 100 / Convert.ToInt32(textBox3.Tag);
                }
                else if (tahmin.Text == textBox4.Text)
                {
                    kat = 100 / Convert.ToInt32(textBox4.Tag);
                }
                kazanc.Text = (Convert.ToInt32(bahisNumeric.Value) * kat).ToString();
            }
        }

        bool hata = false;

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "")
            {
                hata = false;
                ArrayList kontrol = new ArrayList();
                kontrol.Add(textBox1.Text.Trim());
                if (kontrol.Contains(textBox2.Text.Trim()))
                {
                    hata = true;
                    kontrol.Add(textBox2.Text.Trim());
                }

                if (kontrol.Contains(textBox3.Text.Trim()))
                {
                    hata = true;
                    kontrol.Add(textBox3.Text.Trim());
                }

                if (kontrol.Contains(textBox4.Text.Trim()))
                {
                    hata = true;
                    kontrol.Add(textBox4.Text.Trim());
                }

                if (hata == false)
                {
                    Random rast = new Random();
                    int uret1 = rast.Next(0, oranlar.Length);
                    birinciAtOran.Text = "%" + oranlar[uret1];
                    textBox1.Tag = oranlar[uret1];
                    int uret2 = rast.Next(0, oranlar.Length);
                    ikinciAtOran.Text = "%" + oranlar[uret2];
                    textBox2.Tag = oranlar[uret2];
                    int uret3 = rast.Next(0, oranlar.Length);
                    ucuncuAtOran.Text = "%" + oranlar[uret3];
                    textBox3.Tag = oranlar[uret3];
                    int uret4 = rast.Next(0, oranlar.Length);
                    dorduncuAtOran.Text = "%" + oranlar[uret4];
                    textBox4.Tag = oranlar[uret4];
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    button2.Enabled = false;
                    kullaniciAdi.Enabled = true;
                    sifre.Enabled = true;
                    bahisNumeric.Enabled = true;
                    tahmin.Enabled = true;
                    kazanc.Enabled = true;
                    button1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Tüm at isimleri farklı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (tahmin.SelectedIndex != -1)
            {
                int kat = 0;
                if (tahmin.Text == textBox1.Text)
                {
                    kat = 100 / Convert.ToInt32(textBox1.Tag);
                }
                else if (tahmin.Text == textBox2.Text)
                {
                    kat = 100 / Convert.ToInt32(textBox2.Tag);
                }
                else if (tahmin.Text == textBox3.Text)
                {
                    kat = 100 / Convert.ToInt32(textBox3.Tag);
                }
                else if (tahmin.Text == textBox4.Text)
                {
                    kat = 100 / Convert.ToInt32(textBox4.Tag);
                }

                kazanc.Text = (Convert.ToInt32(bahisNumeric.Value) * kat).ToString();
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (kullaniciAdi.Text.Trim() != "" && sifre.Text.Trim() != "" && tahmin.SelectedIndex != -1 && kazanc.Text != "0")
            {
                if (!kisiler.Contains(kullaniciAdi.Text.Trim()))
                {
                    baglan(baglanti);
                    OleDbCommand komut = new OleDbCommand("SELECT * FROM kisiler WHERE kullanici='" + kullaniciAdi.Text.Trim() + "'", baglanti);
                    OleDbDataReader okuyucu = komut.ExecuteReader();
                    int count = 0;
                    string kullanici = "";
                    string nSifre = "";
                    string bakiye = "";
                    string yenilenme = "";
                    while (okuyucu.Read())
                    {
                        kullanici = okuyucu["kullanici"].ToString();
                        nSifre = okuyucu["sifre"].ToString();
                        bakiye = okuyucu["para"].ToString();
                        yenilenme = okuyucu["yenilenme"].ToString();
                        count++;
                    }

                    if (count > 0)
                    {
                        if (Convert.ToInt32(bakiye) > 0)
                        {
                            if (nSifre == sifre.Text.Trim())
                            {
                                if (Convert.ToInt32(bakiye) >= Convert.ToInt32(bahisNumeric.Value))
                                {
                                    int items = listView1.Items.Count;
                                    listView1.Items.Add(kullanici);
                                    listView1.Items[items].SubItems.Add(bakiye);
                                    listView1.Items[items].SubItems.Add(Convert.ToInt32(bahisNumeric.Value).ToString());
                                    listView1.Items[items].SubItems.Add(kazanc.Text);
                                    listView1.Items[items].SubItems.Add(tahmin.Text.Trim());
                                    sifre.Text = "";
                                    kisiler.Add(kullaniciAdi.Text.Trim());
                                    MessageBox.Show("Bahsiniz başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Bakiyeniz yetersiz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Hatalı giriş.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bakiyeniz 0. " + yenilenme + " yarış sonra bakiyeniz tekrar 100 olarak güncellenecektir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Şuan üye değilsiniz. Üye olmak ister misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            baglan(baglanti);
                            OleDbCommand uyelik = new OleDbCommand("INSERT INTO kisiler (kullanici, sifre) VALUES ('" + kullaniciAdi.Text.Trim() + "', '" + sifre.Text.Trim() + "')", baglanti);
                            uyelik.ExecuteNonQuery();
                            uyelik.Dispose();
                            baglanti.Close();
                            int items = listView1.Items.Count;
                            listView1.Items.Add(kullaniciAdi.Text.Trim());
                            listView1.Items[items].SubItems.Add("100");
                            listView1.Items[items].SubItems.Add(Convert.ToInt32(bahisNumeric.Value).ToString());
                            listView1.Items[items].SubItems.Add(kazanc.Text);
                            listView1.Items[items].SubItems.Add(tahmin.Text.Trim());
                            sifre.Text = "";
                            kisiler.Add(kullaniciAdi.Text.Trim());
                            MessageBox.Show("Üyelik işlemi başarıyla tamamlandı ve bahisiniz eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Zaten bahis eklediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
