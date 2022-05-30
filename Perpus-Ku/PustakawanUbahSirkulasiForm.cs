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
    public partial class PustakawanUbahSirkulasiForm : Form
    {
        public PustakawanUbahSirkulasiForm()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static int id;

        void loadForm()
        {
            dataGridView1.Rows.Clear(); dataGridView1.Refresh();
            int z = 0;
            foreach (var i in PustakawanHapusBukuDariSirkulasiNotification.book)
            {
                dataGridView1.Rows.Add(z + 1, i);
                dataGridView1[2, z].Value = "Hapus";
                z++;
            }
        }

        private bool rt;

        void loadIn()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            PustakawanHapusBukuDariSirkulasiNotification.book = new List<string>();
            var st = dc._circulations.Where(s => s._circulation_id == id).FirstOrDefault();
            textBox1.Text = st._member_id.ToString();
            foreach(var i in dc._circulation_details.Where(s => s._circulation_id == id))
            {
                PustakawanHapusBukuDariSirkulasiNotification.book.Add(i._isbn);
            }
            dateTimePicker1.Format = DateTimePickerFormat.Custom; dateTimePicker2.Format = DateTimePickerFormat.Custom; dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMMM yyyy"; dateTimePicker2.CustomFormat = "dd MMMM yyyy"; dateTimePicker3.CustomFormat = " ";
            dateTimePicker1.Value = st._date_start;
            dateTimePicker2.Value = st._date_finish;
            rt = false;
            if(st._date_return != null)
            {
                dateTimePicker3.CustomFormat = "dd MMMM yyyy";
                dateTimePicker3.Value = (DateTime)st._date_return;
            }
        }

        private void PustakawanUbahSirkulasiForm_Load(object sender, EventArgs e)
        {
            loadIn();
            var list = new List<string> { "No", "ISBN" };
            foreach (var st in list) { dataGridView1.Columns.Add(st, st); }
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn); dataGridView1.AllowUserToAddRows = false; dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            loadForm();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                PustakawanHapusBukuDariSirkulasiNotification.id = e.RowIndex;
                PustakawanHapusBukuDariSirkulasiNotification fr = new PustakawanHapusBukuDariSirkulasiNotification();
                fr.FormClosed += Fr_FormClosed1; Method.form(fr, this);
            }
        }

        private void Fr_FormClosed1(object sender, FormClosedEventArgs e)
        {
            loadForm();
        }

        int autoInc()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            var st = dc._circulation_details.OrderByDescending(s => s._id).Select(s => s._id);
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

        private void button1_Click(object sender, EventArgs e)
        {
            PustakawanTambahBukuKeSirkulasiForm fr = new PustakawanTambahBukuKeSirkulasiForm();
            fr.FormClosed += Fr_FormClosed; Method.form(fr, this);
        }

        private void Fr_FormClosed(object sender, FormClosedEventArgs e)
        {
            loadForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            if (textBox1.Text == "" || !PustakawanHapusBukuDariSirkulasiNotification.book.Any()) { IsianKosongErrorNotification fr = new IsianKosongErrorNotification(); Method.form(fr, this); }
            else if (!(dateTimePicker1.Value <= dateTimePicker2.Value)) { MessageBox.Show("Tanggal Kembali harus lebih dari Tanggal Pinjam"); }
            else if (!Method.aNumber(textBox1.Text)) { MessageBox.Show("No. Anggota format must be numeric"); }
            else if (!dc._members.Any(s => s._member_id == Convert.ToInt32(textBox1.Text))) { MessageBox.Show("No. Anggota does not valid"); }
            else
            {
                var st = dc._circulations.Where(s => s._circulation_id == id).FirstOrDefault();
                st._member_id = Convert.ToInt32(textBox1.Text);
                st._date_start = dateTimePicker1.Value;
                st._date_finish = dateTimePicker2.Value;
                if (rt)
                {
                    st._date_return = dateTimePicker3.Value;
                }
                st._status = DateTime.Now;
                st._user = Method.id;
                dc.SubmitChanges();
                foreach(var i in dc._circulation_details.Where(s => s._circulation_id == st._circulation_id))
                {
                    var id = dc._books.Where(s => s._isbn == i._isbn).FirstOrDefault();
                    id._amount += 1;
                    dc._circulation_details.DeleteOnSubmit(i);
                    dc.SubmitChanges();
                }
                foreach (var i in PustakawanHapusBukuDariSirkulasiNotification.book)
                {
                    var id = new _circulation_detail();
                    id._id = autoInc();
                    id._circulation_id = st._circulation_id;
                    id._isbn = i;
                    dc._circulation_details.InsertOnSubmit(id);
                    var bk = dc._books.Where(s => s._isbn == i).FirstOrDefault();
                    bk._amount -= 1;
                    dc.SubmitChanges();
                }
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PustakawanHapusSirkulasiNotification fr = new PustakawanHapusSirkulasiNotification();
            Method.form(fr, this);
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            rt = true;
            dateTimePicker3.CustomFormat = "dd MMMM yyyy";
        }
    }
}
