﻿using System;
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
    public partial class Form7 : Form
    {
        public Form7()
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
            // добавление нового поставщика
            string Query, con;
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Магазин2.mdb;Persist Security Info=False;";
            Query = "INSERT INTO Поставщики (Организация, Телефон, Адрес) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "');";
            OleDbDataAdapter dan = new OleDbDataAdapter(Query, con);
            OleDbConnection connect = new OleDbConnection(con);
            connect.Open();
            DataSet ds = new DataSet();
            ds.Clear();
            dan.Fill(ds, "Добавление");
            this.Close();
            connect.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }
            if (e.KeyChar == '-')
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
