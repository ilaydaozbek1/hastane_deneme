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

namespace Proje_Hastane
{
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl=new sqlbaglantisi(); 
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Tbl_Doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Branşları comboboxa çekme
            SqlCommand komut = new SqlCommand("Select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                CmbBrans.Items.Add(dr[0]);
            }

        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd, DoktorSoyad, DoktorBrans, DoktorTC, DoktorSifre) values (@drad,@drsoyad,@drbrans,@drtc,@drsifre)",bgl.baglanti());
            komut.Parameters.AddWithValue("drad",TxtAd.Text);
            komut.Parameters.AddWithValue("drsoyad", TxtSoyad.Text);
            komut.Parameters.AddWithValue("drbrans",CmbBrans.Text);
            komut.Parameters.AddWithValue("drtc",MskTC.Text);
            komut.Parameters.AddWithValue("drsifre",TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Doktorlar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from Tbl_Doktorlar where DoktorTC=@drtc",bgl.baglanti());
            komut.Parameters.AddWithValue("@drtc" , MskTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);


            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Doktorlar",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            TxtAd.Clear();
            TxtSoyad.Clear();
            CmbBrans.Items.Clear();
            TxtSifre.Clear();
            MskTC.Clear();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@drad, DoktorSoyad=@drsoyad, DoktorBrans=@drbrans, DoktorSifre=@drsifre where DoktorTC=@drtc",bgl.baglanti());
            komut.Parameters.AddWithValue("drad", TxtAd.Text);
            komut.Parameters.AddWithValue("drsoyad", TxtSoyad.Text);
            komut.Parameters.AddWithValue("drbrans", CmbBrans.Text);
            komut.Parameters.AddWithValue("drtc", MskTC.Text);
            komut.Parameters.AddWithValue("drsifre", TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Doktorlar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
