using eTicket.Models;

namespace eTicket.Data.Services
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAdderess);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
