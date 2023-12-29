using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;
using System.Data.SQLite;

namespace mor_adisyon
{
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }

        public class Urunler
        {
            public string urun_adi;
            public string urun_fiyati;
        }


        string[] sicak_urun_isim = new string[20];
        string[] sicak_urun_fiyat = new string[20];
        int sicak_icecek_say = 0;

        string[] soguk_urun_isim = new string[20];
        string[] soguk_urun_fiyat = new string[20];
        int soguk_icecek_say = 0;

        string[] yiyecek_urun_isim = new string[20];
        string[] yiyecek_urun_fiyat = new string[20];
        int yiyecek_say = 0;

        string[] tatli_urun_isim = new string[20];
        string[] tatli_urun_fiyat = new string[20];
        int tatli_say = 0;

        string[] diger_urun_isim = new string[20];
        string[] diger_urun_fiyat = new string[20];
        int diger_say = 0;






        string firma_id = "";
        string firma_bilgileri = "";
        string kullanici = "";
        string parola = "";
        string masalar = "";

        int masa_sayim = 0;


        SQLiteCommand cmd;
        SQLiteConnection con = new SQLiteConnection("Data source=.\\moradisyon.db;Versiyon=3");
        string connectionString = "Data source=.\\moradisyon.db;Versiyon=3";
        SQLiteDataAdapter da;
        DataSet ds;


        private void menu_bilgilerini_doldur()
        {
            string siradaki_satir = "";
            string masa_adi = Application.StartupPath + "/menu.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(masa_adi);

            siradaki_satir = file.ReadLine();



            while (siradaki_satir != null)
            {
                try
                {
                    //satır boş olana kadar okutuyoruz
                    string[] parcalar;
                    parcalar = siradaki_satir.Split('#');
                    siradaki_satir = file.ReadLine();

                    if (parcalar[0] == "Sıcak İçecekler")
                    {
                        sicak_urun_isim[sicak_icecek_say] = parcalar[1].ToString();
                        sicak_urun_fiyat[sicak_icecek_say] = parcalar[2].ToString();


                        sicak_icecek_say++;
                    }

                    if (parcalar[0] == "Soğuk İçecekler")
                    {
                        soguk_urun_isim[soguk_icecek_say] = parcalar[1].ToString();
                        soguk_urun_fiyat[soguk_icecek_say] = parcalar[2].ToString();


                        soguk_icecek_say++;
                    }

                    if (parcalar[0] == "Yiyecekler")
                    {
                        yiyecek_urun_isim[yiyecek_say] = parcalar[1].ToString();
                        yiyecek_urun_fiyat[yiyecek_say] = parcalar[2].ToString();



                        yiyecek_say++;
                    }

                    if (parcalar[0] == "Tatlılar")
                    {
                        tatli_urun_isim[tatli_say] = parcalar[1].ToString();
                        tatli_urun_fiyat[tatli_say] = parcalar[2].ToString();


                        tatli_say++;
                    }

                    if (parcalar[0] == "Diğer")
                    {
                        diger_urun_isim[diger_say] = parcalar[1].ToString();
                        diger_urun_fiyat[diger_say] = parcalar[2].ToString();


                        diger_say++;
                    }

                }


                catch
                {

                }



            }

            file.Close();






            textBox_doldurma(sicak_icecekler, sicak_urun_isim, sicak_urun_fiyat);
            textBox_doldurma(soguk_icecekler, soguk_urun_isim, soguk_urun_fiyat);
            textBox_doldurma(yiyecekler, yiyecek_urun_isim, yiyecek_urun_fiyat);
            textBox_doldurma(tatlilar, tatli_urun_isim, tatli_urun_fiyat);
            textBox_doldurma(diger, diger_urun_isim, diger_urun_fiyat);





        }


        private void textBox_doldurma(TabPage gelen_tab_page, string[] gelen_ad, string[] gelen_fiyat)
        {
            int urun_no2 = 1;
            int isim_say2 = 0;
            int fiyat_say2 = 0;

            var textBoxes2 = gelen_tab_page.Controls.OfType<TextBox>().ToArray();

            // Diziyi isimlere göre sırala
            var sortedTextBoxes2 = textBoxes2.OrderBy(tb => int.Parse(new string(tb.Name.Split('_')[2].SkipWhile(c => !char.IsDigit(c)).ToArray()))).ToArray();


            foreach (var urunler2 in sortedTextBoxes2)
            {
                char ayrac = '_';
                string[] urun_durum2 = urunler2.Name.ToString().Split(ayrac);

                if (urun_no2 <= 42)
                {
                    try
                    {
                        if (urun_durum2[1] == "adi")
                        {
                            urunler2.Text = gelen_ad[isim_say2];
                            isim_say2++;
                        }
                        if (urun_durum2[1] == "fiyat")
                        {
                            urunler2.Text = gelen_fiyat[fiyat_say2];
                            fiyat_say2++;
                        }
                        urun_no2++;
                    }
                    catch { }
                }
            }
        }

        string[] masabilgileri = new string[403];

        private void masa_bilgilerini_doldur()
        {

            string siradaki_satir2 = "";
            string masa_adi2 = Application.StartupPath + "/masa_bilgi.txt";
            System.IO.StreamReader file2 = new System.IO.StreamReader(masa_adi2);

            siradaki_satir2 = file2.ReadLine();


            while (siradaki_satir2 != null)
            {
                //satır boş olana kadar okutuyoruz
                string[] parcalar;
                parcalar = siradaki_satir2.Split('#');
                int masa_dongu_no = int.Parse(parcalar[1].ToString());
                masabilgileri[masa_dongu_no] = parcalar[3].ToString();
                siradaki_satir2 = file2.ReadLine();
                masa_sayim++;

            }

            file2.Close();

        }


        private void verileri_getir()
        {
            da = new SQLiteDataAdapter("Select * from firma_bilgileri", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "firma_bilgileri");

            DataTable firmaTable = ds.Tables["firma_bilgileri"];



            firma_id = firmaTable.Rows[0]["id"].ToString(); // Örneğin, "FirmaAdi" sütununu alıyoruz
            firma_bilgileri = firmaTable.Rows[0]["firma_adi"].ToString(); // Örneğin, "FirmaAdi" sütununu alıyoruz
            kullanici = firmaTable.Rows[0]["kullanici_Adi"].ToString(); // Örneğin, "FirmaAdi" sütununu alıyoruz
            parola = firmaTable.Rows[0]["parola"].ToString(); // Örneğin, "FirmaAdi" sütununu alıyoruz
            masalar = firmaTable.Rows[0]["masa_sayisi"].ToString(); // Örneğin, "FirmaAdi" sütununu alıyoruz


            textBox1.Text = firma_bilgileri.ToString();
            kullaniciText.Text = kullanici.ToString();
            parolaText.Text = parola.ToString();
            masa_sayisi.Text = masalar.ToString();

            int mevcut_masalar = int.Parse(masalar.ToString());
            if (mevcut_masalar < 1 || mevcut_masalar > 42)
            {
                MessageBox.Show("Masa sayısı 1-42 arasında olmalıdır");
            }

            else
            {
                masa_bilgilerini_doldur();


       
                foreach (var x in tabPage3.Controls.OfType<TextBox>())
                {


                    char ayrac = '_';
                    string[] masa_durum = null;

                    masa_durum = x.Name.ToString().Split(ayrac);
                    int masa_analiz = Convert.ToInt16(masa_durum[1].ToString());
                    {
                        if (masa_analiz <= masa_sayim)
                        {
                            x.Visible = true;
                            
                            
                               x.Text = masabilgileri[masa_analiz].ToString();
                            
                        }
                    }

                }

            }
            con.Close();


        }
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Ayarlar_Load(object sender, EventArgs e)
        {

            verileri_getir();
            menu_bilgilerini_doldur();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update firma_bilgileri set masa_sayisi='" + masa_sayisi.Text + "' where id=" + firma_id + "";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Masa sayısı güncellendi.. \n Masa isimlerini güncellemeyi unutmayınız !!!");

            masalar = masa_sayisi.Text;
            con.Close();








            int masa_sayi = Convert.ToInt16(masa_sayisi.Text);
            if (masa_sayi < 1 || masa_sayi > 42)
            {
                MessageBox.Show("Masa sayısı 1-42 arasında olmalıdır");
            }

            else
            {
                foreach (var masalar in tabPage3.Controls.OfType<TextBox>())
                {
                    masalar.Visible = false;
                    char ayrac = '_';
                    string[] masa_durum = null;

                    masa_durum = masalar.Name.ToString().Split(ayrac);
                    int masa_analiz = Convert.ToInt16(masa_durum[1].ToString());
                    if (masa_analiz <= masa_sayi)
                    {
                        masalar.Visible = true;
                    }

                }

            }

            tabControl1.SelectedTab = tabPage3; 

        }

        private void soguk_adi_1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
        }




        private void toplu_urun_ekle(List<Urunler> liste, string kategori)
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
                        int rowCount = (int)cmd2.ExecuteScalar();

                        // MessageBox.Show($"Ürün adı '{x.urun_adi}' içeren satır sayısı: {rowCount}");
                        if (rowCount == 0)
                        {
                            cmd.CommandText = "insert into urunler (urun_adi,urun_fiyati,urun_kategori) values ('" + x.urun_adi + "','" + x.urun_fiyati + "','" + kategori + "')";
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
            }

            con.Close();

        }


        private void sicak_kaydet_Click(object sender, EventArgs e)
        {
            
        }
        private void txt_guncelle()
        {
            string txt_icerik = "";

            da = new SQLiteDataAdapter("Select * from urunler", con);
            ds = new DataSet();
            con.Close();
            con.Open();
            da.Fill(ds, "urunler");


            DataTable urunTable = ds.Tables["urunler"];

            int donguNo = 0;
            foreach (DataRow row in urunTable.Rows)
            {
                string urun_kategori = row["urun_kategori"].ToString();
                string urun_adi = row["urun_adi"].ToString();
                string urun_fiyati = row["urun_fiyati"].ToString();

                txt_icerik += urun_kategori + "#" + urun_adi + "#" + urun_fiyati + "\n";

                donguNo++;
            }
            con.Close();


            string dosyaYolu = Application.StartupPath + "/menu.txt";


            System.IO.File.Delete(dosyaYolu);



            StreamWriter Dosya = File.CreateText(Application.StartupPath+"/menu.txt");
            Dosya.Close();
            StreamWriter Dosya2 = File.AppendText(Application.StartupPath + "/menu.txt");//dosyayı aç
            Dosya2.WriteLine(txt_icerik);//yaz
            Dosya2.Close();





        }

        private void toplu_urun_guncelle(List<Urunler> liste, string kategori)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Close();
                con.Open(); ;

                using (SQLiteTransaction transaction = con.BeginTransaction())
                {
                    // İşlemler buraya gelir

                    transaction.Commit(); // veya transaction.Rollback(); 

                    using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE FROM urunler WHERE urun_kategori = @kategori";
                    cmd.Parameters.AddWithValue("@kategori", kategori);
                    cmd.ExecuteNonQuery();
                }

                foreach (var x in liste)
                {
                    if (!string.IsNullOrEmpty(x.urun_adi) || !string.IsNullOrEmpty(x.urun_fiyati))
                    {
                        using (SQLiteCommand cmd2 = new SQLiteCommand())
                        {
                            cmd2.Connection = con;
                            cmd2.CommandText = "SELECT COUNT(*) FROM urunler WHERE urun_adi = @urunAdi";
                            cmd2.Parameters.AddWithValue("@urunAdi", x.urun_adi);

                            int rowCount = Convert.ToInt32(cmd2.ExecuteScalar());


                            if (rowCount == 0)
                            {
                                using (SQLiteCommand insertCmd = new SQLiteCommand())
                                {
                                    insertCmd.Connection = con;
                                    insertCmd.CommandText = "INSERT INTO urunler (urun_adi, urun_fiyati, urun_kategori) VALUES (@urunAdi, @urunFiyati, @kategori)";
                                    insertCmd.Parameters.AddWithValue("@urunAdi", x.urun_adi);
                                    insertCmd.Parameters.AddWithValue("@urunFiyati", x.urun_fiyati);
                                    insertCmd.Parameters.AddWithValue("@kategori", kategori);
                                    insertCmd.ExecuteNonQuery();

                                    }
                                }
                        }
                    }
                }
                }
            }




        }


        private void soguk_kaydet_Click(object sender, EventArgs e)
        {
            
        }

        private void yiyecek_ekle_Click(object sender, EventArgs e)
        {
         
        }

        private void button8_Click(object sender, EventArgs e)
        {
       
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
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
                if (i <= int.Parse(masa_sayisi.Text))
                {
                    masa_bilgi += "masa_no#" + i.ToString() + "#masa_isim#" + masalar.Text.ToString() + "\n";
                }
            }
            masa_bilgi = masa_bilgi.Trim();



            string dosyaYolu = Application.StartupPath + "/masa_bilgi.txt";


            System.IO.File.Delete(dosyaYolu);



            StreamWriter Dosya = File.CreateText(Application.StartupPath+"/masa_bilgi.txt");
            Dosya.Close();
            StreamWriter Dosya2 = File.AppendText(Application.StartupPath + "/masa_bilgi.txt");//dosyayı aç
            Dosya2.WriteLine(masa_bilgi);//yaz
            Dosya2.Close();

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
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
                if (i <= int.Parse(masa_sayisi.Text))
                {
                    masa_bilgi += "masa_no#" + i.ToString() + "#masa_isim#" + masalar.Text.ToString() + "\n";
                }
            }
            masa_bilgi = masa_bilgi.Trim();



            string dosyaYolu = Application.StartupPath + "/masa_bilgi.txt";


            System.IO.File.Delete(dosyaYolu);



            StreamWriter Dosya = File.CreateText(dosyaYolu);
            Dosya.Close();
            StreamWriter Dosya2 = File.AppendText(dosyaYolu);//dosyayı aç
            Dosya2.WriteLine(masa_bilgi);//yaz
            Dosya2.Close();

        }

        private void sicak_kaydet_Click_1(object sender, EventArgs e)
        {
            List<Urunler> listem = new List<Urunler>
            {
                new Urunler { urun_adi= sicak_adi_1.Text,urun_fiyati = sicak_fiyat_1.Text},
                new Urunler { urun_adi= sicak_adi_2.Text,urun_fiyati = sicak_fiyat_2.Text},
                new Urunler { urun_adi= sicak_adi_3.Text,urun_fiyati = sicak_fiyat_3.Text},
                new Urunler { urun_adi= sicak_adi_4.Text,urun_fiyati = sicak_fiyat_4.Text},
                new Urunler { urun_adi= sicak_adi_5.Text,urun_fiyati = sicak_fiyat_5.Text},
                new Urunler { urun_adi= sicak_adi_6.Text,urun_fiyati = sicak_fiyat_6.Text},
                new Urunler { urun_adi= sicak_adi_7.Text,urun_fiyati = sicak_fiyat_7.Text},
                new Urunler { urun_adi= sicak_adi_8.Text,urun_fiyati = sicak_fiyat_8.Text},
                new Urunler { urun_adi= sicak_adi_9.Text,urun_fiyati = sicak_fiyat_9.Text},
                new Urunler { urun_adi= sicak_adi_10.Text,urun_fiyati = sicak_fiyat_10.Text},
                new Urunler { urun_adi= sicak_adi_11.Text,urun_fiyati = sicak_fiyat_11.Text},
                new Urunler { urun_adi= sicak_adi_12.Text,urun_fiyati = sicak_fiyat_12.Text},
                new Urunler { urun_adi= sicak_adi_13.Text,urun_fiyati = sicak_fiyat_13.Text},
                new Urunler { urun_adi= sicak_adi_14.Text,urun_fiyati = sicak_fiyat_14.Text},
                new Urunler { urun_adi= sicak_adi_15.Text,urun_fiyati = sicak_fiyat_15.Text},
                new Urunler { urun_adi= sicak_adi_16.Text,urun_fiyati = sicak_fiyat_16.Text},
                new Urunler { urun_adi= sicak_adi_17.Text,urun_fiyati = sicak_fiyat_17.Text},
                new Urunler { urun_adi= sicak_adi_18.Text,urun_fiyati = sicak_fiyat_18.Text},
                new Urunler { urun_adi= sicak_adi_19.Text,urun_fiyati = sicak_fiyat_19.Text},
                new Urunler { urun_adi= sicak_adi_20.Text,urun_fiyati = sicak_fiyat_20.Text},

            };

            toplu_urun_guncelle(listem, "Sıcak İçecekler");
            txt_guncelle();

            MessageBox.Show("Menü Güncellendi..");
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            List<Urunler> listem = new List<Urunler>
            {
                new Urunler { urun_adi= diger_adi_1.Text,urun_fiyati = diger_fiyat_1.Text},
                new Urunler { urun_adi= diger_adi_2.Text,urun_fiyati = diger_fiyat_2.Text},
                new Urunler { urun_adi= diger_adi_3.Text,urun_fiyati = diger_fiyat_3.Text},
                new Urunler { urun_adi= diger_adi_4.Text,urun_fiyati = diger_fiyat_4.Text},
                new Urunler { urun_adi= diger_adi_5.Text,urun_fiyati = diger_fiyat_5.Text},
                new Urunler { urun_adi= diger_adi_6.Text,urun_fiyati = diger_fiyat_6.Text},
                new Urunler { urun_adi= diger_adi_7.Text,urun_fiyati = diger_fiyat_7.Text},
                new Urunler { urun_adi= diger_adi_8.Text,urun_fiyati = diger_fiyat_8.Text},
                new Urunler { urun_adi= diger_adi_9.Text,urun_fiyati = diger_fiyat_9.Text},
                new Urunler { urun_adi= diger_adi_10.Text,urun_fiyati = diger_fiyat_10.Text},
                new Urunler { urun_adi= diger_adi_11.Text,urun_fiyati = diger_fiyat_11.Text},
                new Urunler { urun_adi= diger_adi_12.Text,urun_fiyati = diger_fiyat_12.Text},
                new Urunler { urun_adi= diger_adi_13.Text,urun_fiyati = diger_fiyat_13.Text},
                new Urunler { urun_adi= diger_adi_14.Text,urun_fiyati = diger_fiyat_14.Text},
                new Urunler { urun_adi= diger_adi_15.Text,urun_fiyati = diger_fiyat_15.Text},
                new Urunler { urun_adi= diger_adi_16.Text,urun_fiyati = diger_fiyat_16.Text},
                new Urunler { urun_adi= diger_adi_17.Text,urun_fiyati = diger_fiyat_17.Text},
                new Urunler { urun_adi= diger_adi_18.Text,urun_fiyati = diger_fiyat_18.Text},
                new Urunler { urun_adi= diger_adi_19.Text,urun_fiyati = diger_fiyat_19.Text},
                new Urunler { urun_adi= diger_adi_20.Text,urun_fiyati = diger_fiyat_20.Text},



            };

            toplu_urun_guncelle(listem, "Diğer");
            txt_guncelle();
            MessageBox.Show("Menü Güncellendi..");
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            List<Urunler> listem = new List<Urunler>
            {
                new Urunler { urun_adi= tatli_adi_1.Text,urun_fiyati = tatli_fiyat_1.Text},
                new Urunler { urun_adi= tatli_adi_2.Text,urun_fiyati = tatli_fiyat_2.Text},
                new Urunler { urun_adi= tatli_adi_3.Text,urun_fiyati = tatli_fiyat_3.Text},
                new Urunler { urun_adi= tatli_adi_4.Text,urun_fiyati = tatli_fiyat_4.Text},
                new Urunler { urun_adi= tatli_adi_5.Text,urun_fiyati = tatli_fiyat_5.Text},
                new Urunler { urun_adi= tatli_adi_6.Text,urun_fiyati = tatli_fiyat_6.Text},
                new Urunler { urun_adi= tatli_adi_7.Text,urun_fiyati = tatli_fiyat_7.Text},
                new Urunler { urun_adi= tatli_adi_8.Text,urun_fiyati = tatli_fiyat_8.Text},
                new Urunler { urun_adi= tatli_adi_9.Text,urun_fiyati = tatli_fiyat_9.Text},
                new Urunler { urun_adi= tatli_adi_10.Text,urun_fiyati = tatli_fiyat_10.Text},
                new Urunler { urun_adi= tatli_adi_11.Text,urun_fiyati = tatli_fiyat_11.Text},
                new Urunler { urun_adi= tatli_adi_12.Text,urun_fiyati = tatli_fiyat_12.Text},
                new Urunler { urun_adi= tatli_adi_13.Text,urun_fiyati = tatli_fiyat_13.Text},
                new Urunler { urun_adi= tatli_adi_14.Text,urun_fiyati = tatli_fiyat_14.Text},
                new Urunler { urun_adi= tatli_adi_15.Text,urun_fiyati = tatli_fiyat_15.Text},
                new Urunler { urun_adi= tatli_adi_16.Text,urun_fiyati = tatli_fiyat_16.Text},
                new Urunler { urun_adi= tatli_adi_17.Text,urun_fiyati = tatli_fiyat_17.Text},
                new Urunler { urun_adi= tatli_adi_18.Text,urun_fiyati = tatli_fiyat_18.Text},
                new Urunler { urun_adi= tatli_adi_19.Text,urun_fiyati = tatli_fiyat_19.Text},
                new Urunler { urun_adi= tatli_adi_20.Text,urun_fiyati = tatli_fiyat_20.Text},




            };

            toplu_urun_guncelle(listem, "Tatlılar");
            txt_guncelle();
            MessageBox.Show("Menü Güncellendi..");
        }

        private void yiyecek_ekle_Click_1(object sender, EventArgs e)
        {
            List<Urunler> listem = new List<Urunler>
            {
                new Urunler { urun_adi= yiyecek_adi_1.Text,urun_fiyati = yiyecek_fiyat_1.Text},
                new Urunler { urun_adi= yiyecek_adi_2.Text,urun_fiyati = yiyecek_fiyat_2.Text},
                new Urunler { urun_adi= yiyecek_adi_3.Text,urun_fiyati = yiyecek_fiyat_3.Text},
                new Urunler { urun_adi= yiyecek_adi_4.Text,urun_fiyati = yiyecek_fiyat_4.Text},
                new Urunler { urun_adi= yiyecek_adi_5.Text,urun_fiyati = yiyecek_fiyat_5.Text},
                new Urunler { urun_adi= yiyecek_adi_6.Text,urun_fiyati = yiyecek_fiyat_6.Text},
                new Urunler { urun_adi= yiyecek_adi_7.Text,urun_fiyati = yiyecek_fiyat_7.Text},
                new Urunler { urun_adi= yiyecek_adi_8.Text,urun_fiyati = yiyecek_fiyat_8.Text},
                new Urunler { urun_adi= yiyecek_adi_9.Text,urun_fiyati = yiyecek_fiyat_9.Text},
                new Urunler { urun_adi= yiyecek_adi_10.Text,urun_fiyati = yiyecek_fiyat_10.Text},
                new Urunler { urun_adi= yiyecek_adi_11.Text,urun_fiyati = yiyecek_fiyat_11.Text},
                new Urunler { urun_adi= yiyecek_adi_12.Text,urun_fiyati = yiyecek_fiyat_12.Text},
                new Urunler { urun_adi= yiyecek_adi_13.Text,urun_fiyati = yiyecek_fiyat_13.Text},
                new Urunler { urun_adi= yiyecek_adi_14.Text,urun_fiyati = yiyecek_fiyat_14.Text},
                new Urunler { urun_adi= yiyecek_adi_15.Text,urun_fiyati = yiyecek_fiyat_15.Text},
                new Urunler { urun_adi= yiyecek_adi_16.Text,urun_fiyati = yiyecek_fiyat_16.Text},
                new Urunler { urun_adi= yiyecek_adi_17.Text,urun_fiyati = yiyecek_fiyat_17.Text},
                new Urunler { urun_adi= yiyecek_adi_18.Text,urun_fiyati = yiyecek_fiyat_18.Text},
                new Urunler { urun_adi= yiyecek_adi_19.Text,urun_fiyati = yiyecek_fiyat_19.Text},
                new Urunler { urun_adi= yiyecek_adi_20.Text,urun_fiyati = yiyecek_fiyat_20.Text},



            };

            toplu_urun_guncelle(listem, "Yiyecekler");
            txt_guncelle();
            MessageBox.Show("Menü Güncellendi..");
        }

        private void soguk_kaydet_Click_1(object sender, EventArgs e)
        {
            List<Urunler> listem = new List<Urunler>
            {
                new Urunler { urun_adi= soguk_adi_1.Text,urun_fiyati = soguk_fiyat_1.Text},
                new Urunler { urun_adi= soguk_adi_2.Text,urun_fiyati = soguk_fiyat_2.Text},
                new Urunler { urun_adi= soguk_adi_3.Text,urun_fiyati = soguk_fiyat_3.Text},
                new Urunler { urun_adi= soguk_adi_4.Text,urun_fiyati = soguk_fiyat_4.Text},
                new Urunler { urun_adi= soguk_adi_5.Text,urun_fiyati = soguk_fiyat_5.Text},
                new Urunler { urun_adi= soguk_adi_6.Text,urun_fiyati = soguk_fiyat_6.Text},
                new Urunler { urun_adi= soguk_adi_7.Text,urun_fiyati = soguk_fiyat_7.Text},
                new Urunler { urun_adi= soguk_adi_8.Text,urun_fiyati = soguk_fiyat_8.Text},
                new Urunler { urun_adi= soguk_adi_9.Text,urun_fiyati = soguk_fiyat_9.Text},
                new Urunler { urun_adi= soguk_adi_10.Text,urun_fiyati = soguk_fiyat_10.Text},
                new Urunler { urun_adi= soguk_adi_11.Text,urun_fiyati = soguk_fiyat_11.Text},
                new Urunler { urun_adi= soguk_adi_12.Text,urun_fiyati = soguk_fiyat_12.Text},
                new Urunler { urun_adi= soguk_adi_13.Text,urun_fiyati = soguk_fiyat_13.Text},
                new Urunler { urun_adi= soguk_adi_14.Text,urun_fiyati = soguk_fiyat_14.Text},
                new Urunler { urun_adi= soguk_adi_15.Text,urun_fiyati = soguk_fiyat_15.Text},
                new Urunler { urun_adi= soguk_adi_16.Text,urun_fiyati = soguk_fiyat_16.Text},
                new Urunler { urun_adi= soguk_adi_17.Text,urun_fiyati = soguk_fiyat_17.Text},
                new Urunler { urun_adi= soguk_adi_18.Text,urun_fiyati = soguk_fiyat_18.Text},
                new Urunler { urun_adi= soguk_adi_19.Text,urun_fiyati = soguk_fiyat_19.Text},
                new Urunler { urun_adi= soguk_adi_20.Text,urun_fiyati = soguk_fiyat_20.Text},


            };

            toplu_urun_guncelle(listem, "Soğuk İçecekler");
            txt_guncelle();
            MessageBox.Show("Menü Güncellendi..");
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update firma_bilgileri set firma_adi='" + textBox1.Text + "' where id=" + firma_id + "";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Firma adı güncellendi..");
            con.Close();


            string dosyaYolu = Application.StartupPath + "/firma_adi.txt";


            System.IO.File.Delete(dosyaYolu);



            StreamWriter Dosya = File.CreateText(dosyaYolu);
            Dosya.Close();
            StreamWriter Dosya2 = File.AppendText(dosyaYolu);//dosyayı aç
            Dosya2.WriteLine(textBox1.Text);//yaz
            Dosya2.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update firma_bilgileri set kullanici_adi='" + kullaniciText.Text + "',parola='" + parolaText.Text + "' where id=" + firma_id + "";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Giriş bilgileri güncellendi..");
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hesapGuncelleme h = new hesapGuncelleme();
            h.Show();
            this.Hide();
        }
    }
}
