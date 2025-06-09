namespace HotelMotorShared.Models

{
    public class Order
    {
        public int Id { get; set; }
        public required string Summary { get; set; }
        public required OrderStatus Status { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime DueDate { get; set; }
        public required decimal TotalAmount { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        public ICollection<OrderDetails> OrderDetails { get; set; } = [];
    }
}
