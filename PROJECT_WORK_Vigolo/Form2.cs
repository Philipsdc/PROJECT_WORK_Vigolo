using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_WORK_Vigolo
{
    public partial class Form2 : Form
    {
        string ID = "";
        public Form2()
        {
            InitializeComponent();
            this.Show();
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();
            if (progressBar1.Value == 100)
            {
                this.Hide();
                Form1 form1 = new Form1(ID);//ID
                form1.Show();
                timer1.Enabled = false;
            }

        }


        private void panel1_Click(object sender, EventArgs e)
        {
            textBox1.Text = default;
            textBox1.Enabled = true;
            textBox1.ReadOnly = false;
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((textBox1.Text).Trim() != "")
                {   
                    ID = textBox1.Text;
                    textBox1.Enabled = false;
                    textBox1.ReadOnly = true;
                    progressBar1.Visible = true;
                    timer1.Enabled = true;
                }
                    
            }
        }
    }   
    
}
