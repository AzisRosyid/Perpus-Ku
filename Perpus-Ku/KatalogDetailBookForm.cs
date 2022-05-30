using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perpus_Ku
{
    public partial class KatalogDetailBookForm : Form
    {
        public KatalogDetailBookForm()
        {
            InitializeComponent();
        }

        public static string id;

        private void KatalogDetailBookForm_Load(object sender, EventArgs e)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            textBox1.ReadOnly = true; textBox2.ReadOnly = true; richTextBox1.ReadOnly = true; textBox3.ReadOnly = true; textBox4.ReadOnly = true; textBox5.ReadOnly = true; textBox6.ReadOnly = true; textBox7.ReadOnly = true;
            var st = dc._books.Where(s => s._isbn == id).FirstOrDefault();
            this.Text = "Informasi Buku - " + st._title;
            textBox1.Text = st._isbn;
            textBox2.Text = st._title;
            richTextBox1.Text = st._author;
            textBox3.Text = st._year.ToString();
            textBox4.Text = st._publisher;
            textBox5.Text = dc._classifications.Where(s => s._id == st._classification_id).Select(s => s._name).FirstOrDefault();
            textBox6.Text = st._location;
            textBox7.Text = st._amount.ToString();

            if(st._pdf == null)
            {
                button1.Enabled = false;
                groupBox2.Enabled = false;
            }
            else
            {
                webBrowser1.Navigate(new Uri(Method.path + st._pdf));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            var st = dc._books.Where(s => s._isbn == id).FirstOrDefault();
            SaveFileDialog sv = new SaveFileDialog();
            sv.FileName = st._title;
            sv.Filter = "PDF Document|*.pdf";
            sv.DefaultExt = ".pdf";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                File.Copy(Method.path + st._pdf, sv.FileName, true);
                if(MessageBox.Show("Buka folder download?", "Konfirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start(Path.GetDirectoryName(sv.FileName));
                }
            }
        }
    }
}
