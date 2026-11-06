
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;


namespace InventoryManagementSystem
{
    public partial class btnClear : Form
    {
        // Database connection string
        string connectionString =
            "Server=localhost;Database=inventory_db;Uid=root;Pwd=;";

        public btnClear()
        {
            InitializeComponent();
        }

        // Database connection method
        private void ConnectDatabase()
        {
            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    MessageBox.Show(
                        "Database connected successfully!"
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Connection failed: " + ex.Message
                    );
                }
            }
        }

        // Form Load Event
        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectDatabase();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtSupplier_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text == "" ||
        txtQuantity.Text == "" ||
        txtPrice.Text == "")
            {
                MessageBox.Show("Please fill in required fields!");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO products " +
                        "(product_name, category, quantity, unit_price, supplier) " +
                        "VALUES (@name, @category, @quantity, @price, @supplier)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@quantity", int.Parse(txtQuantity.Text));
                    cmd.Parameters.AddWithValue("@price", decimal.Parse(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@supplier", txtSupplier.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Product Saved Successfully!");

                    txtProductName.Clear();
                    txtCategory.Clear();
                    txtQuantity.Clear();
                    txtPrice.Clear();
                    txtSupplier.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all text boxes
            txtProductName.Clear();
            txtCategory.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtSupplier.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtProductName.Clear();
            txtCategory.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtSupplier.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "DELETE FROM products WHERE product_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", txtProductName.Tag);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Product Deleted Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "UPDATE products SET product_name=@name, category=@category, quantity=@quantity, unit_price=@price, supplier=@supplier WHERE product_id=@id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                cmd.Parameters.AddWithValue("@quantity", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@supplier", txtSupplier.Text);
                cmd.Parameters.AddWithValue("@id", txtProductName.Tag);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Product Updated Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "SELECT * FROM products WHERE product_name LIKE @name";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@name", "%" + txtProductName.Text + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                dgvProducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtProductName.Text = dgvProducts.Rows[e.RowIndex].Cells["product_name"].Value.ToString();
                txtCategory.Text = dgvProducts.Rows[e.RowIndex].Cells["category"].Value.ToString();
                txtQuantity.Text = dgvProducts.Rows[e.RowIndex].Cells["quantity"].Value.ToString();
                txtPrice.Text = dgvProducts.Rows[e.RowIndex].Cells["unit_price"].Value.ToString();
                txtSupplier.Text = dgvProducts.Rows[e.RowIndex].Cells["supplier"].Value.ToString();

                txtProductName.Tag = dgvProducts.Rows[e.RowIndex].Cells["product_id"].Value;
            }
        }
    }
}