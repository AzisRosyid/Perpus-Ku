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
    public partial class PustakawanHapusBukuDariSirkulasiNotification : Form
    {
        public PustakawanHapusBukuDariSirkulasiNotification()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static int id;
        public static List<string> book = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            book.RemoveAt(id);
            this.Close();
        }
    }
}
