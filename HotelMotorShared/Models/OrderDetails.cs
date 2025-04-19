namespace HotelMotorShared.Models

{
    public class OrderDetails
    {
        public int Id { get; set; }
        public required int OrderId { get; set; }
        public required int ServiceId { get; set; }
        public virtual Order Order { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
