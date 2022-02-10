using KaloriUygulama.Data;
using KaloriUygulama.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaloriUygulama
{
    public partial class KullaniciGirisForm : Form
    {
        KaloriUygulamaDbContext db = new KaloriUygulamaDbContext();
        public KullaniciGirisForm()
        {
            InitializeComponent();
            //TODO Açılış ekranı Resimleri
            //TODO timer ile resimleri otomatik geçişli yapabiliriz
            txtEposta.Text = "admin";
            txtParola.Text = "admin";
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string eposta = txtEposta.Text.Trim();
            string parola = txtParola.Text.Trim();

            Kullanici girisYapan = db.Kullanicilar.FirstOrDefault(x => x.Eposta == eposta && x.Parola == parola);

            if (girisYapan == null)
            {
                MessageBox.Show("Kullanıcı E-posta veya parola yanlış");
            }
            else
            {
                UygulamaAnaForm uygulamaAnaForm = new UygulamaAnaForm(db, girisYapan);
                this.Hide();
                uygulamaAnaForm.ShowDialog();
                this.Show();
            }
        }

        private void chkParolaGoster_CheckedChanged(object sender, EventArgs e)
        {
            if (chkParolaGoster.Checked)
            {
                txtParola.PasswordChar = '\0';
            }
            else
            {
                txtParola.PasswordChar = '*';
            }
        }

        private void liblKayitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KullaniciOlusturForm kullaniciOlusturForm = new KullaniciOlusturForm(db);
            kullaniciOlusturForm.ShowDialog();
        }

        private void liblSifremiUnuttum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string eposta = txtEposta.Text.Trim();

            Kullanici girisYapan = db.Kullanicilar.FirstOrDefault(x => x.Eposta == eposta);

            if (girisYapan == null)
            {
                MessageBox.Show("Kullanıcı E-postasını yanlış hatırlıyor olabilirsiniz...");
            }
            else
            {
                SifremiUnuttumForm sifremiUnuttum = new SifremiUnuttumForm(girisYapan);
                sifremiUnuttum.ShowDialog();
            }
        }
    }
}
