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
using static System.Net.Mime.MediaTypeNames;

namespace Proje_Hastane
{
   
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string TCno;

        sqlbaglantisi bgl= new sqlbaglantisi();
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        { 
            MskTC.Text=TCno;
            
            SqlCommand komut = new SqlCommand("Select * from Tbl_Hastalar where HastaTC = @TC",bgl.baglanti());
            komut.Parameters.AddWithValue("@TC",MskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read()) 
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                MskTelefon.Text = dr[4].ToString();
                TxtSifre.Text = dr[5].ToString();
                CmbCinsiyet.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Update Tbl_Hastalar set HastaAd=@ad, HastaSoyad=@soyad, HastaTelefon=@tel, HastaSifre=@sifre, HastaCinsiyet=@cinsiyet where HastaTC=@tc" , bgl.baglanti());
            komut2.Parameters.AddWithValue("@ad", TxtAd.Text);
            komut2.Parameters.AddWithValue("@soyad", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@tel", MskTelefon.Text);
            komut2.Parameters.AddWithValue("@sifre", TxtSifre.Text);
            komut2.Parameters.AddWithValue("@cinsiyet", CmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@tc", MskTC.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

