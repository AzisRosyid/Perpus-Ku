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
    public partial class PerpustakaanLihatAnggotaForm : Form
    {
        public PerpustakaanLihatAnggotaForm()
        {
            InitializeComponent();
        }

        void loadForm()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            dataGridView1.Rows.Clear(); dataGridView1.Refresh();
            var st = dc._members.ToList();
            int z = 0;
            foreach (var i in st)
            {
                dataGridView1.Rows.Add(z + 1, i._member_id, i._identity_number, i._identity_type, i._name, i._registration_date.ToString("dd MMMM yyyy"));
                foreach(var j in Method.anggotaIdentity)
                {
                    if (j.Value == i._identity_type)
                    {
                        dataGridView1["Jenis Identitas", z].Value = j.Name;
                    }
                }
                z++;
            }
        }

        private void PerpustakaanLihatAnggotaForm_Load(object sender, EventArgs e)
        {
            var list = new List<string>() { "No", "No. Anggota", "No. Identitas", "Jenis Identitas", "Nama", "Tanggal Registrasi" };
            foreach (var st in list)
            {
                dataGridView1.Columns.Add(st, st);
            }
            dataGridView1.AllowUserToAddRows = false; dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            loadForm();
        }
    }
}
