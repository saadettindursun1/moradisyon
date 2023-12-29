using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SQLite;

namespace mor_adisyon
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        public string adres;


        SQLiteCommand cmd;
        SQLiteDataReader dr;

        private void button5_Click(object sender, EventArgs e)
        {
            string kullanici = kullaniciText.Text;
            string sifre = parolaText.Text;

            using (SQLiteConnection con = new SQLiteConnection("Data source=.\\moradisyon.db;Versiyon=3"))
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM firma_bilgileri WHERE kullanici_adi = @kullanici AND parola = @sifre";
                    cmd.Parameters.AddWithValue("@kullanici", kullanici);
                    cmd.Parameters.AddWithValue("@sifre", sifre);

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (adres == "ayarlar")
                            {
                                Ayarlar ayar = new Ayarlar();
                                ayar.Show();
                            }
                            else if (adres == "analiz")
                            {
                                analiz a = new analiz();
                                a.Show();
                            }
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı ya da şifre yanlış");
                        }
                    }
                }
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
