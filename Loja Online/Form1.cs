using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace Loja_Online
{

    public partial class Form1 : Form
    {
        static string actualType = "M";
        static double RelogioT = 0, ColarT = 0, AnelT = 0;
        
        public Form1(string LC)
        {
            InitializeComponent();

            if(LC == "LC")
            {
                RelogioT = 0; 
                ColarT = 0; 
                AnelT = 0;
            }

            panel2.Visible = true;

            string tempPath = System.IO.Path.GetTempPath();
            string filepathS = tempPath + "/stock.txt";

            if (!File.Exists(filepathS))
            {
                FileStream fileS = new FileStream(filepathS, FileMode.Append, FileAccess.Write);
                using (StreamWriter wt = new StreamWriter(fileS))
                {
                    wt.WriteLine("50|50|50|");
                    wt.Close();
                }

                fileS.Close();
            }
        }

        public bool checkStock(string item)
        {
            string tempPath = System.IO.Path.GetTempPath();
            string filepath = tempPath + "/stock.txt";

            bool readyTG = false;

            if (!File.Exists(filepath))
            {
                FileStream file = new FileStream(filepath, FileMode.Append, FileAccess.Write);
                using (StreamWriter writetext = new StreamWriter(file))
                {
                    writetext.WriteLine("50|50|50|");
                    writetext.Close();
                }

                file.Close();
                readyTG = false;
            }
            else
            {
                string RelogioS, ColarS, AnelS;

                StreamReader sr;
                sr = new StreamReader(filepath);

                using (sr)
                {

                    while (sr.Peek() > -1)
                    {
                        int RelogioL, ColarL, AnelL;

                        int indexPause = 0;
                        int indexPause2 = 0;
                        int indexPause3 = 0;

                        string rawline = sr.ReadLine();

                        //GET |
                        indexPause = rawline.IndexOf('|', indexPause);
                        indexPause2 = rawline.IndexOf('|', indexPause + 1);
                        indexPause3 = rawline.IndexOf('|', indexPause2 + 1);

                        // GET RELOGIOS
                        RelogioS = rawline.Substring(0, indexPause);
                        RelogioL = RelogioS.Length;

                        //GET COLARS
                        ColarL = (indexPause2 - indexPause) - 1;
                        ColarS = rawline.Substring(indexPause + 1 , ColarL);

                        //GET ANELS
                        AnelL = (indexPause3 - indexPause2) - 1;
                        AnelS = rawline.Substring(indexPause2 + 1 , AnelL);

                        if (item == "Relogio")
                        {
                            if((Convert.ToDouble(RelogioS) - RelogioT) > 0)
                            {
                                RelogioT++;
                                readyTG = true;
                            }
                        }
                        else if (item == "Colar")
                        {
                            if ((Convert.ToDouble(ColarS) - ColarT) > 0)
                            {
                                ColarT++;
                                readyTG = true;
                            }
                        }
                        else if (item == "Anel")
                        {
                            if ((Convert.ToDouble(AnelS) - AnelT) > 0)
                            {
                                AnelT++;
                                readyTG = true;
                            }
                        }
                    }
                }

                sr.Close();
            }

            return readyTG;
        }

        public string sendData(string item, string preco)
        {

            string tempPath = System.IO.Path.GetTempPath();

            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            //MessageBox.Show(tempPath);

            string filepath = tempPath + "/pedidos.txt";

            //string moradaIN = Interaction.InputBox("Introduz a morada: ", "Envio de pedido!");

            FileStream file;
            file = new FileStream(filepath, FileMode.Append, FileAccess.Write);

            string moradaIN = "** Morada do Cliente **";

            Random random = new Random();
            int idGen = random.Next(1000);

            using (StreamWriter writetext = new StreamWriter(file))
            {
               writetext.WriteLine("Items:" + item + "|" + preco + "|" + moradaIN + "|30/09/2022|" + "ID" + idGen + "|");
            }

            file.Close();

            //MessageBox.Show("O pedido foi enviado com sucesso, relembra-mos que o pagamento é efetuado em dinheiro na hora!");

            return "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string password = (Interaction.InputBox("Introduz a password: ", "Gestão da Loja")).ToLower();

            if (password == "admin")
            {
                this.Hide();
                var StaffForm = new Form2();
                StaffForm.Show();
            }
        }

        private void btnMas_Click(object sender, EventArgs e)
        {
            if (actualType == "F")
            {
                actualType = "M";
            }
        }

        private void btnFem_Click(object sender, EventArgs e)
        {

            if (actualType == "M")
            {
                actualType = "F";
            }
        }

        private void btnRelogios_Click(object sender, EventArgs e)
        {
            //panel2.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAneis_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool stockAvailable = checkStock("Colar");
            if (stockAvailable)
            {
                sendData("Colar", "25");
                MessageBox.Show("Produto adicionado ao carrinho!");
            }
            else
            {
                MessageBox.Show("Produto sem stock!");
            }
        }

        private void btnColares_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool stockAvailable = checkStock("Anel");
            if (stockAvailable)
            {
                sendData("Anel", "15");
                MessageBox.Show("Produto adicionado ao carrinho!");
            }
            else
            {
                MessageBox.Show("Produto sem stock!");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool stockAvailable = checkStock("Relogio");
            if (stockAvailable)
            {
                sendData("Relogio", "50");
                MessageBox.Show("Produto adicionado ao carrinho!");
            }
            else
            {
                MessageBox.Show("Produto sem stock!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string tempPath = System.IO.Path.GetTempPath();
            string filepath = tempPath + "/pedidos.txt";


            if (!File.Exists(filepath))
            {
                File.Create(filepath);
            }

            var info = new FileInfo(filepath);
            if (info.Length == 0)
            {
                MessageBox.Show("Carrinho vazio!");
            }
            else
            {
                this.Hide();

                Carrinho form = new Carrinho();
                form.Show();
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
