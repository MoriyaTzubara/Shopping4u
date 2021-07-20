using BE;
using Shopping4u.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Shopping4u.DAL
{
    public class DAL : IDAL
    {
        private MySqlConnection connection;
        private string server;
        private string username;
        private string password;
        private string database;
        public DAL()
        {
            Initialize();
        }
        private void Initialize()
        {
            server = "localhost";
            username = "root";
            password = "tratzon1";
            database = "shopping4u";
            connection = new MySqlConnection();
            connection.ConnectionString = $"server={server};user id={username};password={password};database={database}";
        }
        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        throw new Exception("Cannot connect to server.  Contact administrator");
                    case 1045:
                        throw new Exception("Invalid username/password, please try again");
                }
                return false;
            }
        }
        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region SELECT
        public List<Product> GetProducts()
        {
            List<Product> result = new List<Product>();
            string query = "SELECT * FROM product";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add(new Product
                    {
                        id = int.Parse(dataReader["id"] + ""),
                        imageUrl = dataReader["imageUrl"] + "",
                        name = dataReader["name"] + ""
                    });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return result;
            }
            else
            {
                return result;
            }
        }

        public List<ShoppingList> GetConsumerHistory(int consumerId)
        {
            List<ShoppingList> result = new List<ShoppingList>();
            string query = "SELECT `shoppingListId`, `consumerId`, `date`, `unitPrice`, `quantity`,`branchProductId`" +
                "FROM `shoppinglist` natural join `orderedProduct` natural join `branchProduct` " +
                $"where `consumerId` = {consumerId}";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    ShoppingList shoppingList = new ShoppingList();
                    shoppingList.id = (int)dataReader["ShoppingListId"];
                    shoppingList.consumerId = (int)dataReader["consumerId"];
                    shoppingList.date = (DateTime)dataReader["date"];
                    if (!result.Contains(shoppingList))
                    {
                        shoppingList.products = GetOrderedProductsOfList(shoppingList.id);
                        result.Add(shoppingList);
                    }
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return result;
            }
            else
            {
                return result;
            }
        }
        private List<OrderedProduct> GetOrderedProductsOfList(int shoppingListId)
        {
            List<OrderedProduct> result = new List<OrderedProduct>();
            string query = $"select * from  `orderedProduct` where shoppingListId = {shoppingListId}";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    OrderedProduct orderedProduct = new OrderedProduct();
                    orderedProduct.shoppingListId = (int)dataReader["ShoppingListId"];
                    orderedProduct.branchProductId = (int)dataReader["branchProductId"];
                    orderedProduct.unitPrice = (double)dataReader["unitPrice"];
                    orderedProduct.quantity = (int)dataReader["quantity"];
                    result.Add(orderedProduct);
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return result;
            }
            else
            {
                return result;
            }
        }
        #endregion
        #region INSERT
        public void InsertShoppingList(ShoppingList shoppingList)
        {
            string query = $"INSERT INTO shoppingList (consumerId, date) VALUES({shoppingList.consumerId}, {shoppingList.date})";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
            InsertOrderedProducts(shoppingList.products);

        }
        public void InsertOrderedProducts(List<OrderedProduct> orderedProducts)
        {
            if (this.OpenConnection() == true)
            {
                foreach (OrderedProduct item in orderedProducts)
                {
                    string query = $"INSERT INTO shoppingList (ShoppingListId, branchProductId, unitPrice,quantity) " +
                        $"VALUES({item.shoppingListId}, {item.branchProductId},{item.unitPrice}, {item.quantity})";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                this.CloseConnection();
            }

        }
        #endregion
    }
}
