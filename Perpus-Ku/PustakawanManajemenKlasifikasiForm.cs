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
    public partial class PustakawanManajemenKlasifikasiForm : Form
    {
        public PustakawanManajemenKlasifikasiForm()
        {
            InitializeComponent();
        }

        DataClasses1DataContext dc = new DataClasses1DataContext();

        void loadForm()
        {
            dataGridView1.Rows.Clear(); dataGridView1.Refresh();
            DataClasses1DataContext dc = new DataClasses1DataContext();
            var st = dc._classifications.ToList();
            long z = 0;
            foreach (var i in st)
            {
                dataGridView1.Rows.Add(z + 1, i._id, i._name);
                z++;
            }
        }

        private void PustakawanManajemenKlasifikasiForm_Load(object sender, EventArgs e)
        {
            var list = new List<string> { "No", "Id", "Klasifikasi" };
            foreach (var st in list)
            {
                dataGridView1.Columns.Add(st, st);
            }
            dataGridView1.Columns["Id"].Visible = false; dataGridView1.AllowUserToAddRows = false; dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            loadForm();
        }

        int autoInc()
        {
            var st = dc._classifications.OrderByDescending(s => s._id).Select(s => s._id);
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
            if(textBox1.Text == "") { IsianKosongErrorNotification fr = new IsianKosongErrorNotification(); Method.form(fr, this); }
            else
            {
                DataClasses1DataContext dc = new DataClasses1DataContext();
                var st = new _classification();
                st._id = autoInc();
                st._name = textBox1.Text;
                dc._classifications.InsertOnSubmit(st);
                dc.SubmitChanges();
                textBox1.Text = "";
                loadForm();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PustakawanUbahKlasifikasiForm fr = new PustakawanUbahKlasifikasiForm();
            PustakawanUbahKlasifikasiForm.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
            fr.FormClosed += Fr_FormClosed; Method.form(fr, this);
        }

        private void Fr_FormClosed(object sender, FormClosedEventArgs e)
        {
            loadForm();
        }
    }
}
