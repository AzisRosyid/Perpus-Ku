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
    public partial class PustakawanTambahAnggotaForm : Form
    {
        public PustakawanTambahAnggotaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PustakawanTambahAnggotaForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Method.anggotaIdentity.ToList();
            comboBox1.DisplayMember = "Name"; comboBox1.ValueMember = "Value"; comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        DataClasses1DataContext dc = new DataClasses1DataContext();

        int autoInc()
        {
            var st = dc._members.OrderByDescending(s => s._member_id).Select(s => s._member_id);
            if (st.Any())
            {
                var id = st.FirstOrDefault();
                return id + 1;
            }
            else
            {
                return 1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || comboBox1.Text == "" || textBox2.Text == "") { IsianKosongErrorNotification fr = new IsianKosongErrorNotification(); Method.form(fr, this); }
            else if (!Method.aNumber(textBox1.Text)) { MessageBox.Show("No. Identitas format must be numeric!"); }
            else
            {
                var st = new _member();
                st._member_id = autoInc();
                st._identity_number = textBox1.Text;
                st._identity_type = Convert.ToInt32(comboBox1.SelectedValue);
                st._name = textBox2.Text;
                st._registration_date = DateTime.Now;
                dc._members.InsertOnSubmit(st);
                dc.SubmitChanges();
                this.Close();
            }
        }
    }
}
