using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Öğrenci_Not_Takip_Sistemi
{
    public partial class chartOgrenciNotlari : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-44RLCIGQ\SQLEXPRESS05;Initial Catalog=veritabani;Integrated Security=True");

        public chartOgrenciNotlari()
        {
            InitializeComponent();
        }

        private void OgrenciBilgileriFormu_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand verioku = new SqlCommand("select * from notlar where ogrenci_numara=@p1", conn);
            verioku.Parameters.AddWithValue("@p1", Form1.ogrNo);
            verioku.ExecuteNonQuery();
            SqlDataReader oku;
            oku = verioku.ExecuteReader();

            while (oku.Read())
            {

                dersBirNot.Text = oku["Ders1Vize"] + " - " + oku["Ders1Final"] + " - " + oku["Ders1Ort"];
                dersIkiNot.Text = oku["Ders2Vize"] + " - " + oku["Ders2Final"] + " - " + oku["Ders2Ort"];
                dersUcNot.Text = oku["Ders3Vize"] + " - " + oku["Ders3Final"] + " - " + oku["Ders3Ort"];
                dersDortNot.Text = oku["Ders4Vize"] + " - " + oku["Ders4Final"] + " - " + oku["Ders4Ort"];
                dersBesNot.Text = oku["Ders5Vize"] + " - " + oku["Ders5Final"] + " - " + oku["Ders5Ort"];

                // Grafik üzerindeki mevcut serileri temizler
                chartOgrenciNotlari1.Series.Clear();

                // Y ekseni (vertical axis) sınırlamasını belirler
                chartOgrenciNotlari1.ChartAreas[0].AxisY.Maximum = 100;

                // Her ders için yeni bir dizi ekler
                AddCourseToChart("Türkçe", Convert.ToDouble(oku["Ders1Ort"]));
                AddCourseToChart("Resim", Convert.ToDouble(oku["Ders2Ort"]));
                AddCourseToChart("Fizik", Convert.ToDouble(oku["Ders3Ort"]));
                AddCourseToChart("Biyoloji", Convert.ToDouble(oku["Ders4Ort"]));
                AddCourseToChart("Felsefe", Convert.ToDouble(oku["Ders5Ort"]));
            }
            oku.Close();
            conn.Close();


            conn.Open();
            SqlCommand verioku2 = new SqlCommand("select * from ogrenciler where numara=@p1", conn);
            verioku2.Parameters.AddWithValue("@p1", Form1.ogrNo);
            verioku2.ExecuteNonQuery();
            SqlDataReader oku2;
            oku2 = verioku2.ExecuteReader();

            while (oku2.Read())
            {

                lblOgrenciAdi.Text = oku2["Ad"] + " " + oku2["Soyad"];
                pictureBox2.ImageLocation = oku2["foto_yol"].ToString();
            }
            oku2.Close();
            conn.Close();
        }

        private void AddCourseToChart(string dersAdi, double ortalamaNot)
        {
            // Grafiğe yeni bir dizi ekler
            var dizi = chartOgrenciNotlari1.Series.Add(dersAdi);

            // Ortalama notu içeren bir veri noktası ekler
            dizi.Points.Add(ortalamaNot);
        }


        private void OgrenciBilgileriFormu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void btnOgretmeneMesajGonder_Click(object sender, EventArgs e)
        {
            MesajGonderForm mj = new MesajGonderForm();
            this.Hide();
            mj.ShowDialog();
        }
    }
}
