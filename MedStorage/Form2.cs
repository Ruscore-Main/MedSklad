using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace MedStorage
{
    public partial class Form2 : Form
    {
        private DataBaseModelContainer _db;
        Product currentProduct;
        public string printContent = "";
        public Form2(DataBaseModelContainer db = null)
        {
            InitializeComponent();
            if (db == null)
            {
                this._db = new DataBaseModelContainer();
            }
            else
            {
                this._db = db;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Назад
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void button3_Click(object sender, EventArgs e) // добавить товар
        {
            Form3 form3 = new Form3(_db);
            this.Hide();
            form3.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            UpdateList();

            foreach (Storage storage in _db.StorageSet)
            {
                comboBox1.Items.Add(storage);
            }

            comboBox2.Items.Add("Dr. Web");
            comboBox2.Items.Add("Амбробене");
            comboBox2.Items.Add("BAYER");
            comboBox2.Items.Add("ОТИСИФАРМ");
            comboBox2.Items.Add("NOVARTIS");
            comboBox2.Items.Add("STADA");
            comboBox2.Items.Add("SANOFI");

            button1.Enabled = false;
            button2.Enabled = false;
            

        }

        public void UpdateList()
        {
            listBox1.Items.Clear();
            foreach (Product product in _db.ProductSet)
            {
                listBox1.Items.Add(product);
            }
        }

        // Изменение выбора
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentProduct = listBox1.SelectedItem as Product;
                if (currentProduct != null)
                {
                    textBox1.Text = currentProduct.Name;
                    textBox2.Text = Convert.ToString(currentProduct.Price);
                    textBox3.Text = Convert.ToString(currentProduct.ProductWeigth);
                    textBox4.Text = currentProduct.Type;
                    textBox5.Text = currentProduct.ExpirationDate;
                    textBox8.Text = Convert.ToString(currentProduct.Amount);
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(currentProduct.Storage);
                    comboBox2.SelectedIndex = comboBox2.Items.IndexOf(currentProduct.Manufacturer);
                    button1.Enabled = true;
                    button2.Enabled = true;
                }
            }
            catch { }


        }


        private void button1_Click(object sender, EventArgs e) // Изменить товар
        {
            currentProduct.Name = textBox1.Text;
            currentProduct.Price = Convert.ToInt32(textBox2.Text);
            currentProduct.ProductWeigth = Convert.ToInt32(textBox3.Text);
            currentProduct.Type = textBox4.Text;
            currentProduct.ExpirationDate = textBox5.Text;
            currentProduct.Amount = Convert.ToInt32(textBox8.Text);
            currentProduct.Storage = comboBox1.SelectedItem as Storage;
            currentProduct.Manufacturer = Convert.ToString(comboBox2.SelectedItem);

            _db.SaveChanges();
            UpdateList();
            MessageBox.Show("Успешно изменено!");
        }

        private void button2_Click(object sender, EventArgs e) // Удалить
        {
            _db.ProductSet.Remove(currentProduct);
            _db.SaveChanges();
            UpdateList();
            MessageBox.Show("Успешно удалено!");
        }
        /////////

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e) // Вес в МГ
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) // Цена
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e) // Вид
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)  // Название 
        {

        }

        

        private void textBox5_TextChanged(object sender, EventArgs e) // Срок годности
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // Местоположение
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // Производитель
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e) // Количество
        {

        }

        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(printContent, new System.Drawing.Font("Arial", 16), Brushes.Black, 0, 0);
        }

        private DocX outputDoc()
        {
            string pathDocument = "otchet.docx";
            DocX document = DocX.Create(pathDocument);
            document.MarginLeft = 60.0f;
            document.MarginRight = 60.0f;
            document.MarginTop = 60.0f;
            document.MarginBottom = 60.0f;

            document.InsertParagraph("Отчет: Оставшиеся товары на складе\n").Bold().Font("Times New Roman").FontSize(16);
            document.InsertParagraph("").FontSize(16);
            document.InsertParagraph("").FontSize(16);

            Table table = document.AddTable(_db.ProductSet.Count() + 1, 4);
            table.Alignment = Alignment.center;
            table.Design = TableDesign.TableGrid;

            table.Rows[0].Cells[0].Paragraphs[0].Append("Название").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[1].Paragraphs[0].Append("Кол-во").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[2].Paragraphs[0].Append("Тип").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[3].Paragraphs[0].Append("Цена").Font("Times New Roman").FontSize(12).Bold();


            int row = 1;
            foreach (Product product in _db.ProductSet)
            {
                table.Rows[row].Cells[0].Paragraphs[0].Append(product.Name).Font("Times New Roman").FontSize(12);

                table.Rows[row].Cells[1].Paragraphs[0].Append($"{product.Amount}").Font("Times New Roman").FontSize(12);

                table.Rows[row].Cells[2].Paragraphs[0].Append(product.Type).Font("Times New Roman").FontSize(12);

                table.Rows[row].Cells[3].Paragraphs[0].Append($"{product.Price}").Font("Times New Roman").FontSize(12);

                row++;
            }

            table.AutoFit = AutoFit.Contents;


            document.InsertParagraph().InsertTableAfterSelf(table);

            document.Save();

            return document;

        }

        private void button4_Click(object sender, EventArgs e) // печать отчета
        {
            DocX docs = outputDoc();

            printContent = docs.Text;
            PrintDocument printDocument = new PrintDocument();
            PrintDialog printDialog = new PrintDialog();
            printDocument.PrintPage += PrintPageHandler;
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDialog.Document.Print();
            }
        }
    }
}
