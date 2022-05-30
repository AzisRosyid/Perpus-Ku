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
    public partial class PustakawanTambahBukuForm : Form
    {
        public PustakawanTambahBukuForm()
        {
            InitializeComponent();
        }

        DataClasses1DataContext dc = new DataClasses1DataContext();

        void loadForm()
        {
            comboBox1.DataSource = dc._classifications.OrderBy(s => s._name).ToList();
            comboBox1.DisplayMember = "_name"; comboBox1.ValueMember = "_id"; comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            src = ""; file = "";
        }

        private void PustakawanTambahBukuForm_Load(object sender, EventArgs e)
        {
            loadForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string src, file;

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || richTextBox1.Text == "" || richTextBox2.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "" || richTextBox3.Text == "" || textBox4.Text == "") { IsianKosongErrorNotification fr = new IsianKosongErrorNotification(); Method.form(fr, this); }
            else if (!Method.validISBN(textBox1.Text)) { MessageBox.Show("ISBN format does not correct (example: 978-602-1111-93-9)"); }
            else if (dc._books.Any(s => s._isbn == textBox1.Text)) { MessageBox.Show("ISBN conflict"); }
            else if (!Method.aNumber(textBox2.Text)) { MessageBox.Show("Tahun format must be numeric"); }
            else if (!Method.aNumber(textBox4.Text)) { MessageBox.Show("Jumlah format must be numeric"); }
            else
            {
                var st = new _book();
                st._isbn = textBox1.Text;
                st._title = richTextBox1.Text;
                st._author = richTextBox2.Text;
                st._year = Convert.ToInt32(textBox2.Text);
                st._publisher = textBox3.Text;
                st._classification_id = Convert.ToInt32(comboBox1.SelectedValue);
                st._location = richTextBox3.Text;
                st._amount = Convert.ToInt32(textBox4.Text);
                if(src != "")
                {
                    if (!Directory.Exists(Method.path))
                    {
                        Directory.CreateDirectory(Method.path);
                    }
                    st._pdf = file;
                    File.Copy(src, Method.path + file, true);
                }
                dc._books.InsertOnSubmit(st);
                dc.SubmitChanges();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PustakawanManajemenKlasifikasiForm fr = new PustakawanManajemenKlasifikasiForm();
            fr.FormClosed += Fr_FormClosed; Method.form(fr, this);
        }

        private void Fr_FormClosed(object sender, FormClosedEventArgs e)
        {
            loadForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "PDF|*.pdf";
            if(op.ShowDialog() == DialogResult.OK)
            {
                label11.Text = Path.GetFileName(op.FileName);
                src = op.FileName;
                file = "PDF_" + DateTime.Now.ToString("dd_MM_yyyy_HHmmss_") + textBox1.Text + Path.GetExtension(op.FileName);
            }
        }
    }
}
