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
    public partial class PustakawanUbahKlasifikasiForm : Form
    {
        public PustakawanUbahKlasifikasiForm()
        {
            InitializeComponent();
        }

        public static int id;

        DataClasses1DataContext dc = new DataClasses1DataContext();

        private void PustakawanUbahKlasifikasiForm_Load(object sender, EventArgs e)
        {
            var st = dc._classifications.Where(s => s._id == id).FirstOrDefault();
            textBox1.Text = st._name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "") { IsianKosongErrorNotification fr = new IsianKosongErrorNotification(); Method.form(fr, this); }
            else
            {
                var st = dc._classifications.Where(s => s._id == id).FirstOrDefault();
                st._name = textBox1.Text;
                dc.SubmitChanges();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PustakawanHapusKlasifikasiForm fr = new PustakawanHapusKlasifikasiForm();
            Method.form(fr, this);
        }
    }
}
