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
    public partial class Form1 : Form
    {
        string currentUser = "Customer";
        DataBaseModelContainer _db = new DataBaseModelContainer();
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration registrationForm = new Registration(_db, this);
            registrationForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentUser == "Customer")
            {
                Customer customer = _db.CustomerSet.FirstOrDefault(el => el.Name == textBox1.Text && el.Password == textBox4.Text);
                if (customer != null)
                {
                    Form4 form4 = new Form4(_db, customer); // for zakazchik
                    form4.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Заказчик не найден!");
                }

            }
            else
            {
                Employee employee = _db.EmployeeSet.FirstOrDefault(el => el.Name == textBox1.Text && el.Password == textBox4.Text);
                if (employee != null)
                {
                    Form2 form2 = new Form2(_db); // for sotrudnik
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Сотрудник не найден!");
                }

            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;

            if (_db.StorageSet.Count() == 0)
            {
                Storage storage1 = new Storage()
                {
                    Capacity = "4500",
                    Address = "г. Казань, ул Ешкина"
                };

                Storage storage2 = new Storage()
                {
                    Capacity = "9000",
                    Address = "г. Москва, ул Королёва"
                };

                _db.StorageSet.Add(storage1);
                _db.StorageSet.Add(storage2);

                _db.SaveChanges();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            currentUser = "Employee";

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            currentUser = "Customer";
        }


    }
}
