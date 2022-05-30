using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedStorage
{
    public partial class Form2 : Form
    {
        private DataBaseModelContainer _db;
        Product currentProduct;
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

        

        

        
    }
}
