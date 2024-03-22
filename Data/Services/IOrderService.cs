using eTicket.Models;

namespace eTicket.Data.Services
{
	public interface IOrderService
	{
		Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAdderess);
		Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
	}
}
