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
    public partial class FrmDoktorBılgıDuzenle : Form
    {
        public FrmDoktorBılgıDuzenle()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        public string drTC;
        private void FrmDoktorBılgıDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = drTC;


            SqlCommand komut = new SqlCommand("Select * from Tbl_Doktorlar where DoktorTC=@drtc" ,bgl.baglanti());
            komut.Parameters.AddWithValue("@drtc",MskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                CmbBrans.Text = dr[3].ToString();
                TxtSifre.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();
        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Doktorlar set DoktorAd= @drad, DoktorSoyad=@drsoyad, DoktorBrans=@drbrans, DoktorSifre=@drsifre where DoktorTc=@drtc",bgl.baglanti());
            komut.Parameters.AddWithValue("@drad", TxtAd.Text);
            komut.Parameters.AddWithValue("@drsoyad", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@drbrans", CmbBrans.Text);
            komut.Parameters.AddWithValue("@drsifre", TxtSifre.Text);
            komut.Parameters.AddWithValue("@drtc", MskTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi");
        }
    }
}
