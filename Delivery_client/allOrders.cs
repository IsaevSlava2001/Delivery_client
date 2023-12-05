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
    public partial class allOrders : Form
    {
        public int iD;
        public string NaMe;
        DataBase dataBase = new DataBase();
        public allOrders(int id, string name)
        {
            InitializeComponent();
            iD = id;
            NaMe = name;
        }
        private void CreateColumns()
        {

            dataGridView1.Columns.Add("ID_Shopcart", "ID_корзины");
            dataGridView1.Columns.Add("FIO", "Фио клиента");
            dataGridView1.Columns.Add("ID_Tarif", "тариф");
            dataGridView1.Columns.Add("ID_Courier", "Курьер");
            dataGridView1.Columns.Add("ID_Status", "Статус");
            dataGridView1.Columns.Add("Total_Price", "Цена");
            dataGridView1.Columns.Add("Time", "Дата");
            dataGridView1.Columns.Add("Rate", "Оценка");

        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetDouble(5), record.GetDateTime(6),record.GetDouble(7));
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            string querrystring = $"SELECT o.ID_Shopcart,cl.FIO,t.Condition,co.FIO_Courier,s.order_status,o.Total_Price,o.Time,o.Rate FROM [Order] o INNER JOIN Clients cl ON cl.ID_Client = o.ID_Client INNER JOIN Tarif t ON t.ID_Tarif = o.ID_Tarif INNER JOIN Couriers co ON co.ID_Courier = o.ID_Courier INNER JOIN Status s ON s.ID_Order_Status = o.ID_Status WHERE cl.ID_Client = {iD};";

            SqlCommand command = new SqlCommand(querrystring, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
            dataBase.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm(iD.ToString(), NaMe);
            mainForm.Show();
            this.Hide();
        }

        private void allOrders_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }
    }
}
