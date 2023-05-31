using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Loja_Online
{
    public partial class Form2 : Form
    {
        public int GetData(string IDRemove)
        {

            string tempPath = System.IO.Path.GetTempPath();
            string filepath = tempPath + "/pedidosC.txt";


            if (!File.Exists(filepath))
            {

                File.Create(filepath);
            }

            StreamReader srr;
            srr = new StreamReader(filepath);

            using (srr)
            {
                    bool readyTORemove = false;

                    while (srr.Peek() > -1)
                    {
                        string Item = "", Preco, Morada, Date, ID;

                        int ItemL, PrecoL, MoradaL, DateL, IDL;

                        int itemPause = 0;

                        int indexPause = 0;
                        int indexPause2 = 0;
                        int indexPause3 = 0;
                        int indexPause4 = 0;
                        int indexPause5 = 0;

                        bool eitem = false, epreco = false, emorada = false, edate = false, eid = false;

                        string rawline = srr.ReadLine();
                        //MessageBox.Show(rawline);

                        //GET |
                        itemPause = rawline.IndexOf('|', itemPause) - 6;


                        indexPause = rawline.IndexOf('|', indexPause);
                        indexPause2 = rawline.IndexOf('|', indexPause + 1);
                        indexPause3 = rawline.IndexOf('|', indexPause2 + 1);
                        indexPause4 = rawline.IndexOf('|', indexPause3 + 1);
                        indexPause5 = rawline.IndexOf('|', indexPause4 + 1);


                        // GET ITEM

                        Item = rawline.Substring(6, itemPause);
                        //MessageBox.Show(Item);
                        ItemL = Item.Length;

                        eitem = true;

                        //GETPRICE

                        // o -1 é para não contar a barra
                        PrecoL = (indexPause2 - indexPause) - 1;
                        //MessageBox.Show(PrecoL.ToString());

                        if (ItemL < 7)
                        {
                            int tempItem = 7 - ItemL;

                            ItemL = tempItem + ItemL;
                        }

                        Preco = rawline.Substring(itemPause + ItemL, PrecoL);
                        //MessageBox.Show(Preco);

                        epreco = true;
                        //GETADRESS

                        MoradaL = (indexPause3 - indexPause2) - 1;

                        Morada = rawline.Substring(itemPause + ItemL + PrecoL + 1, MoradaL);
                        //MessageBox.Show(Morada);

                        emorada = true;
                        //GETDATE

                        DateL = (indexPause4 - indexPause3) - 1;

                        Date = rawline.Substring(itemPause + ItemL + PrecoL + 1 + MoradaL + 1, DateL);
                        //MessageBox.Show(Date);

                        edate = true;
                        //GETID

                        IDL = (indexPause5 - indexPause4) - 1;

                        ID = rawline.Substring(itemPause + ItemL + PrecoL + 1 + MoradaL + 1 + DateL + 1, IDL);
                        //MessageBox.Show(ID);

                        eid = true;

                        if (IDRemove != "nulo")
                        {

                            if (IDRemove == ID)
                            {
                                readyTORemove = true;

                                MessageBox.Show("ID REMOVIDO !!");

                                int LINHA = srr.Peek();
                            }
                        }
                        else
                        {
                            if (eitem && epreco && emorada && edate && eid)
                            {
                                dataGridView1.Rows.Add(ID, Item, Preco, Morada, Date, Date);
                            }
                            else
                            {
                                MessageBox.Show("ERROR");
                            }
                        }

                    }

                    if (IDRemove != "nulo" && readyTORemove == false)
                    {
                        MessageBox.Show("ID não encontrado!");
                    }
                srr.Close();
            }

            srr.Close();

            return 0;
        }

        public Form2()
        {
            InitializeComponent();

            /*
            string screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();
            string screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();

            int screenWidthF = int.Parse(screenWidth) - 100;
            int screenHeightF = int.Parse(screenHeight) - 100;

            Size = new Size(screenWidthF, screenHeightF);
            */

            GetData("nulo");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnQueixas_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            string IDRemove = Interaction.InputBox("Introduz o ID do pedido a remover: ", "Gestão da Loja");

            GetData(IDRemove);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Hide();

            var Forms1 = new Form1("NULO");
            Forms1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            var Forms3 = new Form3();
            Forms3.Show();
        }
    }
}
