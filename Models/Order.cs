using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTicket.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		public string Email { get; set; }


		public string UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public ApplicationUser User { get; set; }


		public List<OrderItem> OrderItems { get; set; }
	}
}
