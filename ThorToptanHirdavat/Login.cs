using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThorToptanHirdavat
{
    public partial class Login : Form
    {
        bool girisvar = false;
        DataModel dm = new DataModel();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!girisvar)
            {
                Application.Exit();
            }
        }

        private void btn_kapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_giris_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_kullaniciadi.Text))
            {
                if (!string.IsNullOrEmpty(tb_sifre.Text))
                {
                    Manager model = dm.ManagerLogin(tb_kullaniciadi.Text, tb_sifre.Text);
                    if (model.Nickname != null)
                    {
                        Helpers.GirisYapanKullanici = model;
                        girisvar = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Bilgileri Hatalı", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Şifre Boş Bırakılamaz");
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı Boş Bırakılamaz");
            }
        }
    }
}
