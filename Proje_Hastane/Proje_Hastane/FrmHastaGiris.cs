﻿using System;
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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }
        
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit fr = new FrmHastaKayit();
            fr.Show();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From Tbl_Hastalar where HastaTC=@p1 and HastaSifre=@p2", bgl.baglanti()); 
            komut.Parameters .AddWithValue("@p1",MskTC.Text);
            komut.Parameters.AddWithValue("@p2",TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) 
            {
                FrmHastDetay fr = new FrmHastDetay();
                fr.tc=MskTC.Text;
                fr.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Hatalı TC & Şifre");
            }
            bgl.baglanti().Close();
        }

        private void FrmHastaGiris_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
