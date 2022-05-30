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
    public partial class Registration : Form
    {
        string currentUser = "Customer";
        DataBaseModelContainer _db;
        Form1 form1;
        public Registration(DataBaseModelContainer db, Form1 backForm)
        {
            InitializeComponent();
            this._db = db;
            this.form1 = backForm;
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            currentUser = "Employee";
            groupBox1.Visible = false;
            groupBox2.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            currentUser = "Customer";
            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (currentUser == "Customer")
                {
                    Customer customer = new Customer()
                    {
                        Name = textBox1.Text,
                        Surname = textBox2.Text,
                        Patronymic = textBox3.Text,
                        Address = textBox4.Text,
                        PhoneNumber = textBox5.Text,
                        Password = textBox6.Text
                    };

                    _db.CustomerSet.Add(customer);
                }

                else
                {
                    Employee employee = new Employee()
                    {
                        Name = textBox12.Text,
                        Surname = textBox11.Text,
                        Patronymic = textBox10.Text,
                        Password = textBox7.Text,
                        Post = Convert.ToString(comboBox1.SelectedItem)
                    };

                    _db.EmployeeSet.Add(employee);
                }

                _db.SaveChanges();
                MessageBox.Show("Успешно зарегистрировано!");
                this.Close();
            }

            catch
            {
                MessageBox.Show("Неправильный формат данных!");
            }
        }

        private void Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            form1.Show();
        }
    }
}
