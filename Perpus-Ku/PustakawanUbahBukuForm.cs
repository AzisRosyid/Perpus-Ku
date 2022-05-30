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
    public partial class PustakawanUbahBukuForm : Form
    {
        public PustakawanUbahBukuForm()
        {
            InitializeComponent();
        }

        private string src, file;
        public static string id;

        private bool del;

        private int label;

        void loadForm()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            comboBox1.DataSource = dc._classifications.OrderBy(s => s._name).ToList();
            comboBox1.DisplayMember = "_name"; comboBox1.ValueMember = "_id"; comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox1.ReadOnly = true;
            var st = dc._books.Where(s => s._isbn == id).FirstOrDefault();
            if((st._pdf != null || src != "") && !del)
            {
                button5.Visible = true; 
                label11.Text = st._pdf;
                if(src != "") { label11.Text = Path.GetFileName(src); }
                label11.Location = new Point(label, label11.Location.Y);
            }
            else
            {
                button5.Visible = false;
                label11.Location = new Point(label - 100, label11.Location.Y);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button5_Click(object sender, EventArgs e)
        {
            del = true;
            label11.Text = ".pdf";
            src = ""; file = "";
            loadForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PustakawanHapusBukuNotification fr = new PustakawanHapusBukuNotification();
            Method.form(fr, this);
        }

        private void PustakawanUbahBukuForm_Load(object sender, EventArgs e)
        {
            src = ""; file = ""; label = label11.Location.X; del = false;
            loadForm();
            DataClasses1DataContext dc = new DataClasses1DataContext();
            var st = dc._books.Where(s => s._isbn == id).FirstOrDefault();
            textBox1.Text = id;
            richTextBox1.Text = st._title;
            richTextBox2.Text = st._author;
            richTextBox3.Text = st._location;
            textBox2.Text = st._year.ToString();
            textBox3.Text = st._publisher;
            textBox4.Text = st._amount.ToString();
            textBox4.TextAlign = HorizontalAlignment.Right;
            foreach(var i in dc._classifications)
            {
                if(i._id == st._classification_id)
                {
                    comboBox1.Text = i._name;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "PDF|*.pdf";
            if (op.ShowDialog() == DialogResult.OK)
            {
                label11.Text = Path.GetFileName(op.FileName);
                src = op.FileName;
                file = "PDF_" + DateTime.Now.ToString("dd_MM_yyyy_HHmmss_") + textBox1.Text + Path.GetExtension(op.FileName);
                del = false;
                loadForm();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            if (textBox1.Text == "" || richTextBox1.Text == "" || richTextBox2.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "" || richTextBox3.Text == "" || textBox4.Text == "") { IsianKosongErrorNotification fr = new IsianKosongErrorNotification(); Method.form(fr, this); }
            else if (!Method.aNumber(textBox2.Text)) { MessageBox.Show("Tahun format must be numeric"); }
            else if (!Method.aNumber(textBox4.Text)) { MessageBox.Show("Jumlah format must be numeric"); }
            else
            {
                var st = dc._books.Where(s => s._isbn == id).FirstOrDefault();
                st._title = richTextBox1.Text;
                st._author = richTextBox2.Text;
                st._year = Convert.ToInt32(textBox2.Text);
                st._publisher = textBox3.Text;
                st._classification_id = Convert.ToInt32(comboBox1.SelectedValue);
                st._location = richTextBox3.Text;
                st._amount = Convert.ToInt32(textBox4.Text);
                if (src != "")
                {
                    if (!Directory.Exists(Method.path))
                    {
                        Directory.CreateDirectory(Method.path);
                    }
                    if (st._pdf != null)
                    {
                        File.Delete(Method.path + st._pdf);
                    }
                    st._pdf = file;
                    File.Copy(src, Method.path + file, true);
                } 
                else if (del)
                {
                    if (st._pdf != null)
                    {
                        File.Delete(Method.path + st._pdf);
                    }
                    st._pdf = null;
                }
                dc.SubmitChanges();
                this.Close();
            }
        }
    }
}
