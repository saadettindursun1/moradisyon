using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;


namespace mor_adisyon
{
    public partial class hesapGuncelleme : Form
    {
        public hesapGuncelleme()
        {
            InitializeComponent();
        }




        SQLiteConnection con = new SQLiteConnection("Data source=.\\moradisyon.db;Versiyon=3");

        SQLiteDataAdapter da;
        DataSet ds;




        private void masa_getir()
        {
            da = new SQLiteDataAdapter("SELECT * FROM adisyonlar", con);


            ds = new DataSet();
            con.Close();
            con.Open();
            da.Fill(ds, "adisyonlar");


            DataTable urunTable = ds.Tables["adisyonlar"];


            ListViewItem item;

            foreach (DataRow row in urunTable.Rows)
            {
                item = new ListViewItem(row["adisyon_id"].ToString());
                item.SubItems.Add(row["masa_no"].ToString());
                item.SubItems.Add(row["adisyon_tarih"].ToString());
                item.SubItems.Add(row["tutar"].ToString());
                listView1.Items.Add(item);


            }
        }
        private void hesapGuncelleme_Load(object sender, EventArgs e)
        {
            masa_getir();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id = listView1.SelectedItems[0].SubItems[0].Text;

            guncellemeEkrani g = new guncellemeEkrani();
            g.gelen_id = id;
            g.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
