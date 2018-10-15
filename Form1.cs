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

namespace StockControl
{
    public partial class Form1 : Form
    {

        //sql connection to the database
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-MTC8R1N\MICHAEL;Initial Catalog='Stock Control';Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void panelAddProduct_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picBoxLogo_Click(object sender, EventArgs e)
        {
            panelAddProduct.Visible = false;
            panelPurchaseOrder.Visible = false;
            

        }

        private void picBoxBtnAddProduct_Click(object sender, EventArgs e)
        {

            panelAddProduct.Visible = true;
            panelPurchaseOrder.Visible = false;
            
        }

        private void InPanelAddProduct_btnAddProduct_Click(object sender, EventArgs e)
        {
            //open a connection to the database
            sqlCon.Open();
            SqlCommand cmd = sqlCon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into TBL_PRODUCTS values('" +InPanelAddProduct_txtProductID.Text + "', '" +InPanelAddProduct_txtProductName.Text+ "', '" +InPanelAddProduct_txtProductCategory.Text + "' , '" +InPanelAddProduct_ImageOfProduct.Image+ "' , '" +InPanelAddProduct_txtProductDescription.Text + "' , '" +InPanelAddProduct_txtProductStockQuantity.Text + "' , '" +InPanelAddProduct_ComboBoxSaleFlag.Text + "' , '" +InPanelAddProduct_txtProductSalePrice.Text + "' )";
            cmd.ExecuteNonQuery();
            //Close the connection to the database
            sqlCon.Close();


            


        }

        private void InPanelAddProduct_btnBrowseImage_Click(object sender, EventArgs e)
        {
            openFileDialogImagePanelAddProduct.Filter = "jpg|*.jpg";

            DialogResult res = openFileDialogImagePanelAddProduct.ShowDialog();
            if(res == DialogResult.OK)
            {
                InPanelAddProduct_ImageOfProduct.Image = Image.FromFile(openFileDialogImagePanelAddProduct.FileName);
            }


        }

        private void picBoxSearchEditDelete_Click(object sender, EventArgs e)
        {
            
            panelAddProduct.Visible = false;
            try
            {
                disp_data();
            }
            catch (Exception error)
            {
                
            }
            


        }

        private void panelSearchEditDelete_Paint(object sender, PaintEventArgs e)
        {

        }


        public void disp_data()
        {
            sqlCon.Open();
            SqlCommand cmd = sqlCon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from TBL_PRODUCTS ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            InpaneSearchEditDelete_DataGridViewAllProducts.DataSource = dt;
            sqlCon.Close();
          
        }

        public void disp_dataProductOrders()
        {
            sqlCon.Open();
            SqlCommand cmd = sqlCon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from TBL_PURCHASE_ORDER ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            InPanelPurchaseOrder_DataGridViewAllOrders.DataSource = dt;
            sqlCon.Close();

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InPanelSearchEditDelete_btnDisplayAllProducts_Click(object sender, EventArgs e)
        {
            sqlCon.Open();
            SqlCommand cmd = sqlCon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update TBL_PRODUCTS set CHR_Name= '"+InPanelAddProduct_txtProductName.Text+"' where NUM_ID = '"+InPanelAddProduct_txtSearchProductID.Text+"'";
            cmd.CommandText = "update TBL_PRODUCTS set CHR_Category= '" + InPanelAddProduct_txtProductCategory.Text + "' where NUM_ID = '" + InPanelAddProduct_txtSearchProductID.Text + "'";
            cmd.CommandText = "update TBL_PRODUCTS set CHR_Description= '" + InPanelAddProduct_txtProductDescription.Text + "' where NUM_ID = '" + InPanelAddProduct_txtSearchProductID.Text + "'";
            cmd.CommandText = "update TBL_PRODUCTS set CHR_Saleflag= '" + InPanelAddProduct_ComboBoxSaleFlag.Text + "' where NUM_ID = '" + InPanelAddProduct_txtSearchProductID.Text + "'";
            cmd.CommandText = "update TBL_PRODUCTS set FLT_SalePrice= '" + InPanelAddProduct_txtProductSalePrice.Text + "' where NUM_ID = '" + InPanelAddProduct_txtSearchProductID.Text + "'";
            cmd.CommandText = "update TBL_PRODUCTS set NUM_Stock_Quantity= '" + InPanelAddProduct_txtProductStockQuantity.Text + "' where NUM_ID = '" + InPanelAddProduct_txtSearchProductID.Text + "'";
            cmd.CommandText = "update TBL_PRODUCTS set IMG_Image= '" + InPanelAddProduct_ImageOfProduct.Image + "' where NUM_ID = '" + InPanelAddProduct_txtSearchProductID.Text + "'";


            cmd.ExecuteNonQuery();
            sqlCon.Close();
            disp_data();
        }

        private void InPanelSearchEditDelete_btnDeleteProduct_Click(object sender, EventArgs e)
        {
            sqlCon.Open();
            SqlCommand cmd = sqlCon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from TBL_PRODUCTS where NUM_ID ='" + InPanelAddProduct_txtProductID.Text + "'";
            cmd.ExecuteNonQuery();
            sqlCon.Close();
            disp_data();
        }

        private void InPanelAddProduct_txtSearchProductID_TextChanged(object sender, EventArgs e)
        {

        }

        private void InPanelAddProduct_btnSearchProduct_Click(object sender, EventArgs e)
        {
            disp_data();
            
        }

        private void picBoxBtnPurchaseOrder_Click(object sender, EventArgs e)
        {
            panelPurchaseOrder.Visible = true;
            panelAddProduct.Visible = false;
            disp_dataProductOrders();

        }

       private void InPanelPurchaseOrder_btnAddOrder_Click(object sender, EventArgs e)
        {
            //open a connection to the database
            sqlCon.Open();
            SqlCommand cmd = sqlCon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into TBL_PURCHASE_ORDER values('" + InPanelPurchaseOrder_txtOrderID.Text + "', '" + InPanelPurchaseOrder_txtProductIDs.Text + "', '" + InPanelPurchaseOrder_ProductQuantity.Text + "' , '" + InPanelPurchaseOrder_txtPurchaseOrderDate.Text + "' )";
            cmd.ExecuteNonQuery();
            //Close the connection to the database
            sqlCon.Close();
        }

        private void InpaneSearchEditDelete_DataGridViewAllProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void InPanelAddProduct_btnProductSearch_Click(object sender, EventArgs e)
        {
            //open a connection to the database
            sqlCon.Open();
            SqlCommand cmd = sqlCon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from TBL_PRODUCTS where NUM_ID = '"+InPanelAddProduct_txtSearchProductID.Text+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            InpaneSearchEditDelete_DataGridViewAllProducts.DataSource = dt;
            //Close the connection to the database
            sqlCon.Close();
        }

        private void InPanelPurchaseOrder_btnProductSearch_Click(object sender, EventArgs e)
        {
            //open a connection to the database
            sqlCon.Open();
            SqlCommand cmd = sqlCon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from TBL_PURCHASE_ORDER where NUM_Order_ID = '" + InPanelPurchaseOrder_txtSearchProduct.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            InPanelPurchaseOrder_DataGridViewAllOrders.DataSource = dt;
            //Close the connection to the database
            sqlCon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            disp_dataProductOrders();
        }
    }
}
