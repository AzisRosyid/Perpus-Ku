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
    public partial class UserUpdateForm : Form
    {
        public UserUpdateForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        DataClasses1DataContext dc = new DataClasses1DataContext();

        public static int id;

        private void button3_Click(object sender, EventArgs e)
        {
            UserResetPasswordNotification fr = new UserResetPasswordNotification();
            Method.form(fr, this);
        }

        void loadForm()
        {
            var st = dc._users.Where(s => s._user_id == id).FirstOrDefault();
            textBox1.Text = st._identity_number;
            textBox2.Text = st._name;
            textBox3.Text = st._username;
            foreach(var i in Method.userIdentity)
            {
                if(i.Value == st._identity_type)
                {
                    comboBox1.Text = i.Name;
                }
            }
            if(st._level == 3) { checkBox1.Checked = true; checkBox2.Checked = true; }
            else if (st._level == 1) { checkBox1.Checked = true; }
            else if (st._level == 2) { checkBox2.Checked = true; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || !(checkBox1.Checked || checkBox2.Checked)) { IsianKosongErrorNotification fr = new IsianKosongErrorNotification(); Method.form(fr, this); }
            else if (!Method.aNumber(textBox1.Text)) { MessageBox.Show("No. Identitas format must be numeric"); }
            else
            {
                var st = dc._users.Where(s => s._user_id == id).FirstOrDefault();
                st._identity_number = textBox1.Text;
                st._identity_type = Convert.ToInt32(comboBox1.SelectedValue);
                st._name = textBox2.Text;
                st._username = textBox3.Text;
                if (checkBox1.Checked && checkBox2.Checked) { st._level = 3; }
                else if (checkBox1.Checked) { st._level = 1; }
                else if (checkBox2.Checked) { st._level = 2; }
                dc.SubmitChanges();
                this.Close();
            }
        }

        private void UserUpdateForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Method.userIdentity;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Value";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            loadForm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UserHapusNotification fr = new UserHapusNotification();
            Method.form(fr, this);
        }
    }
}
