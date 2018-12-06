using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zzh.Utility;
using System.Threading;
using Zzh.Tester.Properties;
using System.Globalization;
using System.Diagnostics;
using Zzh.Tester;

namespace Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.label1.Text = Thread.CurrentThread.CurrentCulture.Name;
            //this.label2.Text = Resources.testName;
            ////设置区域
            //Thread.CurrentThread.CurrentUICulture =  new CultureInfo("en-US");
            //this.label3.Text = Resources.testName;
            //this.label4.Text = Resources.names;
        }
        static void AddBooks(BookDB bookDB)
        {
            bookDB.AddBook("The C Programming Language", "Brian W. Kernighan and Dennis M. Ritchie", 19.95m, true);
            bookDB.AddBook("The Unicode Standard 2.0", "The Unicode Consortium", 39.95m, true);
            bookDB.AddBook("The MS-DOS Encyclopedia", "Ray Duncan", 129.95m, false);
            bookDB.AddBook("Dogbert's Clues for the Clueless", "Scott Adams", 12.00m, true);
        }
        static void PrintTitle(Book b)
        {
            MessageBox.Show(string.Format("{0}", b.Title));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            BookDB bookDB = new BookDB();

            // Initialize the database with some books:
            AddBooks(bookDB);

            // Print all the titles of paperbacks:
            MessageBox.Show("Paperback Book Titles:");

            // Create a new delegate object associated with the static 
            // method Test.PrintTitle:
            bookDB.ProcessPaperBooks(PrintTitle);
        }
    }
}
