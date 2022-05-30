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
    public partial class UserHapusNotification : Form
    {
        public UserHapusNotification()
        {
            InitializeComponent();
        }

        DataClasses1DataContext dc = new DataClasses1DataContext();

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var st = dc._users.Where(s => s._user_id == UserUpdateForm.id).FirstOrDefault();
            dc._users.DeleteOnSubmit(st);
            dc.SubmitChanges();
            this.Owner.Close(); this.Close();
        }
    }
}
