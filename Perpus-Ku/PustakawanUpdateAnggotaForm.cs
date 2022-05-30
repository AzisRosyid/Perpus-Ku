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
    public partial class PustakawanUpdateAnggotaForm : Form
    {
        public PustakawanUpdateAnggotaForm()
        {
            InitializeComponent();
        }

        DataClasses1DataContext dc = new DataClasses1DataContext();

        public static int id;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PustakawanHapusAnggotaNotification fr = new PustakawanHapusAnggotaNotification();
            Method.form(fr, this);
        }

        private void PustakawanUpdateAnggotaForm_Load(object sender, EventArgs e)
        {
            var st = dc._members.Where(s => s._member_id == id).FirstOrDefault();
            comboBox1.DataSource = Method.anggotaIdentity.ToList();
            comboBox1.DisplayMember = "Name"; comboBox1.ValueMember = "Value"; comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox1.Text = st._identity_number; comboBox1.Text = Method.anggotaIdentity.Where(s => s.Value == st._identity_type).Select(s => s.Name).FirstOrDefault(); textBox2.Text = st._name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox2.Text == "") { IsianKosongErrorNotification fr = new IsianKosongErrorNotification(); Method.form(fr, this); }
            else if (!Method.aNumber(textBox1.Text)) { MessageBox.Show("No. Identitas format must be numeric!"); }
            else
            {
                var st = dc._members.Where(s => s._member_id == id).FirstOrDefault();
                st._identity_number = textBox1.Text;
                st._identity_type = Convert.ToInt32(comboBox1.SelectedValue);
                st._name = textBox2.Text;
                dc.SubmitChanges();
                this.Close();
            }
        }
    }
}
