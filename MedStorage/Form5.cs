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
    public partial class Form5 : Form
    {
        DataBaseModelContainer _db;
        NacladnayaItem currentNacladnayaItem;
        Customer currentCustomer;
        Employee currentEmployee;
        public string printContent = "";
        Form4 form4;
        public Form5(DataBaseModelContainer db, Customer customer, Form4 backForm)
        {
            InitializeComponent();
            _db = db;
            currentCustomer = customer;
            form4 = backForm;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //назад
        {
            form4.Show();
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;


            UpdateList();

            button1.Enabled = false;
            button2.Enabled = false;
            UpdateSum();

            foreach (Employee employee in _db.EmployeeSet)
            {
                comboBox1.Items.Add(employee);
            }

        }

        private void UpdateSum()
        {
            int sum = 0;
            foreach (NacladnayaItem product in currentCustomer.Nacladnaya.NacladnayaItem)
            {
                sum += product.Product.Price * product.Amount;
            }

            currentCustomer.Nacladnaya.Sum = sum;
            labelSum.Text = "Общая сумма: " + Convert.ToString(sum);
            _db.SaveChanges();
        }

        private void UpdateList()
        {
            listBox1.Items.Clear();
            foreach (NacladnayaItem product in currentCustomer.Nacladnaya.NacladnayaItem)
            {
                listBox1.Items.Add(product);
            }
        }

        // Именение выбора
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentNacladnayaItem = listBox1.SelectedItem as NacladnayaItem;
                if (currentNacladnayaItem != null)
                {
                    Product currentProduct = currentNacladnayaItem.Product;
                    textBox1.Text = currentProduct.Name;
                    textBox2.Text = Convert.ToString(currentProduct.Price);
                    textBox3.Text = Convert.ToString(currentProduct.ProductWeigth);
                    textBox4.Text = currentProduct.Type;
                    textBox5.Text = currentProduct.ExpirationDate;
                    textBox6.Text = currentProduct.Storage.Address;
                    textBox7.Text = currentProduct.Manufacturer;
                    textBox8.Text = Convert.ToString(currentNacladnayaItem.Amount);
                    button1.Enabled = true;
                }
            }
            catch { }
        }

        private void textBox8_TextChanged(object sender, EventArgs e) // количество
        {
            try
            {
                currentNacladnayaItem.Amount = Convert.ToInt32(textBox8.Text);
                UpdateSum();
                if (currentNacladnayaItem.Amount <= currentNacladnayaItem.Product.Amount)
                {
                    _db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Недостаточно товара на складе!");
                }
            }
            
            catch { }
        }

        private void button1_Click(object sender, EventArgs e) // удалить из корзины
        {
            currentCustomer.Nacladnaya.NacladnayaItem.Remove(currentNacladnayaItem);
            _db.SaveChanges();
            UpdateSum();
            UpdateList();
            MessageBox.Show("Успешно удалено!");
        }

        private void button2_Click(object sender, EventArgs e) // заказать
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

        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(printContent, new System.Drawing.Font("Arial", 16), Brushes.Black, 0, 0);
        }

        // Выбор сотрудника для накладной
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentEmployee = comboBox1.SelectedItem as Employee;
            button2.Enabled = true;
        }

        private DocX outputDoc()
        {
            string pathDocument = "first.docx";
            DocX document = DocX.Create(pathDocument);
            document.MarginLeft = 60.0f;
            document.MarginRight = 60.0f;
            document.MarginTop = 60.0f;
            document.MarginBottom = 60.0f;

            document.InsertParagraph("Накладная").Bold().Font("Times New Roman").FontSize(16);
            document.InsertParagraph($"Обслуживающий сотрудник: {currentEmployee.Name} {currentEmployee.Surname} - {currentEmployee.Post}").Font("Times New Roman").FontSize(14);
            document.InsertParagraph("").FontSize(16);
            document.InsertParagraph("").FontSize(16);

            Table table = document.AddTable(currentCustomer.Nacladnaya.NacladnayaItem.Count() + 1, 5);
            table.Alignment = Alignment.center;
            table.Design = TableDesign.TableGrid;

            table.Rows[0].Cells[0].Paragraphs[0].Append("Название").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[1].Paragraphs[0].Append("Кол-во").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[2].Paragraphs[0].Append("Вид").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[3].Paragraphs[0].Append("Производитель").Font("Times New Roman").FontSize(12).Bold();
            table.Rows[0].Cells[4].Paragraphs[0].Append("Общая-сумма").Font("Times New Roman").FontSize(12).Bold();


            int row = 1;
            foreach (NacladnayaItem item in currentCustomer.Nacladnaya.NacladnayaItem)
            {
                Product product = item.Product;
                table.Rows[row].Cells[0].Paragraphs[0].Append(product.Name).Font("Times New Roman").FontSize(12);

                table.Rows[row].Cells[1].Paragraphs[0].Append($"{item.Amount}").Font("Times New Roman").FontSize(12);

                table.Rows[row].Cells[2].Paragraphs[0].Append(product.Type).Font("Times New Roman").FontSize(12);

                table.Rows[row].Cells[3].Paragraphs[0].Append(product.Manufacturer).Font("Times New Roman").FontSize(12);

                table.Rows[row].Cells[4].Paragraphs[0].Append($"{product.Price * item.Amount}").Font("Times New Roman").FontSize(12);
                row++;
            }

            table.AutoFit = AutoFit.Contents;


            document.InsertParagraph().InsertTableAfterSelf(table);

            document.Save();

            return document;

        }

        ///////////
        private void textBox1_TextChanged(object sender, EventArgs e) // название
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) // цена
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e) // вес в МГ
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e) // Вид
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e) // срок ггодности
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e) // местоположение
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e) // производитель
        {

        }

        
    }
}
