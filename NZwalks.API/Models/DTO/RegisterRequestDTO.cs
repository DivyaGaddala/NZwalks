using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class RegisterRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]

        public string UserName { get; set; }

       
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] roles { get; set; }
    }
}
