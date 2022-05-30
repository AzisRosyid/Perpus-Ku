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
    public partial class UserResetPasswordNotification : Form
    {
        public UserResetPasswordNotification()
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
            var st = dc._users.Where(s => s._user_id == UserUpdateForm.id).FirstOrDefault();
            st._password = Method.sha("12345");
            dc.SubmitChanges();
            this.Owner.Close();
            this.Close();
        }
    }
}
