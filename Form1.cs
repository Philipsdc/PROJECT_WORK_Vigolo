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
using System.IO;

namespace PROJECT_WORK_Vigolo
{
    public partial class Game : Form
    {
        public int movement;                //Dichiaro ed inizializzo alcune delle variabili che mi serviranno
        public int down;
        public int rotate;
        public PictureBox[] ciao;
        string ID = "";
        bool exist = false;
        int score = 0;
        string username = "userdefault";

        public Game(string gg)
        {
            InitializeComponent();

            ID = gg;
           

            if(ID!= "userdefault")
            {
                loadUserdataFromFile();
            }
            else
            {
                exist = true;
                txtName.Text = "userdefault";
                txtBstScore.Text = "0";
                score = 0;
            }

        }

        public void writeUserdataIntoFile()         //creo un nuovo nome con punteggio
        {
            string[] dataFile = File.ReadAllLines(@"..\..\Resources\Data1.txt");

            File.AppendAllText(@"..\..\Resources\Data1.txt", "\n" + ID + ";" + score.ToString());
            txtName.Text = ID;
            txtBstScore.Text = score.ToString();
        }


        public void loadUserdataFromFile() {            //vedo se è presente il nome inserito e lo inserisco nella text box con il punteggio 

           
            int scoreUser = 0;

            string[] dataFile = File.ReadAllLines(@"..\..\Resources\Data1.txt");

            if (exist == false)
            {
                foreach (var record in dataFile)
                {
                string[] fields = record.Split(';');

                string name = fields[0];

                int score1 = Convert.ToInt32(fields[1]);

                if (name.ToLower() == ID.ToLower())
                {
                    exist = true;
                    username = name;
                    scoreUser = score1;
                    score = score1;
                    txtName.Text = username;
                    txtBstScore.Text = scoreUser.ToString();
                }
                }
            }

            if (exist == false)
                writeUserdataIntoFile();
        }
        
        Color selected=Color.Aqua;      //assegno un colore iniziale

        private void button1_Click(object sender, EventArgs e)
        {

            tmrMovimento.Enabled = true;        //faccio partire il gioco e il timer
            tmrCrono.Enabled = true;
            btnStart.Visible = false;
        }
        int i = -10;
        int y = 0;
        int spostx = 0;
        Random random = new Random();
        int n1random = -1;

        int rotation = 0;

        int agg1 = 0;
        int agg2 = 0;
        int agg3 = 0;
        int agg4 = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
        
            if ((y < 9&&n1random!=2) || (n1random==2&&rotation==0&&y<9))
            {
                i += 10;
                spostx += movement;
            }
            else if(n1random == 2 && y < 10 && rotation==1)
            {
                i += 10;
                spostx += movement;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if (ciao[i].BackColor == selected&&tmrMovimento.Enabled==true)
                        controllaperdita();
                }
                calcolaedeelimina();
                y = 0;
                i = 0;
            }
            
           

            if (y == 0)
            {
               
                if(n1random==6)
                {
                    n1random = -1;
                }
                
                if (n1random < 7)
                {
                    n1random += 1;
                }
                spostx = 0; 
                rotate = 0;
                rotation = 0;

                agg1 = 0;
                agg2 = 0;
                agg3 = 0;
                agg4 = 0;
            }

            //salvo tutti 100 picturebox in array di pictureboxes 
            PictureBox[] pictureBoxes = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10,
                                                           pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20,
                                                           pictureBox21, pictureBox22, pictureBox23, pictureBox24, pictureBox25, pictureBox26, pictureBox27, pictureBox28, pictureBox29, pictureBox30,
                                                           pictureBox31, pictureBox32, pictureBox33, pictureBox34, pictureBox35, pictureBox36, pictureBox37, pictureBox38, pictureBox39, pictureBox40,
                                                           pictureBox41, pictureBox42, pictureBox43, pictureBox44, pictureBox45, pictureBox46, pictureBox47, pictureBox48, pictureBox49, pictureBox50,
                                                           pictureBox51, pictureBox52, pictureBox53, pictureBox54, pictureBox55, pictureBox56, pictureBox57, pictureBox58, pictureBox59, pictureBox60,
                                                           pictureBox61, pictureBox62, pictureBox63, pictureBox64, pictureBox65, pictureBox66, pictureBox67, pictureBox68, pictureBox69, pictureBox70,
                                                           pictureBox71, pictureBox72, pictureBox73, pictureBox74, pictureBox75, pictureBox76, pictureBox77, pictureBox78, pictureBox79, pictureBox80,
                                                           pictureBox81, pictureBox82, pictureBox83, pictureBox84, pictureBox85, pictureBox86, pictureBox87, pictureBox88, pictureBox89, pictureBox90,
                                                           pictureBox91, pictureBox92, pictureBox93, pictureBox94, pictureBox95, pictureBox96, pictureBox97, pictureBox98, pictureBox99, pictureBox100,};

            int[,] startpoint = new int[7, 4] { { 5, 15, 16, 17 }, { 5, 6, 15, 16 }, { 5, 15, 25, 35 }, { 5, 14, 15, 16 }, { 5, 6, 15, 14 }, { 5, 15, 14, 13 }, { 4, 5, 15, 16 } };  //1°L, 2°cubo, 3°pilastro, 4°WASD, 5°biscio, 6°L contraria, 7° biscio contrario 
            ciao = pictureBoxes;
            //1°biscio ruotato: 5, 26, 15, 16 
            //pilasro ruotato: 4, 5, 6, 7
            //2° biscio ruotato: 14, 5, 15, 24

            if (n1random == 6 && i + startpoint[n1random, 3] + spostx + agg4 > pictureBoxes.Length-1)
                y = 11;

            if (i == 100 && n1random == 2 && rotation == 1)//pilastro
            {
                i = i - 10;
            }

            if (n1random == 2 && rotation == 0 && (i + startpoint[n1random, 3] + spostx) > pictureBoxes.Length)//pilastro
                y = 11;

            //Definisco i limiti dei tetramini e se un movimento sbagliato porterebbe ad errori grafici

            if (i >= 10)
            {
                if (i >= 10 && n1random == 5 && ((spostx < -3 || spostx > 4) || (movement == 1 && (pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
               pictureBoxes[(i + startpoint[n1random, 1] - 10 + agg2 + spostx)].BackColor == selected ||
               pictureBoxes[(i + startpoint[n1random, 1] + agg2 + spostx)].BackColor == selected)) ||           //L contraria

              (movement == -1 && (pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
              pictureBoxes[(i + startpoint[n1random, 3] - 10 + agg4 + spostx)].BackColor == selected ||
              pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected))))
                {
                    if (movement == -1)
                    {
                        spostx = spostx + 1;
                        movement = 0;
                    }
                    else if (movement == 1)
                    {
                        spostx = spostx - 1;
                        movement = 0;
                    }
                }

                if (i >= 10 && n1random == 3 && ((spostx < -4 || spostx > 3) || (movement == 1 && (pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] - 10 + agg4 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected)) ||           //WASD

                   (movement == -1 && (pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 1] - 10 + agg2 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 1] + agg2 + spostx)].BackColor == selected))))
                {
                    if (movement == -1)
                    {
                        spostx = spostx + 1;
                        movement = 0;
                    }
                    else if (movement == 1)
                    {
                        spostx = spostx - 1;
                        movement = 0;
                    }
                }

                if (i >= 10 && n1random == 1 && ((spostx < -5 || spostx > 3) || (movement == 1 && (pictureBoxes[(i + startpoint[n1random, 1] - 10 + agg2 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] - 10 + agg4 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected)) ||           //CUBO

                   (movement == -1 && (pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 2] - 10 + agg3 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 2] + agg3 + spostx)].BackColor == selected))))
                {
                    if (movement == -1)
                    {
                        spostx = spostx + 1;
                        movement = 0;
                    }
                    else if (movement == 1)
                    {
                        spostx = spostx - 1;
                        movement = 0;
                    }
                }

                if (n1random == 0 && ((spostx < -5 || spostx > 2) || (movement == 1 && (pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected ||
                    movement == 1 && (pictureBoxes[(i + startpoint[n1random, 3] - 10 + agg4 + spostx)].BackColor == selected)) || //L

                   (movement == -1 && pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
                   movement == -1 && (pictureBoxes[(i + startpoint[n1random, 1] - 10 + agg2 + spostx)].BackColor == selected ||
                   movement == -1 && (pictureBoxes[(i + startpoint[n1random, 1] + agg2 + spostx)].BackColor == selected))))))
                {
                    if (movement == -1)
                    {
                        spostx = spostx + 1;
                        movement = 0;
                    }
                    else if (movement == 1)
                    {
                        spostx = spostx - 1;
                        movement = 0;
                    }
                }





                if (i >= 10 && rotate == 0 && n1random == 4 && ((spostx < -4 || spostx > 3) || (movement == 1 && (pictureBoxes[(i + startpoint[n1random, 1] - 10 + agg2 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 2] + agg3 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 1] + agg2 + spostx)].BackColor == selected)) ||           //biscio 4

                   (movement == -1 && (pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 3] - 10 + agg4 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected))))
                {
                    if (movement == -1)
                    {
                        spostx = spostx + 1;
                        movement = 0;
                    }
                    else if (movement == 1)
                    {
                        spostx = spostx - 1;
                        movement = 0;
                    }

                }
                if (i >= 10 && rotate == 1 && n1random == 4 && ((spostx < -5 || spostx > 3) || (movement == 1 && (pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] - 10 + agg4 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 1] + agg2 + spostx)].BackColor == selected)) ||           //biscio 4 ruotato

                   (movement == -1 && (pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 2] - 10 + agg3 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 2] + agg3 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 1] + agg2 + spostx)].BackColor == selected))))
                {
                    if (movement == -1)
                    {
                        spostx = spostx + 1;
                        movement = 0;
                    }
                    else if (movement == 1)
                    {
                        spostx = spostx - 1;
                        movement = 0;
                    }

                }

                if (i >= 10 && rotate == 0 && n1random == 6 && ((spostx < -4 || spostx > 3) || (movement == 1 && (pictureBoxes[(i + startpoint[n1random, 1] - 10 + agg2 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] - 10 + agg4 + spostx)].BackColor == selected)) ||           //biscio 6

                   (movement == -1 && (pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 0] + agg4 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 2] + agg4 + spostx)].BackColor == selected))))
                {
                    if (movement == -1)
                    {
                        spostx = spostx + 1;
                        movement = 0;
                    }
                    else if (movement == 1)
                    {
                        spostx = spostx - 1;
                        movement = 0;
                    }

                }
                if (i >= 10 && rotate == 1 && n1random == 6 && ((spostx < -4 || spostx > 4) || (movement == 1 && (pictureBoxes[(i + startpoint[n1random, 1] - 10 + agg2 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 2] - 10 + agg3 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 2] + agg3 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected)) ||           //biscio 6 ruotato

                   (movement == -1 && (pictureBoxes[(i + startpoint[n1random, 1] - 10 + agg2 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 0] + agg1 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected))))
                {
                    if (movement == -1)
                    {
                        spostx = spostx + 1;
                        movement = 0;
                    }
                    else if (movement == 1)
                    {
                        spostx = spostx - 1;
                        movement = 0;
                    }

                }

                if (rotation == 1 && n1random == 2 && ((spostx < -4 || spostx > 2) || (movement == 1 && (pictureBoxes[(i + startpoint[n1random, 0] + agg1 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected)) ||
                   //pilastro ruotato collisioni
                   (movement == -1 && (pictureBoxes[(i + startpoint[n1random, 0] + agg1 + spostx)].BackColor == selected ||
                   pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected))))
                {
                    if (movement == -1)
                    {
                        spostx = spostx + 1;
                        movement = 0;
                    }
                    else if (movement == 1)
                    {
                        spostx = spostx - 1;
                        movement = 0;
                    }

                }

                if (y < 9 && i >= 10 && rotation == 0 && n1random == 2 && ((spostx < -5 || spostx > 4) || (movement == 1 && (pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 1] + agg2 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 2] + agg3 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 1] - 10 + agg4 + spostx)].BackColor == selected)) ||
                   //pilastro collisioni
                   (movement == -1 && ((pictureBoxes[(i + startpoint[n1random, 0] - 10 + agg1 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 1] + agg2 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 2] + agg3 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 3] + agg4 + spostx)].BackColor == selected ||
                    pictureBoxes[(i + startpoint[n1random, 1] - 10 + agg4 + spostx)].BackColor == selected)))))
                {
                    if (movement == -1)
                    {
                        spostx = spostx + 1;
                        movement = 0;
                    }
                    else if (movement == 1)
                    {
                        spostx = spostx - 1;
                        movement = 0;
                    }

                }
            }
           

            //Definisco la fine di ogni tetramino, ovvero l'inizio di un nuovo ciclo



            if ((i + startpoint[n1random, 1] + spostx + agg2) > pictureBoxes.Length && n1random==4 && rotation==1)
            {
                y = 10;

            }



            if (y<9 && rotation==0 && startpoint[n1random, 3]+spostx + i < pictureBoxes.Length                               //pilastro
                && pictureBoxes[(i + startpoint[n1random, 3] + spostx)].BackColor == selected
                && n1random == 2)
            {
                y = 10;
            }
            else if (n1random==2&&rotation==1&&(pictureBoxes[(i + startpoint[n1random, 3]+agg4 + spostx)].BackColor == selected||
                pictureBoxes[(i + startpoint[n1random, 2] + agg3 + spostx)].BackColor == selected||
                pictureBoxes[(i + startpoint[n1random, 1] + agg2 + spostx)].BackColor == selected||
                pictureBoxes[(i + startpoint[n1random, 0] + agg1 + spostx)].BackColor == selected))
            {
                y = 11;
            }



            else if (startpoint[n1random, 3] + i < pictureBoxes.Length && (pictureBoxes[(i + startpoint[n1random, 3] + spostx)].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx)].BackColor == selected)    //cubo
                      && n1random == 1)
            {
                y = 10;
            }
            else if ((n1random == 4 || n1random == 6) && rotation == 0)                  //biscio
            {
                if (movement == 1 && ((n1random == 4 && (pictureBoxes[(i + startpoint[n1random, 1] + spostx) - 1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx) - 1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 3] + spostx) - 1].BackColor == selected))

                      ||

                      (n1random == 6 && (pictureBoxes[(i + startpoint[n1random, 0] + spostx) - 1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx) - 1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 3] + spostx) - 1].BackColor == selected))))
                {
                    y = 11;
                }
                else if (movement == -1 && ((n1random == 4 && (pictureBoxes[(i + startpoint[n1random, 1] + spostx) + 1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx) + 1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 3] + spostx) + 1].BackColor == selected))

                      ||

                      (n1random == 6 && (pictureBoxes[(i + startpoint[n1random, 0] + spostx) + 1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx) + 1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 3] + spostx) + 1].BackColor == selected))))
                {
                    y = 11;
                }
                else if (movement == 0 && ((n1random == 4 && (pictureBoxes[(i + startpoint[n1random, 1] + spostx)].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx)].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 3] + spostx)].BackColor == selected))

                      ||

                      (n1random == 6 && (pictureBoxes[(i + startpoint[n1random, 0] + spostx)].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx)].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 3] + spostx)].BackColor == selected))))
                {
                    y = 11;
                }

            } else if ((n1random == 6||n1random==4) && rotation == 1 &&y<9)                  //biscio invertito
                {

                if (movement == 1 && (((n1random == 4 && pictureBoxes[(i + startpoint[n1random, 1] + spostx + agg2) -1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx + agg3) -1].BackColor == selected))

                      ||

                      (n1random == 6 && (pictureBoxes[(i + startpoint[n1random, 3] + spostx + agg4) - 1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx + agg3) - 1].BackColor == selected))))
                {
                    y = 11;
                }

                else if (movement == -1 && (((n1random == 4 && pictureBoxes[(i + startpoint[n1random, 1] + spostx + agg2) + 1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx + agg3) + 1].BackColor == selected))

                      ||

                      (n1random == 6 && (pictureBoxes[(i + startpoint[n1random, 3] + spostx + agg4) + 1].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx + agg3) + 1].BackColor == selected))))
                {
                    y = 11;
                }
                else if (movement == 0 && (((n1random == 4 && pictureBoxes[(i + startpoint[n1random, 1] + spostx + agg2)].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx + agg3) ].BackColor == selected))

                      ||

                      (n1random == 6 && (pictureBoxes[(i + startpoint[n1random, 3] + spostx + agg4)].BackColor == selected ||
                      pictureBoxes[(i + startpoint[n1random, 2] + spostx + agg3)].BackColor == selected))))
                {
                    y = 11;
                }
            }
                

                else if ((n1random == 0 || n1random == 3 || n1random == 5)&&(pictureBoxes[(i + startpoint[n1random, 1] + spostx)].BackColor == selected ||     //serve a far ripartire il ciclo quando il tetramino conclude il suo giro: L, L contraria
                        pictureBoxes[(i + startpoint[n1random, 2] + spostx)].BackColor == selected ||     //maggior parte dei casi
                        pictureBoxes[(i + startpoint[n1random, 3] + spostx)].BackColor == selected) )
                {
                    y = 10;
                }

                
                //Cancello il tetramino in causa


                if (i >= 10 && ((startpoint[n1random, 3]+agg3+spostx + i < pictureBoxes.Length&&n1random!=2) || ((n1random == 2 && rotation == 0 && y < 9) || (n1random == 2 && rotation == 1 && y < 10))))                           //elimina moduli
                {
                    if (((n1random == 2&&rotation==0&& startpoint[n1random, 3] + i < pictureBoxes.Length)||n1random==2&&rotation==1) && pictureBoxes[(i + startpoint[n1random, 3]) + agg4 + spostx].BackColor != selected)   //caso pilastro 
                    {
                        if (movement == 1)
                        {
                            pictureBoxes[(i + startpoint[n1random, 0]) + agg1 - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) + agg2 - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) + agg3 - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) + agg4 -10 + spostx - 1].BackColor = default;
                        }
                        else if (movement == -1)
                        {
                            pictureBoxes[(i + startpoint[n1random, 0]) + agg1 - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) + agg2 - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) + agg3 - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) + agg4 - 10 + spostx + 1].BackColor = default;
                        }
                        else if (movement == 0)
                        {
                            pictureBoxes[(i + startpoint[n1random, 0]) + agg1 - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) + agg2 - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) + agg3 - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) + agg4 - 10 + spostx].BackColor = default;
                        }
                    }
                    else if (n1random == 1 && pictureBoxes[(i + startpoint[n1random, 2]) + spostx].BackColor != selected &&  //caso cubo
                        pictureBoxes[(i + startpoint[n1random, 3]) + spostx].BackColor != selected)
                    {

                        if (movement == 1)
                        {
                            pictureBoxes[(i + startpoint[n1random, 0]) - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) - 10 + spostx - 1].BackColor = default;
                        }
                        else if (movement == -1)
                        {
                            pictureBoxes[(i + startpoint[n1random, 0]) - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) - 10 + spostx + 1].BackColor = default;
                        }
                        else if (movement == 0)
                        {
                            pictureBoxes[(i + startpoint[n1random, 0]) - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) - 10 + spostx].BackColor = default;
                        }
                        
                    }

                    else if (((n1random == 4 || n1random == 6)&&y<9)||(n1random==6&&rotation==1&&y<8))
                    {

                        if (movement == 1)
                        {
                            pictureBoxes[(i + startpoint[n1random, 0]) + agg1 - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) + agg2 - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) + agg3 - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) + agg4 - 10 + spostx - 1].BackColor = default;
                        }
                        else if (movement == -1)
                        {
                            pictureBoxes[(i + startpoint[n1random, 0]) + agg1 - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) + agg2 - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) + agg3 - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) + agg4 - 10 + spostx + 1].BackColor = default;
                        }
                        else if (movement == 0)
                        {


                            pictureBoxes[(i + startpoint[n1random, 0]) + agg1 - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) + agg2 - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) + agg3 - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) + agg4 - 10 + spostx].BackColor = default;


                        }
                    }

                    else if (n1random!=4 && n1random != 6 &&pictureBoxes[(i + startpoint[n1random, 1]) + spostx].BackColor != selected &&      //tutti altri casi
                    pictureBoxes[(i + startpoint[n1random, 2]) + spostx].BackColor != selected &&
                    pictureBoxes[(i + startpoint[n1random, 3]) + spostx].BackColor != selected)
                    {
                        Console.WriteLine(-(spostx));
                        if (movement == 1)
                        {
                            pictureBoxes[(i + startpoint[n1random, 0]) - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) - 10 + spostx - 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) - 10 + spostx - 1].BackColor = default;
                        }
                        else if (movement == -1)
                        {
                            pictureBoxes[(i + startpoint[n1random, 0]) - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) - 10 + spostx + 1].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) - 10 + spostx + 1].BackColor = default;
                        }
                        else if (movement == 0)
                        {
                            pictureBoxes[(i + startpoint[n1random, 0]) - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 1]) - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 2]) - 10 + spostx].BackColor = default;
                            pictureBoxes[(i + startpoint[n1random, 3]) - 10 + spostx].BackColor = default;
                        }

                    }
                }

            //Definisco se è possibile ruotare i pezzi che hanno la possibilità di eseguire il movimento

            //biscio 6
            if (n1random == 6 && rotate == 1 && rotation == 0)
            {

                if (i >= 80)
                {
                    agg1 = 0;
                    agg2 = 0;
                    agg3 = 0;
                    agg4 = 0;
                    rotate = 0;
                }
                else if (pictureBoxes[startpoint[n1random, 0] + 10 + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 1] + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 3] + 8 + i + spostx].BackColor != selected)
                {
                    agg1 = 10;
                    agg2 = 0;
                    agg3 = 0;
                    agg4 = 8;
                }
                else if (pictureBoxes[startpoint[n1random, 0] + i + spostx].BackColor == selected ||
                   pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor == selected ||
                   pictureBoxes[startpoint[n1random, 1] - 20 + i + spostx].BackColor == selected ||
                   pictureBoxes[startpoint[n1random, 3] - 2 + i + spostx].BackColor == selected)
                {
                    agg1 = 0;
                    agg2 = 0;
                    agg3 = 0;
                    agg4 = 0;
                    rotate = 0;
                    
                }
            }

            if (n1random == 6 && rotate == 0 && rotation == 1)
            {
                if (spostx == 4)
                    spostx = 3;

                if (i >= 80)
                {
                    agg1 = 10;
                    agg2 = 0;
                    agg3 = 0;
                    agg4 = 8;
                    rotate = 1;
                }
                else if (pictureBoxes[startpoint[n1random, 0] + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 1] + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 3] + i + spostx].BackColor != selected)
                {
                    agg1 = 0;
                    agg2 = 0;
                    agg3 = 0;
                    agg4 = 0;
                }
                else if (pictureBoxes[startpoint[n1random, 0] + i + spostx].BackColor == selected ||
                    pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor == selected ||
                    pictureBoxes[startpoint[n1random, 1] + i + spostx].BackColor == selected ||
                    pictureBoxes[startpoint[n1random, 3] + i + spostx].BackColor == selected)
                {
                    agg1 = 10;
                    agg2 = 0;
                    agg3 = 0;
                    agg4 = 8;
                    rotate = 1;
                }
            }

            //biscio 4
            if (n1random==4&& rotate==1 && rotation == 0)
            {
                if (i>=80)
                {
                    agg1 = 0;
                    agg2 = 0;
                    agg3 = 0;
                    agg4 = 0;
                    rotate = 0;
                }
                else if (pictureBoxes[startpoint[n1random, 0] + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor != selected&&
                    pictureBoxes[startpoint[n1random, 1]+20 + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 3]+2 + i + spostx].BackColor != selected)
                {
                    agg1 = 0;
                    agg2 = 20;
                    agg3 = 0;
                    agg4 = 2;
                }
                else if (pictureBoxes[startpoint[n1random, 0] + i + spostx].BackColor == selected ||
                   pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor == selected ||
                   pictureBoxes[startpoint[n1random, 1] - 20 + i + spostx].BackColor == selected ||
                   pictureBoxes[startpoint[n1random, 3] - 2 + i + spostx].BackColor == selected)
                {
                    agg1 = 0;
                    agg2 = 0;
                    agg3 = 0;
                    agg4 = 0;
                    rotate = 0;
                }
            }

            if(n1random==4 && rotate==0 && rotation == 1)
            {
                if (spostx == -5)
                    spostx = -4;

                if (i>=80)
                {
                    agg1 = 0;
                    agg2 = 20;
                    agg3 = 0;
                    agg4 = 2;
                    rotate = 1;
                }
                else if (pictureBoxes[startpoint[n1random, 0] + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 1] + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 3] + i + spostx].BackColor != selected)
                {
                    agg1 = 0;
                    agg2 = 0;
                    agg3 = 0;
                    agg4 = 0;
                }
                else if (pictureBoxes[startpoint[n1random, 0] + i + spostx].BackColor == selected ||
                    pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor == selected ||
                    pictureBoxes[startpoint[n1random, 1] + i + spostx].BackColor == selected ||
                    pictureBoxes[startpoint[n1random, 3] + i + spostx].BackColor == selected)
                {
                    agg1 = 0;
                    agg2 = 20;
                    agg3 = 0;
                    agg4 = 2;
                    rotate = 1;
                    
                }
            }

            //pilastro
            if (n1random == 2 && rotation == 1 && rotate == 0)
            {

                if (i>60)
                {
                    agg1 = -1;
                    agg2 = -10;
                    agg3 = -19;
                    agg4 = -28;
                    rotate = 1;
                }
                else
                {
                    if (spostx == 2)
                    {
                        spostx = 4;
                        if (pictureBoxes[startpoint[n1random, 0] + i + spostx].BackColor != selected &&
                        pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor != selected &&
                        pictureBoxes[startpoint[n1random, 3] + i + spostx].BackColor != selected)
                        {
                            agg1 = 0;
                            agg2 = 0;
                            agg3 = 0;
                            agg4 = 0;
                        }
                        else
                        {
                            spostx = 2;
                            agg1 = -1;
                            agg2 = -10;
                            agg3 = -19;
                            agg4 = -28;
                            rotate = 1;
                        }
                    }
                    else if (spostx == -4)
                    {
                        spostx = -5;
                        if (pictureBoxes[startpoint[n1random, 0] + i + spostx].BackColor != selected &&
                        pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor != selected &&
                        pictureBoxes[startpoint[n1random, 3] + i + spostx].BackColor != selected)
                        {
                            agg1 = 0;
                            agg2 = 0;
                            agg3 = 0;
                            agg4 = 0;
                        }
                        else
                        {
                            spostx = -4;
                            agg1 = -1;
                            agg2 = -10;
                            agg3 = -19;
                            agg4 = -28;
                            rotate = 1;
                        }
                    }
                    else if (spostx > -4 && spostx < 2)
                    {
                        if (pictureBoxes[startpoint[n1random, 0] + i + spostx].BackColor != selected &&
                        pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor != selected &&
                        pictureBoxes[startpoint[n1random, 3] + i + spostx].BackColor != selected)
                        {
                            agg1 = 0;
                            agg2 = 0;
                            agg3 = 0;
                            agg4 = 0;
                        }
                        else
                        {
                            agg1 = -1;
                            agg2 = -10;
                            agg3 = -19;
                            agg4 = -28;
                            rotate = 1;
                        }

                    }
                }
 
            }

            if (n1random == 2 && rotation == 0 && rotate == 1)
            {

                if(pictureBoxes[startpoint[n1random, 0]-1 + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 2]-19 + i + spostx].BackColor != selected &&
                    pictureBoxes[startpoint[n1random, 3]-28 + i + spostx].BackColor != selected)
                {
                    agg1 = -1;
                    agg2 = -10;
                    agg3 = -19;
                    agg4 = -28;

                    if (spostx == 4)
                        spostx = 2;

                    if (spostx == -5)
                        spostx = -4;
                }
                else if(pictureBoxes[startpoint[n1random, 0] - 1 + i + spostx].BackColor == selected ||
                    pictureBoxes[startpoint[n1random, 2] - 19 + i + spostx].BackColor == selected ||
                    pictureBoxes[startpoint[n1random, 3] - 28 + i + spostx].BackColor == selected)
                {
                    agg1 = 0;
                    agg2 = 0;
                    agg3 = 0;
                    agg4 = 0;
                    rotate = 0;
                }
                    
            }

           //Stampo la nuova figura 


            if (y < 9 ||(n1random==2 && rotate==1 && y<10))
                {

                if (rotate==0&&n1random == 2 && i == 70)   //biscio dritto
                {

                    //pictureBoxes[startpoint[n1random, 3] - 10].BackColor = selected;
                }
                else if(n1random == 2 && rotate==1)              //biscio storto
                {
                    pictureBoxes[startpoint[n1random, 0] + agg1 + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 1] + agg2 + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 2] + agg3 + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 3] + agg4 + i + spostx].BackColor = selected;
                }
                else if (pictureBoxes[(i + startpoint[n1random, 1]) + spostx].BackColor == selected && (n1random == 4 || n1random == 6))
                {
                    y = 10;
                }
                else if ((n1random == 4 || n1random == 6) && rotate == 1 && ((i + startpoint[n1random, 1] + spostx + agg2) < pictureBoxes.Length && n1random==4)
                    ||(i + startpoint[n1random, 3] + spostx + agg2) < pictureBoxes.Length && n1random==6 && y<8)    
                {
                    pictureBoxes[startpoint[n1random, 0] + agg1 + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 1] + agg2 + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 2] + agg3 + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 3] + agg4 + i + spostx].BackColor = selected;
                }
                else if ((n1random == 4 || n1random == 6) && rotate == 1 && (i + startpoint[n1random, 1] + spostx + agg2) > pictureBoxes.Length)
                {
                    pictureBoxes[startpoint[n1random, 0] + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 1] + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 2] + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 3] + i + spostx].BackColor = selected;
                    agg1 = 0;
                    agg2 = 0;
                    agg3 = 0;
                    agg4 = 0;
                }


                else if (pictureBoxes[(i + startpoint[n1random, 1] + spostx)].BackColor != selected &&     //stampa moduli
                pictureBoxes[(i + startpoint[n1random, 2] + spostx)].BackColor != selected &&
                pictureBoxes[(i + startpoint[n1random, 3] + spostx)].BackColor != selected &&
                (startpoint[n1random, 0] + agg1 + i < pictureBoxes.Length &&
                startpoint[n1random, 1] + agg2 + i < pictureBoxes.Length &&
                startpoint[n1random, 2] + agg3 + i < pictureBoxes.Length &&
                startpoint[n1random, 3] + agg4 + i < pictureBoxes.Length))
                {
                    pictureBoxes[startpoint[n1random, 0] + agg1 + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 1] + agg2 + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 2] + agg3 + i + spostx].BackColor = selected;
                    pictureBoxes[startpoint[n1random, 3] + agg4 + i + spostx].BackColor = selected;
                }


                }

                //Controllo se l'utente ha perso

                if (((i + startpoint[n1random, 0] + agg1 + spostx) < pictureBoxes.Length&&
                (i + startpoint[n1random, 1] + agg2 + spostx) < pictureBoxes.Length&&
                (i + startpoint[n1random, 2] + agg3 + spostx) < pictureBoxes.Length&&
                (i + startpoint[n1random, 3] +agg4 + spostx) < pictureBoxes.Length))
                {   
                    if((y <= 9 || (n1random == 2  && y <= 10)) && (pictureBoxes[startpoint[n1random, 0] + agg1 + i + spostx].BackColor != selected ||
                   pictureBoxes[startpoint[n1random, 1] + agg2 + i + spostx].BackColor != selected ||
                   pictureBoxes[startpoint[n1random, 2] + agg3 + i + spostx].BackColor != selected ||
                   pictureBoxes[startpoint[n1random, 3] + agg4 + i + spostx].BackColor != selected))
                    controllaperdita();
                }




            //aumento y che mi farà da contatore
            if (i < pictureBoxes.Length - 10)
                    y += 1;

                movement = 0;
                rotation = rotate; 
            
               
            }


            int millisecondi = 0;
            int secondi = 0;
            int minuti = 0;
            int ore = 0;
            string[] tempoarr = { }; 

            private void timer2_Tick(object sender, EventArgs e)
            {
            

            string tempo = txtCrono.Text;
            tempoarr = tempo.Split(':');
            tempo = "";

            ore = Convert.ToInt32(tempoarr[0]);
            minuti = Convert.ToInt32(tempoarr[1]);
            secondi = Convert.ToInt32(tempoarr[2]);
            millisecondi = Convert.ToInt32(tempoarr[3]);

            if (millisecondi == 99)
            {
                tempoarr[3] = "00";
                millisecondi = 0;
                secondi += 1;
            }
            else
            {
                millisecondi += 1;
                if (millisecondi < 10)
                {
                    tempoarr[3] = "0" + millisecondi.ToString();
                }
                else if(millisecondi>9&&millisecondi<100)
                {
                    tempoarr[3] =  millisecondi.ToString();
                }
                
            }


            if (secondi == 59)
            {
                tempoarr[2] = "00";
                secondi = 0;
                minuti += 1;
            }
            else
            {
                if (secondi < 10)
                {
                    tempoarr[2] = "0" + secondi.ToString();
                }
                else if (secondi > 9)
                {
                    tempoarr[2] = secondi.ToString();
                }
            }


            if (minuti == 59)
            {
                tempoarr[1] = "00";
                minuti = 0;
                ore += 1;
            }
            else
            {
                if (minuti < 10)
                {
                    tempoarr[1] = "0" + minuti.ToString();
                }
                else if (minuti > 9)
                {
                    tempoarr[1] = minuti.ToString();
                }
            }

            if (ore == 100)
            {
                controllaperdita();
                MessageBox.Show("You must go on the tournaments", "BYE");
            }
            int i = 0;
            foreach (var item in tempoarr)
            {   
                if(i!=3)
                    tempo = tempo+item+":";
                else
                    tempo = tempo + item;

                i++;
            }
            txtCrono.Text = tempo;

        }




            private void Form1_KeyUp(object sender, KeyEventArgs e)
            {

                Console.WriteLine("KeyUp");
                if (e.KeyCode == Keys.Right)
                {
                    movement = 1;
                }
                if (e.KeyCode == Keys.Left)
                {
                    movement = -1;
                }
                if (e.KeyCode == Keys.Up && rotate==0)
                {
                    rotate = 1;
                }
                else if(e.KeyCode == Keys.Up && rotate == 1)
                {
                    rotate = 0;
                }

                if ((e.KeyCode == Keys.P || e.KeyCode == Keys.Escape) && (tmrMovimento.Enabled == true && tmrCrono.Enabled==true))
                {
                    tmrMovimento.Enabled = false;
                    tmrCrono.Enabled = false;
                }
                else
                {
                    tmrMovimento.Enabled = true;
                    tmrCrono.Enabled = true;
                }
            }

            void calcolaedeelimina()

            {
                int rigapartenza = 0; 
                int caselle = 0;
                int riga = 0;
                try
                {

                for (int i =ciao.Length-10 ; i > 0; i-=10)
                {
                    for (int y = 9; y >= 0; y--)
                    {
                        if (ciao[i + y].BackColor == selected)
                            caselle += 1;
                        if (caselle == 10)
                        {   
                            if(rigapartenza==0)
                            rigapartenza = i;

                            riga += 1;
                            caselle = 0;
                        }
                    }
                    caselle = 0;
                }



                score = score+riga;


                    if (riga>0)
                    {

                        //aggiornare i punti e togliere le caselle + portare quelle sopra sotto
                        for (int i = rigapartenza+10 - (10 * riga); i > 0; i--)
                        {
                            ciao[i + (10 * riga) - 1].BackColor = ciao[i - 1].BackColor;
                        }
                        txtScore.Text = $"{Convert.ToString(Convert.ToInt32(txtScore.Text) + riga)}";
                    if(ID!="userdefault")
                    aggiornascore();
                    }
                }
                catch { }
            }

        void aggiornascore()
         {

            string[] dataFile = File.ReadAllLines(@"..\..\Resources\Data1.txt");

            File.WriteAllText(@"..\..\Resources\Data1.txt",""); 
            foreach (var item in dataFile)
            {
                string[] gg = item.Split(';');

                string nome = gg[0];

                string punti = gg[1];
                

                if (nome.ToLower() == ID.ToLower() && Convert.ToInt32(punti) < score && dataFile[dataFile.Length-1]==item)
                {
                    File.AppendAllText(@"..\..\Resources\Data1.txt", nome + ";" + score.ToString());
                }
                else if (nome.ToLower() == ID.ToLower() && Convert.ToInt32(punti) < score)
                {
                    File.AppendAllText(@"..\..\Resources\Data1.txt", nome + ";" + score.ToString() + "\n");
                }
                else if (dataFile[dataFile.Length - 1] == item)
                {
                    File.AppendAllText(@"..\..\Resources\Data1.txt", nome + ";" + punti);
                }
                else
                {
                    File.AppendAllText(@"..\..\Resources\Data1.txt", nome + ";" + punti+"\n");
                }
            }
            txtBstScore.Text = score.ToString();

                
            
        }




        void controllaperdita()     //il giocatore ha perso quindi mi resetta il form
        {
            if (tmrMovimento.Enabled == true && tmrCrono.Enabled == true)
            {
                tmrMovimento.Enabled = false;
                tmrCrono.Enabled = false;
            }
            
            MessageBox.Show($"{ID} has end the match with {score} points", "WARNING");
            for (int i = 0; i < ciao.Length; i++)
            {
                ciao[i].BackColor = default;
            }
            btnStart.Visible = true;
            movement = 0;
            rotate = 0;
            spostx = 0;
            y = 0;
            i = -10;
            agg1 = 0;
            agg2 = 0;
            agg3 = 0;
            agg4 = 0;
            millisecondi = 0;
            secondi = 0;
            minuti = 0;
            ore = 0;
            txtCrono.Text = "00:00:00:00";
            txtScore.Text = "0";
        }


        private void chooseColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = dlgColor.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                selected = dlgColor.Color;

                for (int i = 0; i < ciao.Length; i++)
                {   
                    if(ciao[i].BackColor != Color.Black)
                    ciao[i].BackColor = selected;
                }
            }
                
        }

        private void chooseFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = dlgFont.ShowDialog();       //chiedo quale font e lo applico dopo l'OK dato dall'utente
            if (dialogResult == DialogResult.OK)
            {
                Font font = dlgFont.Font;
                txtBstScore.Font = font;
                txtCrono.Font = font;
                txtName.Font = font;
                txtScore.Font = font;
                btnStart.Font = font;
                mnsGame.Font = font;

            }
        }

    }

    }
 
