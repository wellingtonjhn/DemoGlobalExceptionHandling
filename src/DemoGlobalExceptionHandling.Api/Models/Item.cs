using System.ComponentModel.DataAnnotations;

namespace DemoGlobalExceptionHandling.Api.Models
{
    public class Item
    {
        [Required]
        public string Name { get; }

        [Range(1, 10)]
        public int Value { get;  }

        public Item(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
