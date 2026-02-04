using myfirst.Models;

namespace myfirst.Data
{
     class OrderData
    {
       public List<OrderModel> Orders { get; } = new List<OrderModel>
  {
    new OrderModel
    {
        OrderId = 1,
        CustomerName = "Pragna",
        IsVipCustomer = true,
        OrderAmount = 60000,
        Status = OrderStatus.Completed,
        Discount = null
    },
    new OrderModel
    {
        OrderId = 2,
        CustomerName = "sravani",
        IsVipCustomer = false,
        OrderAmount = 45000,
        Status = OrderStatus.Completed,
        Discount = null
    },
    new OrderModel
    {
        OrderId = 3,
        CustomerName = "vijaya",
        IsVipCustomer = false,
        OrderAmount = 30000,
        Status = OrderStatus.Failed,
        Discount = null
    },
    new OrderModel
    {
        OrderId = 4,
        CustomerName = "ramya",
        IsVipCustomer = true,
        OrderAmount = 80000,
        Status = OrderStatus.Pending,
        Discount = null
    }
};

    }
}