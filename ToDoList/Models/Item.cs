using System.Collections.Generic;
using MySqlConnector;

namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int Id { get; set; }

    public Item(string description)
    {
      Description = description;
    }

    public Item(string description, int id)
    {
      Description = description;
      Id = id;
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = (this.Id == newItem.Id);
        bool descriptionEquality = (this.Description == newItem.Description);
        return (idEquality && descriptionEquality);
      }
    }
    
    public override int GetHashCode()
    {
      return Id.GetHashCode();
    }


    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item> { };

      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString); // MySqlConnection class connects app with database -- using our specified user/pass
      conn.Open(); //open connection

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand; // MySqlCommand is a class, which allows communicating - app to database and allowing us to have access to create commands
      cmd.CommandText = "SELECT * FROM items;"; // this string is the terminal command

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader; // To be able to interpret the information received by database, we must use a Reader
      while (rdr.Read()) // goes through each row in database
      { // Example row (record) in database:
        // { 1, "Mow the lawn" };  
        // array[0] is "id"
        // array[1] is "description"
        int itemId = rdr.GetInt32(0); // grabs the Id (via index 0)
        string itemDescription = rdr.GetString(1); // grabs the description (via index 1)
        Item newItem = new Item(itemDescription, itemId); // uses above info to create an instance of Item class (an object).
        allItems.Add(newItem); // adds this new item to the list declared above
      }
      conn.Close(); // closes app to database connection
      if (conn != null) // on event it fails to close, and still exists
      {
        conn.Dispose(); // runs if conn fails to close
      }
      return allItems; // returns populated item list
    }

    public static void ClearAll()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString); // MySqlConnection class connects app with database -- using our specified user/pass
      conn.Open(); //open connection

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand; // MySqlCommand is a class, which allows communicating - app to database and allowing us to have access to create commands
      cmd.CommandText = "DELETE FROM items;"; // this string is the terminal command
      cmd.ExecuteNonQuery(); // If not getting info from database

      conn.Close(); // closes app to database connection
      if (conn != null) // on event it fails to close, and still exists
      {
        conn.Dispose(); // runs if conn fails to close
      }
    }

    // public static Item Find(int searchId)
    // {
    //   // return _instances[searchId-1];
    //   Item placeholderItem = new Item("placeholder item");
    //   return placeholderItem;      
    // }

    public static Item Find(int id)
    {
      // We open a connection.
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      conn.Open();

      // We create MySqlCommand object and add a query to its CommandText property. 
      // We always need to do this to make a SQL query.
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM items WHERE id = @ThisId;";

      // We have to use parameter placeholders @ThisId and a `MySqlParameter` object to 
      // prevent SQL injection attacks. 
      // This is only necessary when we are passing parameters into a query. 
      // We also did this with our Save() method.
      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ThisId";
      param.Value = id;
      cmd.Parameters.Add(param);

      // We use the ExecuteReader() method because our query will be returning results and 
      // we need this method to read these results. 
      // This is in contrast to the ExecuteNonQuery() method, which 
      // we use for SQL commands that don't return results like our Save() method.
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemDescription = "";
      while (rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
      }
      Item foundItem = new Item(itemDescription, itemId);

      // We close the connection.
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundItem;
    }
    
    public void Save()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

      // Begin new code
      cmd.CommandText = "INSERT INTO items (description) VALUES (@ItemDescription);";
      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ItemDescription";
      param.Value = this.Description;
      cmd.Parameters.Add(param);    
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      // End new code

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
