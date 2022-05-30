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
    public partial class Form3 : Form
    {
        DataBaseModelContainer _db;
        public Form3(DataBaseModelContainer db)
        {
            InitializeComponent();
            this._db = db;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Назад
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Close();
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
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
        }

        // Добавление товара
        private void button1_Click(object sender, EventArgs e) // Добавить товар
        {
            /*try
            {*/
                Product product = new Product()
                {
                    Name = textBox1.Text,
                    Price = Convert.ToInt32(textBox2.Text),
                    ProductWeigth = Convert.ToInt32(textBox3.Text),
                    Type = textBox4.Text,
                    ExpirationDate = textBox5.Text,
                    StoragePlace = textBox6.Text,
                    Manufacturer = Convert.ToString(comboBox2.SelectedItem),
                    Storage = comboBox1.SelectedItem as Storage,
                    Amount = Convert.ToInt32(textBox8.Text)
                };
                _db.ProductSet.Add(product);
                _db.SaveChanges();
                MessageBox.Show("Успешно добавлено!");
                Form2 form2 = new Form2();
                form2.Show();
                this.Close();


/*
            }
            catch
            {
                MessageBox.Show("Неправильный формат данных!");
            }*/
        }

        ///////////////////////


        private void textBox1_TextChanged(object sender, EventArgs e) // Название
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) // Цена
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e) // Вес в МГ
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e) // Вид
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
