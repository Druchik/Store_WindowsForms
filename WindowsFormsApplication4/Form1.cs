using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            employee();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|Магазин2.mdb;Persist Security Info=False;";
            OleDbDataAdapter dan = new OleDbDataAdapter("select*from Сотрудники", con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Таблица");
            ds.Tables["Таблица"].Columns.Add("fio", typeof(string), "Фамилия+' '+Имя+' '+Отчество");
            connect.Close();
            comboBox1.DataSource = ds.Tables["Таблица"];
            comboBox1.DisplayMember = "fio";
            comboBox1.ValueMember = "Сотрудник_ID";
            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Back)
                return;
            }
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    employee();
            }
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }
            e.Handled = true;
        }
        private void employee()
        {
            string con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|Магазин2.mdb;Persist Security Info=False;";
            OleDbConnection connect = new OleDbConnection(con);
            if (comboBox1.Text.Length == 0)
            {
                MessageBox.Show("Поля не должны быть пустыми");
            }
            else
            {
                OleDbCommand oleDbCommand = new OleDbCommand("SELECT * FROM Сотрудники WHERE Сотрудники.Сотрудник_ID = " + comboBox1.SelectedValue.ToString() + " AND Сотрудники.Пароль = " + textBox1.Text + "", connect);
                connect.Open();
                if (textBox1.Text.Length == 0 ^ comboBox1.Text.Length == 0)
                {
                    MessageBox.Show("Поля не должны быть пустыми");
                }
                else if (oleDbCommand.ExecuteScalar() == null)
                {
                    MessageBox.Show("Не верный пользователь или пароль");
                    // Действия, осуществляемые при неудачном входе
                }
                else
                {
                    MessageBox.Show("Вход выполнен успешно");
                    // Действия, осуществляемые при удачном входе
                    Form2 form2 = new Form2();
                    this.Hide();
                    form2.Show();
                }
            }
            connect.Close();
        }
    }
}
