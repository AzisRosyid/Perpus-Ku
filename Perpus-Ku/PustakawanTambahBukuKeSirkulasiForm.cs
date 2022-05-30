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
    public partial class PustakawanTambahBukuKeSirkulasiForm : Form
    {
        public PustakawanTambahBukuKeSirkulasiForm()
        {
            InitializeComponent();
        }

        void loadForm()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            dataGridView1.Rows.Clear(); dataGridView1.Refresh();
            var st = from s in dc._books
                     join c in dc._classifications on s._classification_id equals c._id
                     where s._isbn.Contains(textBox1.Text) || s._title.Contains(textBox1.Text) || s._author.Contains(textBox1.Text) || s._year.ToString().Contains(textBox1.Text) || s._publisher.ToString().Contains(textBox1.Text) || c._name.Contains(textBox1.Text) || s._location.Contains(textBox1.Text) || s._amount.ToString().Contains(textBox1.Text)
                     select new { s, c };
            if (comboBox1.Text == "ISBN") { st = st.OrderBy(s => s.s._isbn); }
            else if (comboBox1.Text == "Judul Buku") { st = st.OrderBy(s => s.s._title); }
            else if (comboBox1.Text == "Penulis") { st = st.OrderBy(s => s.s._author); }
            else if (comboBox1.Text == "Tahun") { st = st.OrderBy(s => s.s._year); }
            else if (comboBox1.Text == "Penerbit") { st = st.OrderBy(s => s.s._publisher); }
            else if (comboBox1.Text == "Klasifikasi") { st = st.OrderBy(s => s.c._name); }
            else if (comboBox1.Text == "Lokasi") { st = st.OrderBy(s => s.s._location); }
            else if (comboBox1.Text == "Jumlah") { st = st.OrderBy(s => s.s._amount); }
            int z = 0;
            foreach (var i in st)
            {
                dataGridView1.Rows.Add(z + 1, i.s._isbn, i.s._title, i.s._author, i.s._year, i.s._publisher, i.c._name, i.s._location, i.s._amount, i.s._pdf);
                if (dataGridView1["PDF", z].Value != null) { dataGridView1["PDF", z].Value = "Tersedia"; } else { dataGridView1["PDF", z].Value = "Tidak Tersedia"; }
                z++;
            }
        }

        private void PustakawanTambahBukuKeSirkulasiForm_Load(object sender, EventArgs e)
        {
            var list = new List<string>() { "No", "ISBN", "Judul Buku", "Penulis", "Tahun", "Penerbit", "Klasifikasi", "Lokasi", "Jumlah", "PDF" };
            var list2 = new List<string>() { "", "ISBN", "Judul Buku", "Penulis", "Tahun", "Penerbit", "Klasifikasi", "Lokasi", "Jumlah" };
            comboBox1.DataSource = list2; comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (var st in list)
            {
                dataGridView1.Columns.Add(st, st);
            }
            dataGridView1.AllowUserToAddRows = false; dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToInt32(dataGridView1.CurrentRow.Cells["Jumlah"].Value.ToString()) <= 0) { MessageBox.Show("Buku yang dipilih harus memiliki jumlah minimal 1"); }
            else
            {
                PustakawanHapusBukuDariSirkulasiNotification.book.Add(dataGridView1.CurrentRow.Cells["ISBN"].Value.ToString());
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PustakawanTambahBukuForm fr = new PustakawanTambahBukuForm();
            fr.FormClosed += Fr_FormClosed; Method.form(fr, this);
        }

        private void Fr_FormClosed(object sender, FormClosedEventArgs e)
        {
            loadForm();
        }
    }
}
