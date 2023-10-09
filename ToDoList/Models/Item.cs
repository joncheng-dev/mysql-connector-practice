using System.Collections.Generic;
using MySqlConnector;

namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int ItemId { get; set; } 
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public List<ItemTag> JoinEntities { get; }
  }
}
