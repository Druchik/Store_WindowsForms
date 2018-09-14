using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "Магазин2DataSet.Продажи". При необходимости она может быть перемещена или удалена.
            this.ПродажиTableAdapter.Fill(this.Магазин2DataSet.Продажи);

            this.reportViewer1.RefreshReport();
        }
    }
}
