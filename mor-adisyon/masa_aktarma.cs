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


namespace mor_adisyon
{
    public partial class masa_aktarma : Form
    {

       public int masasayi;
        public masa_aktarma()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            masa_aktar();
        }


        private void masa_aktar()
        {

            DialogResult result = MessageBox.Show("Masa aktarımını onaylıyor musunuz?", "Onaylama Ekranı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kullanıcı "Evet" butonuna tıklarsa
            if (result == DialogResult.Yes)
            {
                
                int index = Array.IndexOf(masa_isimleri, comboBox1.Text);

                int index2 = Array.IndexOf(masa_isimleri, comboBox2.Text);


                string klasorYolu = Application.StartupPath + "/masalar/";

                string aktarilacak_yolu = dosya_isimleri[index];

                string aktarilan_yolu = dosya_isimleri[index2];
                string dosyaYolu = Path.Combine(klasorYolu, aktarilan_yolu);



                string yolumuz = Application.StartupPath + "/masalar/" + aktarilacak_yolu;
                string yeni_yolumuz = Application.StartupPath + "/masalar/" + aktarilan_yolu;
                string dosyaIcerigi = File.ReadAllText(yolumuz).Trim();


                if(comboBox1.Text != comboBox2.Text) { 

                if (File.Exists(dosyaYolu))
                {
                    string dosyaIcerigi2 = File.ReadAllText(yeni_yolumuz).Trim();

                    string[] icerikOku = dosyaIcerigi.Split('\n');
                    string[] icerikOku2 = dosyaIcerigi2.Split('\n');

                    if (icerikOku.Length == icerikOku2.Length)
                    {

                        for (int i = 0; i < icerikOku.Length; i++)
                        {
                            string[] parcalar = icerikOku[i].Split('#');
                            string[] parcalar2 = icerikOku2[i].Split('#');
                            int toplam = int.Parse(parcalar[3]) + int.Parse(parcalar2[3]);

                            icerikOku2[i] = parcalar2[0] + "#" + parcalar2[1] + "#" + parcalar2[2] + "#" + toplam.ToString();
                        }



                    }


                    System.IO.File.Delete(yolumuz);
                    System.IO.File.Delete(yeni_yolumuz);


                    string yazilacak_icerik = "";
                    foreach (string x in icerikOku2)
                    {
                        yazilacak_icerik += x + "\n";
                    }

                    StreamWriter Dosya = File.CreateText(yeni_yolumuz);
                    Dosya.Close();
                    StreamWriter Dosya2 = File.AppendText(yeni_yolumuz);//dosyayı aç
                    Dosya2.WriteLine(yazilacak_icerik);//yaz
                    Dosya2.Close();
                    // masa varsa
                }

                else
                {




                    System.IO.File.Delete(yolumuz);
                    StreamWriter Dosya = File.CreateText(yeni_yolumuz);
                    Dosya.Close();
                    StreamWriter Dosya2 = File.AppendText(yeni_yolumuz);//dosyayı aç
                    Dosya2.WriteLine(dosyaIcerigi);//yaz
                    Dosya2.Close();
                    //masa yoksa
                }

                MessageBox.Show("Masa Aktarma Başarılı..");

                masa_aktarma m = new masa_aktarma();
                m.Show();
                this.Hide();
                }
                else
                {
                    MessageBox.Show("Seçtiğiniz iki masa aynı olamaz!");
                }
            }
            else
            {
                // Kullanıcı "Hayır" butonuna tıklarsa veya pencereyi kapatırsa
            }




           
        }


        string[] masa_isimleri = new string[42];
        string[] dosya_isimleri = new string[42];

        private void dolu_masalar()
        {

            int isim_say = 0;
            string siradaki_satir = "";
            string masa_adi =Application.StartupPath+ "/masa_bilgi.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(masa_adi);
            siradaki_satir = file.ReadLine();
                while(siradaki_satir != null)
                {
                
                //satır boş olana kadar okutuyoruz
                string[] parcalar;
                parcalar = siradaki_satir.Split('#');
                masa_isimleri[isim_say] = parcalar[3];
                siradaki_satir = file.ReadLine();


                string klasorYolu = Application.StartupPath + "/masalar/";
                string dosyaAdi = "masa_" + (parcalar[1]) + ".txt";

                string dosyaYolu = Path.Combine(klasorYolu, dosyaAdi);


                if (File.Exists(dosyaYolu))
                {
                    int adet_say = 0;
                    string siradaki_satir2 = "";
                    string masa_adi2 = Application.StartupPath + "/masalar/masa_" + parcalar[1]+".txt";
                    System.IO.StreamReader file2 = new System.IO.StreamReader(masa_adi2);
                    siradaki_satir2 = file2.ReadLine();
                    while(siradaki_satir2 != null)
                    {
                     
                            string[] parcalar2;
                            parcalar2 = siradaki_satir2.Split('#');
                        if (parcalar2.Length > 3) { 
                            if (siradaki_satir != "")
                            {
                                adet_say += int.Parse(parcalar2[3]);
                            }
                        }
                        siradaki_satir2 = file2.ReadLine();


                    }
                    if (adet_say > 0) { 
                    comboBox1.Items.Add(parcalar[3]);
                    }
                    
                        comboBox2.Items.Add(parcalar[3]);

                    file2.Close();
                    

                }
                else
                {
                    comboBox2.Items.Add(parcalar[3]);
                }
                dosya_isimleri[isim_say] = "masa_" + (isim_say + 1).ToString()+ ".txt";


                isim_say++;
              
            }

            try
            {
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
            catch
            {

            }


            file.Close();
        




        }
        private void masa_aktarma_Load(object sender, EventArgs e)
        {
            dolu_masalar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
