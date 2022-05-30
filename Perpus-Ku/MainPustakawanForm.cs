using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perpus_Ku
{
    public partial class MainPustakawanForm : Form
    {
        public MainPustakawanForm()
        {
            InitializeComponent();
        }

        DataClasses1DataContext dc = new DataClasses1DataContext();

        private void MainPustakawanForm_Load(object sender, EventArgs e)
        {
            var st = dc._users.Where(s => s._user_id == Method.id).FirstOrDefault();
            this.Text = "Perpus-Ku - Selamat Datang, [" + st._name + "]";
            LogOutNotification.signOut = false; PengaturanPasswordForm.pengaturan = false;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss");
        }

        private void MainPustakawanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!LogOutNotification.signOut && !PengaturanPasswordForm.pengaturan)
            {
                ExitBNotification fr = new ExitBNotification();
                Method.form(fr, this);
                e.Cancel = true;
            }
        }

        private void toolStripStatusLabel7_Click(object sender, EventArgs e)
        {
            LogOutNotification fr = new LogOutNotification();
            Method.form(fr, this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PustakawanManajemenAnggotaForm fr = new PustakawanManajemenAnggotaForm();
            Method.form(fr, this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PustakawanDaftarBukuForm fr = new PustakawanDaftarBukuForm();
            Method.form(fr, this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PustakawanSirkulasiForm fr = new PustakawanSirkulasiForm();
            Method.form(fr, this);
        }
    }
}
