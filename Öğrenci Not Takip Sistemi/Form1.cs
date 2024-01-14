using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Öğrenci_Not_Takip_Sistemi
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-44RLCIGQ\SQLEXPRESS05;Initial Catalog=veritabani;Integrated Security=True");

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public static string ogrNo = "";

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            if(txtKullaniciAdi.Text == "0")
            {
                OgretmenBilgileriFormu ogr = new OgretmenBilgileriFormu();
                this.Hide();
                ogr.Show();
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM ogrenciler where numara=@p1";
                cmd.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    ogrNo = dr["numara"].ToString();
                    chartOgrenciNotlari ogrBilgiForm = new chartOgrenciNotlari();
                    this.Hide();
                    ogrBilgiForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Numarası Hatalı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conn.Close();
            }
        }
    }
}
