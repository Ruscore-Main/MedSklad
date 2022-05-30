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
    public partial class Form4 : Form
    {
        private DataBaseModelContainer _db;
        Product currentProduct;
        Customer currentCustomer;
        public Form4(DataBaseModelContainer db, Customer customer)
        {
            InitializeComponent();
            currentCustomer = customer;
            this._db = db;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // назад
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e) // добавить в корзину
        {

            NacladnayaItem productItem = currentCustomer.Nacladnaya.NacladnayaItem.FirstOrDefault(el => el.ProductId == currentProduct.Id);

            if (productItem == null)
            {
                productItem = new NacladnayaItem()
                {
                    Amount = 1,
                    Product = currentProduct
                };

                currentCustomer.Nacladnaya.NacladnayaItem.Add(productItem);
            }
            else
            {
                productItem.Amount += 1;
            }

            _db.SaveChanges();
            
        }

        private void UpdateList()
        {
            listBox1.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e) // перейти в корзину
        {
            Form5 form5 = new Form5(_db, currentCustomer,this);
            form5.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;
            

            button1.Enabled = false;

            foreach (Product product in _db.ProductSet)
            {
                listBox1.Items.Add(product);
            }

            if (currentCustomer.Nacladnaya == null)
            {
                Nacladnaya nacladnaya = new Nacladnaya();

                currentCustomer.Nacladnaya = nacladnaya;

                _db.SaveChanges();
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
                    textBox6.Text = currentProduct.Storage.Address;
                    textBox7.Text = currentProduct.Manufacturer;
                    textBox8.Text = Convert.ToString(currentProduct.Amount);
                    button1.Enabled = true;
                }
            }
            catch { }
        }

        //////////

        private void textBox1_TextChanged(object sender, EventArgs e) // Название
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) // Цена
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e) // Вес В МГ
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e) // Вид
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e) // Срок годности
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e) // Местоположение
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e) // Производитель
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e) // Количество
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) // Выбор сотрудника
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // Фильтр по производителю
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // Фильтр по местоположению
        {

        }

        

        
    }
}
