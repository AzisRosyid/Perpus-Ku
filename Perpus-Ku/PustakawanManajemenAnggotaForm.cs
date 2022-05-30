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
    public partial class PustakawanManajemenAnggotaForm : Form
    {
        public PustakawanManajemenAnggotaForm()
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
            var st = dc._members.Where(s => s._member_id.ToString().Contains(textBox1.Text) || s._identity_number.ToString().Contains(textBox1.Text) || s._registration_date.ToString().Contains(textBox1.Text)).Select(s => s);
            if(comboBox1.Text == "No. Anggota") { st = st.OrderBy(s => s._member_id); }
            else if(comboBox1.Text == "No. Identitas") { st = st.OrderBy(s => s._identity_number); }
            else if(comboBox1.Text == "Jenis Identitas") { st = st.OrderBy(s => s._identity_type); }
            else if(comboBox1.Text == "Nama") { st = st.OrderBy(s => s._name); }
            else if(comboBox1.Text == "Tanggal Registrasi") { st = st.OrderBy(s => s._registration_date); }
            int z = 0;
            foreach (var i in st)
            {
                dataGridView1.Rows.Add(z + 1, i._member_id, i._identity_number, i._identity_type, i._name, i._registration_date.ToString("dd MMMM yyyy"));
                foreach (var j in Method.anggotaIdentity)
                {
                    if (j.Value == i._identity_type)
                    {
                        dataGridView1["Jenis Identitas", z].Value = j.Name;
                    }
                }
                z++;
            }
        }

        private void PustakawanManajemenAnggotaForm_Load(object sender, EventArgs e)
        {
            var list = new List<string>() { "No", "No. Anggota", "No. Identitas", "Jenis Identitas", "Nama", "Tanggal Registrasi" };
            var list2 = new List<string>() { "", "No. Anggota", "No. Identitas", "Jenis Identitas", "Nama", "Tanggal Registrasi" };
            comboBox1.DataSource = list2; comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (var st in list)
            {
                dataGridView1.Columns.Add(st, st);
            }
            dataGridView1.AllowUserToAddRows = false; dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            timer1.Start();
            loadForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; comboBox1.Text = "";
            loadForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PustakawanTambahAnggotaForm fr = new PustakawanTambahAnggotaForm();
            fr.FormClosed += Fr_FormClosed; Method.form(fr, this);
        }

        private void Fr_FormClosed(object sender, FormClosedEventArgs e)
        {
            loadForm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PustakawanUpdateAnggotaForm.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["No. Anggota"].Value.ToString());
            PustakawanUpdateAnggotaForm fr = new PustakawanUpdateAnggotaForm();
            fr.FormClosed += Fr_FormClosed1; Method.form(fr, this);
        }

        private void Fr_FormClosed1(object sender, FormClosedEventArgs e)
        {
            loadForm();
        }
    }
}
