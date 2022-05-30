using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perpus_Ku
{
    internal class Method
    {
        public static int id;

        public static string path = Path.GetFullPath(Environment.CurrentDirectory + "../../../user_files/");

        public static List<Item> userLevel = new List<Item> { new Item { Name = "administrator", Value = 1 }, new Item { Name = "pustakawan", Value = 2 }, new Item { Name = "administrator, pustakawan", Value = 3 } };
        public static List<Item> userIdentity = new List<Item> { new Item { Name = "NIP", Value = 1 }, new Item { Name = "NUP", Value = 2 } };
        public static List<Item> anggotaIdentity = new List<Item> { new Item { Name = "Kartu Pegawai", Value = 1 }, new Item { Name = "Kartu Pelajar", Value = 2 } };

        public static void form(Form fr, Form st, bool load = false)
        {
            fr.Owner = st; fr.StartPosition = FormStartPosition.CenterScreen;
            fr.ShowDialog();
        }

        private static string Sha(string s)
        {
            using(var t = SHA1.Create())
            {
                return string.Concat(t.ComputeHash(Encoding.UTF8.GetBytes(s)).Select(x => x.ToString("x2")));
            }
        }

        public static string sha(string s) => Sha(Sha(s));

        public static bool validISBN(string s)
        {
            try
            {
                Convert.ToInt64(s.Replace("-", ""));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool aNumber(string s, bool st = false)
        {
            if (st)
            {
                double t;
                return double.TryParse(s, out t);
            }
            else
            {
                long t;
                return long.TryParse(s, out t);
            }
        }

        public static bool validEmail(string s)
        {
            try
            {
                var t = new MailAddress(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

    class Item
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
