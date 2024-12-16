namespace api.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public int ModelId { get; set; }
        public float Price { get; set; }
    }
}
