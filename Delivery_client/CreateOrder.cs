using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Delivery_client
{
    public partial class CreateOrder : Form
    {
        DataBase dataBase = new DataBase();
        string curDish;
        int curOrder;
        string curTarif;
        string queryorderdish = "INSERT INTO Shopcart_has_Products (ID_Shopcart, ID_Product, Product_Quantity) VALUES";
        public int ID;
        public string NaMe;
        public CreateOrder(int id, string name)
        {
            InitializeComponent();
            ID = id;
            NaMe = name;
        }

        private void ReadSingleRow(ComboBox cb, IDataRecord record)
        {
            cb.Items.Add("Товар: " + record.GetString(0) + ". Состав: " + record.GetString(1) + ". Цена: " + record.GetString(2));
        }
        private void createorder_Load(object sender, EventArgs e)
        {
            numericUpDown1.Text = "1";
            numericUpDown1.ReadOnly = true;
            string querrystring = $"SELECT [Name], Sostav, Product_value FROM Products ";
            SqlCommand command = new SqlCommand(querrystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(comboBox1, reader);
            }
            reader.Close();
            querrystring = $"SELECT ID_Tarif, Condition, Value FROM Tarif ";
            command = new SqlCommand(querrystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(comboBox2, reader);
            }
            reader.Close();

            querrystring = $"INSERT INTO Shopcarts (Shopcart_Costs) VALUES (0);";
            command = new SqlCommand(querrystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            reader = command.ExecuteReader();
            reader.Close();
            querrystring = $"SELECT MAX(ID_Shopcart) FROM Shopcarts";
            command = new SqlCommand(querrystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                curOrder = reader.GetInt32(0);
            }
            reader.Close();
            //querrystring = $"INSERT INTO [Order] (ID_Shopcart,ID_Client,ID_Tarif,ID_Courier,ID_Status,Total_Price,Time) VALUES ({curOrder}, {ID},{}});";
            command = new SqlCommand(querrystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            reader = command.ExecuteReader();
            reader.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            curDish = comboBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int count = Convert.ToInt32(numericUpDown1.Text);
            textBox1.AppendText(Environment.NewLine);
            textBox1.Text += $"{curDish} x{count}";
            for (int i = 0; i < count; i++)
            {
                queryorderdish += $"({curOrder},(SELECT ID FROM Products WHERE [Name] LIKE '{curDish.Split(':')[1]}'), {count}),";
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            queryorderdish = queryorderdish.TrimEnd(',');
            queryorderdish += ";";

            SqlCommand command = new SqlCommand(queryorderdish, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
            dataBase.CloseConnection();
            queryorderdish = $"INSERT INTO [Order] (ID_Shopcart,ID_Client,ID_Tarif,ID_Courier,ID_Status,Total_Price)";
            MessageBox.Show($"Ваш заказ создан. Ему присвоен номер {curOrder}. Отслеживать его вы можете в окне \"Посмотреть мои заказы\".", "Внимание!", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm createOrderGuest = new mainForm(ID.ToString(),NaMe);
            this.Hide();
            createOrderGuest.ShowDialog();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            curTarif = comboBox2.Text;
        }
    }
}
