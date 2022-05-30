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
    public partial class UserManajemenForm : Form
    {
        public UserManajemenForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss");
        }

        void loadForm()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            dataGridView1.Rows.Clear(); dataGridView1.Refresh();
            var st = dc._users.ToList();
            int z = 0;
            foreach(var i in st)
            {
                dataGridView1.Rows.Add(z + 1, i._user_id, i._identity_number, i._identity_type, i._name, i._username, i._level);
                foreach(var j in Method.userIdentity)
                {
                    if(j.Value == i._identity_type)
                    {
                        dataGridView1["Jenis Identitas", z].Value = j.Name;
                    }
                }
                foreach(var j in Method.userLevel)
                {
                    if(j.Value == i._level)
                    {
                        dataGridView1["Level", z].Value = j.Name;
                    }
                }
                z++;
            }
            st.Clear();
        }

        private void UserManajemenForm_Load(object sender, EventArgs e)
        {
            var list = new List<string> { "No", "Id", "No. Identitas", "Jenis Identitas", "Nama", "Username", "Level" };
            foreach (var i in list)
            {
                dataGridView1.Columns.Add(i, i);
            }
            dataGridView1.Columns["Id"].Visible = false; dataGridView1.AllowUserToAddRows = false; dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            timer1.Start();
            loadForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserTambahForm fr = new UserTambahForm();
            fr.FormClosed += Fr_FormClosed;
            Method.form(fr, this);
        }

        private void Fr_FormClosed(object sender, FormClosedEventArgs e)
        {
            loadForm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UserUpdateForm fr = new UserUpdateForm();
            UserUpdateForm.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
            fr.FormClosing += Fr_FormClosing;
            Method.form(fr, this);
        }

        private void Fr_FormClosing(object sender, FormClosingEventArgs e)
        {
            loadForm();
        }
    }
}
