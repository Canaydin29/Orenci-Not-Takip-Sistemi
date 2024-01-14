using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using AForge.Video;  //Referansları ekliyoruz
using AForge.Video.DirectShow; //Referansları ekliyoruz


namespace Öğrenci_Not_Takip_Sistemi
{
    public partial class OgretmenBilgileriFormu : Form
    {
        private FilterInfoCollection webcam;//webcam isminde tanımladığımız değişken bilgisayara kaç kamera bağlıysa onları tutan bir dizi. 
        private VideoCaptureDevice cam;//cam ise bizim kullanacağımız aygıt.

        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-44RLCIGQ\SQLEXPRESS05;Initial Catalog=veritabani;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public OgretmenBilgileriFormu()
        {
            InitializeComponent();
        }
      
        private void OgretmenBilgileriFormu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(txtDers1Vize.Text.Length > 0)
            {
                if (double.Parse(txtDers1Vize.Text) > 100)
                {
                    txtDers1Vize.Text = "100";
                }
                if (double.Parse(txtDers1Vize.Text) < 0)
                {
                    txtDers1Vize.Text = "0";
                }
            }

            if (txtDers1Final.Text.Length > 0)
            {
                lblDers1Ortalama.Text = (double.Parse(txtDers1Vize.Text) * 0.4 + double.Parse(txtDers1Final.Text) * 0.6).ToString("0.00");

                if (double.Parse(lblDers1Ortalama.Text) >= 60)
                {
                    lblDers1Ortalama.BackColor = Color.Green;
                }
                else
                {
                    lblDers1Ortalama.BackColor = Color.Red;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (txtDers1Final.Text.Length > 0)
            {
                if (double.Parse(txtDers1Final.Text) > 100)
                {
                    txtDers1Final.Text = "100";
                }
                if (double.Parse(txtDers1Final.Text) < 0)
                {
                    txtDers1Final.Text = "0";
                }
            }

            if (txtDers1Vize.Text.Length > 0)
            {
                lblDers1Ortalama.Text = (double.Parse(txtDers1Vize.Text) * 0.4 + double.Parse(txtDers1Final.Text) * 0.6) .ToString("0.00");

                if (double.Parse(lblDers1Ortalama.Text) >= 60)
                {
                    lblDers1Ortalama.BackColor = Color.Green;
                }
                else
                {
                    lblDers1Ortalama.BackColor = Color.Red;
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtDers2Final_TextChanged(object sender, EventArgs e)
        {
            if (txtDers2Final.Text.Length > 0)
            {
                if (double.Parse(txtDers2Final.Text) > 100)
                {
                    txtDers2Final.Text = "100";
                }
                if (double.Parse(txtDers2Final.Text) < 0)
                {
                    txtDers2Final.Text = "0";
                }
            }

            if (txtDers2Vize.Text.Length > 0)
            {
                lblDers2Ortalama.Text = (double.Parse(txtDers2Vize.Text) * 0.4 + double.Parse(txtDers2Final.Text) * 0.6).ToString("0.00");

                if (double.Parse(lblDers2Ortalama.Text) >= 60)
                {
                    lblDers2Ortalama.BackColor = Color.Green;
                }
                else
                {
                    lblDers2Ortalama.BackColor = Color.Red;
                }
            }
        }

        private void txtDers2Vize_TextChanged(object sender, EventArgs e)
        {
            if (txtDers2Vize.Text.Length > 0)
            {
                if (double.Parse(txtDers2Vize.Text) > 100)
                {
                    txtDers2Vize.Text = "100";
                }
                if (double.Parse(txtDers2Vize.Text) < 0)
                {
                    txtDers2Vize.Text = "0";
                }
            }

            if (txtDers2Final.Text.Length > 0)
            {
                lblDers2Ortalama.Text = (double.Parse(txtDers2Vize.Text) * 0.4 + double.Parse(txtDers2Final.Text) * 0.6).ToString("0.00");

                if (double.Parse(lblDers2Ortalama.Text) >= 60)
                {
                    lblDers2Ortalama.BackColor = Color.Green;
                }
                else
                {
                    lblDers2Ortalama.BackColor = Color.Red;
                }
            }
        }

        private void txtDers3Vize_TextChanged(object sender, EventArgs e)
        {
            if (txtDers3Vize.Text.Length > 0)
            {
                if (double.Parse(txtDers3Vize.Text) > 100)
                {
                    txtDers3Vize.Text = "100";
                }
                if (double.Parse(txtDers3Vize.Text) < 0)
                {
                    txtDers3Vize.Text = "0";
                }
            }

            if (txtDers3Final.Text.Length > 0)
            {
                lblDers3Ortalama.Text = (double.Parse(txtDers3Vize.Text) * 0.4 + double.Parse(txtDers3Final.Text) * 0.6).ToString("0.00");

                if (double.Parse(lblDers3Ortalama.Text) >= 60)
                {
                    lblDers3Ortalama.BackColor = Color.Green;
                }
                else
                {
                    lblDers3Ortalama.BackColor = Color.Red;
                }
            }
        }

        private void txtDers3Final_TextChanged(object sender, EventArgs e)
        {
            if (txtDers3Final.Text.Length > 0)
            {
                if (double.Parse(txtDers3Final.Text) > 100)
                {
                    txtDers3Final.Text = "100";
                }
                if (double.Parse(txtDers3Final.Text) < 0)
                {
                    txtDers3Final.Text = "0";
                }
            }

            if (txtDers3Vize.Text.Length > 0)
            {
                lblDers3Ortalama.Text = (double.Parse(txtDers3Vize.Text) * 0.4 + double.Parse(txtDers3Final.Text) * 0.6).ToString("0.00");

                if (double.Parse(lblDers3Ortalama.Text) >= 60)
                {
                    lblDers3Ortalama.BackColor = Color.Green;
                }
                else
                {
                    lblDers3Ortalama.BackColor = Color.Red;
                }
            }
        }

        private void txtDers4Vize_TextChanged(object sender, EventArgs e)
        {
            if (txtDers4Vize.Text.Length > 0)
            {
                if (double.Parse(txtDers4Vize.Text) > 100)
                {
                    txtDers4Vize.Text = "100";
                }
                if (double.Parse(txtDers4Vize.Text) < 0)
                {
                    txtDers4Vize.Text = "0";
                }
            }

            if (txtDers4final.Text.Length > 0)
            {
                lblDers4Ortalama.Text = (double.Parse(txtDers4Vize.Text) * 0.4 + double.Parse(txtDers4final.Text) * 0.6).ToString("0.00");

                if (double.Parse(lblDers4Ortalama.Text) >= 60)
                {
                    lblDers4Ortalama.BackColor = Color.Green;
                }
                else
                {
                    lblDers4Ortalama.BackColor = Color.Red;
                }
            }
        }

        private void txtDers4final_TextChanged(object sender, EventArgs e)
        {
            if (txtDers4final.Text.Length > 0)
            {
                if (double.Parse(txtDers4final.Text) > 100)
                {
                    txtDers4final.Text = "100";
                }
                if (double.Parse(txtDers4final.Text) < 0)
                {
                    txtDers4final.Text = "0";
                }
            }

            if (txtDers4Vize.Text.Length > 0)
            {
                lblDers4Ortalama.Text = (double.Parse(txtDers4Vize.Text) * 0.4 + double.Parse(txtDers4final.Text) * 0.6).ToString("0.00");

                if (double.Parse(lblDers4Ortalama.Text) >= 60)
                {
                    lblDers4Ortalama.BackColor = Color.Green;
                }
                else
                {
                    lblDers4Ortalama.BackColor = Color.Red;
                }
            }
        }

        private void txtDers5Vize_TextChanged(object sender, EventArgs e)
        {
            if (txtDers5Vize.Text.Length > 0)
            {
                if (double.Parse(txtDers5Vize.Text) > 100)
                {
                    txtDers5Vize.Text = "100";
                }
                if (double.Parse(txtDers5Vize.Text) < 0)
                {
                    txtDers5Vize.Text = "0";
                }
            }

            if (txtDers5Final.Text.Length > 0)
            {
                lblDers5Ortalama.Text = (double.Parse(txtDers5Vize.Text) * 0.4 + double.Parse(txtDers5Final.Text) * 0.6).ToString("0.00");

                if (double.Parse(lblDers5Ortalama.Text) >= 60)
                {
                    lblDers5Ortalama.BackColor = Color.Green;
                }
                else
                {
                    lblDers5Ortalama.BackColor = Color.Red;
                }
            }
        }

        private void txtDers5Final_TextChanged(object sender, EventArgs e)
        {
            if (txtDers5Final.Text.Length > 0)
            {
                if (double.Parse(txtDers5Final.Text) > 100)
                {
                    txtDers5Final.Text = "100";
                }
                if (double.Parse(txtDers5Final.Text) < 0)
                {
                    txtDers5Final.Text = "0";
                }
            }

            if (txtDers5Vize.Text.Length > 0)
            {
                lblDers5Ortalama.Text = (double.Parse(txtDers5Vize.Text) * 0.4 + double.Parse(txtDers5Final.Text) * 0.6).ToString("0.00");
            
                if(double.Parse(lblDers5Ortalama.Text) >= 60)
                {
                    lblDers5Ortalama.BackColor = Color.Green;
                }
                else
                {
                    lblDers5Ortalama.BackColor = Color.Red;
                }
            }
        }

        public int VarMi(string araanan)
        {

            conn.Open();
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select count(numara) as sonuc from ogrenciler where numara=@ara";
            cmd.Parameters.AddWithValue("@ara", araanan);
            

            object sonuc = cmd.ExecuteScalar();

            
           int varmi1=sonuc!=null ? Convert.ToInt32(sonuc) : 0;
            conn.Close();
            return varmi1;
        }

        void Temizle()
        {
            txtOgrAd.Text = "";
            txtOgrSoyad.Text = "";
            txtOgrNumara.Text = "";
            txtTelefon.Text = "";
            txtErkek.Checked = false;
            txtKadin.Checked = false;
            txtSinif.Text = "";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            if (txtErkek.Checked)
            {
                cinsiyet = "Erkek";
            }
            if(txtKadin.Checked)
            {
                cinsiyet = "Kadın";
            }

            if(txtOgrAd.Text != "" && txtOgrSoyad.Text != "" && txtTelefon.Text != "" && cinsiyet != "" && txtOgrNumara.Text != "")
            {
                if(VarMi(txtOgrNumara.Text) != 0)
                {
                    MessageBox.Show("Bu numarada öğrenci kayıtlı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    cmd = new SqlCommand("insert into Ogrenciler(ad, soyad, cinsiyet, telefon, kayit_tarihi, numara, foto_yol, sinif) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", conn);
                    cmd.Parameters.AddWithValue("@p1", txtOgrAd.Text);
                    cmd.Parameters.AddWithValue("@p2", txtOgrSoyad.Text);
                    cmd.Parameters.AddWithValue("@p3", cinsiyet);
                    cmd.Parameters.AddWithValue("@p4", txtTelefon.Text);
                    cmd.Parameters.AddWithValue("@p5", DateTime.Now);
                    cmd.Parameters.AddWithValue("@p6", txtOgrNumara.Text);
                    cmd.Parameters.AddWithValue("@p7", Application.StartupPath + "\\" + txtOgrAd.Text + "." + txtOgrSoyad.Text + "." + txtOgrNumara.Text + ".png");
                    cmd.Parameters.AddWithValue("@p8", txtSinif.Text);

                    cmd.ExecuteNonQuery();
                    pictureBox4.Image.Save(txtOgrAd.Text + "." + txtOgrSoyad.Text + "." + txtOgrNumara.Text + ".png");
                    MessageBox.Show("Ögrenci Başarıyla Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();

                    conn.Open();
                    cmd = new SqlCommand("insert into notlar (ogrenci_numara, Ders1Vize, Ders1Final, Ders1Ort, Ders2Vize, Ders2Final, Ders2Ort," +
                        "Ders3Vize, Ders3Final, Ders3Ort, Ders4Vize, Ders4Final, Ders4Ort, Ders5Vize, Ders5Final, Ders5Ort) values (@p7, @p8," +
                        " @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, @p20, @p21, @p22)", conn);

                    cmd.Parameters.AddWithValue("@p7", txtOgrNumara.Text);
                    cmd.Parameters.AddWithValue("@p8", "0");
                    cmd.Parameters.AddWithValue("@p9", "0");
                    cmd.Parameters.AddWithValue("@p10", "0");
                    cmd.Parameters.AddWithValue("@p11", "0");
                    cmd.Parameters.AddWithValue("@p12", "0");
                    cmd.Parameters.AddWithValue("@p13", "0");
                    cmd.Parameters.AddWithValue("@p14", "0");
                    cmd.Parameters.AddWithValue("@p15", "0");
                    cmd.Parameters.AddWithValue("@p16", "0");
                    cmd.Parameters.AddWithValue("@p17", "0");
                    cmd.Parameters.AddWithValue("@p18", "0");
                    cmd.Parameters.AddWithValue("@p19", "0");
                    cmd.Parameters.AddWithValue("@p20", "0");
                    cmd.Parameters.AddWithValue("@p21", "0");
                    cmd.Parameters.AddWithValue("@p22", "0");
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    Temizle();
                    Listele();
                    Listele1();
                }
            }

            else
            {
                MessageBox.Show("Boş alan bırakmayınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtOgrNumara_TextChanged(object sender, EventArgs e)
        {

        }

        public void Listele()
        {
            cmbOgrenciSec.Items.Clear();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM ogrenciler ORDER BY numara ASC", conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cmbOgrenciSec.Items.Add(dr["numara"] + " - " + dr["ad"] + " " + dr["soyad"]);

            }
            conn.Close();
        }

        private void Listele1()
        {
            listBox1.Items.Clear();

            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT ogrenciler.numara, ogrenciler.ad, ogrenciler.soyad, ogrenciler.cinsiyet, ogrenciler.telefon, ogrenciler.sinif, notlar.Ders1Vize, notlar.Ders1Final, notlar.Ders1Ort, notlar.Ders2Vize, notlar.Ders2Final, notlar.Ders2Ort, notlar.Ders3Vize, notlar.Ders3Final, notlar.Ders3Ort, notlar.Ders4Vize, notlar.Ders4Final, notlar.Ders4Ort, notlar.Ders5Vize, notlar.Ders5Final, notlar.Ders5Ort FROM ogrenciler INNER JOIN notlar ON ogrenciler.numara = notlar.ogrenci_numara ORDER BY ogrenciler.numara ASC", conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string studentInfo = $"{dr["numara"]} - {dr["ad"]} - {dr["soyad"]} - {dr["cinsiyet"]} - {dr["telefon"]} - {dr["sinif"]}";
                string gradesInfo = $"Notlar: Ders1 ({dr["Ders1Vize"]}, {dr["Ders1Final"]}, {dr["Ders1Ort"]}) - Ders2 ({dr["Ders2Vize"]}, {dr["Ders2Final"]}, {dr["Ders2Ort"]}) - Ders3 ({dr["Ders3Vize"]}, {dr["Ders3Final"]}, {dr["Ders3Ort"]}) - Ders4 ({dr["Ders4Vize"]}, {dr["Ders4Final"]}, {dr["Ders4Ort"]}) - Ders5 ({dr["Ders5Vize"]}, {dr["Ders5Final"]}, {dr["Ders5Ort"]})";
                listBox1.Items.Add($"{studentInfo} - {gradesInfo}");
            }

            conn.Close();
        }

        private void OgretmenBilgileriFormu_Load(object sender, EventArgs e)
        {
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);//webcam dizisine mevcut kameraları dolduruyoruz.
            foreach (FilterInfo videocapturedevice in webcam)
            {
                comboBox2.Items.Add(videocapturedevice.Name);//kameraları combobox a dolduruyoruz.
            }
            comboBox2.SelectedIndex = 0; //Comboboxtaki ilk index numaralı kameranın ekranda görünmesi için

            Listele();
            Listele1();
        }

        private void btnKamera_Click(object sender, EventArgs e)
        {
            if (btnKamera.Text == "Kamera Aç")
            {
                cam = new VideoCaptureDevice(webcam[comboBox2.SelectedIndex].MonikerString);
                cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
                cam.Start();

                btnKamera.Text = "Kamera Kapat";
            }
           
            else if(btnKamera.Text == "Kamera Kapat")
            {
                if (cam.IsRunning) //kamera açıksa kapatıyoruz.
                {
                    cam.Stop();
                    pictureBox3.Image = null;
                    btnKamera.Text = "Kamera Aç";
                }
            }
        }
        private void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            pictureBox3.Image = bit;
        }

        private void OgretmenBilgileriFormu_FormClosing(object sender, FormClosingEventArgs e)
        {
         
        }

        private void btnCek_Click(object sender, EventArgs e)
        {
            pictureBox4.Image = pictureBox3.Image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

        private void btnGozat_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.png| Video|*.avi| Tüm Dosyalar |*.*";
            dosya.Title = "Öğrenci Resmi Seç";
            dosya.ShowDialog();
            string DosyaYolu = dosya.FileName;
            pictureBox4.ImageLocation = DosyaYolu;
        }

        private void btnNotVer_Click(object sender, EventArgs e)
        {
            if (cmbOgrenciSec.Text != "")
            {
                string[] ogrNo = cmbOgrenciSec.Text.Split('-');

                conn.Open();
                cmd = new SqlCommand("UPDATE notlar SET Ders1Vize=@p1, Ders1Final=@p2, Ders1Ort=@p3, Ders2Vize=@p4," +
                " Ders2Final=@p5, Ders2Ort=@p6, Ders3Vize=@p7, Ders3Final=@p8, Ders3Ort=@p9, Ders4Vize=@p10, Ders4Final=@p11," +
                " Ders4Ort=@p12, Ders5Vize=@p13, Ders5Final=@p14, Ders5Ort=@p15 WHERE ogrenci_numara=@p16", conn);

                cmd.Parameters.AddWithValue("@p1", txtDers1Vize.Text);
                cmd.Parameters.AddWithValue("@p2", txtDers1Final.Text);
                cmd.Parameters.AddWithValue("@p3", lblDers1Ortalama.Text);
                cmd.Parameters.AddWithValue("@p4", txtDers2Vize.Text);
                cmd.Parameters.AddWithValue("@p5", txtDers2Final.Text);
                cmd.Parameters.AddWithValue("@p6", lblDers2Ortalama.Text);
                cmd.Parameters.AddWithValue("@p7", txtDers3Vize.Text);
                cmd.Parameters.AddWithValue("@p8", txtDers3Final.Text);
                cmd.Parameters.AddWithValue("@p9", lblDers3Ortalama.Text);
                cmd.Parameters.AddWithValue("@p10", txtDers4Vize.Text);
                cmd.Parameters.AddWithValue("@p11", txtDers4final.Text);
                cmd.Parameters.AddWithValue("@p12", lblDers4Ortalama.Text);
                cmd.Parameters.AddWithValue("@p13", txtDers5Vize.Text);
                cmd.Parameters.AddWithValue("@p14", txtDers5Final.Text);
                cmd.Parameters.AddWithValue("@p15", lblDers5Ortalama.Text);
                cmd.Parameters.AddWithValue("@p16", ogrNo[0]);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Ögrenci Notu Başarıyla Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
            }
            else
            {
                MessageBox.Show("Ögrenci Seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnYaniMesaj_Click(object sender, EventArgs e)
        {
            MesajlarForm msj = new MesajlarForm();
            this.Hide();
            msj.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }
    }
}