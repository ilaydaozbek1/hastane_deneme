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
    public partial class FrmSekreterKayit : Form
    {
        public FrmSekreterKayit()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void BtnKayitYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Sekreter(SekreterAdSoyad,SekreterTC,SekreterSifre)values (@sad,@stc,@ssifre)", bgl.baglanti());
            komut.Parameters.AddWithValue("@sad", TxtAd.Text);
            komut.Parameters.AddWithValue("@stc", MskTC.Text);
            komut.Parameters.AddWithValue("@ssifre", TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Gerçekleşmiştir. Şifreniz:" + TxtSifre.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
