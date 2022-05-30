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
    public partial class PustakawanHapusAnggotaNotification : Form
    {
        public PustakawanHapusAnggotaNotification()
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
            var st = dc._members.Where(s => s._member_id == PustakawanUpdateAnggotaForm.id).FirstOrDefault();
            dc._members.DeleteOnSubmit(st);
            dc.SubmitChanges();
            this.Owner.Close();
            this.Close();
        }
    }
}
