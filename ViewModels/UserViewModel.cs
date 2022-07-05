using System.ComponentModel.DataAnnotations;

namespace Chatik.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
