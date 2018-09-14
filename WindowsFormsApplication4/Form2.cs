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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {// В зависимости от открытой вкладки загружаем таблицу из БД
            string Query, con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    Query = "SELECT Товары.Товар_ID, Товары.Наименование, Товары.Количество, Товары.Цена, Поставщики.Организация, Отделы.Отдел FROM Отделы INNER JOIN (Поставщики INNER JOIN Товары ON Поставщики.Поставщик_ID = Товары.Поставщик_ID) ON Отделы.Отдел_ID = Товары.Отдел_ID; ";
                    break;
                case 1:
                    Query = "Select * from Отделы";
                    break;
                case 2:
                    Query = "SELECT Сотрудники.Сотрудник_ID, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Отделы.Отдел, Сотрудники.Адрес, Сотрудники.Дата_приема, Сотрудники.Пароль FROM Отделы INNER JOIN Сотрудники ON Отделы.Отдел_ID = Сотрудники.Отдел_ID Order BY Сотрудники.Сотрудник_ID;";
                    break;
                case 3:
                    Query = "Select * from Поставщики";
                    break;
                case 4:
                    Query = "Select * from Клиенты";
                    break;
                case 5:
                    Query = "SELECT Поступления.Код, Поступления.Дата, Товары.Наименование, Поступления.[Кол-во], Поступления.Цена FROM Товары INNER JOIN Поступления ON Товары.Товар_ID = Поступления.Товар_ID;";
                    break;
                default:
                    Query = "SELECT Продажи.Код, Товары.Наименование, Продажи.Количество, Товары.Цена, [Продажи]![Количество]*[Товары]![Цена] AS Сумма, [Клиенты]![Фамилия]+' '+[Клиенты]![Имя]+' '+[Клиенты]![Отчество] AS Клиент, [Сотрудники]![Фамилия]+' '+[Сотрудники]![Имя]+' '+[Сотрудники]![Отчество] AS Сотрудник, Продажи.[Номер_продажи], Продажи.[Дата_продажи] FROM Товары INNER JOIN (Сотрудники INNER JOIN(Клиенты INNER JOIN Продажи ON Клиенты.Код = Продажи.Клиент_ID) ON Сотрудники.Сотрудник_ID = Продажи.Сотрудник_ID) ON Товары.Товар_ID = Продажи.Товар; ";
                    break;
            }
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            DataSet ds = new DataSet();
            connect.Open();
            ds.Clear();
            dan.Fill(ds, "Магазин");
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    dataGridView1.DataSource = ds.Tables["Магазин"].DefaultView;
                    break;
                case 1:
                    dataGridView2.DataSource = ds.Tables["Магазин"].DefaultView;
                    break;
                case 2:
                    dataGridView3.DataSource = ds.Tables["Магазин"].DefaultView;
                    break;
                case 3:
                    dataGridView4.DataSource = ds.Tables["Магазин"].DefaultView;
                    break;
                case 4:
                    dataGridView5.DataSource = ds.Tables["Магазин"].DefaultView;
                    break;
                case 5:
                    dataGridView6.DataSource = ds.Tables["Магазин"].DefaultView;
                    break;
                default:
                    dataGridView7.DataSource = ds.Tables["Магазин"].DefaultView;
                    break;
            }
            connect.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        { // Запрос самый дорогой товар
            Form3 form3 = new Form3();
            form3.Show();
            string Query, con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "SELECT TOP 1 * FROM Товары ORDER BY Товары.Цена DESC;";
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Запрос");
            form3.dataGridView1.DataSource = ds.Tables["Запрос"].DefaultView;
            connect.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {//Запрос товары поставщиков
            Form3 form3 = new Form3();
            form3.Show();
            string Query, con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "SELECT Товары.Наименование, Товары.Цена, Поставщики.Организация FROM Поставщики INNER JOIN Товары ON Поставщики.Поставщик_ID = Товары.Поставщик_ID ORDER BY Поставщики.Организация DESC;";
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Запрос");
            form3.dataGridView1.DataSource = ds.Tables["Запрос"].DefaultView;
            connect.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {// Запрос поступления за последние 60 дней
            Form3 form3 = new Form3();
            form3.Show();
            string Query, con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "SELECT Товары.Наименование, Поступления.[Кол-во], Поступления.Цена, Поставщики.Организация, Поступления.Дата FROM Поставщики INNER JOIN(Товары INNER JOIN Поступления ON Товары.Товар_ID = Поступления.Товар_ID) ON Поставщики.Поставщик_ID = Товары.Поставщик_ID WHERE (((Поступления.Дата)Between Date() And Date() - 60));";
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Запрос");
            form3.dataGridView1.DataSource = ds.Tables["Запрос"].DefaultView;
            connect.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {// Запрос сумма продаж
            if (dateTimePicker4.Value > dateTimePicker3.Value)
            {
                MessageBox.Show("Неправильный интервал даты");
            }
            else
            {
                Form3 form3 = new Form3();
                form3.Show();
                string Query, con;
                string formateddate1 = "#" + dateTimePicker4.Value.Month.ToString() + "/" + dateTimePicker4.Value.Day.ToString() + "/" + dateTimePicker4.Value.Year.ToString() + "#";
                string formateddate2 = "#" + dateTimePicker3.Value.Month.ToString() + "/" + dateTimePicker3.Value.Day.ToString() + "/" + dateTimePicker3.Value.Year.ToString() + "#";
                con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
                Query = "SELECT Товары.Наименование, Продажи.Количество, Товары.Цена, [Продажи]![Количество]*[Товары]![Цена] AS Итого FROM Товары INNER JOIN Продажи ON Товары.Товар_ID = Продажи.Товар WHERE Продажи.[Дата_продажи] Between " + formateddate1 + " And " + formateddate2 + ";";
                OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
                OleDbConnection connect = new OleDbConnection(con);
                connect.Open();
                DataSet ds = new DataSet();
                ds.Clear();
                dan.Fill(ds, "Запрос");
                form3.dataGridView1.DataSource = ds.Tables["Запрос"].DefaultView;
                connect.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {// Запрос продажи по сотрудникам
            Form3 form3 = new Form3();
            form3.Show();
            string Query, con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "SELECT [Сотрудники]![Фамилия] + ' ' +[Сотрудники]![Имя] + ' ' +[Сотрудники]![Отчество] AS Сотрудник, Продажи.[Номер_продажи], Продажи.[Дата_продажи], Товары.Наименование, Продажи.Количество, Товары.Цена, [Продажи]![Количество]*[Товары]![Цена] AS Сумма, [Клиенты]![Фамилия]+' '+[Клиенты]![Имя]+' '+[Клиенты]![Отчество] AS Клиент FROM Товары INNER JOIN (Сотрудники INNER JOIN (Клиенты INNER JOIN Продажи ON Клиенты.Код = Продажи.Клиент_ID) ON Сотрудники.Сотрудник_ID = Продажи.Сотрудник_ID) ON Товары.Товар_ID = Продажи.Товар ORDER BY[Сотрудники]![Фамилия]+' '+[Сотрудники]![Имя]+' '+[Сотрудники]![Отчество];";
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Запрос");
            form3.dataGridView1.DataSource = ds.Tables["Запрос"].DefaultView;
            connect.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {// Запрос информация о поставщике по № продажи
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("Введите номер продажи");
            }
            else
            { 
                Form3 form3 = new Form3();
                form3.Show();
                string Query, con;
                con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
                Query = "SELECT Продажи.[Номер_продажи], Поставщики.Организация, Поставщики.Адрес, Поставщики.Телефон FROM(Поставщики INNER JOIN Товары ON Поставщики.Поставщик_ID = Товары.Поставщик_ID) INNER JOIN Продажи ON Товары.Товар_ID = Продажи.Товар WHERE Продажи.[Номер_продажи] = " + textBox2.Text + ";";
                OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
                OleDbConnection connect = new OleDbConnection(con);
                connect.Open();
                DataSet ds = new DataSet();
                ds.Clear();
                dan.Fill(ds, "Запрос");
                form3.dataGridView1.DataSource = ds.Tables["Запрос"].DefaultView;
                connect.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {// Запрос информация о покупателе по № продажи
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Введите номер продажи");
            }
            else
            {
                Form3 form3 = new Form3();
                form3.Show();
                string Query, con;
                con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
                Query = "SELECT Продажи.[Номер_продажи], Клиенты.Фамилия, Клиенты.Имя, Клиенты.Отчество, Клиенты.Организация, Клиенты.Телефон FROM Клиенты INNER JOIN Продажи ON Клиенты.Код = Продажи.Клиент_ID WHERE Продажи.[Номер_продажи] =" + textBox1.Text + ";";
                OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
                OleDbConnection connect = new OleDbConnection(con);
                connect.Open();
                DataSet ds = new DataSet();
                ds.Clear();
                dan.Fill(ds, "Запрос");
                form3.dataGridView1.DataSource = ds.Tables["Запрос"].DefaultView;
                connect.Close();
            }     
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
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
                    textBox3.Focus();
                return;
            }
            e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }
            
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    textBox1.Focus();
                return;
            }
            e.Handled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            FindGoods();
        }

        private void button13_Click(object sender, EventArgs e)
        {// Поиск товара по цене

            if (textBox4.Text.Length == 0)
            {
                MessageBox.Show("Введите цену");
            }
            else
            {
                Form3 form3 = new Form3();
                form3.Show();
                string Query, con;
                con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
                Query = "SELECT Товары.Товар_ID, Товары.Наименование, Товары.Количество, Товары.[Цена], Поставщики.Организация, Отделы.Отдел FROM Отделы INNER JOIN (Поставщики INNER JOIN Товары ON Поставщики.Поставщик_ID = Товары.Поставщик_ID) ON Отделы.Отдел_ID = Товары.Отдел_ID WHERE Товары.[Цена] =" + textBox4.Text + ";";
                OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
                OleDbConnection connect = new OleDbConnection(con);
                connect.Open();
                DataSet ds = new DataSet();
                ds.Clear();
                dan.Fill(ds, "Запрос");
                form3.dataGridView1.DataSource = ds.Tables["Запрос"].DefaultView;
                connect.Close();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FindProvider();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                return;
            }
            
            if (Char.IsWhiteSpace(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Space)
                    return;
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    FindGoods();
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '"')
            {
                return;
            }
            if (Char.IsLetter(e.KeyChar))
            {
                return;
            }
            
            if (Char.IsWhiteSpace(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Space)
                    return;
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    FindProvider();
                return;
            }
            e.Handled = true;
        }

        private void FindProvider()
        {// Поиск товара по поставщику
            if (textBox5.Text.Length == 0)
            {
                MessageBox.Show("Введите поставщика");
            }
            else
            {
                Form3 form3 = new Form3();
                form3.Show();
                string Query, con;
                con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
                Query = "SELECT Товары.Товар_ID, Товары.Наименование, Товары.Количество, Товары.Цена, Поставщики.Организация, Отделы.Отдел FROM Отделы INNER JOIN(Поставщики INNER JOIN Товары ON Поставщики.Поставщик_ID = Товары.Поставщик_ID) ON Отделы.Отдел_ID = Товары.Отдел_ID WHERE Поставщики.Организация = '" + textBox5.Text + "';";
                OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
                OleDbConnection connect = new OleDbConnection(con);
                connect.Open();
                DataSet ds = new DataSet();
                ds.Clear();
                dan.Fill(ds, "Поставщик товара");
                form3.dataGridView1.DataSource = ds.Tables["Поставщик товара"].DefaultView;
                connect.Close();
            }
        }
        private void FindGoods()
        { // Поиск товара по наименованию
            if (textBox3.Text.Length == 0)
            {
                MessageBox.Show("Введите название товара");
            }
            else
            {
                Form3 form3 = new Form3();
                form3.Show();
                string Query, con;
                con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
                Query = "SELECT Товары.Товар_ID, Товары.Наименование, Товары.Количество, Товары.Цена, Поставщики.Организация, Отделы.Отдел FROM Отделы INNER JOIN(Поставщики INNER JOIN Товары ON Поставщики.Поставщик_ID = Товары.Поставщик_ID) ON Отделы.Отдел_ID = Товары.Отдел_ID WHERE Товары.Наименование = '" + textBox3.Text + "';";
                OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
                OleDbConnection connect = new OleDbConnection(con);
                connect.Open();
                DataSet ds = new DataSet();
                ds.Clear();
                dan.Fill(ds, "Поиск товара");
                form3.dataGridView1.DataSource = ds.Tables["Поиск товара"].DefaultView;
                connect.Close();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {// Хит продаж
            Form3 form3 = new Form3();
            form3.Show();
            string Query, con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "SELECT TOP 1 Продажи.Код, Товары.Наименование, Продажи.Количество, Товары.Цена, [Продажи]![Количество]*[Товары]![Цена] AS Сумма, [Клиенты]![Фамилия]+' '+[Клиенты]![Имя]+' '+[Клиенты]![Отчество] AS Клиент, [Сотрудники]![Фамилия]+' '+[Сотрудники]![Имя]+' '+[Сотрудники]![Отчество] AS Сотрудник, Продажи.[Номер_продажи], Продажи.[Дата_продажи] FROM Товары INNER JOIN (Сотрудники INNER JOIN(Клиенты INNER JOIN Продажи ON Клиенты.Код = Продажи.Клиент_ID) ON Сотрудники.Сотрудник_ID = Продажи.Сотрудник_ID) ON Товары.Товар_ID = Продажи.Товар ORDER BY Продажи.Количество DESC;";
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Запрос");
            form3.dataGridView1.DataSource = ds.Tables["Запрос"].DefaultView;
            connect.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {// Товарный чек
            if (textBox6.Text.Length == 0)
            {
                MessageBox.Show("Введите номер продажи");
            }
            else
            {
                Form10 form10 = new Form10();
                form10.Show();
                string Query, con;
                int k, j, sum;
                con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
                Query = "SELECT Товары.Наименование, Продажи.Количество, Товары.Цена, [Продажи]![Количество]*[Товары]![Цена] AS Сумма FROM Товары INNER JOIN Продажи ON Товары.Товар_ID = Продажи.Товар WHERE Продажи.[Номер_продажи] =" + textBox6.Text + ";";
                OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
                OleDbConnection connect = new OleDbConnection(con);
                connect.Open();
                DataSet ds = new DataSet();
                ds.Clear();
                dan.Fill(ds, "Товарный чек");
                form10.dataGridView1.DataSource = ds.Tables["Товарный чек"].DefaultView;
                connect.Close();
                k = form10.dataGridView1.RowCount;
                sum = 0;
                for (int i = 0; i < k - 1; i++)
                {
                    j = Convert.ToInt32(form10.dataGridView1.Rows[i].Cells[3].Value);
                    sum += j;
                }
                form10.label10.Text = Convert.ToString(sum);
                Query = "SELECT[Сотрудники]![Фамилия] + ' ' +[Сотрудники]![Имя] + ' ' +[Сотрудники]![Отчество] AS Продавец, Продажи.[Номер_продажи], Продажи.[Дата_продажи] FROM Сотрудники INNER JOIN Продажи ON Сотрудники.Сотрудник_ID = Продажи.Сотрудник_ID WHERE Продажи.[Номер_продажи] =" + textBox6.Text + ";";
                OleDbDataAdapter dan1 = new OleDbDataAdapter(Query, con);
                connect.Open();
                DataSet ds1 = new DataSet();
                ds1.Clear();
                dan1.Fill(ds1, "Товарный чек");
                form10.label5.Text = ds1.Tables["Товарный чек"].Rows[0][0].ToString();
                form10.label11.Text = ds1.Tables["Товарный чек"].Rows[0][1].ToString();
                form10.label3.Text = ds1.Tables["Товарный чек"].Rows[0][2].ToString();
                connect.Close();
                
            }
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

        private void button17_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("Неправильный интервал даты");
            }
            else
            {
                PeriodSales();
            }
        }

        private void PeriodSales()
        {//Продажи за определенный период
            Form3 form3 = new Form3();
            form3.Show();
            string Query, con;
            string formateddate1 = "#" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Year.ToString() + "#";
            string formateddate2 = "#" + dateTimePicker2.Value.Month.ToString() + "/" + dateTimePicker2.Value.Day.ToString() + "/" + dateTimePicker2.Value.Year.ToString() + "#";
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "SELECT Продажи.[Номер_продажи], Продажи.[Дата_продажи], Товары.Наименование, Продажи.Количество, Товары.Цена, [Продажи]![Количество]*[Товары]![Цена] AS Сумма, [Клиенты]![Фамилия]+' '+[Клиенты]![Имя]+' '+[Клиенты]![Отчество] AS Клиент, [Сотрудники]![Фамилия]+' '+[Сотрудники]![Имя]+' '+[Сотрудники]![Отчество] AS Сотрудник FROM Товары INNER JOIN (Сотрудники INNER JOIN(Клиенты INNER JOIN Продажи ON Клиенты.Код = Продажи.Клиент_ID) ON Сотрудники.Сотрудник_ID = Продажи.Сотрудник_ID) ON Товары.Товар_ID = Продажи.Товар WHERE Продажи.[Дата_продажи] Between " + formateddate1 + " And " + formateddate2 + " ORDER BY Продажи.[Дата_продажи];";
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Запрос");
            form3.dataGridView1.DataSource = ds.Tables["Запрос"].DefaultView;
            connect.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12();
            form.Show();
        }
    }
}

