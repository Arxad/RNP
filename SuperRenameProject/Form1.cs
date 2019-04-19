using System;
using System.IO;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SuperRenameProject
{
    public partial class Form1 : Form
    {
        public string nameIMG, imagepath;
        public string[,] abr = new string[100, 2];


        public void ReadFiles(string path) //Вывод списка фаилов
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                string e = Path.GetExtension(file);// Проверка расширеня фаила
                if ((e == ".jpg") | (e == ".png") | (e == ".JPG") | (e == ".PNG"))
                    listBox3.Items.Add(file.Remove(0, file.LastIndexOf('\\') + 1));
            }
        }

        public Form1()
        {
            InitializeComponent();

            if (File.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, "SystemInfo.txt"))) // Проверка наличия SystemInfo
            {
                string[] line = File.ReadAllLines(System.IO.Path.Combine(Environment.CurrentDirectory, "SystemInfo.txt"), Encoding.Default);// Запись всех строк к строковый массив
                textBox1.Text = line[0];//вывод пути импорта
                textBox3.Text = line[1];//Вывод пути экспорта

                //abr = new string[(line.Length) - 2, 2];// Создание строкового массива нужной длины

                for (int i = 0; i < ((line.Length) - 2); i++)
                {
                    string[] result = line[i + 2].Split(new char[] { '^' });
                    abr[i, 0] = result[0];
                    abr[i, 1] = result[1];
                    listBox1.Items.Add(abr[i, 0]);
                }
                string path = textBox1.Text;

                ReadFiles(path); //Вывод списка фаилов
            }


        }

        private void button1_Click(object sender, EventArgs e)// импорт
        {
            listBox3.Items.Clear(); //очистка списка филов 

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = (folderBrowserDialog1.SelectedPath) + "\\";
            }
            string pathIn = textBox1.Text;
            string pathOut = textBox3.Text;

            ReadFiles(pathIn); //Вывод списка фаилов

            string x1 = pathIn;// test
            string x2 = pathOut;// test
            string[] line1 = new string[] { x1, x2 };// test
            //System.IO.File.AppendAllText("SystemInfo.txt", path); 
            System.IO.File.WriteAllLines("SystemInfo.txt", line1, Encoding.Default);//Создание SystemInfo  

        }


        private void ImageVis(object sender, EventArgs e)
        {
            //textBox2.Text = Convert.ToString(listBox3.SelectedIndex);
            //ListView.SelectedIndexCollection indexes = this.ListView1.SelectedIndeces;
            string two = Convert.ToString(listBox3.Items[listBox3.SelectedIndex]);
            imagepath = textBox1.Text + "\\" + two;
            using (var memoryStream = new MemoryStream(File.ReadAllBytes(imagepath)))
            {
                pictureBox1.Image = new Bitmap(memoryStream);
            }
            //pictureBox1.Image = Image.FromFile(imagepath);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = (textBox2.Text+"_"+Convert.ToString(abr[listBox1.SelectedIndex,1]));
        }


        private void Сохранить_Click(object sender, EventArgs e)
        {
            string extension = Path.GetExtension(imagepath);
            string path2 = (textBox3.Text + "\\" + textBox4.Text + extension);




            string path = textBox1.Text;

            

            //pictureBox1.Image = Image.FromFile(imagepath);
            //Close();
            File.Move(imagepath, path2);

            listBox3.Items.Clear();
            ReadFiles(path);
            textBox4.Text = path2;

            listBox3.SetSelected(0, true);

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
