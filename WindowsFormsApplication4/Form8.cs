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
    public partial class Form8 : Form
    {
        public Form8()
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
            // добавление нового поступления с корректировкой базы товары
            string Query, con, a, c; 
            int b, d;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "SELECT Товары.[Количество] FROM Товары WHERE Товары.[Товар_ID] = " + comboBox1.SelectedValue.ToString() + ";";
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Добавление");
            a = ds.Tables["Добавление"].Rows[0][0].ToString();
            connect.Close();
            c = textBox3.Text;
            b = Convert.ToInt32(c);
            d = Convert.ToInt32(a);
            OleDbConnection conMain = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;");
            OleDbCommand comUpdate = new OleDbCommand("Update Товары set Количество = ? where Товар_ID = ?", conMain);
            comUpdate.Parameters.Add("Количество", OleDbType.Integer).Value = b + d;
            comUpdate.Parameters.Add("Товар_ID", OleDbType.Integer).Value = comboBox1.SelectedValue;
            conMain.Open();
            comUpdate.ExecuteNonQuery();
            conMain.Close();
            Query = "INSERT INTO [Поступления] ([Дата], [Товар_ID], [Кол-во], [Цена]) VALUES ('" + dateTimePicker1.Text + "','" + comboBox1.SelectedValue.ToString() + "','" + textBox3.Text + "','" + textBox4.Text + "');";
            OleDbDataAdapter dan1 = new OleDbDataAdapter(Query, con);
            connect.Open();
            DataSet ds1 = new DataSet();
            ds1.Clear();
            dan1.Fill(ds1, "Добавление");
            this.Close();
            connect.Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    textBox3.Focus();
                return;
            }
            e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    textBox4.Focus();
                return;
            }
            e.Handled = true;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            // заполнение ComboBox'а товар
            string con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            OleDbDataAdapter adap = new OleDbDataAdapter("select*from Товары", con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataTable tbl = new DataTable();
            adap.Fill(tbl);
            comboBox1.DataSource = tbl;
            comboBox1.DisplayMember = "Наименование";
            comboBox1.ValueMember = "Товар_ID";
            connect.Close();
            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
