using System.Collections.Generic;

namespace TheCatDomain.Entities
{
    public class Category
    {
        public Category()
        {
        }

        public Category(int categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<ImageUrl> Images { get; set; } = new List<ImageUrl>();

        public bool IsValid()
        {
            return CategoryId > 0 && !string.IsNullOrEmpty(Name);
        }
    }
}
