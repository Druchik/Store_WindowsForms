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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Form9 form9 = new Form9();
            form9.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // добавление новой продажи с корректировкой базы товары
            string Query, con, a, c;
            int b, d;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "SELECT Товары.[Количество] FROM Товары WHERE Товары.[Товар_ID] = " + comboBox2.SelectedValue.ToString() + ";";
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Добавление");
            a = ds.Tables["Добавление"].Rows[0][0].ToString();
            connect.Close();
            c = textBox2.Text;
            b = Convert.ToInt32(c);
            d = Convert.ToInt32(a);
            if (d >= b)
            {
                OleDbConnection conMain = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;");
                OleDbCommand comUpdate = new OleDbCommand("Update Товары set Количество = ? where Товар_ID = ?", conMain);
                comUpdate.Parameters.Add("Количество", OleDbType.Integer).Value = d - b;
                comUpdate.Parameters.Add("Товар_ID", OleDbType.Integer).Value = comboBox2.SelectedValue;
                conMain.Open();
                comUpdate.ExecuteNonQuery();
                conMain.Close();
                Query = "INSERT INTO Продажи (Товар, Количество, Клиент_ID, Сотрудник_ID, Номер_продажи, Дата_продажи) VALUES ('" + comboBox2.SelectedValue.ToString() + "','" + textBox2.Text + "','" + comboBox3.SelectedValue.ToString() + "','" + comboBox1.SelectedValue.ToString() + "','" + textBox6.Text + "','" + dateTimePicker1.Text + "');";
                OleDbDataAdapter dan1 = new OleDbDataAdapter(Query, con);
                DataSet ds1 = new DataSet();
                connect.Open();
                ds1.Clear();
                dan1.Fill(ds1, "Добавление");
                this.Close();
                connect.Close();
            }
            else MessageBox.Show ("Малое количество товара на складе");
            
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    textBox6.Focus();
                return;
            }
            e.Handled = true;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // заполнение трех Combobox'ов: товар, клиент, сотрудник
            string con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            OleDbDataAdapter adap1 = new OleDbDataAdapter("select*from Товары", con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataTable tbl1 = new DataTable();
            adap1.Fill(tbl1);
            comboBox2.DataSource = tbl1;
            comboBox2.DisplayMember = "Наименование";
            comboBox2.ValueMember = "Товар_ID";
            connect.Close();
            comboBox2.SelectedIndex = -1;
            OleDbDataAdapter dan1 = new OleDbDataAdapter("select*from Клиенты", con);
            connect.Open();
            DataSet ds1 = new DataSet();
            ds1.Clear();
            dan1.Fill(ds1, "Клиенты");
            ds1.Tables["Клиенты"].Columns.Add("fio", typeof(string), "Фамилия+' '+Имя+' '+Отчество");
            connect.Close();
            comboBox3.DataSource = ds1.Tables["Клиенты"];
            comboBox3.DisplayMember = "fio";
            comboBox3.ValueMember = "Код";
            comboBox3.SelectedIndex = -1;
            OleDbDataAdapter dan2 = new OleDbDataAdapter("select*from Сотрудники", con);
            connect.Open();
            DataSet ds2 = new DataSet();
            ds2.Clear();
            dan2.Fill(ds2, "Сотрудники");
            ds2.Tables["Сотрудники"].Columns.Add("fio", typeof(string), "Фамилия+' '+Имя+' '+Отчество");
            connect.Close();
            comboBox1.DataSource = ds2.Tables["Сотрудники"];
            comboBox1.DisplayMember = "fio";
            comboBox1.ValueMember = "Сотрудник_ID";
            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '1') && (e.KeyChar <= '9'))
            {
                return;
            }
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    textBox2.Focus();
                return;
            }
            e.Handled = true;
        }
    }
}
