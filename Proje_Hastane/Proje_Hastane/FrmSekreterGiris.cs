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
    public partial class FrmSekreterGiris : Form
    {
        sqlbaglantisi bgl=new sqlbaglantisi();
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        private void FrmSekreterGiris_Load(object sender, EventArgs e)
        {

        }
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("Select * from Tbl_Sekreter where SekreterTC=@tc and SekreterSifre =@sifre ", bgl.baglanti());
            komut1.Parameters.AddWithValue("@tc", MskTC.Text);
            komut1.Parameters.AddWithValue("@sifre", TxtSifre.Text);
            SqlDataReader dr = komut1.ExecuteReader();
            if (dr.Read())
            {
                FrmSekreterDetay fr = new FrmSekreterDetay();
                fr.TCnumara = MskTC.Text; 
                fr.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Hatalı TC & Sifre");
            }

            bgl.baglanti().Close();
        }

        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmSekreterKayit frm = new FrmSekreterKayit();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
