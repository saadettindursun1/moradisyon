using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection.Emit;
using System.Xml;



namespace mor_adisyon
{
    public partial class Form1 : Form
    {

     
        public Form1()
        {
            InitializeComponent();
        }


        public class Urunler
        {
            public string urun_adi { get; set; }
            public string urun_fiyati { get; set; }
            public string urun_adeti { get; set; }
        }

        int masa_sayim=0;
        int dolu_masa_sayi_info = 0;



        public void GuncelleMasaDurumu()
        {
            form_acilis();
            // Burada masa durumunu güncelleyebilirsiniz
            // Örneğin, masa adını ve durumu içeren bir veri yapısı kullanabilirsiniz
            // Ve bu veri yapısını kullanarak masa durumunu güncelleyebilirsiniz
        }
        private bool dolu_goster(string masa_adi2)
        {

            bool donus = false;

            try
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(Application.StartupPath+"/masalar/"+masa_adi2 + ".txt"))
                {
                    string siradaki_satir = file.ReadLine();

                    while (siradaki_satir != null)
                    {
                        string[] parcalar = siradaki_satir.Split('#');

                        // Check if the array has at least 4 elements
                        if (parcalar.Length >= 4)
                        {
                            // Try to parse the value at index 3
                            if (int.TryParse(parcalar[3], out int value))
                            {
                                if (value > 0)
                                {
                                    donus = true;
                                    dolu_masa_sayi_info++;
                                    break; // exit the loop since we found a positive value
                                }
                            }
                        }

                        siradaki_satir = file.ReadLine();
                    }
                }
            }
            catch 
            {
                // Handle the exception (you might want to log it or show a message)
             //   MessageBox.Show("Error reading file: " + ex.Message);
            }

            return donus;
        }




        void txt_olustur(string dosya_adi, string deger)
        {

            StreamWriter Dosya = File.CreateText(dosya_adi);
            Dosya.Close();
            StreamWriter Dosya2 = File.AppendText(dosya_adi);//dosyayı aç
            Dosya2.WriteLine(deger);//yaz
            Dosya2.Close();
            // FileStream fs = new FileStream("masa1.txt", FileMode.Append, FileAccess.Write, FileShare.Write);

        }


        public  void form_acilis()
        {

            System.IO.StreamReader file3 = new System.IO.StreamReader(Application.StartupPath+"/firma_adi.txt");
            string siradaki_satir3 = file3.ReadLine();
            label1.Text = siradaki_satir3;





            string[] masa_isim = new string[42];
            string siradaki_satir = "";
            string masa_adi = "masa_bilgi.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(masa_adi);
            siradaki_satir = file.ReadLine().Trim();


            while (siradaki_satir != null)
            {
                try
                {
                    //satır boş olana kadar okutuyoruz
                    string[] parcalar;
                    parcalar = siradaki_satir.Split('#');
                    siradaki_satir = file.ReadLine()?.Trim();
                    masa_sayim++;

                    masa_isim[masa_sayim] = parcalar[3].ToString();


                }


                catch
                {

                }
            }

            foreach (var masalar in panel1.Controls.OfType<Button>())
            {


                bool sonucum = dolu_goster(masalar.Name.ToString());
                if (sonucum == true)
                {
                    masalar.BackColor = Color.LightGreen;
                }



                char ayrac = '_';
                string[] masa_durum = null;

                masa_durum = masalar.Name.ToString().Split(ayrac);
                int masa_no = Convert.ToInt16(masa_durum[1].ToString());
                if (masa_no <= masa_sayim)
                {
                    masalar.Visible = true;
                    try
                    {
                        masalar.Text = masa_isim[masa_no];
                    }
                    catch { }
                }

            }

            file.Close();
            file3.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            form_acilis();
            dolu_masa_info.Text = "Dolu Masa Sayısı: " + dolu_masa_sayi_info.ToString();


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void masa_1_Click(object sender, EventArgs e)
        {
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                // buttonName'i kullanabilirsiniz

                string[] masa_no = buttonName.Split('_');

                string klasorYolu = Application.StartupPath + "/masalar/";
                string dosyaAdi = buttonName + ".txt";

                string dosyaYolu = Path.Combine(klasorYolu, dosyaAdi);

                if (File.Exists(dosyaYolu))
                {
                }
                else
                {
                    string masa_adi = Application.StartupPath+"/masalar/masa_" + masa_no[1].ToString() + ".txt";

                    string masa_icerik = "";
                    string siradaki_satir = "";
                    System.IO.StreamReader file = new System.IO.StreamReader(Application.StartupPath+"/menu.txt");
                    siradaki_satir = file.ReadLine();


                    while (siradaki_satir != null)
                    {
                       
                            //satır boş olana kadar okutuyoruz
                            masa_icerik +=siradaki_satir + "#0" + "\n";
                        siradaki_satir = file.ReadLine();


                    }
                    file.Close();

                    txt_olustur(masa_adi,masa_icerik);

                }

                adisyon a = new adisyon();
                a.gelen_masa = buttonName;

                a.masa_adi.Text = clickedButton.Text;
                this.Hide();

                a.Show();



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            masa_aktarma m = new masa_aktarma();
            m.masasayi = dolu_masa_sayi_info;
            m.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
              Giris g = new Giris();
               g.adres = "ayarlar";
               g.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Giris g = new Giris();
            g.adres = "analiz";
            g.Show();

            this.Hide();
        }
    }
}
