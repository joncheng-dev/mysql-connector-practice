using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Item
  {
    public int ItemId { get; set; } // uppercase and syntax name must match what these are called in the database. Primary key (id number) follows [ClassName]Id syntax.
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<ItemTag> JoinEntities { get; }
  }
}
