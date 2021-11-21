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
using System.Media;

namespace PROJECT_WORK_Vigolo
{   

    public partial class Form2 : Form
    {
        string ID = "";
        public Form2()
        {
            Intro.PlayLooping();   
            InitializeComponent();
            this.Show();
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            

        }
        SoundPlayer Intro = new SoundPlayer("intro.wav");
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();
            if (progressBar1.Value == 100)
            {
                Intro.Stop();
                this.Hide();
                Game form1 = new Game(ID);//ID
                form1.Show();
                timer1.Enabled = false;
            }

        }


        private void panel1_Click(object sender, EventArgs e)
        {
            textBox1.Text = default;        //abilito la scrittura sulla textbox
            textBox1.Enabled = true;
            textBox1.ReadOnly = false;
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)            //controllo se l'utente schiaccia il pulsante ENTER
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

        private void button1_Click(object sender, EventArgs e)
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
