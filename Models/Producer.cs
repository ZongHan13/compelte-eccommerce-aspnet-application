using eTicket.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTicket.Models
{
    public class Producer: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name =  "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePicture { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string FullName { get; set; }
		[Display(Name = "Biography")]
		[Required(ErrorMessage = "Biography Name is required")]
		public string Bio { get; set; }
        

        //Relawtionships
        public List<Movie>? Movies { get; set; }
    }
}
