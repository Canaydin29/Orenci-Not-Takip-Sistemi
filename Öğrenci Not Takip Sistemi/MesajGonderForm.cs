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
    public partial class MesajGonderForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-44RLCIGQ\SQLEXPRESS05;Initial Catalog=veritabani;Integrated Security=True");

        public MesajGonderForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            chartOgrenciNotlari pgr = new chartOgrenciNotlari();
            this.Hide();
            pgr.Show();
        }

        private void btnMesajGonder_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into mesajlar (mesaj_konu, mesaj_icerik, mesaj_kimden) values (@p1, @p2, @p3)", conn);
            cmd.Parameters.AddWithValue("@p1", txtKonu.Text);
            cmd.Parameters.AddWithValue("@p2", txtMesaj.Text);
            cmd.Parameters.AddWithValue("@p3", Form1.ogrNo);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Mesaj Başarıyla Gönderildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
        }

        private void MesajGonderForm_Load(object sender, EventArgs e)
        {

        }
    }
}
