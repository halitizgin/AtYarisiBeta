using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtYarisi
{
    public partial class Form11 : Form
    {
        public Form11(string birinciAt, string ikinciAt, string ucuncuAt, string dorduncuAt, string[,] dizi, bool oyun)
        {
            InitializeComponent();
            timer1.Tick += timer1_Tick;
            bahisler = dizi;
            gBirinciAt = birinciAt;
            gIkinciAt = ikinciAt;
            gUcuncuAt = ucuncuAt;
            gDorduncuAt = dorduncuAt;
            gOyun = oyun;
            if (gOyun == true)
            {
                oyunuBaslat();
            }
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=veriler.accdb;Jet OLEDB:Database Password=kodevreni");
        string[,] bahisler;
        bool gOyun = false;
        string gBirinciAt = "Birinci At";
        string gIkinciAt = "İkinci At";
        string gUcuncuAt = "Üçüncü At";
        string gDorduncuAt = "Dördüncü At";
        public string hakkinda = "At Yarışı Beta\nSürüm: 2.0.0.0\nhttp://www.kodevreni.com\nKodlayan: Ready\nProgramın betadan çıkması için oluşan hataları Ready nickli yöneticiye özelden resimli olarak bildirmenizi rica ediyoruz.";
        double saniye = 0;
        ArrayList atlar = new ArrayList();

        public int siralamaBul(string atismi, ArrayList siralama)
        {
            int donecek = 0;
            if (siralama[0].ToString().Split('|')[1] == atismi)
            {
                donecek = 1;
            }
            else if (siralama[1].ToString().Split('|')[1] == atismi)
            {
                donecek = 2;
            }
            else if (siralama[2].ToString().Split('|')[1] == atismi)
            {
                donecek = 3;
            }
            else if (siralama[3].ToString().Split('|')[1] == atismi)
            {
                donecek = 4;
            }
            return donecek;
        }

        public string virgulAl(string veri)
        {
            string final = "";
            char karak;
            for (int i = 0; i < veri.Length; i++)
            {
                karak = char.Parse(veri.Substring(i, 1));
                if (karak == ',')
                {
                    final += karak + veri.Substring(i + 1, 1);
                    break;
                }
                else
                {
                    final += karak;
                }
            }
            if (final.Length >= 3)
            {
                if (final.Substring(0, 3) == "100")
                {
                    final = "100";
                }
            }
            return final;
        }

        public void atIlerletme()
        {
            Random rast = new Random();
            birinciAt.Location = new Point(birinciAt.Location.X + rast.Next(1, 5), birinciAt.Location.Y);
            ikinciAt.Location = new Point(ikinciAt.Location.X + rast.Next(1, 5), ikinciAt.Location.Y);
            ucuncuAt.Location = new Point(ucuncuAt.Location.X + rast.Next(1, 5), ucuncuAt.Location.Y);
            dorduncuAt.Location = new Point(dorduncuAt.Location.X + rast.Next(1, 5), dorduncuAt.Location.Y);
        }

        bool bittimi = false;
        int baslangic = 148;
        int son = 994;

        private void timer1_Tick(object sender, EventArgs e)
        {
            saniye += 0.1;
            atIlerletme();
            atlar.Clear();
            int katedilen1 = birinciAt.Right - baslangic;
            birinciAtKatMes.Text = katedilen1.ToString();
            int katedilen2 = ikinciAt.Right - baslangic;
            ikinciAtKatMes.Text = katedilen2.ToString();
            int katedilen3 = ucuncuAt.Right - baslangic;
            ucuncuAtKatMes.Text = katedilen3.ToString();
            int katedilen4 = dorduncuAt.Right - baslangic;
            dorduncuAtKatMes.Text = katedilen4.ToString();
            atlar.Add((katedilen1) + "|" + gBirinciAt);
            atlar.Add((katedilen2) + "|" + gIkinciAt);
            atlar.Add((katedilen3) + "|" + gUcuncuAt);
            atlar.Add((katedilen4) + "|" + gDorduncuAt);
            atlar.Sort();
            atlar.Reverse();
            if (birinciAtKatMes.Text != "" && birinciAtKatMes.Text != "0" && ikinciAtKatMes.Text != "" && ikinciAtKatMes.Text != "0" && ucuncuAtKatMes.Text != "" && ucuncuAtKatMes.Text != "0" && dorduncuAtKatMes.Text != "" && dorduncuAtKatMes.Text != "0")
            {
                birinciAtKatedilen.Text = "Saniyede katedilen mesafe: " + virgulAl((Convert.ToDouble(katedilen1) / Convert.ToDouble(saniye)).ToString());
                ikinciAtKatedilen.Text = "Saniyede katedilen mesafe: " + virgulAl((Convert.ToDouble(katedilen2) / Convert.ToDouble(saniye)).ToString());
                ucuncuAtKatedilen.Text = "Saniyede katedilen mesafe: " + virgulAl((Convert.ToDouble(katedilen3) / Convert.ToDouble(saniye)).ToString());
                dorduncuAtKatedilen.Text = "Saniyede katedilen mesafe: " + virgulAl((Convert.ToDouble(katedilen4) / Convert.ToDouble(saniye)).ToString());
            }
            double yuzde1 = ((double)katedilen1 / son) * 100;
            double yuzde2 = ((double)katedilen2 / son) * 100;
            double yuzde3 = ((double)katedilen3 / son) * 100;
            double yuzde4 = ((double)katedilen4 / son) * 100;
            birinciAtMesafe.Value = Convert.ToInt32(yuzde1);
            GC.SuppressFinalize(yuzde1);
            GC.SuppressFinalize(birinciAtMesafe);
            ikinciAtMesafe.Value = Convert.ToInt32(yuzde2);
            GC.SuppressFinalize(yuzde2);
            GC.SuppressFinalize(ikinciAtMesafe);
            ucuncuAtMesafe.Value = Convert.ToInt32(yuzde3);
            GC.SuppressFinalize(yuzde3);
            GC.SuppressFinalize(ucuncuAtMesafe);
            dorduncuAtMesafe.Value = Convert.ToInt32(yuzde4);
            GC.SuppressFinalize(yuzde4);
            GC.SuppressFinalize(dorduncuAtMesafe);
            label12.Text = "%" + virgulAl(yuzde1.ToString());
            label13.Text = "%" + virgulAl(yuzde2.ToString());
            label14.Text = "%" + virgulAl(yuzde3.ToString());
            label15.Text = "%" + virgulAl(yuzde4.ToString());
            siraBirinciAt.Text = siralamaBul(gBirinciAt, atlar).ToString();
            siraIkinciAt.Text = siralamaBul(gIkinciAt, atlar).ToString();
            siraUcuncuAt.Text = siralamaBul(gUcuncuAt, atlar).ToString();
            siraDorduncuAt.Text = siralamaBul(gDorduncuAt, atlar).ToString();
            listBox1.Items.Clear();
            listBox1.Items.Add("1. " + atlar[0].ToString().Split('|')[1]);
            listBox1.Items.Add("2. " + atlar[1].ToString().Split('|')[1]);
            listBox1.Items.Add("3. " + atlar[2].ToString().Split('|')[1]);
            listBox1.Items.Add("4. " + atlar[3].ToString().Split('|')[1]);
            listView1.Items.Clear();
            for (int i = 0; i < bahisler.GetLength(0); i++)
            {
                int itemsCount = listView1.Items.Count;
                listView1.Items.Add(bahisler[i, 0]); // Kullanıcı Adı
                listView1.Items[itemsCount].SubItems.Add(bahisler[i, 2]); // Bahis
                listView1.Items[itemsCount].SubItems.Add(bahisler[i, 4]); // Kazanacak
                listView1.Items[itemsCount].SubItems.Add(siralamaBul(bahisler[i, 3], atlar).ToString()); // Tahmin
            }

            if (birinciAt.Right >= label1.Left)
            {
                bittimi = true;
                timer1.Stop();
                birinciAt.Location = new Point(label1.Left - 148, birinciAt.Location.Y);
                birinciAtKatMes.Text = "994";
                birinciAt.Image = Image.FromFile("at_normal.png");
                ikinciAt.Image = Image.FromFile("at_normal.png");
                ucuncuAt.Image = Image.FromFile("at_normal.png");
                dorduncuAt.Image = Image.FromFile("at_normal.png");
                atlar.Clear();
                int aKatedilen1 = birinciAt.Right - baslangic;
                birinciAtKatMes.Text = aKatedilen1.ToString();
                GC.SuppressFinalize(aKatedilen1);
                int aKatedilen2 = ikinciAt.Right - baslangic;
                ikinciAtKatMes.Text = aKatedilen2.ToString();
                GC.SuppressFinalize(aKatedilen2);
                int aKatedilen3 = ucuncuAt.Right - baslangic;
                ucuncuAtKatMes.Text = aKatedilen3.ToString();
                GC.SuppressFinalize(aKatedilen3);
                int aKatedilen4 = dorduncuAt.Right - baslangic;
                dorduncuAtKatMes.Text = aKatedilen4.ToString();
                GC.SuppressFinalize(aKatedilen4);
                atlar.Add((katedilen1) + "|" + gBirinciAt);
                GC.SuppressFinalize(katedilen1);
                atlar.Add((katedilen2) + "|" + gIkinciAt);
                GC.SuppressFinalize(katedilen2);
                atlar.Add((katedilen3) + "|" + gUcuncuAt);
                GC.SuppressFinalize(katedilen3);
                atlar.Add((katedilen4) + "|" + gDorduncuAt);
                GC.SuppressFinalize(katedilen4);
                atlar.Sort();
                atlar.Reverse();
                siraBirinciAt.Text = siralamaBul(gBirinciAt, atlar).ToString();
                siraIkinciAt.Text = siralamaBul(gIkinciAt, atlar).ToString();
                siraUcuncuAt.Text = siralamaBul(gUcuncuAt, atlar).ToString();
                siraDorduncuAt.Text = siralamaBul(gDorduncuAt, atlar).ToString();
                GC.SuppressFinalize(atlar);
                listBox1.Items.Clear();
                listBox1.Items.Add("1. " + atlar[0].ToString().Split('|')[1]);
                listBox1.Items.Add("2. " + atlar[1].ToString().Split('|')[1]);
                listBox1.Items.Add("3. " + atlar[2].ToString().Split('|')[1]);
                listBox1.Items.Add("4. " + atlar[3].ToString().Split('|')[1]);
                GC.SuppressFinalize(atlar);
                kazanan.Text = "Kazanan: " + gBirinciAt;
                kazanan.Tag = gBirinciAt;
                MessageBox.Show("Kazanan At: " + gBirinciAt, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ikinciAt.Right >= label1.Left)
            {
                bittimi = true;
                timer1.Stop();
                ikinciAt.Location = new Point(label1.Left - 148, ikinciAt.Location.Y);
                ikinciAtKatMes.Text = "994";
                birinciAt.Image = Image.FromFile("at_normal.png");
                ikinciAt.Image = Image.FromFile("at_normal.png");
                ucuncuAt.Image = Image.FromFile("at_normal.png");
                dorduncuAt.Image = Image.FromFile("at_normal.png");
                atlar.Clear();
                int bKatedilen1 = birinciAt.Right - baslangic;
                birinciAtKatMes.Text = bKatedilen1.ToString();
                GC.SuppressFinalize(bKatedilen1);
                int bKatedilen2 = ikinciAt.Right - baslangic;
                ikinciAtKatMes.Text = bKatedilen2.ToString();
                GC.SuppressFinalize(bKatedilen2);
                int bKatedilen3 = ucuncuAt.Right - baslangic;
                ucuncuAtKatMes.Text = bKatedilen3.ToString();
                GC.SuppressFinalize(bKatedilen3);
                int bKatedilen4 = dorduncuAt.Right - baslangic;
                dorduncuAtKatMes.Text = bKatedilen4.ToString();
                GC.SuppressFinalize(bKatedilen4);
                atlar.Add((katedilen1) + "|" + gBirinciAt);
                GC.SuppressFinalize(gBirinciAt);
                GC.SuppressFinalize(katedilen1);
                atlar.Add((katedilen2) + "|" + gIkinciAt);
                GC.SuppressFinalize(gIkinciAt);
                GC.SuppressFinalize(katedilen2);
                atlar.Add((katedilen3) + "|" + gUcuncuAt);
                GC.SuppressFinalize(gUcuncuAt);
                GC.SuppressFinalize(katedilen3);
                atlar.Add((katedilen4) + "|" + gDorduncuAt);
                GC.SuppressFinalize(gDorduncuAt);
                GC.SuppressFinalize(katedilen4);
                atlar.Sort();
                atlar.Reverse();
                siraBirinciAt.Text = siralamaBul(gBirinciAt, atlar).ToString();
                siraIkinciAt.Text = siralamaBul(gIkinciAt, atlar).ToString();
                siraUcuncuAt.Text = siralamaBul(gUcuncuAt, atlar).ToString();
                siraDorduncuAt.Text = siralamaBul(gDorduncuAt, atlar).ToString();
                GC.SuppressFinalize(atlar);
                listBox1.Items.Clear();
                listBox1.Items.Add("1. " + atlar[0].ToString().Split('|')[1]);
                listBox1.Items.Add("2. " + atlar[1].ToString().Split('|')[1]);
                listBox1.Items.Add("3. " + atlar[2].ToString().Split('|')[1]);
                listBox1.Items.Add("4. " + atlar[3].ToString().Split('|')[1]);
                GC.SuppressFinalize(atlar);
                kazanan.Text = "Kazanan: " + gIkinciAt;
                kazanan.Tag = gIkinciAt;
                MessageBox.Show("Kazanan At: " + gIkinciAt, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ucuncuAt.Right >= label1.Left)
            {
                bittimi = true;
                timer1.Stop();
                ucuncuAt.Location = new Point(label1.Left - 148, ucuncuAt.Location.Y);
                ucuncuAtKatMes.Text = "994";
                birinciAt.Image = Image.FromFile("at_normal.png");
                ikinciAt.Image = Image.FromFile("at_normal.png");
                ucuncuAt.Image = Image.FromFile("at_normal.png");
                dorduncuAt.Image = Image.FromFile("at_normal.png");
                atlar.Clear();
                int cKatedilen1 = birinciAt.Right - baslangic;
                birinciAtKatMes.Text = cKatedilen1.ToString();
                GC.SuppressFinalize(cKatedilen1);
                int cKatedilen2 = ikinciAt.Right - baslangic;
                ikinciAtKatMes.Text = cKatedilen2.ToString();
                GC.SuppressFinalize(cKatedilen2);
                int cKatedilen3 = ucuncuAt.Right - baslangic;
                ucuncuAtKatMes.Text = cKatedilen3.ToString();
                GC.SuppressFinalize(cKatedilen3);
                int cKatedilen4 = dorduncuAt.Right - baslangic;
                dorduncuAtKatMes.Text = cKatedilen4.ToString();
                GC.SuppressFinalize(cKatedilen4);
                atlar.Add((katedilen1) + "|" + gBirinciAt);
                GC.SuppressFinalize(katedilen1);
                GC.SuppressFinalize(gBirinciAt);
                atlar.Add((katedilen2) + "|" + gIkinciAt);
                GC.SuppressFinalize(katedilen2);
                GC.SuppressFinalize(gIkinciAt);
                atlar.Add((katedilen3) + "|" + gUcuncuAt);
                GC.SuppressFinalize(katedilen3);
                GC.SuppressFinalize(gUcuncuAt);
                atlar.Add((katedilen4) + "|" + gDorduncuAt);
                GC.SuppressFinalize(katedilen4);
                GC.SuppressFinalize(gDorduncuAt);
                atlar.Sort();
                atlar.Reverse();
                siraBirinciAt.Text = siralamaBul(gBirinciAt, atlar).ToString();
                siraIkinciAt.Text = siralamaBul(gIkinciAt, atlar).ToString();
                siraUcuncuAt.Text = siralamaBul(gUcuncuAt, atlar).ToString();
                siraDorduncuAt.Text = siralamaBul(gDorduncuAt, atlar).ToString();
                GC.SuppressFinalize(atlar);
                listBox1.Items.Clear();
                listBox1.Items.Add("1. " + atlar[0].ToString().Split('|')[1]);
                listBox1.Items.Add("2. " + atlar[1].ToString().Split('|')[1]);
                listBox1.Items.Add("3. " + atlar[2].ToString().Split('|')[1]);
                listBox1.Items.Add("4. " + atlar[3].ToString().Split('|')[1]);
                GC.SuppressFinalize(atlar);
                kazanan.Text = "Kazanan: " + gUcuncuAt;
                kazanan.Tag = gUcuncuAt;
                MessageBox.Show("Kazanan At: " + gUcuncuAt, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dorduncuAt.Right >= label1.Left)
            {
                bittimi = true;
                timer1.Stop();
                dorduncuAt.Location = new Point(label1.Left - 148, dorduncuAt.Location.Y);
                dorduncuAtKatMes.Text = "994";
                birinciAt.Image = Image.FromFile("at_normal.png");
                ikinciAt.Image = Image.FromFile("at_normal.png");
                ucuncuAt.Image = Image.FromFile("at_normal.png");
                dorduncuAt.Image = Image.FromFile("at_normal.png");
                atlar.Clear();
                int dKatedilen1 = birinciAt.Right - baslangic;
                birinciAtKatMes.Text = dKatedilen1.ToString();
                int dKatedilen2 = ikinciAt.Right - baslangic;
                ikinciAtKatMes.Text = dKatedilen2.ToString();
                int dKatedilen3 = ucuncuAt.Right - baslangic;
                ucuncuAtKatMes.Text = dKatedilen3.ToString();
                int dKatedilen4 = dorduncuAt.Right - baslangic;
                dorduncuAtKatMes.Text = dKatedilen4.ToString();
                atlar.Add((katedilen1) + "|" + gBirinciAt);
                atlar.Add((katedilen2) + "|" + gIkinciAt);
                atlar.Add((katedilen3) + "|" + gUcuncuAt);
                atlar.Add((katedilen4) + "|" + gDorduncuAt);
                atlar.Sort();
                atlar.Reverse();
                siraBirinciAt.Text = siralamaBul(gBirinciAt, atlar).ToString();
                siraIkinciAt.Text = siralamaBul(gIkinciAt, atlar).ToString();
                siraUcuncuAt.Text = siralamaBul(gUcuncuAt, atlar).ToString();
                siraDorduncuAt.Text = siralamaBul(gDorduncuAt, atlar).ToString();
                listBox1.Items.Clear();
                listBox1.Items.Add("1. " + atlar[0].ToString().Split('|')[1]);
                listBox1.Items.Add("2. " + atlar[1].ToString().Split('|')[1]);
                listBox1.Items.Add("3. " + atlar[2].ToString().Split('|')[1]);
                listBox1.Items.Add("4. " + atlar[3].ToString().Split('|')[1]);
                kazanan.Text = "Kazanan: " + gDorduncuAt;
                kazanan.Tag = gDorduncuAt;
                MessageBox.Show("Kazanan At: " + gDorduncuAt, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (bittimi == true)
            {
                listView1.Items.Clear();
                for (int i = 0; i < bahisler.GetLength(0); i++)
                {
                    int itemsCount = listView1.Items.Count;
                    listView1.Items.Add(bahisler[i, 0]); // Kullanıcı Adı
                    listView1.Items[itemsCount].SubItems.Add(bahisler[i, 2]); // Bahis
                    listView1.Items[itemsCount].SubItems.Add(bahisler[i, 4]); // Kazanacak
                    listView1.Items[itemsCount].SubItems.Add(siralamaBul(bahisler[i, 3], atlar).ToString()); // Tahmin
                }

                int iSayi = 0;
                for (int i = 0; i < bahisler.GetLength(0); i++)
                {
                    if (siralamaBul(bahisler[i, 3], atlar) == 1)
                    {
                        iSayi = Convert.ToInt32(bahisler[i, 1]) + Convert.ToInt32(bahisler[i, 4]);
                    }
                    else
                    {
                        iSayi = Convert.ToInt32(bahisler[i, 1]) - Convert.ToInt32(bahisler[i, 2]);
                    }

                    if (iSayi == 0)
                    {
                        baglan(baglanti);
                        OleDbCommand komut = new OleDbCommand("UPDATE kisiler SET para='" + iSayi + "' AND yenilenme='7' WHERE kullanici='" + bahisler[i, 0] + "'", baglanti);
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                        richTextBox1.Text = bahisler[i, 0] + " adlı kullanıcının bakiyesi " + iSayi + " olarak güncellenmiştir! Bakiyeniz 6 yarış sonra tekrar 100 olarak güncelenecektir.\n";
                    }
                    else
                    {
                        baglan(baglanti);
                        OleDbCommand komut = new OleDbCommand("UPDATE kisiler SET para='" + iSayi + "' WHERE kullanici='" + bahisler[i, 0] + "'", baglanti);
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                        richTextBox1.Text = bahisler[i, 0] + " adlı kullanıcının bakiyesi " + iSayi + " olarak güncellenmiştir!\n";
                    }
                }
                baglanti.Close();
                baglan(baglanti);
                OleDbCommand komut2 = new OleDbCommand("SELECT * FROM kisiler", baglanti);
                OleDbDataReader okuyucu = komut2.ExecuteReader();
                while (okuyucu.Read())
                {
                    if (okuyucu["para"].ToString() == "0")
                    {
                        int sayi = Convert.ToInt32(okuyucu["yenilenme"].ToString());
                        if (sayi > 1)
                        {
                            sayi--;
                            OleDbCommand duzenle = new OleDbCommand("UPDATE kisiler SET yenilenme='" + sayi + "' WHERE kullanici='" + okuyucu["kullanici"].ToString() + "'", baglanti);
                            duzenle.ExecuteNonQuery();
                            duzenle.Dispose();
                        }
                        else if (sayi <= 1)
                        {
                            OleDbCommand duzenle2 = new OleDbCommand("UPDATE kisiler SET para='100', yenilenme='7' WHERE kullanici='" + okuyucu["kullanici"].ToString() + "'", baglanti);
                            duzenle2.ExecuteNonQuery();
                            duzenle2.Dispose();
                            richTextBox1.Text += okuyucu["kullanici"].ToString() + " adlı kullanıcının bakiyesi tekrar 100 olarak güncellendi.\n";
                        }
                    }
                }
                baglanti.Close();
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

        public void oyunuBaslat()
        {
            listView1.Columns.Add("Kullanıcı");
            listView1.Columns.Add("Bahis");
            listView1.Columns.Add("Kazanacak");
            listView1.Columns.Add("Sıralama");
            if (Settings1.Default.optimize == false)
            {
                birinciAt.Image = Image.FromFile("at_hareketli.gif");
                ikinciAt.Image = Image.FromFile("at_hareketli.gif");
                ucuncuAt.Image = Image.FromFile("at_hareketli.gif");
                dorduncuAt.Image = Image.FromFile("at_hareketli.gif");
            }
            else
            {
                birinciAt.Image = Image.FromFile("at_normal.png");
                ikinciAt.Image = Image.FromFile("at_normal.png");
                ucuncuAt.Image = Image.FromFile("at_normal.png");
                dorduncuAt.Image = Image.FromFile("at_normal.png");
            }
            label2.Text = gBirinciAt;
            label3.Text = gIkinciAt;
            label4.Text = gUcuncuAt;
            label5.Text = gDorduncuAt;
            baslangic = birinciAt.Width;
            son = label1.Left - baslangic;
            label17.Text = son.ToString();
            label18.Text = son.ToString();
            label20.Text = son.ToString();
            label22.Text = son.ToString();
            timer1.Start();
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(hakkinda, "Hakkında", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void siteyeGitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.kodevreni.com");
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void oyunuBaşlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            atlar.Clear();
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void anaMenüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void optimizasyonAyarlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
        }
    }
}
