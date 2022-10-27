using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) { textBox3.Text = "AppId不可為空！！"; return; }

            textBox3.Text = "查詢中，請稍後...";
            var result = new ConnectionLibrary.Class1().GetConnectionStrBy(textBox1.Text.Trim(), textBox2.Text.Trim());

            textBox3.Text = result.IsSuccess ? result.Data : result.ErrorMessage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
