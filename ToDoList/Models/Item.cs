using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
  public class Item
  {
    public int ItemId { get; set; } // uppercase and syntax name must match what these are called in the database. Primary key (id number) follows [ClassName]Id syntax.
    [Required(ErrorMessage = "Item description required!")]
    public string Description { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "You must add your item to a category. Have you created a category yet?")]
    public int CategoryId { get; set; }

    public Category Category { get; set; }
    public List<ItemTag> JoinEntities { get; }
  }
}
