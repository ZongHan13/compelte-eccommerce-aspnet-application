using eTicket.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Data.Services
{
	public class OrdersService : IOrderService
	{
		private readonly AppDbContext _context;
		public OrdersService(AppDbContext context)
		{
			_context = context;
		}
		public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
		{
			var orders = await _context.Orders
				.Include(n => n.OrderItems)
				.ThenInclude(n => n.Movie)
				.Include(n => n.User)
				.ToListAsync();
			if(userRole != "Admin")
			{
				orders = orders.Where(n => n.UserId ==userId).ToList();
			}
			return orders;
		}

		public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAdderess)
		{
			var order = new Order()
			{
				UserId = userId,
				Email = userEmailAdderess
			};
			await _context.Orders.AddAsync(order);
			await _context.SaveChangesAsync();

			foreach (var item in items)
			{
				var orderItem = new OrderItem()
				{
					Amount = item.Amount,
					MovieId = item.Movie.Id,
					OrderId = order.Id,
					Price = item.Movie.Price
				};
				await _context.OrderItems.AddAsync(orderItem);
			}
			await _context.SaveChangesAsync();
		}
	}
}
