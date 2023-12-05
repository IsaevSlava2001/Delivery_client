using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delivery_client
{
    public partial class mainForm : Form
    {
        public int id;
        public string name;
        public mainForm(string iD, object v)
        {
            InitializeComponent();
            id = Convert.ToInt32(iD);
            name = v.ToString();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            label1.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateOrder createOrder = new CreateOrder(id, name);
            createOrder.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            allOrders allOrders = new allOrders(id, name);
            allOrders.Show();
            this.Hide();
        }
    }
}
