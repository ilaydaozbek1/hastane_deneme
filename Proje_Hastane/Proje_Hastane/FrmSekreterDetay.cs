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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;
        sqlbaglantisi bgl= new sqlbaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = TCnumara; 

            //Ad Soyad

            SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad from Tbl_Sekreter where SekreterTc =@tc",bgl.baglanti());
            komut1.Parameters.AddWithValue("@tc" , LblTC.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while(dr1.Read())
            {
                LblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();


            //Branşları Datagride Aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter("Select * from Tbl_Branslar ",bgl.baglanti());
            dap.Fill(dt1);
            dataGridView1.DataSource = dt1;


            //Doktorları Datagride Aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter dap2 = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as 'Doktorlar', DoktorBrans from Tbl_Doktorlar", bgl.baglanti());
            dap2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşı comboboxa Aktarma
            SqlCommand komut2 = new SqlCommand("Select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@rtarih,@rsaat,@rbrans,@rdoktor)",bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@rtarih", MskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@rsaat", MskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@rbrans",CmbBrans .Text);
            komutkaydet.Parameters.AddWithValue("@rdoktor", CmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu");
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();

            SqlCommand komut3 = new SqlCommand("Select DoktorAd, DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@dbrans",bgl.baglanti());
            komut3.Parameters.AddWithValue("dbrans",CmbBrans.Text);
            SqlDataReader dr = komut3.ExecuteReader();

            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0] + " "+ dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void BtnDuyuruOluştur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@duyuru)",bgl.baglanti());
            komut.Parameters.AddWithValue("@duyuru", RchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru oluşturuldu");

            RchDuyuru.Clear();
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frm = new FrmDoktorPaneli();
            frm.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBransPaneli frm = new FrmBransPaneli();
            frm.Show();
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frm = new FrmRandevuListesi();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris frm = new FrmSekreterGiris();
            frm.Show();
            this.Hide();
        }
    }
}
