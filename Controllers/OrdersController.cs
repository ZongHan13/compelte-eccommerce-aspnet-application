using eTicket.Data.Cart;
using eTicket.Data.Services;
using eTicket.Data.Static;
using eTicket.Data.ViewModels;
using eTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTicket.Controllers
{
	[Authorize]
	public class OrdersController : Controller
	{
		private readonly IMoviesService _moviesService;
		private readonly ShoppingCart _shoppingCart;
		private readonly IOrderService _orderService;
		public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrderService orderService)
		{
			_moviesService = moviesService;
			_shoppingCart = shoppingCart;
			_orderService = orderService;
		}

		public async Task<IActionResult> Index()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string userRole = User.FindFirstValue(ClaimTypes.Role);

			var orders = await _orderService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
			return View(orders);
		}


		public IActionResult ShoppingCart()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;
			var response = new ShoppingCartVM()
			{
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
			};

			return View(response);
		}

		public async Task<IActionResult> AddItemToShoppingCart(int id)
		{
			var item = await _moviesService.GetMovieByIdAsync(id);
			if (item != null)
			{
				_shoppingCart.AddItemToCart(item);
			}
			return RedirectToAction(nameof(ShoppingCart));
		}

		public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
		{
			var item = await _moviesService.GetMovieByIdAsync(id);
			if (item != null)
			{
				_shoppingCart.RemoveItemFromCart(item);
			}
			return RedirectToAction(nameof(ShoppingCart));
		}
		[ActionName("OrderCompleted")]
		public async Task<IActionResult> CompleteOrder()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); ;
			string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

			await _orderService.StoreOrderAsync(items, userId, userEmailAddress);
			await _shoppingCart.ClearShoppingCartAsync();

			return View("OrderComplete");
		}
	}
}
