using System.Collections.Generic;

namespace TheCatDomain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<ImageUrl> Images { get; set; } = new List<ImageUrl>();
    }
}
