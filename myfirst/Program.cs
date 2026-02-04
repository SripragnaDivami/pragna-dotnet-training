using System;
using myfirst.Data;
using myfirst.Services;
namespace myfirst
{
    class Program
    {
        static void Main(string[] args)
        {
         OrderData orderData = new OrderData();

         var orders = orderData.Orders;
         OrderService orderService = new OrderService();

        var completedOrders = orderService.GetCompletedOrders(orders);
        var totalRevenue = orderService.GetTotalRevenue(orders);
        var firstFailedOrder = orderService.GetFirstFailed(orders);
        var ordersWithDiscounts = orderService.ApplyDiscounts(orders);

        Console.WriteLine("Completed Orders Count: " + completedOrders.Count);
        Console.WriteLine("Total Revenue: " + totalRevenue);
        Console.WriteLine("First Failed Order ID: " + (firstFailedOrder != null ? firstFailedOrder.OrderId : "No order is failed"));
        Console.WriteLine("Orders with Discounts:");
        foreach (var order in ordersWithDiscounts)
        {
            Console.WriteLine($"Order ID: {order.OrderId}, Discount: {order.Discount}");

           
        }
    }
 }
}
