using System.ComponentModel.DataAnnotations;

namespace RHView_TCC.Models.DTOs {
    public class LoginDTO {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
