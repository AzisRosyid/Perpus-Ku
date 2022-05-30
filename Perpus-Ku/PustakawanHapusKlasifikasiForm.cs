using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perpus_Ku
{
    public partial class PustakawanHapusKlasifikasiForm : Form
    {
        public PustakawanHapusKlasifikasiForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        DataClasses1DataContext dc = new DataClasses1DataContext();

        private void button1_Click(object sender, EventArgs e)
        {
            var st = dc._books.Where(s => s._classification_id == PustakawanUbahKlasifikasiForm.id);
            foreach(var i in st)
            {
                dc._books.DeleteOnSubmit(i);
                if(i._pdf != null)
                {
                    File.Delete(Method.path + i._pdf);
                }
            }
            dc.SubmitChanges();
            var id = dc._classifications.Where(s => s._id == PustakawanUbahKlasifikasiForm.id).FirstOrDefault();
            dc._classifications.DeleteOnSubmit(id);
            dc.SubmitChanges();
            this.Owner.Close(); this.Close();
        }
    }
}
