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
using System.Data.SQLite;

namespace mor_adisyon
{
    public partial class sihirbaz : Form
    {

        SQLiteCommand cmd;

        public sihirbaz()
        {
            InitializeComponent();
        }

        public class Urunler
        {
            public string urun_adi;
            public string urun_fiyati;
        }

        SQLiteConnection con = new SQLiteConnection("Data source=.\\moradisyon.db;Versiyon=3");

        int adim = 1;

        string firma_adi,kullanici_adi,parola;


        void txt_olustur(string dosya_adi, string deger)
        {
        
            StreamWriter Dosya = File.CreateText(dosya_adi);
            Dosya.Close();
            StreamWriter Dosya2 = File.AppendText(dosya_adi);//dosyayı aç
            Dosya2.WriteLine(deger);//yaz
            Dosya2.Close();
            // FileStream fs = new FileStream("masa1.txt", FileMode.Append, FileAccess.Write, FileShare.Write);
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int harfsay = textBox1.TextLength;

            if(harfsay > 2) {

                txt_olustur("firma_adi.txt",textBox1.Text);
                firma_adi = textBox1.Text;
                adim=2;
                tabControl1.SelectedTab = tabPage10;
            
            }
            else
            {
                MessageBox.Show("Firma adı en az 3 karakter olmalıdır..");
            }
        }

        int masa_sayi;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            kullanici_adi = kullaniciText.Text;
            parola = parolaText.Text;
            adim = 3;
            tabControl1.SelectedTab = tabPage2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
             masa_sayi = Convert.ToInt16(masa_sayisi.Text);
            if(masa_sayi <1 || masa_sayi > 42)
            {
                MessageBox.Show("Masa sayısı 1-42 arasında olmalıdır");
            }

            else {
                adim = 4;
            foreach (var masalar in tabPage3.Controls.OfType<TextBox>())
            {
                char ayrac = '_';
                string[] masa_durum = null;

                masa_durum = masalar.Name.ToString().Split(ayrac);
                int masa_analiz = Convert.ToInt16( masa_durum[1].ToString());
                if(masa_analiz <= masa_sayi)
                {
                    masalar.Visible = true;
                }
               
            }

                cmd = new SQLiteCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into firma_bilgileri (firma_adi,masa_sayisi,kullanici_adi,parola) values ('" + firma_adi + "','" + masa_sayi.ToString() + "','" + kullanici_adi + "','" + parola + "')";
                cmd.ExecuteNonQuery();
                con.Close();

                tabControl1.SelectedTab = tabPage3;

            }

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void sihirbaz_Load(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Update();
            try { 
            string klasorYolu = Application.StartupPath;
            string dosyaAdi = "firma_adi.txt";

            string dosyaYolu = Path.Combine(klasorYolu, dosyaAdi);

            if (File.Exists(dosyaYolu))
            {
                    Form1 f1 = new Form1();
                    f1.Show();
                    this.Hide();
                }
            else
            {

              
            }
            }
            catch { }



        }


        private void toplu_urun_ekle(List<Urunler> liste,string kategori)
        {

            cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;


            foreach (var x in liste)
            {
                if (x.urun_adi != "" || x.urun_fiyati != "")
                {

                    using (SQLiteCommand cmd2 = new SQLiteCommand())
                    {
                        cmd2.Connection = con; // connection, bir OleDbConnection nesnesi olmalıdır.

                        // Parametre kullanımıyla güvenli bir sorgu oluşturuluyor
                        cmd2.CommandText = "SELECT COUNT(*) FROM urunler WHERE urun_adi = @urunAdi";

                        // Parametre eklemek
                        cmd2.Parameters.AddWithValue("@urunAdi", x.urun_adi);

                        // Sorguyu çalıştır ve sonucu al
                        int rowCount = 0;

                        object result = cmd2.ExecuteScalar();
                        if (result != null)
                        {
                             rowCount = Convert.ToInt32(result);
                            // rowCount ile devam et...
                        }
                        else
                        {
                            // result null ise uygun bir hata işleme stratejisi uygula.
                        }

                        // MessageBox.Show($"Ürün adı '{x.urun_adi}' içeren satır sayısı: {rowCount}");
                        if (rowCount == 0)
                        {
                            cmd.CommandText = "insert into urunler (urun_adi,urun_fiyati,urun_kategori) values ('" + x.urun_adi + "','" + x.urun_fiyati + "','" + kategori + "')";
                            cmd.ExecuteNonQuery();
                        }
                    }

                }

            }
            MessageBox.Show("Kaydetme başarılı..");
            con.Close();

        }
        private void sicak_kaydet_Click(object sender, EventArgs e)
        {


            List<Urunler> listem = new List<Urunler>
            {
                new Urunler { urun_adi= urun1_adi.Text,urun_fiyati = urun1_fiyati.Text},
                new Urunler { urun_adi= urun2_adi.Text,urun_fiyati = urun2_fiyati.Text},
                new Urunler { urun_adi= urun3_adi.Text,urun_fiyati = urun3_fiyati.Text},
                new Urunler { urun_adi= urun4_adi.Text,urun_fiyati = urun4_fiyati.Text},
                new Urunler { urun_adi= urun5_adi.Text,urun_fiyati = urun5_fiyati.Text},
                new Urunler { urun_adi= urun6_adi.Text,urun_fiyati = urun6_fiyati.Text},
                new Urunler { urun_adi= urun7_adi.Text,urun_fiyati = urun7_fiyati.Text},
                new Urunler { urun_adi= urun8_adi.Text,urun_fiyati = urun8_fiyati.Text},
                new Urunler { urun_adi= urun9_adi.Text,urun_fiyati = urun9_fiyati.Text},
                new Urunler { urun_adi= urun10_adi.Text,urun_fiyati = urun10_fiyati.Text},
                new Urunler { urun_adi= urun11_adi.Text,urun_fiyati = urun11_fiyati.Text},
                new Urunler { urun_adi= urun12_adi.Text,urun_fiyati = urun12_fiyati.Text},
                new Urunler { urun_adi= urun13_adi.Text,urun_fiyati = urun13_fiyati.Text},
                new Urunler { urun_adi= urun14_adi.Text,urun_fiyati = urun14_fiyati.Text},
                new Urunler { urun_adi= urun15_adi.Text,urun_fiyati = urun15_fiyati.Text},
                new Urunler { urun_adi= urun16_adi.Text,urun_fiyati = urun16_fiyati.Text},
                new Urunler { urun_adi= urun17_adi.Text,urun_fiyati = urun17_fiyati.Text},
                new Urunler { urun_adi= urun18_adi.Text,urun_fiyati = urun18_fiyati.Text},
                new Urunler { urun_adi= urun19_adi.Text,urun_fiyati = urun19_fiyati.Text},
                new Urunler { urun_adi= urun20_adi.Text,urun_fiyati = urun20_fiyati.Text},
            };

            toplu_urun_ekle(listem,"Sıcak İçecekler");
          



        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sorgum = "select * From urunler";
            con.Open();

            DataSet dtst = new DataSet();

            SQLiteDataAdapter adtr = new SQLiteDataAdapter(sorgum, con);

            adtr.Fill(dtst, "urunler");

            string urun_Txt = "";

             foreach(DataRow urunlerim in dtst.Tables["urunler"].Rows)
            {
                urun_Txt += urunlerim["urun_kategori"].ToString()+ "#" +urunlerim["urun_adi"].ToString() + "#" + urunlerim["urun_fiyati"].ToString() + "\n";
                
            }
            con.Close();

            string dosya_isim = Application.StartupPath + "/menu.txt";
            txt_olustur(dosya_isim, urun_Txt);

            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        void tab_search(int gelen_adim)
        {
            if(gelen_adim == 1)
            {
                tabControl1.SelectedTab = tabPage1;
            }
            if (gelen_adim == 2)
            {
                tabControl1.SelectedTab = tabPage10;
            }
            if (gelen_adim == 3)
            {
                tabControl1.SelectedTab = tabPage2;
            }
            if (gelen_adim == 4)
            {
                tabControl1.SelectedTab = tabPage3;
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if((tabControl1.SelectedTab == tabPage10 && adim <= 1) || (tabControl1.SelectedTab == tabPage2 && adim <= 2) || (tabControl1.SelectedTab == tabPage3 && adim <= 3) || (tabControl1.SelectedTab == tabPage4 && adim <= 4))
            {
                tab_search(adim);
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void soguk_kaydet_Click(object sender, EventArgs e)
        {
            List<Urunler> listem = new List<Urunler>
            {
                new Urunler { urun_adi= soguk_adi1.Text,urun_fiyati = soguk_fiyat1.Text},
                new Urunler { urun_adi= soguk_adi2.Text,urun_fiyati = soguk_fiyat2.Text},
                new Urunler { urun_adi= soguk_adi3.Text,urun_fiyati = soguk_fiyat3.Text},
                new Urunler { urun_adi= soguk_adi4.Text,urun_fiyati = soguk_fiyat4.Text},
                new Urunler { urun_adi= soguk_adi5.Text,urun_fiyati = soguk_fiyat5.Text},
                new Urunler { urun_adi= soguk_adi6.Text,urun_fiyati = soguk_fiyat6.Text},
                new Urunler { urun_adi= soguk_adi7.Text,urun_fiyati = soguk_fiyat7.Text},
                new Urunler { urun_adi= soguk_adi8.Text,urun_fiyati = soguk_fiyat8.Text},
                new Urunler { urun_adi= soguk_adi9.Text,urun_fiyati = soguk_fiyat9.Text},
                new Urunler { urun_adi= soguk_adi10.Text,urun_fiyati = soguk_fiyat10.Text},
                new Urunler { urun_adi= soguk_adi11.Text,urun_fiyati = soguk_fiyat11.Text},
                new Urunler { urun_adi= soguk_adi12.Text,urun_fiyati = soguk_fiyat12.Text},
                new Urunler { urun_adi= soguk_adi13.Text,urun_fiyati = soguk_fiyat13.Text},
                new Urunler { urun_adi= soguk_adi14.Text,urun_fiyati = soguk_fiyat14.Text},
                new Urunler { urun_adi= soguk_adi15.Text,urun_fiyati = soguk_fiyat15.Text},
                new Urunler { urun_adi= soguk_adi16.Text,urun_fiyati = soguk_fiyat16.Text},
                new Urunler { urun_adi= soguk_adi17.Text,urun_fiyati = soguk_fiyat17.Text},
                new Urunler { urun_adi= soguk_adi18.Text,urun_fiyati = soguk_fiyat18.Text},
                new Urunler { urun_adi= soguk_adi19.Text,urun_fiyati = soguk_fiyat19.Text},
                new Urunler { urun_adi= soguk_adi20.Text,urun_fiyati = soguk_fiyat20.Text},
            };

            toplu_urun_ekle(listem, "Soğuk İçecekler");
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            List<Urunler> listem = new List<Urunler>
            {
                new Urunler { urun_adi= yiyecek_ad1.Text,urun_fiyati = yiyecek_fiyat1.Text},
                new Urunler { urun_adi= yiyecek_ad2.Text,urun_fiyati = yiyecek_fiyat2.Text},
                new Urunler { urun_adi= yiyecek_ad3.Text,urun_fiyati = yiyecek_fiyat3.Text},
                new Urunler { urun_adi= yiyecek_ad4.Text,urun_fiyati = yiyecek_fiyat4.Text},
                new Urunler { urun_adi= yiyecek_ad5.Text,urun_fiyati = yiyecek_fiyat5.Text},
                new Urunler { urun_adi= yiyecek_ad6.Text,urun_fiyati = yiyecek_fiyat6.Text},
                new Urunler { urun_adi= yiyecek_ad7.Text,urun_fiyati = yiyecek_fiyat7.Text},
                new Urunler { urun_adi= yiyecek_ad8.Text,urun_fiyati = yiyecek_fiyat8.Text},
                new Urunler { urun_adi= yiyecek_ad9.Text,urun_fiyati = yiyecek_fiyat9.Text},
                new Urunler { urun_adi= yiyecek_ad10.Text,urun_fiyati = yiyecek_fiyat10.Text},
                new Urunler { urun_adi= yiyecek_ad11.Text,urun_fiyati = yiyecek_fiyat11.Text},
                new Urunler { urun_adi= yiyecek_ad12.Text,urun_fiyati = yiyecek_fiyat12.Text},
                new Urunler { urun_adi= yiyecek_ad13.Text,urun_fiyati = yiyecek_fiyat13.Text},
                new Urunler { urun_adi= yiyecek_ad14.Text,urun_fiyati = yiyecek_fiyat14.Text},
                new Urunler { urun_adi= yiyecek_ad15.Text,urun_fiyati = yiyecek_fiyat15.Text},
                new Urunler { urun_adi= yiyecek_ad16.Text,urun_fiyati = yiyecek_fiyat16.Text},
                new Urunler { urun_adi= yiyecek_ad17.Text,urun_fiyati = yiyecek_fiyat17.Text},
                new Urunler { urun_adi= yiyecek_ad18.Text,urun_fiyati = yiyecek_fiyat18.Text},
                new Urunler { urun_adi= yiyecek_ad19.Text,urun_fiyati = yiyecek_fiyat19.Text},
                new Urunler { urun_adi= yiyecek_ad20.Text,urun_fiyati = yiyecek_fiyat20.Text},

            };

            toplu_urun_ekle(listem, "Yiyecekler");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<Urunler> listem = new List<Urunler>
            {
                new Urunler { urun_adi= tatli_adi1.Text,urun_fiyati = tatli_fiyati1.Text},
                new Urunler { urun_adi= tatli_adi2.Text,urun_fiyati = tatli_fiyati2.Text},
                new Urunler { urun_adi= tatli_adi3.Text,urun_fiyati = tatli_fiyati3.Text},
                new Urunler { urun_adi= tatli_adi4.Text,urun_fiyati = tatli_fiyati4.Text},
                new Urunler { urun_adi= tatli_adi5.Text,urun_fiyati = tatli_fiyati5.Text},
                new Urunler { urun_adi= tatli_adi6.Text,urun_fiyati = tatli_fiyati6.Text},
                new Urunler { urun_adi= tatli_adi7.Text,urun_fiyati = tatli_fiyati7.Text},
                new Urunler { urun_adi= tatli_adi8.Text,urun_fiyati = tatli_fiyati8.Text},
                new Urunler { urun_adi= tatli_adi9.Text,urun_fiyati = tatli_fiyati9.Text},
                new Urunler { urun_adi= tatli_adi10.Text,urun_fiyati = tatli_fiyati10.Text},
                new Urunler { urun_adi= tatli_adi11.Text,urun_fiyati = tatli_fiyati11.Text},
                new Urunler { urun_adi= tatli_adi12.Text,urun_fiyati = tatli_fiyati12.Text},
                new Urunler { urun_adi= tatli_adi13.Text,urun_fiyati = tatli_fiyati13.Text},
                new Urunler { urun_adi= tatli_adi14.Text,urun_fiyati = tatli_fiyati14.Text},
                new Urunler { urun_adi= tatli_adi15.Text,urun_fiyati = tatli_fiyati15.Text},
                new Urunler { urun_adi= tatli_adi16.Text,urun_fiyati = tatli_fiyati16.Text},
                new Urunler { urun_adi= tatli_adi17.Text,urun_fiyati = tatli_fiyati17.Text},
                new Urunler { urun_adi= tatli_adi18.Text,urun_fiyati = tatli_fiyati18.Text},
                new Urunler { urun_adi= tatli_adi19.Text,urun_fiyati = tatli_fiyati19.Text},
                new Urunler { urun_adi= tatli_adi20.Text,urun_fiyati = tatli_fiyati20.Text},

            };

            toplu_urun_ekle(listem, "Tatlılar");
        }

        private void button9_Click(object sender, EventArgs e)
        {

            List<Urunler> listem = new List<Urunler>
            {
                new Urunler { urun_adi= diger_adi1.Text,urun_fiyati = diger_fiyat1.Text},
                new Urunler { urun_adi= diger_adi2.Text,urun_fiyati = diger_fiyat2.Text},
                new Urunler { urun_adi= diger_adi3.Text,urun_fiyati = diger_fiyat3.Text},
                new Urunler { urun_adi= diger_adi4.Text,urun_fiyati = diger_fiyat4.Text},
                new Urunler { urun_adi= diger_adi5.Text,urun_fiyati = diger_fiyat5.Text},
                new Urunler { urun_adi= diger_adi6.Text,urun_fiyati = diger_fiyat6.Text},
                new Urunler { urun_adi= diger_adi7.Text,urun_fiyati = diger_fiyat7.Text},
                new Urunler { urun_adi= diger_adi8.Text,urun_fiyati = diger_fiyat8.Text},
                new Urunler { urun_adi= diger_adi9.Text,urun_fiyati = diger_fiyat9.Text},
                new Urunler { urun_adi= diger_adi10.Text,urun_fiyati = diger_fiyat10.Text},
                new Urunler { urun_adi= diger_adi11.Text,urun_fiyati = diger_fiyat11.Text},
                new Urunler { urun_adi= diger_adi12.Text,urun_fiyati = diger_fiyat12.Text},
                new Urunler { urun_adi= diger_adi13.Text,urun_fiyati = diger_fiyat13.Text},
                new Urunler { urun_adi= diger_adi14.Text,urun_fiyati = diger_fiyat14.Text},
                new Urunler { urun_adi= diger_adi15.Text,urun_fiyati = diger_fiyat15.Text},
                new Urunler { urun_adi= diger_adi16.Text,urun_fiyati = diger_fiyat16.Text},
                new Urunler { urun_adi= diger_adi17.Text,urun_fiyati = diger_fiyat17.Text},
                new Urunler { urun_adi= diger_adi18.Text,urun_fiyati = diger_fiyat18.Text},
                new Urunler { urun_adi= diger_adi19.Text,urun_fiyati = diger_fiyat19.Text},
                new Urunler { urun_adi= diger_adi20.Text,urun_fiyati = diger_fiyat20.Text},

            };

            toplu_urun_ekle(listem, "Diğer");

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void masa_sayisi_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece sayıları ve kontrol tuşlarını kabul et
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string masa_bilgi = "";
            int i = 0;

            // TextBox kontrol elemanlarını diziye al
            var textBoxes = tabPage3.Controls.OfType<TextBox>().ToArray();

            // Diziyi masa numarasına göre sırala
            var sortedTextBoxes = textBoxes.OrderBy(tb => int.Parse(tb.Name.Split('_')[1])).ToArray();

            foreach (var masalar in sortedTextBoxes)
            {
                i++;
                if (i <= masa_sayi) { 
                masa_bilgi += "masa_no#" + i.ToString() + "#masa_isim#" + masalar.Text.ToString() + "\n";
                }
            }

            string dosya_isim = Application.StartupPath + "/masa_bilgi.txt";
            txt_olustur(dosya_isim, masa_bilgi.Trim());
            adim = 5;
            tabControl1.SelectedTab = tabPage4;

            

        }
    }
}
