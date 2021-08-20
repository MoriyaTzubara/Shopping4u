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
using Org.BouncyCastle.Asn1.IsisMtt.X509;

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
            using (StreamReader r = new StreamReader(Environment.CurrentDirectory + "/databaseConnection.json"))
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
            string query = "SELECT * FROM baseproduct order by categoryName, name";
            if (OpenConnection() == true)
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
                        imageUrl = dataReader["itemImageUrl"] + "",
                        name = dataReader["name"] + "",
                        category = dataReader["categoryName"] + ""
                    });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return result;
            }
            else
            {
                return result;
            }
        }
        public List<Branch> GetBranches()
        {
            List<Branch> result = new List<Branch>();
            string query = "SELECT * FROM branch";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add(new Branch
                    {
                        id = int.Parse(dataReader["branchId"] + ""),
                        name = dataReader["name"] + ""
                    });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

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
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = new BranchProduct
                    {
                        productId = int.Parse(dataReader["productId"] + ""),
                        branchId = (int)dataReader["branchId"],
                        price = (double)dataReader["price"],
                        branchProductId = (int)dataReader["branchProductId"]
                    };
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Product GetProduct(int productId)
        {
            Product result = null;
            string query = $"SELECT * FROM baseproduct where {productId} = productId";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = new Product
                    {
                        id = int.Parse(dataReader["productId"] + ""),
                        imageUrl = dataReader["itemImageUrl"] + "",
                        name = dataReader["name"] + "",
                        category = dataReader["categoryName"] + ""
                    };
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Consumer GetConsumer(int consumerId)
        {
            Consumer result = null;
            string query = $"SELECT * FROM consumer where {consumerId} = consumerId";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = new Consumer
                    {
                        id = int.Parse(dataReader["consumerId"] + ""),
                        profileImageUrl = dataReader["profileImageUrl"] + "",
                        firstName = dataReader["firstName"] + "",
                        lastName = dataReader["lastName"] + "",
                        email = dataReader["email"] + "",
                        phoneNumber = dataReader["phoneNumber"] + ""
                    };
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Consumer GetConsumer(string email)
        {
            Consumer result = null;
            string query = $"SELECT * FROM consumer where '{email}' = email";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = new Consumer
                    {
                        id = int.Parse(dataReader["consumerId"] + ""),
                        profileImageUrl = dataReader["profileImageUrl"] + "",
                        firstName = dataReader["firstName"] + "",
                        lastName = dataReader["lastName"] + "",
                        email = dataReader["email"] + "",
                        phoneNumber = dataReader["phoneNumber"] + "",
                        password = dataReader["password"] + ""
                    };
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Branch GetBranch(int branchId)
        {
            Branch result = new Branch();
            string query = $"SELECT * FROM branch where {branchId} = branchId";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = new Branch
                    {
                        id = int.Parse(dataReader["branchId"] + ""),
                        name = dataReader["name"] + ""
                    };
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public List<ShoppingList> GetConsumerHistory(int consumerId)
        {
            List<ShoppingList> result = new List<ShoppingList>();
            string query = "SELECT * " +
                "FROM `shoppinglist` " +
                $"where `consumerId` = {consumerId}";
            if (OpenConnection() == true)
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
                    shoppingList.approved = dataReader.GetBoolean("approved");
                    result.Add(shoppingList);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
                result.ForEach(x => x.products = GetOrderedProductsOfList(x.id));
                //return list to be displayed
                return result;
            }
            else
            {
                return result;
            }
        }
        public string[] GetShoppingListsOfConsumer(int consumerId)
        {
            List<string> result = new List<string>();
            List<int> shoppingListsId = new List<int>();
            string query = "SELECT shoppingListId" +
                "FROM shoppinglist " +
                $"WHERE consumerId = {consumerId} and approved = {true}";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    shoppingListsId.Add((int)dataReader["ShoppingListId"]);
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            result = shoppingListsId.Select(id => GetProductsIdOfList(id)).ToList();
            return result.ToArray();
        }
        public string[] GetShoppingLists()
        {
            List<string> result = new List<string>();
            List<int> shoppingListsId = new List<int>();
            string query = "SELECT shoppingListId " +
                "FROM shoppinglist " +
                $"WHERE approved = {true}";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    shoppingListsId.Add((int)dataReader["ShoppingListId"]);
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            result = shoppingListsId.Select(id => GetProductsIdOfList(id)).ToList();
            return result.ToArray();
        }
        public IEnumerable<string> GetProductsIdInList()
        {
            List<string> result = new List<string>();
            string query = "SELECT productId " +
                "FROM baseProduct ";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add(((int)dataReader["productId"]).ToString());
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public string GetProductsIdOfList(int shoppingListId)
        {
            string result = "";
            string query = "SELECT productId " +
               $"FROM (select * from orderedProduct where shoppingListId = {shoppingListId}) as products " +
                "natural join branchProduct";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result += dataReader["productId"].ToString() + ",";
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

            }
            //return list to be displayed
            return result;
        }
        private List<OrderedProduct> GetOrderedProductsOfList(int shoppingListId)
        {
            List<OrderedProduct> result = new List<OrderedProduct>();
            string query = $"select * from  `orderedProduct` where shoppingListId = {shoppingListId}";
            if (OpenConnection() == true)
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
                    orderedProduct.id = (int)dataReader["orderedProductid"];
                    result.Add(orderedProduct);
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

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
            if (OpenConnection() == true)
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
                CloseConnection();
            }
            return result;
        }
        public string GetBranchName(int branchId)
        {
            string result = "";
            string query = $"SELECT name FROM branch where {branchId} = branchId";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = dataReader["name"] + "";
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public string GetProductName(int productId)
        {
            string result = "";
            string query = $"SELECT name FROM baseProduct where {productId} = productId";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = dataReader["name"] + "";
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public double GetTotalOfShoppingList(int shoppingListId)
        {
            double result = 0;
            string query = $"SELECT sum(quantity * unitPrice) AS total" +
                $"FROM orderedProduct where {shoppingListId} = shoppingListId";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = (double)dataReader["total"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public ShoppingList GetShoppingList(int shoppingListId)
        {
            ShoppingList shoppingList = new ShoppingList();
            string query = "SELECT * " +
                "FROM shoppingList " +
                $"WHERE shoppingListId = {shoppingListId}";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    shoppingList.id = (int)dataReader["ShoppingListId"];
                    shoppingList.consumerId = (int)dataReader["consumerId"];
                    shoppingList.date = (DateTime)dataReader["date"];
                    shoppingList.approved = dataReader.GetBoolean("approved");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            shoppingList.products = GetOrderedProductsOfList(shoppingList.id);
            return shoppingList;
        }
        public ShoppingList GetUnapprovedShoppingListOfConsumer(int consumerId)
        {
            ShoppingList shoppingList = new ShoppingList();
            string query = "SELECT * " +
                "FROM shoppingList " +
                $"WHERE consumerId = {consumerId} and approved = {false}";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    shoppingList.id = (int)dataReader["ShoppingListId"];
                    shoppingList.consumerId = (int)dataReader["consumerId"];
                    shoppingList.date = (DateTime)dataReader["date"];
                    shoppingList.approved = dataReader.GetBoolean("approved");
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            shoppingList.products = GetOrderedProductsOfList(shoppingList.id);
            return shoppingList;
        }
        public IEnumerable<string> GetProductsNamesInList()
        {
            List<string> result = new List<string>();
            string query = "SELECT name" +
                "FROM baseProduct ";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add((string)dataReader["name"]);
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public List<BranchProduct> GetBranchProductsOfSpecificProduct(int productId)
        {
            List<BranchProduct> result = new List<BranchProduct>();
            string query = $"SELECT * FROM branchProduct where productId = {productId}";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add(new BranchProduct
                    {
                        branchId = (int)dataReader["branchId"],
                        branchProductId = (int)dataReader["branchProductId"],
                        price = dataReader.GetDouble("price"),
                        productId = (int)dataReader["productId"]
                    });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public List<string> GetProductsNameOfSpecificBranch(int branchId)
        {
            List<string> result = new List<string>();
            string query = $"SELECT name FROM branchProduct natural join baseProduct where branchId = {branchId}";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result.Add((string)dataReader["name"]);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public List<string> GetCategoriesNames()
        {
            List<string> result = new List<string>();
            string query = "select * from category";
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add((string)dataReader["categoryName"]);
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public string GetProductNameByBranchProductId(int branchProductId)
        {
            string result = "";
            string query = "SELECT name " +
                "FROM branchProduct natural join baseProduct " +
                $"WHERE branchProductId = {branchProductId}";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = (string)dataReader["name"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }

        #endregion
        #region INSERT
        public ShoppingList CreateUnapprovedShoppingList(int consumerId)
        {
            ShoppingList result = GetUnapprovedShoppingListOfConsumer(consumerId);
            if (result.id != 0)
                return result;
            result = new ShoppingList { date = DateTime.Now, consumerId = consumerId, approved = false, products = new List<OrderedProduct>() };
            string query = $"INSERT INTO shoppingList (consumerId, date,approved) VALUES({result.consumerId}, '{result.date.ToString("yyyy-MM-dd")}',{result.approved})";
            //open connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();
                // get auto increment primary key
                query = "SELECT LAST_INSERT_ID() from shoppingList";
                cmd = new MySqlCommand(query, connection);
                var id = cmd.ExecuteScalar();
                result.id = int.Parse(id.ToString());
                //close connection
                CloseConnection();
            }
            return result;
        }
        //NOT NEEDED
        public void InsertApprovedShoppingList(ShoppingList shoppingList)
        {
            
            string query = $"INSERT INTO shoppingList (consumerId, date,approved) VALUES({shoppingList.consumerId}, {shoppingList.date},{true})";
            int shoppingListId = -1;
            //open connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();
                // get auto increment primary key
                query = "SELECT LAST_INSERT_ID() from shoppingList";
                cmd = new MySqlCommand(query, connection);
                var id = cmd.ExecuteScalar();
                shoppingListId = int.Parse(id.ToString());
                //close connection
                CloseConnection();
            }
            InsertOrderedProducts(shoppingList.products, shoppingListId);
        }
        public void InsertOrderedProducts(List<OrderedProduct> orderedProducts, int shoppingListId)
        {
            foreach (OrderedProduct item in orderedProducts)
            {
                InsertOrderedProduct(item);
            }

        }
        public OrderedProduct InsertOrderedProduct(OrderedProduct orderedProduct)
        {
            OrderedProduct orderedProductExists = GetShoppingList(orderedProduct.shoppingListId).products.FirstOrDefault(o => o.branchProductId == orderedProduct.branchProductId);
            if (orderedProductExists != null)
            {
                orderedProductExists.quantity += orderedProduct.quantity;
                UpdateOrderedProduct(orderedProductExists);
                return orderedProductExists;
            }
            if (OpenConnection() == true)
            {
                string query = $"INSERT INTO orderedProduct (ShoppingListId, branchProductId, unitPrice,quantity) " +
                      $"VALUES({orderedProduct.shoppingListId}, {orderedProduct.branchProductId},{orderedProduct.unitPrice}, {orderedProduct.quantity})";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                // get auto increment primary key
                query = "SELECT LAST_INSERT_ID() from orderedProduct";
                cmd = new MySqlCommand(query, connection);
                var id = cmd.ExecuteScalar();
                orderedProduct.id = int.Parse(id.ToString());
                CloseConnection();
            }
            return orderedProduct;
        }
        public bool DoesCategoryExists(string category)
        {
            if (GetCategoriesNames().Contains(category))
                return true;
            return false;
        }
        public void InsertCategory(string category)
        {
            if (DoesCategoryExists(category) == false)
            {
                if (OpenConnection() == true)
                {

                    string query = $"INSERT INTO category (categoryName) " +
                        $"VALUES ({category})";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                CloseConnection();
            }
        }
        public void InsertBaseProduct(Product product)
        {
            InsertCategory(product.category);
            if (GetProduct(product.id) == null)
            {
                if (OpenConnection() == true)
                {

                    string query = $"INSERT INTO baseproduct (productId,name,itemImageUrl,categoryName) " +
                        $"VALUES ({product.id},'{product.name}','{product.imageUrl}','{product.category}')";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                CloseConnection();
            }
        }
        public Consumer InsertConsumer(Consumer consumer)
        {
            Consumer result = GetConsumer(consumer.id);
            if (result == null)
            {
                if (OpenConnection() == true)
                {
                    string query = $"INSERT INTO consumer (profileImageUrl,phoneNumber, email, firstName,lastName) " +
                        $"VALUES ({consumer.profileImageUrl},{consumer.phoneNumber},{consumer.email},{consumer.firstName},{consumer.lastName})";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    // get auto increment primary key
                    query = "SELECT LAST_INSERT_ID() from consumer";
                    cmd = new MySqlCommand(query, connection);
                    var id = cmd.ExecuteScalar();
                    result.id = int.Parse(id.ToString());
                    CloseConnection();
                }
            }
            return result;
        }
        public Branch InsertBranch(Branch branch)
        {
            string query = $"SELECT * FROM branch WHERE name = '{branch.name}'";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (!dataReader.Read())
                {
                    //close Data Reader
                    dataReader.Close();
                    query = $"INSERT INTO branch (name) " +
                        $"VALUES ('{branch.name}')";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    // get auto increment primary key
                    query = "SELECT LAST_INSERT_ID() from branch";
                    cmd = new MySqlCommand(query, connection);
                    var id = cmd.ExecuteScalar();
                    branch.id = int.Parse(id.ToString());
                }
                else
                {
                    branch.id = (int)dataReader["branchId"];
                    //close Data Reader
                    dataReader.Close();
                }

                //close Connection
                CloseConnection();
                return branch;
            }
            return branch;
        }
        private bool DoesBranchProductExist(int branchId, int productId)
        {
            string query = $"select branchId,productId from branchProduct";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if ((int)dataReader["branchId"] == branchId && (int)dataReader["productId"] == productId)
                    {
                        dataReader.Close();
                        CloseConnection();
                        return true;
                    }
                }
                dataReader.Close();
                CloseConnection();                
            }
            return false;
        }
        public BranchProduct InsertBranchProduct(Product product, Branch branch, double price)
        {
            InsertBaseProduct(product);
            branch = InsertBranch(branch);
            BranchProduct branchProduct = new BranchProduct();
            if (branch.id != 0)
            {            
                if (OpenConnection() == true)
                {
                    string query = $"SELECT * FROM branchProduct WHERE branchId = {branch.id} and productId = {product.id}";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.Read())
                    {
                        dataReader.Close();
                        query = $"INSERT INTO branchProduct (branchId,productId,price) " +
                                    $"VALUES({branch.id},{product.id},{price})";
                        cmd = new MySqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        // get auto increment primary key
                        query = "SELECT LAST_INSERT_ID() from branchproduct";
                        cmd = new MySqlCommand(query, connection);
                        var id = cmd.ExecuteScalar();
                        branchProduct = new BranchProduct
                        {
                            branchProductId = int.Parse(id.ToString()),
                            branchId = branch.id,
                            productId = product.id,
                            price = price
                        };
                        dataReader.Close();
                        CloseConnection();
                    }
                    else
                    {
                        branchProduct = new BranchProduct
                        {
                            productId = int.Parse(dataReader["productId"] + ""),
                            branchId = (int)dataReader["branchId"],
                            price = price,
                            branchProductId = (int)dataReader["branchProductId"]
                        };
                        dataReader.Close();
                        CloseConnection();
                        UpdateBranchProduct(branchProduct);
                    }
                    
                }
            }
            return branchProduct;
        }
        #endregion
        #region UPDATE
        public void UpdateProductPicture(string url, int productId)
        {
            if (GetProduct(productId) != null) 
            {
                if (OpenConnection() == true)
                {
                    try
                    {
                        string query = $"UPDATE baseproduct set itemImageUrl = '{url}' " +
                            $"where productId = {productId}";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e) { }
                }
                CloseConnection();
            }
        }
        public void UpdateBranchProduct(BranchProduct branchProduct)
        {
            if (OpenConnection() == true)
            {
                string query = $"UPDATE branchProduct set price = {branchProduct.price} WHERE branchProductId = {branchProduct.branchProductId} ";                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
        public void UpdateOrderedProduct(OrderedProduct orderedProduct)
        {
            if (OpenConnection() == true)
            {
                string query = $"UPDATE orderedProduct set quantity = {orderedProduct.quantity}, branchProductId = {orderedProduct.branchProductId} " +
                    $"where orderedProductId = {orderedProduct.id}";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
        public void SaveShoppingList(int shoppingListId)
        {
            if (OpenConnection() == true)
            {
                string query = $"UPDATE shoppingList set approved = {true},date = '{DateTime.Now.ToString("yyyy-MM-dd")}' " +
                    $"where shoppingListId = {shoppingListId}";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
        #endregion
        #region DELETE
        public void DeleteOrderedProduct(int orderedProductId)
        {
            if (OpenConnection() == true)
            {
                string query = $"DELETE FROM orderedProduct " +
                    $"where OrderedProductId = {orderedProductId}";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
        public void DeleteUnapprovedShoppingList(int consumerId)
        {
            if (OpenConnection() == true)
            {
                string query = $"DELETE FROM shoppingList " +
                    $"where shoppingListId = {1} and consumerId = {consumerId}";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
        #endregion
        #region FILTERS
        public Product GetProductByName(string name)
        {
            Product result = new Product();
            string query = $"SELECT * FROM baseproduct where name LIKE '%{name}%'";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = new Product
                    {
                        id = int.Parse(dataReader["productId"] + ""),
                        imageUrl = dataReader["itemImageUrl"] + "",
                        name = dataReader["name"] + "",
                        category = dataReader["categoryName"] + ""
                    };
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
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
            if (OpenConnection() == true)
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
                        unitPrice = (double)dataReader["unitPrice"],
                        id = (int)dataReader["orderedProductid"]
                    });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        } 
        public IDictionary<string, List<string>> GetUsualShoppingsForEachDay(int consumerId,double minPrecent = 0.3)
        {
            IDictionary<string,List<string>> result = new Dictionary<string, List<string>>();
            string query = $"select dayOfWeek, name, CAST(numOfTimesBuyingProduct AS decimal)/CAST(numOfShoppings AS decimal) as precent " +
                $"from (select dayname(date) as dayOfWeek, sum(distinct(shoppingListId)) as numOfShoppings " +
                $"from orderedProduct natural join shoppingList " +
                $"where consumerId = {consumerId} " +
                $"group by dayname(date)) as denominator " +
                $"natural join " +
                $"(select dayname(date) as dayOfWeek, name, sum(distinct(shoppingListId)) as numOfTimesBuyingProduct " +
                $"from orderedProduct natural join shoppingList natural join baseproduct " +
                $"where consumerId = {consumerId} " +
                $"group by dayname(date)) as Numerator " +
                $"order by dayOfWeek, precent";
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey((string)dataReader["dayOfWeek"]))
                        result[(string)dataReader["dayOfWeek"]] = new List<string>();
                    if (dataReader.GetDouble("precent") >= minPrecent)
                        result[(string)dataReader["dayOfWeek"]].Add((string)dataReader["name"]);
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public IDictionary<DateTime, int> GetProductBetweenTwoDates(DateTime start, DateTime end, int consumerId, int productId)
        {
            IDictionary<DateTime, int> result = new Dictionary<DateTime, int>();
            string query = $"SELECT date(date) AS date,SUM(quantity) AS quantity " +
                           $"FROM OrderedProduct NATURAL JOIN ShoppingList " +
                           $"where productId = {productId} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                           $"GROUP BY date(date)";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result[(DateTime)dataReader["date"]] = (int)dataReader["quantity"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public IDictionary<DateTime, double> GetShoppingsInBranchBetweenTwoDates(DateTime start, DateTime end, int consumerId, int BranchId)
        {
            IDictionary<DateTime, double> result = new Dictionary<DateTime, double>();
            string query = $"SELECT date(date) AS date,SUM(quantity * unitPrice) AS expenses " +
                $"FROM branchProduct NATURAL JOIN OrderedProduct NATURAL JOIN ShoppingList " +
                $"where branchId = {BranchId} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                $"GROUP BY date(date)";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey((DateTime)dataReader["date"]))
                        result[(DateTime)dataReader["date"]] = 0;
                    result[(DateTime)dataReader["date"]] += (double)dataReader["expenses"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public IDictionary<DateTime, double> GetShoppingsInCategoryBetweenTwoDates(DateTime start, DateTime end, int consumerId, int categoryName)
        {
            IDictionary<DateTime, double> result = new Dictionary<DateTime, double>();
            string query = $"SELECT date(date) AS date,SUM(quantity * unitPrice) AS expenses " +
                $"FROM baseProduct NATURAL JOIN branchProduct NATURAL JOIN OrderedProduct NATURAL JOIN ShoppingList " +
                $"where categoryName = {categoryName} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                $"GROUP BY date(date)";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey((DateTime)dataReader["date"]))
                        result[(DateTime)dataReader["date"]] = 0;
                    result[(DateTime)dataReader["date"]] += (double)dataReader["expenses"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        #endregion
        #region GRAPH
        public Dictionary<int, int> OrderedProductsBetweenTwoDates(DateTime start, DateTime end, int consumerId)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            string query = $"SELECT branchProductId,SUM(quantity) AS quantity " +
                $"FROM OrderedProduct NATURAL JOIN ShoppingList " +
                $"where approved = {true} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                $"GROUP BY branchProductId";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey((int)dataReader["branchProductId"]))
                        result[(int)dataReader["branchProductId"]] = 0;
                    result[(int)dataReader["branchProductId"]] += (int)dataReader["quantity"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Dictionary<string, double> CategoryBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId, string categoryName)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            string query = $"SELECT date, SUM(quantity * unitPrice) AS expenses " +
                $"FROM (SELECT productId FROM baseProduct WHERE categoryName = '{categoryName}') AS ProductS NATURAL JOIN branchProduct NATURAL JOIN OrderedProduct NATURAL JOIN ShoppingList " +
                $"where approved = {true} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                $"GROUP BY date";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey(((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")))
                        result[((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")] = 0;
                    result[((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")] += (double)dataReader["expenses"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Dictionary<string, double> CategoryBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId, string categoryName)
        {

            Dictionary<string, double> result = new Dictionary<string, double>();
            while (start.AddDays(7) <= end)
            {
                result[start.ToString("yyyy-MM-dd")] = CategoryBetweenTwoDatesByDay(start, start.AddDays(7), consumerId, categoryName).Sum(s => s.Value);
                start = start.AddDays(7);
            }
            if (start != end)
                result[start.ToString("yyyy-MM-dd")] = CategoryBetweenTwoDatesByDay(start, start.AddDays((int)((end - start).TotalDays)), consumerId, categoryName).Sum(s => s.Value);
            return result;
        }
        public Dictionary<string, double> CategoryBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId, string categoryName)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            string query = $"SELECT MONTHNAME(date) AS month, SUM(quantity * unitPrice) AS expenses " +
                $"FROM (SELECT productId FROM baseProduct WHERE categoryName = '{categoryName}') AS ProductS NATURAL JOIN branchProduct NATURAL JOIN OrderedProduct NATURAL JOIN ShoppingList " +
                $"where approved = {true} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                $"GROUP BY MONTHNAME(date)";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey((string)dataReader["month"]))
                        result[(string)dataReader["month"]] = 0;
                    result[(string)dataReader["month"]] += (double)dataReader["expenses"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Dictionary<string, double> BranchBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId, int branchId)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            string query = $"SELECT date, SUM(quantity * unitPrice) AS expenses " +
                $"FROM branchProduct NATURAL JOIN OrderedProduct NATURAL JOIN ShoppingList " +
                $"where approved = {true} AND branchId = {branchId} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                $"GROUP BY date";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey(((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")))
                        result[((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")] = 0;
                    result[((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")] += (double)dataReader["expenses"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Dictionary<string, double> BranchBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId, int branchId)
        {

            Dictionary<string, double> result = new Dictionary<string, double>();
            while (start.AddDays(7) <= end)
            {
                result[start.ToString("yyyy-MM-dd")] = BranchBetweenTwoDatesByDay(start, start.AddDays(7), consumerId, branchId).Sum(s => s.Value);
                start = start.AddDays(7);
            }
            if (start != end)
                result[start.ToString("yyyy-MM-dd")] = BranchBetweenTwoDatesByDay(start, start.AddDays((int)((end - start).TotalDays)), consumerId, branchId).Sum(s => s.Value);
            return result;
        }
        public Dictionary<string, double> BranchBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId, int branchId)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            string query = $"SELECT MONTHNAME(date) AS month, SUM(quantity * unitPrice) AS expenses " +
                $"FROM branchProduct NATURAL JOIN OrderedProduct NATURAL JOIN ShoppingList " +
                $"where approved = {true} AND branchId = {branchId} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                $"GROUP BY MONTHNAME(date)";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey((string)dataReader["month"]))
                        result[(string)dataReader["month"]] = 0;
                    result[(string)dataReader["month"]] += (double)dataReader["expenses"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Dictionary<string, double> ProductBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId, int productId)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            string query = $"SELECT date,SUM(quantity) AS counter " +
                $"FROM branchProduct NATURAL JOIN OrderedProduct NATURAL JOIN ShoppingList " +
                $"where approved = {true} AND productId = {productId} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                $"GROUP BY date";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey(((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")))
                        result[((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")] = 0;
                    result[((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")] += dataReader.GetDouble("counter");
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Dictionary<string, double> ProductBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId, int productId)
        {

            Dictionary<string, double> result = new Dictionary<string, double>();
            while (start.AddDays(7) <= end)
            {
                result[start.ToString("yyyy-MM-dd")] = ProductBetweenTwoDatesByDay(start, start.AddDays(7), consumerId, productId).Sum(s => s.Value);
                start = start.AddDays(7);
            }
            if (start != end)
                result[start.ToString("yyyy-MM-dd")] = ProductBetweenTwoDatesByDay(start, start.AddDays((int)((end - start).TotalDays)), consumerId, productId).Sum(s => s.Value);
            return result;
        }
        public Dictionary<string, double> ProductBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId, int productId)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            string query = $"SELECT MONTHNAME(date) AS month,SUM(quantity) AS counter " +
                $"FROM branchProduct NATURAL JOIN OrderedProduct NATURAL JOIN ShoppingList " +
                $"where approved = {true} AND productId = {productId} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                $"GROUP BY MONTHNAME(date)";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey((string)dataReader["month"]))
                        result[(string)dataReader["month"]] = 0;
                    result[(string)dataReader["month"]] += dataReader.GetDouble("counter");
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Dictionary<string, double> ShoppingsBetweenTwoDatesByDay(DateTime start, DateTime end, int consumerId)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            string query = $"SELECT date,SUM(quantity * unitPrice) AS expenses " +
                $"FROM OrderedProduct NATURAL JOIN ShoppingList " +
                $"where approved = {true} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                $"GROUP BY date";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey(((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")))
                        result[((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")] = 0;
                    result[((DateTime)dataReader["date"]).ToString("yyyy-MM-dd")] += (double)dataReader["expenses"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        public Dictionary<string, double> ShoppingsBetweenTwoDatesByWeek(DateTime start, DateTime end, int consumerId)
        {

            Dictionary<string, double> result = new Dictionary<string, double>();
            while (start.AddDays(7) <= end)
            {
                result[start.ToString("yyyy-MM-dd")] = ShoppingsBetweenTwoDatesByDay(start, start.AddDays(7), consumerId).Sum(s => s.Value);
                start = start.AddDays(7);
            }
            if (start != end)
                result[start.ToString("yyyy-MM-dd")] = ShoppingsBetweenTwoDatesByDay(start, start.AddDays((int)((end - start).TotalDays)), consumerId).Sum(s => s.Value);
            return result;
        }
        public Dictionary<string, double> ShoppingsBetweenTwoDatesByMonth(DateTime start, DateTime end, int consumerId)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            string query = $"SELECT MONTHNAME(date) AS month, SUM(quantity * unitPrice) AS expenses " +
                $"FROM OrderedProduct NATURAL JOIN ShoppingList " +
                $"where approved = {true} AND {consumerId} = consumerId AND date BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}' " +
                $"GROUP BY MONTHNAME(date)";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!result.ContainsKey((string)dataReader["month"]))
                        result[(string)dataReader["month"]] = 0;
                    result[(string)dataReader["month"]] += (double)dataReader["expenses"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            foreach (string month in ShoppingList.allMonths)
            {
                if (!result.ContainsKey(month))
                    result[month] = 0;
            }
            return result;
        }
        #endregion
        #region APRIORI
        public IDictionary<int, int> GetSupportOfEachItem()
        {
            IDictionary<int, int> forEachProduct = new Dictionary<int, int>();
            if (OpenConnection() == true)
            {
                string query = "SELECT productId,count(*) AS supportX FROM shoppingList natural join branchproduct group by productId";
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    if ((double)dataReader["supportX"]  > 0)
                    {
                        forEachProduct[(int)dataReader["productId"]] = (int)dataReader["supportX"];
                    }
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            else
            {
                return forEachProduct;
            }
            return forEachProduct;
        }

        public int GetNumOfShoppingLists()
        {
            string query = "SELECT COUNT(*) FROM shoppingList";
            int result = -1;
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                var counter = cmd.ExecuteScalar();
                result = int.Parse(counter.ToString());
            }
            return result;
        }

        public IDictionary<List<Product>,double> ProductsThatGoTogether(double minConfidence = 0.01)
        {
            IDictionary<int, int>  forEachProduct = GetSupportOfEachItem();
            IDictionary<List<Product>, double> result = new Dictionary<List<Product>, double>();
            string query = $"SELECT productId,productId1,SUM(distinct(shoppingListId)) AS supportXY " +
                $"FROM orderedProduct natural join " +
                $"(SELECT productId1, branchProductId FROM branchProduct) AS branchProduct1 " +
                $"natural join branchProduct " +
                $"where productId > productId1" +
                $"GROUP BY productId, productId1";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                List<Product> products;
                double confidence;
                while (dataReader.Read())
                {
                    if (forEachProduct.ContainsKey((int)dataReader["productId"]))
                    {
                        confidence = (int)dataReader["supportXY"] / forEachProduct[(int)dataReader["productId"]];
                        if (confidence >= minConfidence)
                        {
                            products = new List<Product> { GetProduct((int)dataReader["productId"]), GetProduct((int)dataReader["productId1"]) };
                            result[products] = confidence;
                        }
                    }
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return result;
            }
            else
            {
                return result;
            }
        }
        #endregion
        #region CONVERT
        public int FindBranchProductIdForThisProduct(int id)
        {
            int result = -1;
            string query = $"SELECT branchProductId FROM BranchProduct NATURAL JOIN baseProduct where productId = {id} limit 1";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = (int)dataReader["branchProductId"];
                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();
            }
            return result;
        }
        #endregion
    }
}
