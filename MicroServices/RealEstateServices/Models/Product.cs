namespace RealEstateServices.Models
{

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; } = 0;
        public string Address { get; set;}

        public Category Category { get; set; }

    }
}
