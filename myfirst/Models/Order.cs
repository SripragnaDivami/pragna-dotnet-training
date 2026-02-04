namespace myfirst.Models
{
    class OrderModel
    {
       public int OrderId { get; set; }
       public string? CustomerName { get; set; }
       public bool IsVipCustomer { get; set; }
       public decimal OrderAmount { get; set; }
       public OrderStatus Status { get; set; }
       
       public decimal? Discount { get; set; }
    }
    
}