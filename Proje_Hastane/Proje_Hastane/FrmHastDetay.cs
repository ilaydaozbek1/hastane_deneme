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
    public partial class FrmHastDetay : Form
    {
        public FrmHastDetay()
        {
            InitializeComponent();
        }

        public string tc;

        sqlbaglantisi bgl= new sqlbaglantisi();
        private void FrmHastDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            // Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC = @p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read()) 
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //Randevu Geçmişi

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where HastaTC=" +tc,bgl.baglanti());
            da.Fill(dt); //Fill Metodu: SqlDataAdapter nesnesinin Fill metodu, SQL sorgusunun sonuçlarını DataTable nesnesine doldurur.
                         //Bu işlem, DataTable içinde veri tablosunu oluşturur ve veriyi bu tabloya aktarır.
            dataGridView1.DataSource = dt;

            //Branşları Çekme

            SqlCommand komut2 = new SqlCommand("Select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }


        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1 ", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where RandevuBrans='"+ CmbBrans.Text+"'" +"and RandevuDoktor='"+ CmbDoktor.Text+ "'and RandevuDurum=0",bgl.baglanti());
            da.Fill(dt);
            dataGridView2. DataSource = dt; 
        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCno = LblTC.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;  
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();

        }

        private void BtnRnadevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_Randevular set RandevuDurum=1, HastaTC=@htc, HastaSikayet=@hsikayet  where Randevuid=@rid",bgl.baglanti());
            komut.Parameters.AddWithValue("@htc",LblTC.Text);
            komut.Parameters.AddWithValue("@hsikayet",RchSikayet.Text);
            komut.Parameters.AddWithValue("@rid",Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Warning);


            Txtid.Clear();
            RchSikayet.Clear();

            //Randevu Geçmişini Yeniler
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from Tbl_Randevular where HastaTC=" + tc, bgl.baglanti());
            da2.Fill(dt2); 
            dataGridView1.DataSource = dt2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmHastaGiris frm = new FrmHastaGiris();
            frm.Show();
            this.Hide();
        }
    }
}
