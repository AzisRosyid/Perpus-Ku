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
    public partial class PustakawanHapusBukuNotification : Form
    {
        public PustakawanHapusBukuNotification()
        {
            InitializeComponent();
        }

        private void PustakawanHapusBukuNotification_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            var st = dc._books.Where(s => s._isbn == PustakawanUbahBukuForm.id).FirstOrDefault();
            if (st._pdf != null)
            {
                File.Delete(Method.path + st._pdf);
            }
            dc._books.DeleteOnSubmit(st);
            dc.SubmitChanges();
            this.Owner.Close(); this.Close();
        }
    }
}
