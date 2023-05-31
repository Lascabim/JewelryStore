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
using Microsoft.VisualBasic;


namespace Loja_Online
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            getStock();
        }


        double[] ItemSArray = new double[3];

        private void getStock()
        {
            string tempPath = System.IO.Path.GetTempPath();
            string filepath = tempPath + "/stock.txt";

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
                    ColarS = rawline.Substring(indexPause + 1, ColarL);

                    //GET ANELS
                    AnelL = (indexPause3 - indexPause2) - 1;
                    AnelS = rawline.Substring(indexPause2 + 1, AnelL);


                    ItemSArray[0] = Convert.ToDouble(RelogioS);
                    ItemSArray[1] = Convert.ToDouble(ColarS);
                    ItemSArray[2] = Convert.ToDouble(AnelS);
                }
            }

            sr.Close();
            label8.Text = "Stock Atual: " + ItemSArray[0];
            label2.Text = "Stock Atual: " + ItemSArray[1];
            label3.Text = "Stock Atual: " + ItemSArray[2];
        }
    
        private void updateStock(double relogioU, double colarU, double anelU)
        {
            string tempPath = System.IO.Path.GetTempPath();
            string filepath = tempPath + "/stock.txt";

            if(File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            FileStream file = new FileStream(filepath, FileMode.Append, FileAccess.Write);
            using (StreamWriter wt = new StreamWriter(file))
            {
                wt.WriteLine(relogioU.ToString() + "|" + colarU.ToString() + "|" + anelU.ToString() + "|");
                wt.Close();
            }

            file.Close();
            getStock();
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            this.Hide();

            var Forms2 = new Form2();
            Forms2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            var Forms1 = new Form1("");
            Forms1.Show();
        }

        private void btnAneis_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            getStock();
            if (ItemSArray[0] == 0 || ItemSArray[0] < 0)
            {
                MessageBox.Show("Este item não tem stock para remover!");
            }
            else if (ItemSArray[0] > 0)
            {
                double qRemove = Convert.ToDouble(Interaction.InputBox("Quantidade a remover do produto: "));
                double CanBeRemoved = (ItemSArray[0] - qRemove);

                if(CanBeRemoved == 0 || CanBeRemoved > 0)
                {
                    updateStock(CanBeRemoved, ItemSArray[1], ItemSArray[2]);
                }
                else
                {
                    MessageBox.Show("Introduz um valor vállido a ser removido!");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            getStock();
            if (ItemSArray[0] == 100 || ItemSArray[0] > 100)
            {
                MessageBox.Show("O stock máximo permitido no armazém e de 100 unidades!");
            }
            else if (ItemSArray[0] < 100)
            {
                double qAdd = Convert.ToDouble(Interaction.InputBox("Quantidade do produto a adicionar: "));
                double CanBeAdded = (ItemSArray[0] + qAdd);

                if (CanBeAdded <= 100)
                {
                    updateStock(CanBeAdded, ItemSArray[1], ItemSArray[2]);
                }
                else
                {
                    MessageBox.Show("O stock máximo permitido no armazém e de 100 unidades!");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getStock();
            if (ItemSArray[1] == 100 || ItemSArray[1] > 100)
            {
                MessageBox.Show("O stock máximo permitido no armazém e de 100 unidades!");
            }
            else if (ItemSArray[1] < 100)
            {
                double qAdd = Convert.ToDouble(Interaction.InputBox("Quantidade do produto a adicionar: "));
                double CanBeAdded = (ItemSArray[1] + qAdd);

                if (CanBeAdded <= 100)
                {
                    updateStock(ItemSArray[0], CanBeAdded, ItemSArray[2]);
                }
                else
                {
                    MessageBox.Show("O stock máximo permitido no armazém e de 100 unidades!");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            getStock();
            if (ItemSArray[1] == 0 || ItemSArray[1] < 0)
            {
                MessageBox.Show("Este item não tem stock para remover!");
            }
            else if (ItemSArray[1] > 0)
            {
                double qRemove = Convert.ToDouble(Interaction.InputBox("Quantidade a remover do produto: "));
                double CanBeRemoved = (ItemSArray[1] - qRemove);

                if (CanBeRemoved == 0 || CanBeRemoved > 0)
                {
                    updateStock(ItemSArray[0], CanBeRemoved, ItemSArray[2]);
                }
                else
                {
                    MessageBox.Show("Introduz um valor vállido a ser removido!");
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            getStock();
            if (ItemSArray[2] == 100 || ItemSArray[2] > 100)
            {
                MessageBox.Show("O stock máximo permitido no armazém e de 100 unidades!");
            }
            else if (ItemSArray[2] < 100)
            {
                double qAdd = Convert.ToDouble(Interaction.InputBox("Quantidade do produto a adicionar: "));
                double CanBeAdded = (ItemSArray[2] + qAdd);

                if (CanBeAdded <= 100)
                {
                    updateStock(ItemSArray[0], ItemSArray[1], CanBeAdded);
                }
                else
                {
                    MessageBox.Show("O stock máximo permitido no armazém e de 100 unidades!");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            getStock();
            if (ItemSArray[2] == 0 || ItemSArray[2] < 0)
            {
                MessageBox.Show("Este item não tem stock para remover!");
            }
            else if (ItemSArray[2] > 0)
            {
                double qRemove = Convert.ToDouble(Interaction.InputBox("Quantidade a remover do produto: "));
                double CanBeRemoved = (ItemSArray[2] - qRemove);

                if (CanBeRemoved == 0 || CanBeRemoved > 0)
                {
                    updateStock(ItemSArray[0], ItemSArray[1], CanBeRemoved);
                }
                else
                {
                    MessageBox.Show("Introduz um valor vállido a ser removido!");
                }
            }
        }
    }
}
