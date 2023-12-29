using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Runtime.InteropServices.ComTypes;
using System.IO;
using System.Data.SQLite;

namespace mor_adisyon
{
    public partial class analiz : Form
    {
        public analiz()
        {
            InitializeComponent();
        }




        SQLiteConnection con = new SQLiteConnection("Data source=.\\moradisyon.db;Versiyon=3");
        SQLiteDataAdapter da;
        DataSet ds;
      


        string[] urunler = new string[200];
        int[] adetler = new int[200];
        int[] fiyatlar = new int[200];
        int dongu_no = 0;

        private void urun_doldur(DataRow gelen_Veri)
        {

          //  MessageBox.Show(gelen_Veri["hesap_icerik"].ToString());
            StringReader sr = new StringReader(gelen_Veri["hesap_icerik"]?.ToString() ?? string.Empty);
            string siradaki_satir = "";

            siradaki_satir = sr.ReadLine()?.Trim();

            while (!string.IsNullOrEmpty(siradaki_satir) && siradaki_satir.Length > 5)
            {
                string[] parcalar = siradaki_satir.Split('#');

                int index = Array.IndexOf(urunler, parcalar[1]);
                siradaki_satir = sr.ReadLine()?.Trim();
                if(index != -1) { 
               // MessageBox.Show(fiyatlar[index].ToString() + " -- " + parcalar[2]);
                }
                if (index == -1)
                {
                    urunler[dongu_no] = parcalar[1];
                    adetler[dongu_no] = int.Parse(parcalar[3]);
                    fiyatlar[dongu_no] = int.Parse(parcalar[2]);
                }

                //fiyatlar[index] != int.Parse(parcalar[2].ToString())
               if (index != -1 && fiyatlar[index] != int.Parse(parcalar[2]))
                {
                    urunler[dongu_no+20] = parcalar[1];
                    adetler[dongu_no+20] = int.Parse(parcalar[3]);
                    fiyatlar[dongu_no+20] = int.Parse(parcalar[2]);
                }
                else
                {
                    if (index != -1) { 
                    adetler[index] += int.Parse(parcalar[3]);
                    }
                }

                dongu_no++;
            }

        }



        private void analiz_getir()
        {
            listView1.Items.Clear();
            Array.Clear(urunler, 0, urunler.Length);
            Array.Clear(adetler, 0, adetler.Length);
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            // Önce tarihleri uygun biçime dönüştürün
            string formattedStartDate = startDate.ToString("yyyy.MM.dd 00:00:00");
            string formattedEndDate = endDate.ToString("yyyy.MM.dd 23:59:59");

            // SQLiteDataAdapter ile sorgu oluşturun
            da = new SQLiteDataAdapter($"SELECT * FROM adisyonlar WHERE adisyon_tarih BETWEEN '{formattedStartDate}' AND '{formattedEndDate}'", con);



            ds = new DataSet();
            con.Close();
            con.Open();
            da.Fill(ds, "adisyonlar");


            DataTable urunTable = ds.Tables["adisyonlar"];

            foreach (DataRow row in urunTable.Rows)
            {

                urun_doldur(row);


            }


            int urun_dongu = 0;
            ListViewItem item;

            int toplam_kazanc = 0;

            foreach (string x in urunler)
            {
                if (x != null)
                {
                    int kazanc = adetler[urun_dongu] * fiyatlar[urun_dongu];
                    item = new ListViewItem(urunler[urun_dongu]);
                    item.SubItems.Add(fiyatlar[urun_dongu].ToString());
                    item.SubItems.Add(adetler[urun_dongu].ToString());
                    item.SubItems.Add(kazanc.ToString());
                    listView1.Items.Add(item);

                    toplam_kazanc += kazanc;
                }
                urun_dongu++;
            }

            kazanc_label.Text = "Toplam Kazanç: " + toplam_kazanc.ToString();
        }
            private void button1_Click(object sender, EventArgs e)
        {

            analiz_getir();

        }

        private void analiz_Load(object sender, EventArgs e)
        {
            analiz_getir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
