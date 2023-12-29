using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace mor_adisyon
{
    public partial class guncellemeEkrani : Form
    {
        public guncellemeEkrani()
        {
            InitializeComponent();
        }


        SQLiteCommand cmd;

        SQLiteConnection con = new SQLiteConnection("Data source=.\\moradisyon.db;Versiyon=3");

        SQLiteDataAdapter da;
        DataSet ds;




        public string gelen_id;

        string hesap="";


        string[] urun_isim = new string[20];
        string[] urun_fiyat = new string[20];
        string[] urun_adet = new string[20];
        int sicak_icecek_say = 0;

        string[] soguk_urun_isim = new string[20];
        string[] soguk_urun_fiyat = new string[20];
        string[] soguk_urun_adet = new string[20];
        int soguk_icecek_say = 0;

        string[] yiyecek_urun_isim = new string[20];
        string[] yiyecek_urun_fiyat = new string[20];
        string[] yiyecek_urun_adet = new string[20];
        int yiyecek_say = 0;

        string[] tatli_urun_isim = new string[20];
        string[] tatli_urun_fiyat = new string[20];
        string[] tatli_urun_adet = new string[20];
        int tatli_say = 0;

        string[] diger_urun_isim = new string[20];
        string[] diger_urun_fiyat = new string[20];
        string[] diger_urun_adet = new string[20];
        int diger_say = 0;


        ListViewItem item;

        private void list_view_doldur(string[] parcalar)
        {
            int tutar = int.Parse(parcalar[2]) * int.Parse(parcalar[3]);
            item = new ListViewItem(parcalar[1]);
            item.SubItems.Add(parcalar[2]);
            item.SubItems.Add(parcalar[3]);
            item.SubItems.Add(tutar.ToString());
            listView1.Items.Add(item);


        }


        private void masa_getir()
        {
            int gelen_idsi = int.Parse(gelen_id);
            da = new SQLiteDataAdapter("SELECT * FROM adisyonlar where adisyon_id="+gelen_idsi, con);


            ds = new DataSet();
            con.Close();
            con.Open();
            da.Fill(ds, "adisyonlar");


            DataTable urunTable = ds.Tables["adisyonlar"];
            hesap = urunTable.Rows[0]["hesap_icerik"].ToString();
            masa_adi.Text = urunTable.Rows[0]["masa_no"].ToString();


            StringReader reader = new StringReader(hesap);
            string siradaki_satir;

            siradaki_satir = reader.ReadLine();



            while (siradaki_satir != null)
            {
                try
                {
                    //satır boş olana kadar okutuyoruz
                    string[] parcalar;
                    parcalar = siradaki_satir.Split('#');
                    siradaki_satir = reader.ReadLine();

                    if (parcalar[0] == "Sıcak İçecekler")
                    {
                        urun_isim[sicak_icecek_say] = parcalar[1].ToString();
                        urun_fiyat[sicak_icecek_say] = parcalar[2].ToString();
                        urun_adet[sicak_icecek_say] = parcalar[3].ToString();

                        int parca_cevir = int.Parse(parcalar[3]);
                        if (parca_cevir > 0)
                        {
                            list_view_doldur(parcalar);
                        }
                        sicak_icecek_say++;
                    }

                    if (parcalar[0] == "Soğuk İçecekler")
                    {
                        soguk_urun_isim[soguk_icecek_say] = parcalar[1].ToString();
                        soguk_urun_fiyat[soguk_icecek_say] = parcalar[2].ToString();
                        soguk_urun_adet[soguk_icecek_say] = parcalar[3].ToString();

                        int parca_cevir = int.Parse(parcalar[3]);
                        if (parca_cevir > 0)
                        {
                            list_view_doldur(parcalar);

                        }
                        soguk_icecek_say++;
                    }

                    if (parcalar[0] == "Yiyecekler")
                    {
                        yiyecek_urun_isim[yiyecek_say] = parcalar[1].ToString();
                        yiyecek_urun_fiyat[yiyecek_say] = parcalar[2].ToString();
                        yiyecek_urun_adet[yiyecek_say] = parcalar[3].ToString();

                        int parca_cevir = int.Parse(parcalar[3]);
                        if (parca_cevir > 0)
                        {
                            list_view_doldur(parcalar);

                        }


                        yiyecek_say++;
                    }

                    if (parcalar[0] == "Tatlılar")
                    {
                        tatli_urun_isim[tatli_say] = parcalar[1].ToString();
                        tatli_urun_fiyat[tatli_say] = parcalar[2].ToString();
                        tatli_urun_adet[tatli_say] = parcalar[3].ToString();

                        int parca_cevir = int.Parse(parcalar[3]);
                        if (parca_cevir > 0)
                        {
                            list_view_doldur(parcalar);

                        }


                        tatli_say++;
                    }

                    if (parcalar[0] == "Diğer")
                    {
                        diger_urun_isim[diger_say] = parcalar[1].ToString();
                        diger_urun_fiyat[diger_say] = parcalar[2].ToString();
                        diger_urun_adet[diger_say] = parcalar[3].ToString();

                        int parca_cevir = int.Parse(parcalar[3]);
                        if (parca_cevir > 0)
                        {
                            list_view_doldur(parcalar);

                        }


                        diger_say++;
                    }

                }


                catch
                {

                }



            }



            // sıcak içecek başlangıç
            foreach (var urunler in tabPage1.Controls.OfType<Label>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= sicak_icecek_say)
                {
                    urunler.Visible = true;
                    try
                    {
                        urunler.Text = urun_isim[urun_no - 1];
                    }
                    catch { }
                }

            }

            foreach (var urunler in tabPage1.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= sicak_icecek_say)
                {
                    urunler.Visible = true;
                    try
                    {
                        urunler.Text = urun_adet[urun_no - 1];
                    }
                    catch { }
                }

            }


            //soğuk içecek başlangıç
            foreach (var urunler in tabPage2.Controls.OfType<Label>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= soguk_icecek_say)
                {
                    urunler.Visible = true;
                    try
                    {
                        urunler.Text = soguk_urun_isim[urun_no - 1];
                    }
                    catch { }
                }

            }


            foreach (var urunler in tabPage2.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= soguk_icecek_say)
                {
                    urunler.Visible = true;
                    try
                    {
                        urunler.Text = soguk_urun_adet[urun_no - 1];
                    }
                    catch { }
                }

            }

            //yiyecekler başlangıç
            foreach (var urunler in tabPage3.Controls.OfType<Label>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= yiyecek_say)
                {
                    urunler.Visible = true;
                    try
                    {
                        urunler.Text = yiyecek_urun_isim[urun_no - 1];
                    }
                    catch { }
                }

            }


            foreach (var urunler in tabPage3.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= yiyecek_say)
                {
                    urunler.Visible = true;
                    try
                    {
                        urunler.Text = yiyecek_urun_adet[urun_no - 1];
                    }
                    catch { }
                }

            }


            //tatlılar başlangıç
            foreach (var urunler in tabPage4.Controls.OfType<Label>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= tatli_say)
                {
                    urunler.Visible = true;
                    try
                    {
                        urunler.Text = tatli_urun_isim[urun_no - 1];
                    }
                    catch { }
                }

            }


            foreach (var urunler in tabPage4.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= tatli_say)
                {
                    urunler.Visible = true;
                    try
                    {
                        urunler.Text = tatli_urun_adet[urun_no - 1];
                    }
                    catch { }
                }

            }


            //diğer başlangıç
            foreach (var urunler in tabPage5.Controls.OfType<Label>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= diger_say)
                {
                    urunler.Visible = true;
                    try
                    {
                        urunler.Text = diger_urun_isim[urun_no - 1];
                    }
                    catch { }
                }

            }


            foreach (var urunler in tabPage5.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= diger_say)
                {
                    urunler.Visible = true;
                    try
                    {
                        urunler.Text = diger_urun_adet[urun_no - 1];
                    }
                    catch { }
                }

            }
        }



        void tutarHesapla()
        {
            listView1.Items.Clear();

            double sicak_tutar = 0;
            double soguk_tutar = 0;
            double yiyecek_tutar = 0;
            double tatli_tutar = 0;
            double diger_tutar = 0;
            double tutar = 0;


            foreach (var urunler in tabPage1.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= sicak_icecek_say)
                {
                    try
                    {
                        double adet_fiyati = Convert.ToInt16(urun_fiyat[urun_no - 1]);
                        double urun_toplam = adet_fiyati * Convert.ToInt16(urunler.Value);

                        if (urunler.Value > 0)
                        {

                            string[] parcalar = { "", urun_isim[urun_no - 1], urun_fiyat[urun_no - 1], urunler.Value.ToString() };
                            list_view_doldur(parcalar);

                        }
                        sicak_tutar += urun_toplam;
                    }
                    catch { }
                }

            }


            foreach (var urunler in tabPage2.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= soguk_icecek_say)
                {
                    try
                    {
                        double adet_fiyati = Convert.ToInt16(soguk_urun_fiyat[urun_no - 1]);
                        double urun_toplam = adet_fiyati * Convert.ToInt16(urunler.Value);


                        if (urunler.Value > 0)
                        {
                            string[] parcalar = { "", soguk_urun_isim[urun_no - 1], soguk_urun_fiyat[urun_no - 1], urunler.Value.ToString() };
                            list_view_doldur(parcalar);

                        }

                        soguk_tutar += urun_toplam;
                    }
                    catch { }
                }

            }


            foreach (var urunler in tabPage3.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= yiyecek_say)
                {
                    try
                    {
                        double adet_fiyati = Convert.ToInt16(yiyecek_urun_fiyat[urun_no - 1]);
                        double urun_toplam = adet_fiyati * Convert.ToInt16(urunler.Value);

                        if (urunler.Value > 0)
                        {
                            string[] parcalar = { "", yiyecek_urun_isim[urun_no - 1], yiyecek_urun_fiyat[urun_no - 1], urunler.Value.ToString() };
                            list_view_doldur(parcalar);

                        }
                        yiyecek_tutar += urun_toplam;
                    }
                    catch { }
                }

            }



            foreach (var urunler in tabPage4.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= tatli_say)
                {
                    try
                    {
                        double adet_fiyati = Convert.ToInt16(tatli_urun_fiyat[urun_no - 1]);
                        double urun_toplam = adet_fiyati * Convert.ToInt16(urunler.Value);

                        if (urunler.Value > 0)
                        {
                            string[] parcalar = { "", tatli_urun_isim[urun_no - 1], tatli_urun_fiyat[urun_no - 1], urunler.Value.ToString() };
                            list_view_doldur(parcalar);

                        }

                        tatli_tutar += urun_toplam;
                    }
                    catch { }
                }

            }


            foreach (var urunler in tabPage5.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= diger_say)
                {
                    try
                    {
                        double adet_fiyati = Convert.ToInt16(diger_urun_fiyat[urun_no - 1]);
                        double urun_toplam = adet_fiyati * Convert.ToInt16(urunler.Value);

                        if (urunler.Value > 0)
                        {
                            string[] parcalar = { "", diger_urun_isim[urun_no - 1], diger_urun_fiyat[urun_no - 1], urunler.Value.ToString() };
                            list_view_doldur(parcalar);

                        }

                        diger_tutar += urun_toplam;
                    }
                    catch { }
                }

            }


            tutar = sicak_tutar + soguk_tutar + yiyecek_tutar + tatli_tutar + diger_tutar;
            tutar_label.Text = tutar.ToString();

        }


        private void guncellemeEkrani_Load(object sender, EventArgs e)
        {
            masa_getir();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }


        private void valuChanged(object sender, EventArgs e)
        {
            tutarHesapla();

        }

        private void icerik_guncelle()
        {

            foreach (var urunler in tabPage1.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= sicak_icecek_say)
                {
                    try
                    {
                        urun_adet[urun_no] = urunler.Value.ToString();


                    }
                    catch { }
                }

            }

            foreach (var urunler in tabPage2.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= soguk_icecek_say)
                {
                    try
                    {
                        soguk_urun_adet[urun_no] = urunler.Value.ToString();


                    }
                    catch { }
                }

            }

            foreach (var urunler in tabPage3.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= yiyecek_say)
                {
                    try
                    {
                        yiyecek_urun_adet[urun_no] = urunler.Value.ToString();


                    }
                    catch { }
                }

            }

            foreach (var urunler in tabPage4.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= tatli_say)
                {
                    try
                    {
                        tatli_urun_adet[urun_no] = urunler.Value.ToString();


                    }
                    catch { }
                }

            }


            foreach (var urunler in tabPage5.Controls.OfType<NumericUpDown>())
            {


                char ayrac = '_';
                string[] urun_durum = null;

                urun_durum = urunler.Name.ToString().Split(ayrac);
                int urun_no = Convert.ToInt16(urun_durum[1].ToString());
                if (urun_no <= diger_say)
                {
                    try
                    {
                        diger_urun_adet[urun_no] = urunler.Value.ToString();


                    }
                    catch { }
                }

            }


            string yeni_veri = "";


            for (int i = 0; i < sicak_icecek_say; i++)
            {
                yeni_veri += "Sıcak İçecekler#" + urun_isim[i].ToString() + "#" + urun_fiyat[i].ToString() + "#" + urun_adet[i + 1].ToString() + "\n";

            }
            for (int i = 0; i < soguk_icecek_say; i++)
            {
                yeni_veri += "Soğuk İçecekler#" + soguk_urun_isim[i].ToString() + "#" + soguk_urun_fiyat[i].ToString() + "#" + soguk_urun_adet[i + 1].ToString() + "\n";

            }

            for (int i = 0; i < yiyecek_say; i++)
            {
                yeni_veri += "Yiyecekler#" + yiyecek_urun_isim[i].ToString() + "#" + yiyecek_urun_fiyat[i].ToString() + "#" + yiyecek_urun_adet[i + 1].ToString() + "\n";

            }

            for (int i = 0; i < tatli_say; i++)
            {
                yeni_veri += "Tatlılar#" + tatli_urun_isim[i].ToString() + "#" + tatli_urun_fiyat[i].ToString() + "#" + tatli_urun_adet[i + 1].ToString() + "\n";

            }

            for (int i = 0; i < diger_say; i++)
            {
                yeni_veri += "Diğer#" + diger_urun_isim[i].ToString() + "#" + diger_urun_fiyat[i].ToString() + "#" + diger_urun_adet[i + 1].ToString() + "\n";

            }

            cmd = new SQLiteCommand();
            con.Close();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update adisyonlar set hesap_icerik='" + yeni_veri + "' ,tutar='" + tutar_label.Text + "' where adisyon_id=" + gelen_id + "";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            icerik_guncelle();
            MessageBox.Show("Adisyon güncellendi..");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            hesapGuncelleme h = new hesapGuncelleme();
            h.Show();
            this.Hide();
        }
    }
}
