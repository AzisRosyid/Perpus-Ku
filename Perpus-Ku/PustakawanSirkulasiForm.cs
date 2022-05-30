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
    public partial class PustakawanSirkulasiForm : Form
    {
        public PustakawanSirkulasiForm()
        {
            InitializeComponent();
        }

        void loadForm()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            dataGridView1.Rows.Clear(); dataGridView1.Refresh();
            var st = dc._circulations.Where(s => s._member_id.ToString().Contains(textBox1.Text) || dc._circulation_details.Any(x => x._circulation_id == s._circulation_id && x._isbn.Contains(textBox1.Text)) || s._date_start.ToString().Contains(textBox1.Text) || s._date_finish.ToString().Contains(textBox1.Text) || s._date_return.ToString().Contains(textBox1.Text)).Select(s => s);
            if (comboBox1.Text == "No. Anggota") { st = st.OrderBy(s => s._member_id); }
            else if (comboBox1.Text == "ISBN") { st = st.OrderBy(s => dc._circulation_details.Where(x => x._circulation_id == s._circulation_id).Select(x => x._isbn).FirstOrDefault()); }
            else if (comboBox1.Text == "Tanggal Pinjam") { st = st.OrderBy(s => s._date_start); }
            else if (comboBox1.Text == "Tanggal Kembali") { st = st.OrderBy(s => s._date_finish); }
            else if (comboBox1.Text == "Tanggal Terima") { st = st.OrderBy(s => s._date_return); }
            int z = 0;
            foreach (var i in st)
            {
                dataGridView1.Rows.Add(z + 1, i._circulation_id, i._member_id, dc._circulation_details.Where(s => s._circulation_id == i._circulation_id).Select(s => s._isbn).FirstOrDefault(), i._date_start.ToString("dd MMMM yyyy"), i._date_finish.ToString("dd MMMM yyyy"), i._date_return, i._status);
                if (i._status > i._date_finish)
                {
                    dataGridView1["Status", z].Value = "Terlambat";
                }
                else if (i._status <= i._date_finish)
                {
                    dataGridView1["Status", z].Value = "Kembali";
                }
                if (dataGridView1["Tanggal Terima", z].Value == null) { dataGridView1["Tanggal Terima", z].Value = "-"; dataGridView1["Status", z].Value = "Dalam Pinjaman"; }
                else
                {
                    dataGridView1["Tanggal Terima", z].Value = i._date_return.Value.ToString("dd MMMM yyyy");
                    if (i._date_return > i._date_finish)
                    {
                        dataGridView1["Status", z].Value = "Terlambat";
                    }
                    else if (i._date_return <= i._date_finish)
                    {
                        dataGridView1["Status", z].Value = "Kembali";
                    }
                }
                z++;
            }
        }

        void loadForm2()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            dataGridView1.Rows.Clear(); dataGridView1.Refresh();
            var st = from s in dc._circulations
                     join d in dc._circulation_details on s._circulation_id equals d._circulation_id
                     where s._member_id.ToString().Contains(textBox1.Text) || d._isbn.Contains(textBox1.Text) || s._date_start.ToString().Contains(textBox1.Text) || s._date_finish.ToString().Contains(textBox1.Text) || s._date_return.ToString().Contains(textBox1.Text)
                     select new { s, d };
            if (comboBox1.Text == "No. Anggota") { st = st.OrderBy(s => s.s._member_id); }
            else if (comboBox1.Text == "ISBN") { st = st.OrderBy(s => s.d._isbn); }
            else if (comboBox1.Text == "Tanggal Pinjam") { st = st.OrderBy(s => s.s._date_start); }
            else if (comboBox1.Text == "Tanggal Kembali") { st = st.OrderBy(s => s.s._date_finish); }
            else if (comboBox1.Text == "Tanggal Terima") { st = st.OrderBy(s => s.s._date_return); }
            int z = 0;
            foreach (var i in st)
            {
                dataGridView1.Rows.Add(z + 1, i.s._circulation_id, i.s._member_id, i.d._isbn, i.s._date_start.ToString("dd MMMM yyyy"), i.s._date_finish.ToString("dd MMMM yyyy"), i.s._date_return, i.s._status);
                if (i.s._status > i.s._date_finish)
                {
                    dataGridView1["Status", z].Value = "Terlambat";
                }
                else if (i.s._status <= i.s._date_finish)
                {
                    dataGridView1["Status", z].Value = "Kembali";
                }
                if (dataGridView1["Tanggal Terima", z].Value == null) { dataGridView1["Tanggal Terima", z].Value = "-"; dataGridView1["Status", z].Value = "Dalam Pinjaman"; }
                else 
                { 
                    dataGridView1["Tanggal Terima", z].Value = i.s._date_return.Value.ToString("dd MMMM yyyy");
                    if (i.s._date_return > i.s._date_finish)
                    {
                        dataGridView1["Status", z].Value = "Terlambat";
                    }
                    else if (i.s._date_return <= i.s._date_finish)
                    {
                        dataGridView1["Status", z].Value = "Kembali";
                    }
                }
                z++;
            }
        }

        private void PustakawanSirkulasiForm_Load(object sender, EventArgs e)
        {
            var list = new List<string>() { "No", "Id", "No. Anggota", "ISBN", "Tanggal Pinjam", "Tanggal Kembali", "Tanggal Terima", "Status" };
            var list2 = new List<string>() { "", "No. Anggota", "ISBN", "Tanggal Pinjam", "Tanggal Kembali", "Tanggal Terima" };
            comboBox1.DataSource = list2; comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (var st in list)
            {
                dataGridView1.Columns.Add(st, st);
            }
            dataGridView1.Columns["Id"].Visible = false; dataGridView1.AllowUserToAddRows = false; dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
            PustakawanTambahSirkulasiForm fr = new PustakawanTambahSirkulasiForm();
            fr.FormClosed += Fr_FormClosed; Method.form(fr, this);
        }

        private void Fr_FormClosed(object sender, FormClosedEventArgs e)
        {
            loadForm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PustakawanUbahSirkulasiForm.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
            PustakawanUbahSirkulasiForm fr = new PustakawanUbahSirkulasiForm();
            fr.FormClosed += Fr_FormClosed1; ; Method.form(fr, this);
        }

        private void Fr_FormClosed1(object sender, FormClosedEventArgs e)
        {
            loadForm();
        }
    }
}
