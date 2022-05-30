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
    public partial class PustakawanHapusSirkulasiNotification : Form
    {
        public PustakawanHapusSirkulasiNotification()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            foreach(var i in dc._circulation_details.Where(s => s._circulation_id == PustakawanUbahSirkulasiForm.id))
            {
                dc._circulation_details.DeleteOnSubmit(i);
                dc.SubmitChanges();
            }
            var st = dc._circulations.Where(s => s._circulation_id == PustakawanUbahSirkulasiForm.id).FirstOrDefault();
            dc._circulations.DeleteOnSubmit(st);
            dc.SubmitChanges();
            this.Owner.Close(); this.Close();
        }
    }
}
