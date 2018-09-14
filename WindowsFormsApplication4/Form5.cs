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
    public partial class Form5 : Form
    {
        int i = 0;
        int n = 0;
        public Form5()
        {
            InitializeComponent();
        }

        private void TabLichDan()
        {
            string Query, con;
            // Заполнение формы данными из таблицы
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "SELECT Сотрудники.Сотрудник_ID, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Отделы.Отдел, Сотрудники.Адрес, Сотрудники.Дата_приема, Сотрудники.Пароль FROM Отделы INNER JOIN Сотрудники ON Отделы.Отдел_ID = Сотрудники.Отдел_ID;";
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Сотрудники");
            textBox6.Text = ds.Tables["Сотрудники"].Rows[i][0].ToString();
            textBox1.Text = ds.Tables["Сотрудники"].Rows[i][1].ToString();
            textBox2.Text = ds.Tables["Сотрудники"].Rows[i][2].ToString();
            textBox3.Text = ds.Tables["Сотрудники"].Rows[i][3].ToString();
            comboBox1.Text = ds.Tables["Сотрудники"].Rows[i][4].ToString();
            textBox5.Text = ds.Tables["Сотрудники"].Rows[i][5].ToString();
            dateTimePicker1.Text = ds.Tables["Сотрудники"].Rows[i][6].ToString();
            textBox4.Text = ds.Tables["Сотрудники"].Rows[i][7].ToString();
            connect.Close();
            n = ds.Tables["Сотрудники"].Rows.Count;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Form9 form9 = new Form9();
            form9.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Query, con;
            // Сохраняем нового сотрудника
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "INSERT INTO Сотрудники (Фамилия, Имя, Отчество, Адрес, Отдел_ID, Дата_приема, Пароль) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + comboBox1.SelectedValue.ToString() + "','" + dateTimePicker1.Text + "','" + textBox4.Text + "');";
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Добавление");
            this.Close();
            connect.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {            
            string con;
            // заполнение поля отдел
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            OleDbDataAdapter adap = new OleDbDataAdapter("select*from Отделы", con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataTable tbl = new DataTable();
            adap.Fill(tbl);
            comboBox1.DataSource = tbl;
            comboBox1.DisplayMember = "Отдел";
            comboBox1.ValueMember = "Отдел_ID";
            connect.Close();
            comboBox1.SelectedIndex = -1;
            TabLichDan();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox4.MaxLength = 3;
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

        private void button3_Click(object sender, EventArgs e)
        {
            // удаление/увольнение сотрудника
            OleDbConnection conMain = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;");
            OleDbCommand comDelete = new OleDbCommand("Delete from Сотрудники where Сотрудник_ID = ?", conMain);
            comDelete.Parameters.Add("Сотрудник_ID", OleDbType.Integer).Value = textBox6.Text;
            conMain.Open();
            comDelete.ExecuteNonQuery();
            conMain.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (i < n - 1)
            {
                i++;
                TabLichDan();
            }

            else
            {
                MessageBox.Show("Конец списка");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (i > 0)
            {
                i--;
                TabLichDan();
            }

            else
            {
                MessageBox.Show("Начало списка");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // корректировка данных о сотруднике
            OleDbConnection conMain = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;");
            OleDbCommand comUpdate = new OleDbCommand("Update Сотрудники set Фамилия = ?, Имя = ?, Отчество = ?, Адрес = ?, Отдел_ID = ?, Дата_приема = ?, Пароль = ?  where Сотрудник_ID = ?", conMain);
            comUpdate.Parameters.Add("Фамилия", OleDbType.VarChar, 50).Value = textBox1.Text;
            comUpdate.Parameters.Add("Имя", OleDbType.VarChar, 50).Value = textBox2.Text;
            comUpdate.Parameters.Add("Отчество", OleDbType.VarChar, 50).Value = textBox3.Text;
            comUpdate.Parameters.Add("Адрес", OleDbType.VarChar, 50).Value = textBox5.Text;
            comUpdate.Parameters.Add("Отдел_ID", OleDbType.Integer).Value = comboBox1.SelectedValue;
            comUpdate.Parameters.Add("Дата_приема", OleDbType.DBDate).Value = dateTimePicker1.Value;
            comUpdate.Parameters.Add("Пароль", OleDbType.Integer).Value = textBox4.Text;
            comUpdate.Parameters.Add("Сотрудник_ID", OleDbType.Integer).Value = textBox6.Text;
            conMain.Open();
            comUpdate.ExecuteNonQuery();
            conMain.Close();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
  
}
