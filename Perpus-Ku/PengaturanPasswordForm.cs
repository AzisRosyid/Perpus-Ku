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
    public partial class PengaturanPasswordForm : Form
    {
        public PengaturanPasswordForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PengaturanPasswordForm_Load(object sender, EventArgs e)
        {
            textBox1.PasswordChar = '*'; textBox2.PasswordChar = '*'; textBox3.PasswordChar = '*';
        }

        DataClasses1DataContext dc = new DataClasses1DataContext();

        public static bool pengaturan = false;

        private void button2_Click(object sender, EventArgs e)
        {
            var st = dc._users.Where(s => s._user_id == Method.id && s._password == Method.sha(textBox1.Text));
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "") { IsianKosongErrorNotification fr = new IsianKosongErrorNotification(); Method.form(fr, this); }
            else if (!st.Any()) { PengaturanPasswordLamaErrorNotification fr = new PengaturanPasswordLamaErrorNotification(); Method.form(fr, this); }
            else if (textBox2.Text != textBox3.Text) { PengaturanPasswordBaruErrorNotification fr = new PengaturanPasswordBaruErrorNotification(); Method.form(fr, this); }
            else
            {
                if(MessageBox.Show("Apakah Anda yakin akan menggubah password?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var id = st.FirstOrDefault();
                    id._password = Method.sha(textBox2.Text);
                    dc.SubmitChanges();
                    pengaturan = true;
                    this.Owner.Close();
                }
            }
        }
    }
}
