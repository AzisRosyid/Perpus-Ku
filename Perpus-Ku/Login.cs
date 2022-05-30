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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        void loadForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox1.Text = "string"; textBox2.Text = "string";
        }

        DataClasses1DataContext dc = new DataClasses1DataContext();

        private void button2_Click(object sender, EventArgs e)
        {
            var st = dc._users.Where(s => s._username == textBox1.Text && s._password == Method.sha(textBox2.Text));
            if (textBox1.Text == "" || textBox2.Text == "") { IsianKosongErrorNotification fr = new IsianKosongErrorNotification(); Method.form(fr, this); }
            else if (!st.Any()) { LoginErrorNotification fr = new LoginErrorNotification(); Method.form(fr, this); }
            else
            {
                var id = st.FirstOrDefault(); Method.id = id._user_id;
                if(id._level == 1)
                {
                    MainAdministratorForm fr = new MainAdministratorForm();
                    login(fr);
                }
                else if(id._level == 2)
                {
                    MainPustakawanForm fr = new MainPustakawanForm();
                    login(fr);
                }
                else if(id._level == 3)
                {
                    PilihLevelForm fr = new PilihLevelForm();
                    login(fr); 
                }
            }
        }

        void login(Form fr)
        {
            fr.FormClosed += Fr_FormClosed; this.Hide(); Method.form(fr, this); 
        }

        private void Fr_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KatalogForm fr = new KatalogForm();
            Method.form(fr, this);
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExitANotification fr = new ExitANotification();
            Method.form(fr, this);
            e.Cancel = true;
        }
    }
}
