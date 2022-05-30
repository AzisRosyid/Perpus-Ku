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
    public partial class UserTambahForm : Form
    {
        public UserTambahForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        DataClasses1DataContext dc = new DataClasses1DataContext();

        int autoInc()
        {
            var st = dc._users.OrderByDescending(s => s._user_id).Select(s => s._user_id);
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

        private void UserTambahForm_Load(object sender, EventArgs e)
        {
            textBox4.PasswordChar = '*';
            comboBox1.DataSource = Method.userIdentity;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Value";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || !(checkBox1.Checked || checkBox2.Checked)) { IsianKosongErrorNotification fr = new IsianKosongErrorNotification(); Method.form(fr, this); }
            else if (!Method.aNumber(textBox1.Text)) { MessageBox.Show("No. Identitas format must be numeric"); }
            else
            {
                var st = new _user();
                st._user_id = autoInc();
                st._identity_number = textBox1.Text;
                st._identity_type = Convert.ToInt32(comboBox1.SelectedValue);
                st._name = textBox2.Text;
                st._username = textBox3.Text;
                st._password = textBox4.Text;
                if (checkBox1.Checked && checkBox2.Checked) { st._level = 3; }
                else if (checkBox1.Checked) { st._level = 1; }
                else if (checkBox2.Checked) { st._level = 2; }
                dc._users.InsertOnSubmit(st);
                dc.SubmitChanges();
                this.Close();
            }
        }
    }
}
