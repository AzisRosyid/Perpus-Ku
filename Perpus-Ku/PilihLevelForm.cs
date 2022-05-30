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
    public partial class PilihLevelForm : Form
    {
        public PilihLevelForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                MainAdministratorForm fr = new MainAdministratorForm();
                form(fr);
            }
            else if (radioButton2.Checked)
            {
                MainPustakawanForm fr = new MainPustakawanForm();
                form(fr);
            }
        }

        void form(Form fr)
        {
            fr.FormClosed += Fr_FormClosed; fr.StartPosition = FormStartPosition.CenterScreen; fr.Show(); this.Hide();
        }

        private void Fr_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }
    }
}
