using System.Collections.Generic;
using MySqlConnector;

namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int ItemId { get; set; } // uppercase and syntax name must match what these are called in the database. Primary key (id number) follows [ClassName]Id syntax.

  }
}
