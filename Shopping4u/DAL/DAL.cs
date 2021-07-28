using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Net;
using ZXing;
using System.Drawing;
using MySqlX.XDevAPI.Relational;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Shopping4u.DAL
{
    public class DAL : IDAL
    {
        #region INITIALIZE
        private MySqlConnection connection;
        public DAL()
        {
            Initialize();
        }
        private void Initialize()
        {
            using (StreamReader r = new StreamReader("databaseConnection.json"))
            {
                string json = r.ReadToEnd();
                DatabaseConnection database = JsonConvert.DeserializeObject<DatabaseConnection>(json);
                connection = new MySqlConnection();
                connection.ConnectionString = $"server={database.server};user id={database.username};password={database.password};database={database.database}";
            }
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
        #endregion
        #region SELECT
        public List<Product> GetProducts()
        {
            List<Product> result = new List<Product>();
            string query = "SELECT * FROM baseproduct";
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
                        id = int.Parse(dataReader["productId"] + ""),
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
        public BranchProduct GetBranchProduct(int branchProductId)
        {
            BranchProduct result = new BranchProduct();
            string query = $"SELECT * FROM BranchProduct where {branchProductId} = branchProductId";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                result = new BranchProduct
                {
                    productId = int.Parse(dataReader["productId"] + ""),
                    branchId =(int) dataReader["imageUrl"],
                    price = (double)dataReader["price"],
                    branchProductId = (int) dataReader["branchProductId"]
                };
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }
        public Product GetProduct(int productId)
        {
            Product result = new Product();
            string query = $"SELECT * FROM baseproduct where {productId} = productId";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                result = new Product
                {
                    id = int.Parse(dataReader["productId"] + ""),
                    imageUrl = dataReader["imageUrl"] + "",
                    name = dataReader["name"] + ""
                };
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }
        public Consumer GetConsumer(int consumerId)
        {
            Consumer result = new Consumer();
            string query = $"SELECT * FROM consumer where {consumerId} = consumerId";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                result = new Consumer
                {
                    id = int.Parse(dataReader["consumerId"] + ""),
                    profileImageUrl = dataReader["profileImageUrl"] + "",
                    firstName = dataReader["firstName"] + "",
                    lastName = dataReader["lastName"] + "",
                    email = dataReader["email"] + "",
                    phoneNumber = dataReader["phoneNumber"] + ""
                };
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }
        public Branch GetBranch(int branchId)
        {
            Branch result = new Branch();
            string query = $"SELECT * FROM branch where {branchId} = branchId";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                result = new Branch
                {
                    id = int.Parse(dataReader["id"] + ""),
                    name = dataReader["name"] + ""
                };
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }
        public List<ShoppingList> GetConsumerHistory(int consumerId)
        {
            List<ShoppingList> result = new List<ShoppingList>();
            string query = "SELECT *`" +
                "FROM `shoppinglist` " +
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
                    shoppingList.approved = (bool)dataReader["approved"];
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
        public List<string> GetBranchesNameInList(int shoppingListId)
        {
            List<string> result = new List<string>();
            string query = $"SELECT branchName " +
                $"FROM (SELECT * FROM orderedProduct WHERE {shoppingListId} = shoppingListId) products " +
                $"natural join branchProduct " +
                $"natural join branch";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add(dataReader["branchName"] + "");
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }
        public string GetBranchName(int branchId)
        {
            string result = "";
            string query = $"SELECT name FROM branch where {branchId} = branchId";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                result = dataReader["name"] + "";
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }
        public string GetProductName(int productId)
        {
            string result = "";
            string query = $"SELECT name FROM baseProduct where {productId} = productId";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                result = dataReader["name"] + "";
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }
        public double GetTotalOfShoppingList(int shoppingListId)
        {
            double result = 0;
            string query = $"SELECT sum(quantity * unitPrice) AS total" +
                $"FROM orderedProduct where {shoppingListId} = shoppingListId";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                result = (double) dataReader["total"];
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }
        #endregion
        #region INSERT
        public void InsertShoppingList(ShoppingList shoppingList)
        {
            string query = $"INSERT INTO shoppingList (consumerId, date) VALUES({shoppingList.consumerId}, {shoppingList.date},{shoppingList.approved})";
            int shoppingListId = -1;
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();
                // get auto increment primary key
                query = "SELECT LAST_INSERT_ID() from shoppingList";
                cmd = new MySqlCommand(query, connection);
                shoppingListId = (int)cmd.ExecuteScalar();
                //close connection
                this.CloseConnection();
            }
            InsertOrderedProducts(shoppingList.products,shoppingListId);

        }
        public void InsertOrderedProducts(List<OrderedProduct> orderedProducts, int shoppingListId)
        {
            foreach (OrderedProduct item in orderedProducts)
            {
                InsertOrderedProduct(item, shoppingListId);
            }

        }
        public void InsertOrderedProduct(OrderedProduct orderedProduct, int shoppingListId)
        {
            if (this.OpenConnection() == true)
            {
                string query = $"INSERT INTO shoppingList (ShoppingListId, branchProductId, unitPrice,quantity) " +
                      $"VALUES({shoppingListId}, {orderedProduct.branchProductId},{orderedProduct.unitPrice}, {orderedProduct.quantity})";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void InsertBaseProduct(Product product)
        {
            if (GetProduct(product.id) == new Product())
            {
                if (this.OpenConnection() == true)
                {
                    string query = $"INSERT INTO baseproduct (productId,name,itemImageUrl) " +
                        $"VALUES ({product.id},{product.name},{product.imageUrl})";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                this.CloseConnection();
            }
        }
        public void InsertConsumer(Consumer consumer)
        {
            if (GetConsumer(consumer.id) == new Consumer())
            {
                if (this.OpenConnection() == true)
                {
                    string query = $"INSERT INTO consumer (consumerId,profileImageUrl,phoneNumber, email, firstName,lastName) " +
                        $"VALUES ({consumer.id},{consumer.profileImageUrl},{consumer.phoneNumber},{consumer.email},{consumer.firstName},{consumer.lastName})";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
            }
        }
        public Branch InsertBranch(Branch branch)
        {
            string query = $"SELECT * FROM branch WHERE {branch.name} = name";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (!dataReader.Read())
                {
                    query = $"INSERT INTO branch (name) " +
                        $"VALUES ({branch.name})";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    // get auto increment primary key
                    query = "SELECT LAST_INSERT_ID() from branch";
                    cmd = new MySqlCommand(query, connection);
                    branch.id = (int)cmd.ExecuteScalar();
                }
                else
                {
                    branch.id = (int)dataReader["branchId"];
                }
                this.CloseConnection();
                return branch;
            }
            return branch;
        }
        public BranchProduct InsertBranchProduct(Product product, Branch branch,double price)
        {
            InsertBaseProduct(product);
            branch = InsertBranch(branch);
            BranchProduct branchProduct = new BranchProduct();
            if (branch.id != 0)
            {
                string query = $"INSERT INTO branchProduct (branchId,productId,price) " +
                  $"VALUES({branch.id},{product.id},{price})";
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    // get auto increment primary key
                    query = "SELECT LAST_INSERT_ID() from branchproduct";
                    cmd = new MySqlCommand(query, connection);
                    branchProduct = new BranchProduct
                    {
                        branchProductId = (int)cmd.ExecuteScalar(),
                        branchId = branch.id,
                        productId = product.id,
                        price = price
                    };
                    this.CloseConnection();
                }
            }
            return branchProduct;
        }
        #endregion
        #region UPDATE
        public void UpdateProductPicture(string url, int productId)
        {
            if (this.OpenConnection() == true)
            {
                if (GetProduct(productId) == new Product())
                {
                    string query = $"UPDATE baseproduct set itemImageUrl = {url} " +
                        $"where productId = {productId}";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                this.CloseConnection();
            }
        }
        #endregion
        #region FILTERS
        public Dictionary<int, int> OrderedProductsBetweenTwoDates(DateTime start, DateTime end, int consumerId)
        {
            Dictionary<int,int> result = new Dictionary<int, int>();
            string query = $"SELECT branchProductId,SUM(quantity) AS quantity " +
                $"FROM OrderedProduct NATURAL JOIN ShoppingList " +
                $"where {consumerId} = consumerId AND date BETWEEN {start} AND {end} " +
                $"GROUP BY branchProductId";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result[(int)dataReader["branchProductId"]] = (int)dataReader["quantity"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            } 
            return result;
        }
        public Dictionary<DateTime, double> ShoppingsBetweenTwoDates(DateTime start, DateTime end, int consumerId)
        {
            Dictionary<DateTime, double> result = new Dictionary<DateTime, double>();
            string query = $"SELECT date,SUM(quantity * unitPrice) AS expenses " +
                $"FROM OrderedProduct NATURAL JOIN ShoppingList " +
                $"where {consumerId} = consumerId AND date BETWEEN {start} AND {end} " +
                $"GROUP BY date";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result[(DateTime)dataReader["date"]] = (double)dataReader["expenses"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }
        public List<Product> GetProductsByName(string name)
        {
            List<Product> result = new List<Product>();
            string query = $"SELECT * FROM baseproduct where name LIKE '%{name}%'";
            if(this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add(new Product
                    {
                        id = int.Parse(dataReader["productId"] + ""),
                        imageUrl = dataReader["imageUrl"] + "",
                        name = dataReader["name"] + ""
                    });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }
        public List<OrderedProduct> FilterByBranches(List<string> branchesNames, int shoppingListId)
        {
            List<OrderedProduct> result = new List<OrderedProduct>();
            string query = $"SELECT shoppingListId, branchProductId, unitPrice, quantity " +
                $"FROM (SELECT * FROM orderedProduct WHERE {shoppingListId} = shoppingListId) ordered " +
                $"NATURAL JOIN branchProduct " +
                $"WHERE branchId IN (SELECT branchId FROM branch WHERE name IN {branchesNames})";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add(new OrderedProduct
                    {
                        shoppingListId = int.Parse(dataReader["shoppingListId"] + ""),
                        branchProductId = (int)dataReader["branchProductId"],
                        quantity = (int)dataReader["quantity"],
                        unitPrice = (double)dataReader["unitPrice"]
                    });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
            }
            return result;
        }
        #endregion

    }
}
