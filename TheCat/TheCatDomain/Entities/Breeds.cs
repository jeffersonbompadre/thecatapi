using System.Collections.Generic;

namespace TheCatDomain.Entities
{
    public class Breeds
    {
        public Breeds()
        {
        }

        public Breeds(string breedsId, string name)
        {
            BreedsId = breedsId;
            Name = name;
        }

        public string BreedsId { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Temperament { get; set; }
        public string Description { get; set; }

        public ICollection<ImageUrl> Images { get; set; } = new List<ImageUrl>();

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(BreedsId) && !string.IsNullOrEmpty(Name);
        }
    }
}
