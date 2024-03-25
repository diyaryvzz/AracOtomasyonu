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

namespace Araç_Otomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglan = new OleDbConnection("provider=microsoft.jet.oledb.4.0; data source=Vt.mdb ");
            baglan.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglan;

            OleDbParameter[] pr = new OleDbParameter[7];
            pr[0] = new OleDbParameter("@aracNo", textBox1.Text);
            pr[1] = new OleDbParameter("@plaka", textBox2.Text);
            pr[2] = new OleDbParameter("@model", textBox3.Text);
            pr[3] = new OleDbParameter("@aracSahibi", textBox4.Text);
            pr[4] = new OleDbParameter("@telNo", textBox5.Text);
            pr[5] = new OleDbParameter("@ucret", textBox6.Text);
            pr[6] = new OleDbParameter("@degisiklik", textBox7.Text);

            komut.Parameters.AddRange(pr);

            komut.CommandText = "insert into AracListesi (aracNo,plaka,model,aracSahibi,telNo,ucret,degisiklik) " +
                "values (@aracNo,@plaka,@model,@aracSahibi,@telNo,@ucret,@degisiklik)";
            komut.ExecuteNonQuery();
            MessageBox.Show("Kaydetme İşlemi Başarılı");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            baglan.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OleDbConnection baglan = new OleDbConnection("provider=microsoft.jet.oledb.4.0; data source=Vt.mdb "))
            {
                baglan.Open();

                using (OleDbCommand komut = new OleDbCommand())
                {
                    komut.Connection = baglan;
                    komut.CommandText = "DELETE FROM aracListesi WHERE aracNo=@aracNo";
                    komut.Parameters.AddWithValue("@aracNo", textBox8.Text);
                    int rowsAffected = komut.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("İlgili Veri Silindi...");

                        textBox8.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Silinecek veri bulunamadı.");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection baglan = new OleDbConnection("provider=microsoft.jet.oledb.4.0; data source=Vt.mdb ");
            baglan.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglan;

            OleDbParameter[] pr = new OleDbParameter[7];
            pr[0] = new OleDbParameter("@aracNo", textBox15.Text);
            pr[1] = new OleDbParameter("@plaka", textBox14.Text);
            pr[2] = new OleDbParameter("@model", textBox13.Text);
            pr[3] = new OleDbParameter("@aracSahibi", textBox12.Text);
            pr[4] = new OleDbParameter("@telNo", textBox11.Text);
            pr[5] = new OleDbParameter("@ucret", textBox10.Text);
            pr[6] = new OleDbParameter("@degisiklik", textBox9.Text);

            komut.Parameters.AddRange(pr);

            komut.CommandText = "update aracListesi set aracNo=@aracNo,plaka=@plaka,model=@model, telNo=@telNo,ucret=@ucret,degisiklik=@degisiklik  where aracNo=@aracNo";
            komut.ExecuteNonQuery();
            MessageBox.Show("Güncelleme İşlemi Başarılı");
            textBox15.Text = "";
            textBox14.Text = "";
            textBox13.Text = "";
            textBox12.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            baglan.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbConnection baglan = new OleDbConnection("provider=microsoft.jet.oledb.4.0; data source=Vt.mdb ");
            baglan.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglan;

            OleDbParameter[] pr = new OleDbParameter[1];
            pr[0] = new OleDbParameter("@aracNo", textBox15.Text);
            komut.Parameters.AddRange(pr);
            komut.CommandText = "select *from aracListesi where aracNo=@aracNo";
            OleDbDataReader oku = default(OleDbDataReader);
            oku = komut.ExecuteReader(CommandBehavior.CloseConnection);
            if (oku.Read())
            {
                textBox15.Text = oku.GetValue(0).ToString();
                textBox14.Text = oku.GetValue(1).ToString();
                textBox13.Text = oku.GetValue(2).ToString();
                textBox12.Text = oku.GetValue(3).ToString();
                textBox11.Text = oku.GetValue(4).ToString();
                textBox10.Text = oku.GetValue(5).ToString();
                textBox9.Text = oku.GetValue(6).ToString();
            }
            else
            {
                MessageBox.Show("Aradığınız Veri Yok");
                textBox15.Text = "";
                textBox14.Text = "";
                textBox13.Text = "";
                textBox12.Text = "";
                textBox11.Text = "";
                textBox10.Text = "";
                textBox9.Text = "";
            }
            baglan.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (OleDbConnection baglan = new OleDbConnection("provider=microsoft.jet.oledb.4.0; data source=Vt.mdb "))
            {
                baglan.Open();

                string query = "SELECT aracNo AS [Araç Numarası], plaka AS Plaka, model AS Model, aracSahibi AS [Araç Sahibi], telNo AS [Telefon Numarası], ucret AS Ücret, degisiklik AS [Neler Değiştirildi] FROM aracListesi";

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, baglan))
                {
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }

                baglan.Close();
            }
        }
    }
}
