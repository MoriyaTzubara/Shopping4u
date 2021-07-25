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
                    address = dataReader["address"] + "",
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
            if (this.OpenConnection() == true)
            {
                foreach (OrderedProduct item in orderedProducts)
                {
                    string query = $"INSERT INTO shoppingList (ShoppingListId, branchProductId, unitPrice,quantity) " +
                        $"VALUES({shoppingListId}, {item.branchProductId},{item.unitPrice}, {item.quantity})";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                this.CloseConnection();
            }

        }
        public void InsertBaseProduct(Product product)
        {
            if(this.OpenConnection() == true)
            {
                if(GetProduct(product.id) == new Product())
                {
                    string query = $"INSERT INTO baseproduct (productId,name,itemImageUrl) " +
                        $"VALUES ({product.id},{product.name},{product.imageUrl})";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                this.CloseConnection();
            }
        }
        public Branch InsertBranch(Branch branch)
        {
            string query = $"SELECT * FROM branch WHERE {branch.name} = name AND {branch.address} = address";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (!dataReader.Read())
                {
                    query = $"INSERT INTO branch (name,address) " +
                        $"VALUES ({branch.name},{branch.address})";
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
        public void InsertBranchProduct(Product product, Branch branch,int price)
        {
            InsertBaseProduct(product);
            branch = InsertBranch(branch);
            if (branch.id != 0)
            {
                string query = $"INSERT INTO branchProduct (branchId,productId,price) " +
                  $"VALUES({branch.id},{product.id},{price})";
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
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
        #endregion
    }
}
