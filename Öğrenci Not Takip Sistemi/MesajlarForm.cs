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
    public partial class MesajlarForm : Form
    {
        public MesajlarForm()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-44RLCIGQ\SQLEXPRESS05;Initial Catalog=veritabani;Integrated Security=True");

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OgretmenBilgileriFormu ogr = new OgretmenBilgileriFormu();
            this.Hide();
            ogr.ShowDialog();
        }

        private void MesajlarForm_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SElect * from mesajlar", conn);
            DataSet ds = new DataSet();
            conn.Open();
            da.Fill(ds, "mesaj_konu");
            dataGridView1.DataSource = ds.Tables["mesaj_konu"];
            conn.Close();
        }
    }
}
